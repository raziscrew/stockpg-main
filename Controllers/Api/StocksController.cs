using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Stocks;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPGStockServer.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/Stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private IStockService _stockService;

        private IMapper _mapper;


        public StocksController(
           IStockService stockService,
            IMapper mapper
           )
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAllStock(DataSourceLoadOptions loadOptions)
        {
            var Stock = await _stockService.GetAllStock();
            var model = _mapper.Map<IList<StockModels>>(Stock);
            // return Ok(model);

            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");

        }

        [HttpGet("lowStock/{id}")]
        public ActionResult<CapacitorModels> getLowStockBy(int id)
        {
            var Stock = _stockService.getLowStockBy(id);
            var model = _mapper.Map<CapacitorModels>(Stock);

            if (Stock != null)


            {
                return Ok(model);
            }
            return NotFound();
        }
        [AllowAnonymous]
        [HttpGet("lowStock")]
        public IEnumerable LowStock()
        {
            var Stock = _stockService.LowStock();
            var model = _mapper.Map<IList<LowStockModels>>(Stock);


            return model;


        }
        [AllowAnonymous]
        [HttpGet("IgnoreList")]
        public IEnumerable IgnoreList()
        {
            var Stock = _stockService.IgnoreList();
            var model = _mapper.Map<IList<LowStockModels>>(Stock);


            return model;


        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult IgnoredLowStock(int id, ignoreLowStock ignoreLowStock)
        {
            var ignored = _stockService.getLowStockBy(id);

            if (id != ignoreLowStock.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(ignoreLowStock, ignored);
                _stockService.IgnoredLowStock(id, ignored);
                _stockService.SaveToDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ignored == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

            //if(ignored == null)
            //{
            //    return NotFound();
            //}
            //_mapper.Map(ignoreLowStock, ignored);
            //_stockService.IgnoredLowStock(ignored);
            //_stockService.savedata();
            //return NoContent();

        }

    }

}

