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
        public IActionResult InsertSale([FromBody] SaleRequest saleRequest)
        {
            if (saleRequest == null) return BadRequest("Body is required.");

            try
            {
                var saleData = SaleFactory.CreateSaleDataFrom(saleRequest);
                _saleService.InsertSale(saleData);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
