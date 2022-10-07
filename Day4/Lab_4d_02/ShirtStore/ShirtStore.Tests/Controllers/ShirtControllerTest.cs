using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShirtStore.Tests.FakeRepositories;
using ShirtStoreWebsite.Controllers;
using ShirtStoreWebsite.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;

namespace ShirtStore.Tests.Controllers
{
    [TestClass]
    public class ShirtControllerTest
    {
        [TestMethod]
        public void IndexmodelShouldContainAllShirts()
        {
            var fakeShirtRepository = new FakeShirtRepository();
            var mockLogger = new Mock<ILogger<ShirtController>>();
            var shirtController = new ShirtController(fakeShirtRepository, mockLogger.Object);
            var viewResult = shirtController.Index() as ViewResult;
            var shirts = viewResult.Model as List<Shirt>;
            Assert.AreEqual(3, shirts.Count);
        }
    }
}
