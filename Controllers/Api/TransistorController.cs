using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Transistor;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //  [Authorize(Roles = "Admin")]
    [Route("api/Transistor")]
    [ApiController]
    public class TransistorController : ControllerBase
    {
        private ITransistorService _TransistorService;

        private IMapper _mapper;


        public TransistorController(
           ITransistorService TransistorService,
            IMapper mapper
           )
        {
            _TransistorService = TransistorService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllOptocouplers(DataSourceLoadOptions loadOptions)
        {
            var Transistors = _TransistorService.GetAllTransistors();
            var model = _mapper.Map<IList<TransistorModels>>(Transistors);
            //return Ok(model);

            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumbers(string Partnumbers)
        {
            var Transistors = _TransistorService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<TransistorModels>>(Transistors);


            if (Transistors == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, TransistorUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _TransistorService.TransistorExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _TransistorService.UpdateQuantity(id, Stock_Id);
                _TransistorService.SaveToDb();
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
        public ActionResult Update(int id, TransistorUpdateModels update)
        {
            var Stock_Id = _TransistorService.TransistorExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _TransistorService.UpdateQuantity(id, Stock_Id);
                _TransistorService.SaveToDb();
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
        public ActionResult<TransistorAddModels> Add(TransistorAddModels TransistorAddModels)
        {

            var add = _mapper.Map<Stock>(TransistorAddModels);


            _TransistorService.Add(add);
            _TransistorService.SaveToDb();

            var read = _mapper.Map<TransistorModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _TransistorService.TransistorExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _TransistorService.Delete(Stock_Id);
            _TransistorService.SaveToDb();
            return NoContent();

        }

    }
}
