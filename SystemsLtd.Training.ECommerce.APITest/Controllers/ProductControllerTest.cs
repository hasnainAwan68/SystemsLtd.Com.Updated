using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemsLtd.Training.ECommerce.API.Controllers;
using SystemsLtd.Training.ECommerce.API.Models;
using SystemsLtd.Training.ECommerce.Model;
using SystemsLtd.Training.ECommerce.Service.Interface;
using Xunit;

namespace SystemsLtd.Training.ECommerce.APITest.Controllers
{
    public class ProductControllerTest
    {
        #region
        public List<Product> ProductData = new List<Product>
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
        [Fact]
        public void GetProductsTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            service.Setup(s => s.GetProducts()).Returns(
                this.ProductData
                );


            var controller = new ProductController(null, service.Object);

            // Act
            var res = controller.GetProducts();

            //Arrange
            Assert.NotNull(res);
            Assert.Equal(3, res.Count());
        }

        [Fact]
        public void GetProductbyIDTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var P = service.Setup(s => s.GetProduct(2)).Returns(this.ProductData.FirstOrDefault(x => x.ProductId ==
            2));
            var controller = new ProductController(null, service.Object);

            //Act
            var res = controller.GetProduct(2);

            //Assert
            Assert.NotNull(res);


        }
        [Fact]
        public void GetProductbyIDNegTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var P = service.Setup(s => s.GetProduct(-2)).Returns(this.ProductData.FirstOrDefault(x => x.ProductId ==
            -2));
            var controller = new ProductController(null, service.Object);

            //Act
            var res = controller.GetProduct(-2);

            //Assert
            Assert.Null(res);


        }

        [Fact]
        public void GetProductbyIDNegTest2()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var P = service.Setup(s => s.GetProduct(100)).Returns(this.ProductData.FirstOrDefault(x => x.ProductId ==
            100));


            var controller = new ProductController(null, service.Object);

            //Act
            var res = controller.GetProduct(100);

            //Assert
            Assert.Null(res);


        }
        [Fact]
        public void GetProductbyIDNegTest3()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var P = service.Setup(s => s.GetProduct(2)).Returns(this.ProductData.FirstOrDefault(x => x.ProductId ==
            null));


            var controller = new ProductController(null, service.Object);

            //Act
            var res = controller.GetProduct(2);

            //Assert
            Assert.Null(res);


        }


        [Fact]
        public void AddProductTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            var product = new ProductAddVM
            {
                ProductName = "Lenovo",
                ProductDescription = "11th Gen",
                PurchasePrice = 100,
                SalesPrice = 1115,
                Active = true,
                CategoryId = 4
            };
            service.Setup(s => s.AddProduct(It.IsAny<Product>())).Returns(
            this.ProductData.Count() + 1
            );

            var controller = new ProductController(null, service.Object);



            // Act
            var res = controller.AddProduct(product);



            //Arrange
            Assert.Equal(this.ProductData.Count() + 1, res);
        }


        [Fact]
        public void DeleteProductTest()
        {
            //Arrange
            var service = new Mock<IProductService>();
            //var P = service.Setup(s => s.GetProduct(2)).Returns(this.ProductData.FirstOrDefault(x => x.ProductId ==
            //2));

            service.Setup(s => s.GetProducts()).Returns(
               this.ProductData
               );
            var controller = new ProductController(null, service.Object);

            //Act

            //Assert

        }


    }
}
