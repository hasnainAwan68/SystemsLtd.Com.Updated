using SystemsLtd.Training.ECommerce.API.Controllers;
using Xunit;

namespace SystemsLtd.Training.ECommerce.APITest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {


        }

        [Fact]
        public void SumTest()
        {
            //Arrange
            int? a = 10;
            int? b = 20;
            var controller = new ProductController(null, null);

            // Act
            var res = controller.Sum(a, b);
            Assert.Equal(30, res);
        }
    }
}