using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Resistor;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //[Authorize(Roles = "Admin")]
    [Route("api/Resistor")]
    [ApiController]
    public class ResistorController : ControllerBase
    {
        private IResistorService _ResistorService;

        private IMapper _mapper;


        public ResistorController(
           IResistorService ResistorService,
            IMapper mapper
           )
        {
            _ResistorService = ResistorService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllOptocouplers(DataSourceLoadOptions loadOptions)
        {
            var Resistors = _ResistorService.GetAllResistors();
            var model = _mapper.Map<IList<ResistorModels>>(Resistors);
            //return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Resistances}")]
        public ActionResult SearchByResistance(string Resistances)
        {
            var Resistors = _ResistorService.SearchByResistance(Resistances);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<ResistorModels>>(Resistors);


            if (Resistors == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, ResistorUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _ResistorService.ResistorExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _ResistorService.UpdateQuantity(id, Stock_Id);
                _ResistorService.SaveToDb();
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
        public ActionResult Update(int id, ResistorUpdateModels update)
        {
            var Stock_Id = _ResistorService.ResistorExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _ResistorService.UpdateQuantity(id, Stock_Id);
                _ResistorService.SaveToDb();
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
        public ActionResult<ResistorAddModels> Add(ResistorAddModels ResistorAddModels)
        {

            var add = _mapper.Map<Stock>(ResistorAddModels);


            _ResistorService.Add(add);
            _ResistorService.SaveToDb();

            var read = _mapper.Map<ResistorModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _ResistorService.ResistorExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _ResistorService.Delete(Stock_Id);
            _ResistorService.SaveToDb();
            return NoContent();

        }

    }
}
