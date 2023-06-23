using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Lcd;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //[Authorize(Roles = "Admin")]
    [Route("api/Lcd")]
    [ApiController]
    public class LcdController : ControllerBase
    {
        private ILcdService _LcdService;

        private IMapper _mapper;


        public LcdController(
           ILcdService LcdService,
            IMapper mapper
           )
        {
            _LcdService = LcdService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllLcds(DataSourceLoadOptions loadOptions)
        {
            var Lcds = _LcdService.GetAllLcds();
            var model = _mapper.Map<IList<LcdModels>>(Lcds);
            //return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Lcds = _LcdService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<LcdModels>>(Lcds);


            if (Lcds == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, LcdUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _LcdService.LcdExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _LcdService.UpdateQuantity(id, Stock_Id);
                _LcdService.SaveToDb();
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
        public ActionResult Update(int id, LcdUpdateModels update)
        {
            var Stock_Id = _LcdService.LcdExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _LcdService.UpdateQuantity(id, Stock_Id);
                _LcdService.SaveToDb();
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
        public ActionResult<LcdAddModels> Add(LcdAddModels LcdAddModels)
        {

            var add = _mapper.Map<Stock>(LcdAddModels);


            _LcdService.Add(add);
            _LcdService.SaveToDb();

            var read = _mapper.Map<LcdModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _LcdService.LcdExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _LcdService.Delete(Stock_Id);
            _LcdService.SaveToDb();
            return NoContent();

        }

    }
}
