using Microsoft.Extensions.Logging;
using SystemsLtd.Training.ECommerce.Repository.Interface;
using SystemsLtd.Training.ECommerce.Model;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SystemsLtd.Training.ECommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<ProductRepository> Logger;
        private readonly ECommerceDBContext ECommerceDBContext;
        private static List<Product> Products = new List<Product>()
            {
                new Product()
                {
                    ProductId =1,
                    ProductName="Dell Laptop",
                    ProductDescription = "Dell Laptop E6410",
                    PurchasePrice = 1000.50m,
                    SalesPrice = 1200.00m,
                    Active = true,
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId =2,
                    ProductName="HP Laptop",
                    ProductDescription = "HP Laptop E6410",
                    PurchasePrice = 1000.50m,
                    SalesPrice = 1200.00m,
                    Active = true,
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId =3,
                    ProductName="Lenovo Laptop",
                    ProductDescription = "Lenovo Laptop E6410",
                    PurchasePrice = 1000.50m,
                    SalesPrice = 1200.00m,
                    Active = true,
                    CategoryId = 1
                }
            };
        #endregion

        #region Constractor
        public ProductRepository(ILogger<ProductRepository> logger, ECommerceDBContext eCommerceDBContext, IConfiguration configuration)
        {
            this.Logger = logger;
            this.ECommerceDBContext = eCommerceDBContext;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }
        #endregion

        #region Public Methods
        public IDbConnection CreateConnection()
           => new SqlConnection(_connectionString);
        public IEnumerable<Product> GetProducts()
        {
            //return this.ECommerceDBContext.Products.AsNoTracking().ToList();
            var products = new List<Product>();
            using (SqlConnection conn =
                     new SqlConnection("Server=ISBLT-9809\\SERVER1;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                products = (List<Product>)conn.Query<Product>("SELECT * FROM Product");
            }
            return products;
        }

        public Product GetProductByID(int id)
        {
            var product = new Product();
            using (SqlConnection conn =
                     new SqlConnection("Server=ISBLT-9809\\SERVER1;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                product = conn.QuerySingle<Product>("SELECT * FROM Product WHERE ProductId = @ID", new { ID = id });
            }
            return product;
        }
        public Product GetProduct(int id)
        {
            return ProductRepository.Products.FirstOrDefault(x => x.ProductId == id);
        }


        public IEnumerable<Product> GetAllProducts(Product product)

        {
            var products = new List<Product>();
            using (SqlConnection conn =
                     new SqlConnection("Server=ISBLT-9809\\SERVER1;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                if (!string.IsNullOrWhiteSpace(product.ProductName) && product.CategoryId > 0)
                {
                    products = (List<Product>)conn.Query<Product>("SELECT * FROM Product WHERE ProductName = " + "'" + product.ProductName + "'" + "and CategoryId = " + product.CategoryId);

                }
                return products;
            }
        }


        public int AddProduct(Product product)
        {
            var res = this.ECommerceDBContext.Products.Add(product);

            var result = this.ECommerceDBContext.SaveChanges();

            return res.Entity.ProductId;
        }

        public bool UpdateProduct(Product product)
        {
            var existingProduct = this.ECommerceDBContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.PurchasePrice = product.PurchasePrice;
                existingProduct.SalesPrice = product.SalesPrice;
                existingProduct.Active = product.Active;
                existingProduct.CategoryId = product.CategoryId;

                var result = this.ECommerceDBContext.SaveChanges();

                return result > 0;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteProduct(Product product)
        {
            var existingProduct = this.ECommerceDBContext.Products.Remove(product);

            var result = this.ECommerceDBContext.SaveChanges();

            return result > 0;
        }

        #endregion
    }
}
