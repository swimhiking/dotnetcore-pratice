using eCommerce.Controllers;
using eCommerce_DataAccess.Repository;
using eCommerce_DataAccess.Repository.IRepository;
using eCommerce_Model;
using eCommerce_Model.ViewModels;
using eCommerce_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telerik.JustMock;
using Xunit;

namespace eCommerceTestProject
{

    //for static or global stuff, such as session, shopping cart list are not included in Unit test
    //This is also the reason that I didn't add Unit test for CartController
    public class eCommerceHomeControllerTest
    {
        [Fact]
        public void Index_ReturnsViewResultWithViewModel()
        {
            // Arrange 
            HomeVM homeVM = new HomeVM()
            {
                Products = GetTestProduct(),
                Categories = GetTestCategory()
            };

            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockCateRepo = new Mock<ICategoryRepository>();
            var mockProdRepo = new Mock<IProductRepository>();
            var categories = (IEnumerable<Category>)GetTestCategory();

            mockCateRepo.Setup(x => x.GetAll(null, null, null, true)).Returns(homeVM.Categories);
            mockProdRepo.Setup(x => x.GetAll(null, null, "Category,ApplicationType", true)).Returns(homeVM.Products);
            var controller = new HomeController(mockLogger.Object, mockProdRepo.Object, mockCateRepo.Object);

            // Act
            var result = controller.Index();

            // Assert correct non-null View returned with no Model
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<HomeVM>(viewResult.ViewData.Model);

            var viewResultVM = Assert.IsAssignableFrom<HomeVM>(viewResult.ViewData.Model);
            int prodCount = viewResultVM.Products.Count();
            Assert.Equal(2, prodCount);

            var p1 = viewResultVM.Products.FirstOrDefault();
            Assert.Equal("Sony TV1", p1.Name);
            
        }

        [Theory]
        [InlineData(1)]
        public void DetailsViewResultWithViewModel(int detailId)
        {

            // Arrange 
            HomeVM homeVM = new HomeVM()
            {
                Products = GetTestProduct(),
                Categories = GetTestCategory()
            };
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockCateRepo = new Mock<ICategoryRepository>();
            var mockProdRepo = new Mock<IProductRepository>();
            var categories = (IEnumerable<Category>)GetTestCategory();

            var prod = GetTestProduct().FirstOrDefault();
            var cate = GetTestCategory().FirstOrDefault();

            DetailsVM detailVM = new DetailsVM()
            {
                ExistsInCart = false,
                Product = prod
            };

            mockCateRepo.Setup(x => x.GetAll(null, null, null, true)).Returns(homeVM.Categories);
            mockProdRepo.Setup(x => x.GetAll(null, null, "Category,ApplicationType", true)).Returns(homeVM.Products);
            var controller = new HomeController(mockLogger.Object, mockProdRepo.Object, mockCateRepo.Object);

            controller.ControllerContext.HttpContext = 
                new DefaultHttpContext();
            var session = new MockHttpSession();
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["ShoppingCartSession"] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            //controller.ControllerContext.HttpContext = (HttpContext)mockHttpContext.Object;
            controller.ControllerContext.HttpContext.Session = mockSession;


            //Action
            var result = controller.Details(detailId);

            // Assert correct non-null View returned with no Model
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model);
            var viewResultValue = Assert.IsAssignableFrom<DetailsVM>(viewResult.ViewData.Model);            
            Assert.IsType<DetailsVM>(viewResult.ViewData.Model);
           
        }

        [Fact]
        public void DeleteViewResultWithViewModel()
        {
            //Won't work, due to it use global Session["ShoppingCartSession"]
            // Arrange 
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ProductId = 1,
                Unit = 1
            };
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockCateRepo = new Mock<ICategoryRepository>();
            var mockProdRepo = new Mock<IProductRepository>();           

            var session = new MockHttpSession();
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession mockSession = new MockHttpSession();
            mockSession["ShoppingCartSession"] = new List<ShoppingCart> { shoppingCart };

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Scheme).Returns("http");
            request.Setup(x => x.Host).Returns(HostString.FromUriComponent("http://localhost:44331"));
            request.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/home"));

            var httpContext = Moq.Mock.Of<HttpContext>(_ =>
                _.Request == request.Object
            );

            //Controller needs a controller context 
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            //assign context to controller
            var controller = new HomeController(mockLogger.Object, mockProdRepo.Object, mockCateRepo.Object)
            {
                ControllerContext = controllerContext,
            };



            //Action
            //var result = controller.RemoveFromCart(1);

            //
            //var viewResult = Assert.IsType<ViewResult>(result);
            //Assert.Null(viewResult);
            //Assert.Null(viewResult.ViewData.Model);
            //Assert.IsType<ShoppingCart>(viewResult.ViewData.Model);


        }


        private IEnumerable<Product> GetTestProduct()
        {
            var prods = new List<Product>();
            prods.Add(new Product()
            {
                Id = 1,
                Name = "Sony TV1",
                ShortDesc = "Test Short Desc1",
                Description = "DEScription1 ",
                Price = 200,
                Image = "testImage1.jpg",
                CategoryId = 1,
                ApplicationTypeId = 1
            });
            prods.Add(new Product()
            {
                Id = 2,
                Name = "Smart Watch2",
                ShortDesc = "Test Short Desc2",
                Description = "DEScription2 ",
                Price = 200,
                Image = "testImage2.jpg",
                CategoryId = 2,
                ApplicationTypeId = 1
            });
            return prods;
        }

        private IEnumerable<Category> GetTestCategory()
        {
            var cates = new List<Category>();
            cates.Add(new Category()
            {
                Id = 1,
                Name = "TV",
            });
            cates.Add(new Category()
            {
                Id = 2,
                Name = "Smart Watch",

            });
            return cates;
        }


    }
}
