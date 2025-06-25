
﻿using AdminDyanamoEnterprises.DTOs;
using Microsoft.Data.SqlClient;
﻿using AdminDyanamoEnterprises.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }        
        public string sqlConnection()

        {
            return _config.GetConnectionString("DyanamoEnterprises_DB");
        }

        public List<ProductType> GetAllProducts()
        {
            List<ProductType> products = new List<ProductType>();
            
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Dynamo.SP_ProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    // Set parameters for SELECT mode
                    cmd.Parameters.AddWithValue("@ProductId", 0);
                    cmd.Parameters.AddWithValue("@Mode", "SELECT");
                    
                    // Add output parameters (required by SP)
                    cmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        ProductType product = new ProductType()
                        {
                            ProductID = Convert.ToInt32(dr["ProductId"]),
                            ProductName = dr["ProductName"].ToString(),
                            Price = Convert.ToDecimal(dr["Price"]),
                            InStock = Convert.ToInt32(dr["Instock"]),
                            Description = dr["Discription"].ToString(),
                            IsActive = Convert.ToBoolean(dr["Active"]),

                            // Extract IDs from the joined result if available
                            // Note: The SP returns category names, not IDs directly
                            // For now, setting to 0 - if you need IDs, we need to modify the SP
                            CategoryID = 0,
                            SubCategoryID = 0,
                            ColorID = 0,
                            FabricID = 0,
                            PatternID = 0,
                            MaterialID = 0
                        };
                        
                        products.Add(product);
                    }
                }
            }
            
            return products;
        }

        public ProductType GetProductById(int productId)
        {
            ProductType product = null;
            
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Dynamo.SP_ProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    // Set parameters for SELECT mode
                    cmd.Parameters.AddWithValue("@ProductId", 0); // Use 0 to get all products
                    cmd.Parameters.AddWithValue("@Mode", "SELECT");
                    
                    // Add output parameters (required by SP)
                    cmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    
                    // Filter by ProductId in the results since the SP returns all products
                    var row = dt.AsEnumerable().FirstOrDefault(r => r.Field<int>("ProductId") == productId);
                    
                    if (row != null)
                    {
                        product = new ProductType()
                        {
                            ProductID = Convert.ToInt32(row["ProductId"]),
                            ProductName = row["ProductName"].ToString(),
                            Price = Convert.ToDecimal(row["Price"]),
                            InStock = Convert.ToInt32(row["Instock"]),
                            Description = row["Discription"].ToString(),
                            IsActive = Convert.ToBoolean(row["Active"]),
                            // Note: We'll need to get the actual IDs from the database
                            // For now, setting to 0 - this needs to be addressed
                            CategoryID = 0,
                            SubCategoryID = 0,
                            ColorID = 0,
                            FabricID = 0,
                            PatternID = 0,
                            MaterialID = 0
                        };
                    }
                }
            }
            
            return product;
        }

        public MasterResponse InsertOrUpdateProduct(AddProductType product)
        {
            MasterResponse response = new MasterResponse();
            
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Dynamo.SP_ProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    // Set parameters for SAVE mode
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SubCategoryId", product.SubCategoryID);
                    cmd.Parameters.AddWithValue("@MaterialID", product.MaterialID);
                    cmd.Parameters.AddWithValue("@ColorId", product.ColorID);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Discription", product.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Instock", product.InStock);
                    cmd.Parameters.AddWithValue("@FabricID", product.FabricID);
                    cmd.Parameters.AddWithValue("@PatternID", product.PatternID);
                    cmd.Parameters.AddWithValue("@Active", product.IsActive);
                    cmd.Parameters.AddWithValue("@Mode", "SAVE");
                    
                    // Add output parameters
                    SqlParameter errorCodeParam = new SqlParameter("@ErrorCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    
                    cmd.Parameters.Add(errorCodeParam);
                    cmd.Parameters.Add(errorMessageParam);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    response.ErrorCode = (int)errorCodeParam.Value;
                    response.ReturnMessage = errorMessageParam.Value.ToString();
                }
            }
            
            return response;
        }

        public MasterResponse DeleteProduct(int productId)
        {
            MasterResponse response = new MasterResponse();
            
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Dynamo.SP_ProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    // Set parameters for DELETE mode
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Mode", "DELETE");
                    
                    // Add output parameters
                    SqlParameter errorCodeParam = new SqlParameter("@ErrorCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 250)
                    {
                        Direction = ParameterDirection.Output
                    };
                    
                    cmd.Parameters.Add(errorCodeParam);
                    cmd.Parameters.Add(errorMessageParam);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    response.ErrorCode = (int)errorCodeParam.Value;
                    response.ReturnMessage = errorMessageParam.Value.ToString();
                }
            }
            
            return response;
        }

        // Additional method to get product with all IDs for editing
        public AddProductType GetProductForEdit(int productId)
        {
            AddProductType product = null;
            
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                // We need a separate query to get the actual IDs since the SP joins return names
                string query = @"
                    SELECT p.ProductId, p.ProductName, p.SubCategoryId, p.MaterialID, 
                           p.ColorId, p.Price, p.Discription, p.Instock, p.FabricID, 
                           p.PatternID, p.Active, sc.CategoryId
                    FROM Products p
                    INNER JOIN MasterSubCategory sc ON p.SubCategoryId = sc.SubCategoryId
                    WHERE p.ProductId = @ProductId";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new AddProductType()
                            {
                                ProductID = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                SubCategoryID = Convert.ToInt32(reader["SubCategoryId"]),
                                MaterialID = Convert.ToInt32(reader["MaterialID"]),
                                ColorID = Convert.ToInt32(reader["ColorId"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Description = reader["Discription"].ToString(),
                                InStock = Convert.ToInt32(reader["Instock"]),
                                FabricID = Convert.ToInt32(reader["FabricID"]),
                                PatternID = Convert.ToInt32(reader["PatternID"]),
                                IsActive = Convert.ToBoolean(reader["Active"]),
                                CategoryID = Convert.ToInt32(reader["CategoryId"])
                            };
                        }
                    }
                }
            }
            
            return product;
        }
    }
}
