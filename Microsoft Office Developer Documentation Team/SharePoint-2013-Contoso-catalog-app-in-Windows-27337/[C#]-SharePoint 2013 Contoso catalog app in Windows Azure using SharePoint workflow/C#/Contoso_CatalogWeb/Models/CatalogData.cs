/*
 * Developed by:    Martin Harwar, www.Point8020.com
 * Developed for:   MSDN and SharePoint Product group
 * First released:  14th February, 2014
 * 
 * This file contains three classes: CatalogData, Category, and Product
 * 
 * Category Class:
 * This class simply defines properites that map to the Category table in the SQL Azure database
 * 
 * Product Class:
 * This class simply defines properites that map to the Product table in the SQL Azure database
 * 
 * CatalogData:
 * This is the most complex class. It contains:
 * 
 * 1. A private function named catalogconnection that returns a SQLConnection object to the SQL Azure database
 * 
 * 2. Nine other functions for retrieving, inserting, updating, and deleting categories and products. These functions all
 * follow very similar patterns in that they call various SQL stored procedures for performing the appropriate data operations.
 * Some of these methods accept parameters, the values of which are then used appropriateley in SQL stored procedure parameters.
 * Note that many of the stored procedures return 'Status' fields. A value of "Point8020.Success" is consistently uses to signal
 * that the data operation was processed as expected. Any other value returned signals that although the procedure completed without
 * error, the data in the database did not represent an expected state (such as listing products for a category that does not exist)
 */
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contoso_CatalogWeb.Models
{
    public class CatalogData
    {

        private SqlConnection catalogConnection()
        {
            SqlConnection sqlCon = new SqlConnection();
            string connStr = string.Empty;
            if (ConfigurationManager.ConnectionStrings["ContosoCatalogDB"] != null)
            {
                sqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["ContosoCatalogDB"].ConnectionString;
                return (sqlCon);
            }
            else
            {
                return (null);
            }
        }

        public List<Category> CategoryList()
        {
            List<Category> categoryList = new List<Category>();
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "ListCategories";
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {

                        Category category = new Category();
                        category.CategoryID = sqlReader["CategoryID"].ToString();
                        category.CategoryName = sqlReader["CategoryName"].ToString();
                        category.CategoryDescription = sqlReader["CategoryDescription"].ToString();
                        category.CategoryImageURL = sqlReader["CategoryImageURL"].ToString();
                        categoryList.Add(category);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (categoryList);

                }
                catch
                {
                    return (categoryList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (categoryList);
        }

        public Category AddCategory(string categoryName, string categoryDescription, string categoryImageUrl)
        {
            Category newCategory = new Category();
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddCategory";
                    SqlParameter catName = sqlCmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar);
                    catName.Value = categoryName;
                    SqlParameter catDesc = sqlCmd.Parameters.Add("@CategoryDescription", SqlDbType.NVarChar);
                    catDesc.Value = categoryDescription;
                    SqlParameter catImage = sqlCmd.Parameters.Add("@CategoryImageURL", SqlDbType.NVarChar);
                    catImage.Value = categoryImageUrl;
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            newCategory.CategoryID = sqlReader["CategoryID"].ToString();
                            newCategory.CategoryName = categoryName;
                            newCategory.CategoryDescription = categoryDescription;
                            newCategory.CategoryImageURL = categoryImageUrl;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (newCategory);
                }
                catch (Exception ex)
                {
                    return (newCategory);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (newCategory);
        }

        public Category UpdateCategory(string categoryID, string categoryName, string categoryDescription, string categoryImageUrl)
        {
            Category updatedCategory = new Category();
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateCategory";
                    SqlParameter catID = sqlCmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier);
                    catID.Value = Guid.Parse(categoryID);
                    SqlParameter catName = sqlCmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar);
                    catName.Value = categoryName;
                    SqlParameter catDesc = sqlCmd.Parameters.Add("@CategoryDescription", SqlDbType.NVarChar);
                    catDesc.Value = categoryDescription;
                    SqlParameter catImage = sqlCmd.Parameters.Add("@CategoryImageURL", SqlDbType.NVarChar);
                    catImage.Value = categoryImageUrl;
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            updatedCategory.CategoryID = categoryID;
                            updatedCategory.CategoryName = categoryName;
                            updatedCategory.CategoryDescription = categoryDescription;
                            updatedCategory.CategoryImageURL = categoryImageUrl;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (updatedCategory);
                }
                catch (Exception ex)
                {
                    return (updatedCategory);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (updatedCategory);
        }

        public bool DeleteCategory(string categoryID)
        {
            bool success = false;
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteCategory";
                    SqlParameter catID = sqlCmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier);
                    catID.Value = Guid.Parse(categoryID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            success = true;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (success);
                }
                catch (Exception ex)
                {
                    return (success);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (success);
        }

        public Product AddProduct(string categoryID, string productName, string productDescription, string productImageURL)
        {
            Product newProduct = new Product();
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "AddProduct";
                    SqlParameter catID = sqlCmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier);
                    catID.Value = Guid.Parse(categoryID);
                    SqlParameter prodName = sqlCmd.Parameters.Add("@ProductName", SqlDbType.NVarChar);
                    prodName.Value = productName;
                    SqlParameter prodDesc = sqlCmd.Parameters.Add("@ProductDescription", SqlDbType.NVarChar);
                    prodDesc.Value = productDescription;
                    SqlParameter prodImage = sqlCmd.Parameters.Add("@ProductImageURL", SqlDbType.NVarChar);
                    prodImage.Value = productImageURL;
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            newProduct.CategoryID = categoryID;
                            newProduct.ProductID = sqlReader["ProductID"].ToString();
                            newProduct.ProductName = productName;
                            newProduct.ProductDescription = productDescription;
                            newProduct.ProductImageURL = productImageURL;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (newProduct);
                }
                catch (Exception ex)
                {
                    return (newProduct);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (newProduct);
        }

        public bool DeleteProduct(string productID)
        {
            bool success = false;
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "DeleteProduct";
                    SqlParameter catID = sqlCmd.Parameters.Add("@ProductID", SqlDbType.UniqueIdentifier);
                    catID.Value = Guid.Parse(productID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            success = true;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (success);
                }
                catch (Exception ex)
                {
                    return (success);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (success);
        }

        public Product UpdateProduct(string CategoryID, string ProductID, string ProductName, string ProductDescription, string ProductImageURL)
        {
            Product updatedProduct = new Product();
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateProduct";
                    SqlParameter catID = sqlCmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier);
                    catID.Value = Guid.Parse(CategoryID);
                    SqlParameter prodID = sqlCmd.Parameters.Add("@ProductID", SqlDbType.UniqueIdentifier);
                    prodID.Value = Guid.Parse(ProductID);
                    SqlParameter prodName = sqlCmd.Parameters.Add("@ProductName", SqlDbType.NVarChar);
                    prodName.Value = ProductName;
                    SqlParameter prodDesc = sqlCmd.Parameters.Add("@ProductDescription", SqlDbType.NVarChar);
                    prodDesc.Value = ProductDescription;
                    SqlParameter prodImage = sqlCmd.Parameters.Add("@ProductImageURL", SqlDbType.NVarChar);
                    prodImage.Value = ProductImageURL;
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            updatedProduct.CategoryID = sqlReader["CategoryID"].ToString(); ;
                            updatedProduct.ProductID = sqlReader["ProductID"].ToString();
                            updatedProduct.ProductName = sqlReader["ProductName"].ToString(); ;
                            updatedProduct.ProductDescription = sqlReader["ProductDescription"].ToString(); ;
                            updatedProduct.ProductImageURL = sqlReader["ProductImageURL"].ToString(); ;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (updatedProduct);
                }
                catch (Exception ex)
                {
                    return (updatedProduct);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (updatedProduct);
        }

        public bool UpdateProductPrice(string ProductID, double ApprovedPrice)
        {
            bool retVal = false;
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();

                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "UpdateProductPrice";
                    SqlParameter prodID = sqlCmd.Parameters.Add("@ProductID", SqlDbType.UniqueIdentifier);
                    prodID.Value = Guid.Parse(ProductID);
                    SqlParameter prodPrice = sqlCmd.Parameters.Add("@ProductPrice", SqlDbType.SmallMoney);
                    prodPrice.Value = ApprovedPrice;
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        if (sqlReader["Status"].ToString() == "Point8020.Success")
                        {
                            retVal = true;
                        }
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (retVal);
                }
                catch (Exception ex)
                {
                    return (retVal);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (retVal);
        }
        
        public List<Product> ProductList(string categoryID)
        {
            List<Product> productList = new List<Product>();
            SqlConnection sqlCon = this.catalogConnection();
            if (sqlCon != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                try
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "ListProductsInCategory";
                    SqlParameter catID = sqlCmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier);
                    catID.Value = Guid.Parse(categoryID);
                    sqlCon.Open();
                    sqlCmd.Connection = sqlCon;
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {

                        Product product = new Product();
                        product.ProductID = sqlReader["ProductID"].ToString();
                        product.ProductName = sqlReader["ProductName"].ToString();
                        product.ProductPrice = decimal.Parse(sqlReader["ProductPrice"].ToString());
                        product.ProductDescription = sqlReader["ProductDescription"].ToString();
                        product.ProductImageURL = sqlReader["ProductImageURL"].ToString();
                        productList.Add(product);
                    }
                    sqlCmd.Dispose();
                    sqlCon.Close();
                    sqlCon.Dispose();
                    return (productList);

                }
                catch
                {
                    return (productList);
                }
                finally
                {
                    sqlCmd.Dispose();
                    sqlCon.Dispose();
                }
            }
            return (productList);
        }

    }

    public class Category
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImageURL { get; set; }

    }

    public class Product
    {
        public string CategoryID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }

    }
}
