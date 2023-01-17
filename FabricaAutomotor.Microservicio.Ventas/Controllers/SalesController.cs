using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using FabricaAutomotor.Microservicio.Ventas.API.Factories;
using FabricaAutomotor.Microservicio.Ventas.Domain.Exceptions;
using FabricaAutomotor.Microservicio.Ventas.Domain.Services;
using FabricaAutomotor.Microservicio.Ventas.Models.Request;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FabricaAutomotor.Microservicio.Ventas.Controllers
{
    /// <summary>
    /// Implements Sales controller class
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <listheader>
    ///     <term>Date</term>
    ///     <term>Who</term>
    ///     <description>Description</description>
    /// </listheader>
    ///  <item>
    ///        <term>17/01/2023</term>
    ///        <term>Ramiro Maldonado</term>
    ///        <description>Initial implementation</description>
    /// </item>
    /// </list>
    /// </remarks>
    [ApiController]
	[Route("[controller]")]
	public class SalesController : ControllerBase
	{
        #region Members
        private ISaleService _saleService;
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saleService"></param>
        /// <param name="logger"></param>
        public SalesController(ISaleService saleService, ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts new sales.
        /// </summary>
        /// <param name="saleRequest"></param>
        /// <returns></returns>
        [HttpPost("InsertSale")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult InsertSale(SaleRequest saleRequest)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                if (saleRequest == null) return BadRequest("Body is required.");
                var saleData = SaleFactory.CreateSaleDataFrom(saleRequest);
                _saleService.InsertSale(saleData);
            }
            catch (StoreNotExistsException e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest(e.Message);
            }
            catch (ItemNotExistsException e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest("Ocurrió un error. Contacte a un administrador.");
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation(string.Format("{0} - Time elapsed: {1}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, timer.Elapsed), ControllerContext.ToString());
            }

            return Ok();
        }

        /// <summary>
        /// Randomly inserts X number of sales.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("InsertRandomSales")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult InsertRandomSales(int count)
        {
            var timer = Stopwatch.StartNew();
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
            }
            catch (StoreNotExistsException e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest(e.Message);
            }
            catch (ItemNotExistsException e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest("Ocurrió un error. Contacte a un administrador.");
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation(string.Format("{0} - Time elapsed: {1}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, timer.Elapsed), ControllerContext.ToString());
            }

            return Ok();
        }

        /// <summary>
        /// Shows the total amount of sales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalSalesCount")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTotalSalesCount()
        {
            decimal saleCount;
            var timer = Stopwatch.StartNew();
            try
            {
                saleCount = _saleService.GetTotalSalesCount();
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest("Ocurrió un error. Contacte a un administrador.");
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation(string.Format("{0} - Time elapsed: {1}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, timer.Elapsed), ControllerContext.ToString());
            }

            return Ok(saleCount);
        }

        /// <summary>
        /// Shows the total amount of sales by store.
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        [HttpGet("GetTotalSalesCountFromStore")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTotalSalesCountFromStore(decimal storeID)
        {
            decimal saleCount;
            var timer = Stopwatch.StartNew();
            try
            {
                saleCount = _saleService.GetTotalSalesCountFromStore(storeID);
            }
            catch (StoreNotExistsException e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest("Ocurrió un error. Contacte a un administrador.");
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation(string.Format("{0} - Time elapsed: {1}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, timer.Elapsed), ControllerContext.ToString());
            }

            return Ok(saleCount);
        }

        /// <summary>
        /// Shows the items sold impact of each store.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetItemSalePercentageByStore")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetItemSalePercentageByStore()
        {
            var timer = Stopwatch.StartNew();
            List<ItemSoldPercentageByStoreResponse> saleCount;
            try
            {
                saleCount = _saleService.GetItemSalePercentageByStore();
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("{0} - {1} - {2}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message, e.StackTrace));
                return BadRequest("Ocurrió un error. Contacte a un administrador.");
            }
            finally
            {
                timer.Stop();
                _logger.LogInformation(string.Format("{0} - Time elapsed: {1}",
                    System.Reflection.MethodBase.GetCurrentMethod().Name, timer.Elapsed), ControllerContext.ToString());
            }
            return Ok(JsonConvert.SerializeObject(saleCount));
        }
        #endregion
    }
}
