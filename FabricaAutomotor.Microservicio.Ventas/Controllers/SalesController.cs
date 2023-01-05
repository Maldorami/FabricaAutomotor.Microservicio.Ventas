using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using FabricaAutomotor.Microservicio.Ventas.API.Factories;
using FabricaAutomotor.Microservicio.Ventas.Domain.Services;
using FabricaAutomotor.Microservicio.Ventas.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FabricaAutomotor.Microservicio.Ventas.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SalesController : ControllerBase
	{

        private ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost("InsertSale")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult InsertSale(SaleRequest saleRequest)
        {
            try
            {
                if (saleRequest == null) return BadRequest("Body is required.");
                var saleData = SaleFactory.CreateSaleDataFrom(saleRequest);
                _saleService.InsertSale(saleData);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("InsertRandomSales")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult InsertRandomSales(int count)
        {
            try
            {
                Random rnd = new Random();

                for (int i = 0; i < count; i++)
                {
                    var saleData = SaleFactory.CreateSaleDataFrom(new SaleRequest()
                    {
                        StoreID = rnd.Next(1, 5),
                        ItemID = rnd.Next(1, 5)
                    });
                    _saleService.InsertSale(saleData);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetTotalSalesCount")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTotalSalesCount()
        {
            try
            {
                var saleCount = _saleService.GetTotalSalesCount();

                return Ok(saleCount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetTotalSalesCountFromStore")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTotalSalesCountFromStore(decimal storeID)
        {
            try
            {
                var saleCount = _saleService.GetTotalSalesCountFromStore(storeID);

                return Ok(saleCount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetItemSalePercentageByStore")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetItemSalePercentageByStore()
        {
            try
            {
                var saleCount = _saleService.GetItemSalePercentageByStore();

                return Ok(JsonConvert.SerializeObject(saleCount));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
