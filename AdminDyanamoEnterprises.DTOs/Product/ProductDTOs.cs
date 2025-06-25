using AdminDyanamoEnterprises.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.DTOs
{
    public class ProductType
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int ColorID { get; set; }
        public int FabricID { get; set; }
        public int PatternID { get; set; }
        public int MaterialID { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? ProductImages { get; set; }
    }

    public class AddProductType
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int ColorID { get; set; }
        public int FabricID { get; set; }
        public int PatternID { get; set; }
        public int MaterialID { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? ProductImages { get; set; }
    }

    public class ProductPageViewModel
    {
        public AddProductType? AddProduct { get; set; }
        public List<ProductType> ProductList { get; set; } = new();
        
        public List<CategoryType> CategoryList { get; set; } = new();
        public List<SubCategoryType> SubCategoryList { get; set; } = new();
        public List<ColorType> ColorList { get; set; } = new();
        public List<FabricType> FabricList { get; set; } = new();
        public List<PatternType> PatternList { get; set; } = new();
        public List<MaterialType> MaterialList { get; set; } = new();
    }
} 