using System;
using System.Linq;
using System.Data.Entity;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Caching;
using EquipmentManagementSystem.Core.Data;
using EquipmentManagementSystem.Core.Domain.Catalog;

namespace EquipmentManagementSystem.Services.Catalog
{
    /// <summary>
    /// Product service
    /// </summary>
    public partial class ProductService : IProductService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        private const string CATEGORIES_BY_ID_KEY = "Ems.product.id-{0}";
       /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CATEGORIES_PATTERN_KEY = "Ems.product.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTCATEGORIES_PATTERN_KEY = "Ems.productproduct.";

        #endregion

        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="workContext">Work context</param>
        public ProductService(ICacheManager cacheManager,
            IRepository<Product> productRepository,
            IWorkContext workContext)
        {
            this._cacheManager = cacheManager;
            this._productRepository = productRepository;
            this._productRepository = productRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.Deleted = true;
            UpdateProduct(product);

        }
        
        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<Product> SearchProducts(string keyword = "", int? categoryId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool loadCategory = false)
        {
            var query = _productRepository.Table;

            query = query.Where(c => !c.Deleted);

            if (!String.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Sku.Contains(keyword) || c.Description.Contains(keyword));
            }

            if (categoryId != null )
            {
                query = query.Where(q => q.CategoryId == categoryId);
            }
            if (loadCategory)
            {
                query = query.Include(q=>q.Category);
            }

            query = query.OrderBy(c => c.Id);

            //paging
            return new PagedList<Product>(query, pageIndex, pageSize);
        }

                
        /// <summary>
        /// Gets a product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual Product GetProductById(int productId)
        {
            if (productId == 0)
                return null;
            
            string key = string.Format(CATEGORIES_BY_ID_KEY, productId);
            return _cacheManager.Get(key, () => _productRepository.GetById(productId));
        }

        /// <summary>
        /// Inserts product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Insert(product);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            _productRepository.Update(product);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

        }
        
        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateHasDiscountsApplied(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            UpdateProduct(product);
        }

      
        #endregion
    }
}
