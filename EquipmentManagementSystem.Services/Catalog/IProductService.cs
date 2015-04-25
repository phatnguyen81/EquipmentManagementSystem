using System.Collections.Generic;
using EquipmentManagementSystem.Core;
using EquipmentManagementSystem.Core.Domain.Catalog;

namespace EquipmentManagementSystem.Services.Catalog
{
    /// <summary>
    /// Product service interface
    /// </summary>
    public partial interface IProductService
    {
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="product">Product</param>
        void DeleteProduct(Product product);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="productName">Product name</param>
        /// <param name="categoryIds"></param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IPagedList<Product> SearchProducts(string keyword = "", int? categoryId = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool loadCategory = false);

      
        /// <summary>
        /// Gets a product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        Product GetProductById(int productId);

        /// <summary>
        /// Inserts product
        /// </summary>
        /// <param name="product">Product</param>
        void InsertProduct(Product product);

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateProduct(Product product);

        
    }
}
