﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Nop.Web.Framework.UI;

namespace EquipmentManagementSystem.Framework.UI
{
    public partial class PageHeadBuilder : IPageHeadBuilder
    {
        #region Fields

        private static readonly object s_lock = new object();

        private readonly List<string> _titleParts;
        private readonly List<string> _metaDescriptionParts;
        private readonly List<string> _metaKeywordParts;
        private readonly Dictionary<ResourceLocation, List<ScriptReferenceMeta>> _scriptParts;
        private readonly Dictionary<ResourceLocation, List<string>> _cssParts;
        private readonly List<string> _canonicalUrlParts;
        private readonly List<string> _headCustomParts;
        #endregion

        #region Ctor

        public PageHeadBuilder()
        {
            this._titleParts = new List<string>();
            this._metaDescriptionParts = new List<string>();
            this._metaKeywordParts = new List<string>();
            this._scriptParts = new Dictionary<ResourceLocation, List<ScriptReferenceMeta>>();
            this._cssParts = new Dictionary<ResourceLocation, List<string>>();
            this._canonicalUrlParts = new List<string>();
            this._headCustomParts = new List<string>();
        }

        #endregion

        #region Utilities

        protected virtual string GetBundleVirtualPath(string prefix, string extension, string[] parts)
        {
            if (parts == null || parts.Length == 0)
                throw new ArgumentException("parts");

            //calculate hash
            var hash = "";
            using (SHA256 sha = new SHA256Managed())
            {
                // string concatenation
                var hashInput = "";
                foreach (var part in parts)
                {
                    hashInput += part;
                    hashInput += ",";
                }

                byte[] input = sha.ComputeHash(Encoding.Unicode.GetBytes(hashInput));
                hash = HttpServerUtility.UrlTokenEncode(input);
            }
            //ensure only valid chars

            var sb = new StringBuilder(prefix);
            sb.Append(hash);
            //we used "extension" when we had "runAllManagedModulesForAllRequests" set to "true" in web.config
            //now we disabled it. hence we should not use "extension"
            //sb.Append(extension);
            return sb.ToString();
        }

        protected virtual IItemTransform GetCssTranform()
        {
            return new CssRewriteUrlTransform();
        }

        #endregion

        #region Methods

        public virtual void AddTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _titleParts.Add(part);
        }
        public virtual void AppendTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;
            
            _titleParts.Insert(0, part);
        }
        public virtual string GenerateTitle(bool addDefaultTitle)
        {
            string result = "";
            string pageTitleSeparator = " - ";
            string defaultTitle = "Equipment Management System";
            var specificTitle = string.Join(pageTitleSeparator, _titleParts.AsEnumerable().Reverse().ToArray());
            if (!String.IsNullOrEmpty(specificTitle))
            {
                if (addDefaultTitle)
                {
                    result = string.Join(pageTitleSeparator, specificTitle, defaultTitle);
                }
                else
                {
                    //page title only
                    result = specificTitle;
                }
            }
            else
            {
                //store name only
                result = defaultTitle;
            }
            return result;
        }


        public virtual void AddMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;
            
            _metaDescriptionParts.Add(part);
        }
        public virtual void AppendMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;
            
            _metaDescriptionParts.Insert(0, part);
        }
       


        public virtual void AddMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;
            
            _metaKeywordParts.Add(part);
        }
        public virtual void AppendMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _metaKeywordParts.Insert(0, part);
        }


        public virtual void AddScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!_scriptParts.ContainsKey(location))
                _scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(part))
                return;

            _scriptParts[location].Add(new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Part = part
            });
        }
        public virtual void AppendScriptParts(ResourceLocation location, string part, bool excludeFromBundle)
        {
            if (!_scriptParts.ContainsKey(location))
                _scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(part))
                return;

            _scriptParts[location].Insert(0, new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Part = part
            });
        }
        public virtual string GenerateScripts(UrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            bool enableJsBundling = false;
            if (!_scriptParts.ContainsKey(location) || _scriptParts[location] == null)
                return "";

            if (_scriptParts.Count == 0)
                return "";
            
            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = enableJsBundling && BundleTable.EnableOptimizations;
            }
            if (bundleFiles.Value)
            {
                var partsToBundle = _scriptParts[location]
                    .Where(x => !x.ExcludeFromBundle)
                    .Select(x => x.Part)
                    .Distinct()
                    .ToArray();
                var partsToDontBundle = _scriptParts[location]
                    .Where(x => x.ExcludeFromBundle)
                    .Select(x => x.Part)
                    .Distinct()
                    .ToArray();


                var result = new StringBuilder();

                if (partsToBundle.Length > 0)
                {
                    //IMPORTANT: Do not use bundling in web farms or Windows Azure
                    string bundleVirtualPath = GetBundleVirtualPath("~/bundles/scripts/", ".js", partsToBundle);
                    //create bundle
                    lock (s_lock)
                    {
                        var bundleFor = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
                        if (bundleFor == null)
                        {
                            var bundle = new ScriptBundle(bundleVirtualPath);
                            //bundle.Transforms.Clear();

                            //"As is" ordering
                            bundle.Orderer = new AsIsBundleOrderer();
                            //disable file extension replacements. renders scripts which were specified by a developer
                            bundle.EnableFileExtensionReplacements = false;
                            bundle.Include(partsToBundle);
                            BundleTable.Bundles.Add(bundle);
                            //we clear ignore list because System.Web.Optimization library adds ignore patterns such as "*.min", "*.debug".
                            //we think it's bad decision and should be disabled by default
                            BundleTable.Bundles.IgnoreList.Clear();
                        }
                    }

                    //parts to bundle
                    result.AppendLine(Scripts.Render(bundleVirtualPath).ToString());
                }

                //parts to do not bundle
                foreach (var path in partsToDontBundle)
                {
                    result.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var path in _scriptParts[location].Select(x => x.Part).Distinct())
                {
                    result.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }


        public virtual void AddCssFileParts(ResourceLocation location, string part)
        {
            if (!_cssParts.ContainsKey(location))
                _cssParts.Add(location, new List<string>());

            if (string.IsNullOrEmpty(part))
                return;

            _cssParts[location].Add(part);
        }
        public virtual void AppendCssFileParts(ResourceLocation location, string part)
        {
            if (!_cssParts.ContainsKey(location))
                _cssParts.Add(location, new List<string>());

            if (string.IsNullOrEmpty(part))
                return;
            
            _cssParts[location].Insert(0, part);
        }
        public virtual string GenerateCssFiles(UrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            bool enableCssBundling = false;
            if (!_cssParts.ContainsKey(location) || _cssParts[location] == null)
                return "";

            //use only distinct rows
            var distinctParts = _cssParts[location].Distinct().ToList();
            if (distinctParts.Count == 0)
                return "";
            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = enableCssBundling && BundleTable.EnableOptimizations;
            }
            if (bundleFiles.Value)
            {
                //bundling is enabled
                var result = new StringBuilder();

                var partsToBundle = distinctParts.ToArray();
                if (partsToBundle.Length > 0)
                {
                    //IMPORTANT: Do not use bundling in web farms or Windows Azure
                    //IMPORTANT: Do not use CSS bundling in virtual categories
                    string bundleVirtualPath = GetBundleVirtualPath("~/bundles/styles/", ".css", partsToBundle);

                    //create bundle
                    lock (s_lock)
                    {
                        var bundleFor = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
                        if (bundleFor == null)
                        {
                            var bundle = new StyleBundle(bundleVirtualPath);
                            //bundle.Transforms.Clear();

                            //"As is" ordering
                            bundle.Orderer = new AsIsBundleOrderer();
                            //disable file extension replacements. renders scripts which were specified by a developer
                            bundle.EnableFileExtensionReplacements = false;
                            foreach (var ptb in partsToBundle)
                            {
                                bundle.Include(ptb, GetCssTranform());
                            }
                            BundleTable.Bundles.Add(bundle);
                            //we clear ignore list because System.Web.Optimization library adds ignore patterns such as "*.min", "*.debug".
                            //we think it's bad decision and should be disabled by default
                            BundleTable.Bundles.IgnoreList.Clear();
                        }
                    }

                    //parts to bundle
                    result.AppendLine(Styles.Render(bundleVirtualPath).ToString());
                }

                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var path in distinctParts)
                {
                    result.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", urlHelper.Content(path));
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }


        public virtual void AddCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;
                       
            _canonicalUrlParts.Add(part);
        }
        public virtual void AppendCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;
                       
            _canonicalUrlParts.Insert(0, part);
        }
        public virtual string GenerateCanonicalUrls()
        {
            var result = new StringBuilder();
            foreach (var canonicalUrl in _canonicalUrlParts)
            {
                result.AppendFormat("<link rel=\"canonical\" href=\"{0}\" />", canonicalUrl);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }

        public virtual void AddHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _headCustomParts.Add(part);
        }
        public virtual void AppendHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _headCustomParts.Insert(0, part);
        }
        public virtual string GenerateHeadCustom()
        {
            //use only distinct rows
            var distinctParts = _headCustomParts.Distinct().ToList();
            if (distinctParts.Count == 0)
                return "";

            var result = new StringBuilder();
            foreach (var path in distinctParts)
            {
                result.Append(path);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }


        #endregion

        #region Nested classes

        private class ScriptReferenceMeta
        {
            public bool ExcludeFromBundle { get; set; }

            public string Part { get; set; }
        }

        #endregion
    }
}
