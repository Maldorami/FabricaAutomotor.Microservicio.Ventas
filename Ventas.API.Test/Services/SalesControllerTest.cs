using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Logging;
using FabricaAutomotor.Microservicio.Ventas.Controllers;
using FabricaAutomotor.Microservicio.Ventas.Domain.Exceptions;
using FabricaAutomotor.Microservicio.Ventas.Domain.Services;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FabricaAutomotor.Microservicio.Ventas.API.Test.Services
{
    [TestClass]
    public class SalesControllerTest
    {
        private readonly SalesController _controller;
        private readonly Mock<ISaleService> _service = new Mock<ISaleService>();
        private readonly Mock<ILogger<SalesController>> _logger = new Mock<ILogger<SalesController>>();


        public SalesControllerTest()
        {
            _controller = new SalesController(_service.Object, _logger.Object);
        }


        [TestMethod]
        public void SalesController_Returns_200OkResponse()
        {
            _service.Setup(service => service.InsertSale(It.IsAny<SaleData>()));

            IActionResult actionResult = _controller.InsertSale(new SaleRequest() { StoreID = 1, ItemID = 1 });
            Assert.IsTrue(typeof(OkResult).IsInstanceOfType(actionResult));
        }

        [TestMethod]
        public void SalesController_Returns_InvalidStoreResponse()
        {
            _service.Setup(service => service.InsertSale(It.IsAny<SaleData>())).Throws(new StoreNotExistsException(1));

            IActionResult actionResult = _controller.InsertSale(new SaleRequest() { StoreID = 1, ItemID = 1 });
            Assert.IsTrue(typeof(NotFoundObjectResult).IsInstanceOfType(actionResult));
        }

        [TestMethod]
        public void SalesController_Returns_InvalidItemResponse()
        {
            _service.Setup(service => service.InsertSale(It.IsAny<SaleData>())).Throws(new ItemNotExistsException(1));

            IActionResult actionResult = _controller.InsertSale(new SaleRequest() { StoreID = 1, ItemID = 1 });
            Assert.IsTrue(typeof(NotFoundObjectResult).IsInstanceOfType(actionResult));
        }
    }}
