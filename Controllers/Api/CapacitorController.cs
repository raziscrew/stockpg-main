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
using System.Collections.Generic;


namespace JPGStockServer.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("api/Capacitor")]
    [ApiController]
    public class CapacitorController : ControllerBase
    {
        private ICapacitorService _CapacitorService;

        private IMapper _mapper;


        public CapacitorController(
           ICapacitorService CapacitorService,
            IMapper mapper
           )
        {
            _CapacitorService = CapacitorService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllCapacitors(DataSourceLoadOptions loadOptions)
        {

            var Capacitor = _CapacitorService.GetAllCapacitors();
            var model = _mapper.Map<IList<CapacitorModels>>(Capacitor);
            //  return Ok(model);

            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{CapacitanceValue}")]
        public ActionResult SearchByCapacitance(string CapacitanceValue)
        {
            var cap = _CapacitorService.SearchByCapacitance(CapacitanceValue);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<CapacitorModels>>(cap);


            if (cap == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, CapacitorUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _CapacitorService.CapacitorExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _CapacitorService.UpdateQuantity(id, Stock_Id);
                _CapacitorService.SaveToDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (Stock_Id == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();


        }


        [Authorize(Roles = "Admin,User")]
        [HttpPut("Update/{id}")]
        public ActionResult Update(int id, int QuantityNotNull, CapacitorUpdateModels update)
        {
            var Stock_Id = _CapacitorService.CapacitorExists(id);
            var Quantity = _CapacitorService.QuantitynotNull(QuantityNotNull);
            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _CapacitorService.UpdateQuantity(id, Stock_Id);
                _CapacitorService.SaveToDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (Stock_Id == null)
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
            }

            return NoContent();


        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CapacitorAddModels> Add(CapacitorAddModels capacitorAddModels)
        {

            var add = _mapper.Map<Stock>(capacitorAddModels);


            _CapacitorService.Add(add);
            _CapacitorService.SaveToDb();

            var read = _mapper.Map<CapacitorModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _CapacitorService.CapacitorExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _CapacitorService.Delete(Stock_Id);
            _CapacitorService.SaveToDb();
            return NoContent();

        }

    }
}
