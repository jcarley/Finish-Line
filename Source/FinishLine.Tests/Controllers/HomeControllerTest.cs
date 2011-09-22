using System.Web.Mvc;
using FinishLine.UI.Web.Controllers;
using NUnit.Framework;

namespace FinishLine.Tests.Controllers
{
    [TestFixture]
    public class when_browsing_to_application_for_first_time : SpecificationBase
    {
        private HomeController _controller = null;
        private ViewResult _result;

        protected override void Given()
        {
            // Arrange
            _controller = new HomeController();
        }

        protected override void When()
        {
            // Act
            _result = _controller.Index() as ViewResult;
        }

        [Then]
        public void it_should_return_the_index_page()
        {
            // Assert
            Assert.That("Welcome to ASP.NET MVC!", Is.EqualTo(_result.ViewBag.Message));
        }

        [Then]
        public void the_index_page_should_not_be_null()
        {
            Assert.That(_result, Is.Not.Null);
        }
    }

    [TestFixture]
    public class when_browsing_to_about_page : SpecificationBase
    {
        private HomeController _controller = null;
        private ViewResult _result = null;

        protected override void Given()
        {
            // Arrange
            _controller = new HomeController();
        }

        protected override void When()
        {
            // Act
            _result = _controller.About() as ViewResult;
        }

        [Then]
        public void it_should_not_be_null()
        {
            // Assert
            Assert.That(_result, Is.Not.Null);
        }
    }
}
