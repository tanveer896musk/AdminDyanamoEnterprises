using AdminDyanamoEnterprises.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public interface IProductRepository
    {
        // Get all products with full details (joins)
        List<ProductType> GetAllProducts();
        
        // Get a specific product by ID
        ProductType GetProductById(int productId);
        
        // Get product with all IDs for editing
        AddProductType GetProductForEdit(int productId);
        
        // Insert or Update product
        MasterResponse InsertOrUpdateProduct(AddProductType product);
        
        // Delete product
        MasterResponse DeleteProduct(int productId);
    }
}
