using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Ic;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //  [Authorize(Roles = "Admin")]
    [Route("api/Ic")]
    [ApiController]
    public class IcController : ControllerBase
    {
        private IIcService _ICService;

        private IMapper _mapper;


        public IcController(
           IIcService ICSService,
            IMapper mapper
           )
        {
            _ICService = ICSService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllDiodes(DataSourceLoadOptions loadOptions)
        {
            var Ics = _ICService.GetAllIcs();
            var model = _mapper.Map<IList<IcModels>>(Ics);
            Response.Headers.Add("TotalCount", model.Count.ToString());
            //return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Ics = _ICService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<IcModels>>(Ics);


            if (Ics == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, IcUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _ICService.IcExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _ICService.UpdateQuantity(id, Stock_Id);
                _ICService.SaveToDb();
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
        public ActionResult Update(int id, IcUpdateModels update)
        {
            var Stock_Id = _ICService.IcExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _ICService.UpdateQuantity(id, Stock_Id);
                _ICService.SaveToDb();
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
        public ActionResult<IcAddModels> Add(IcAddModels IcAddModels)
        {

            var add = _mapper.Map<Stock>(IcAddModels);


            _ICService.Add(add);
            _ICService.SaveToDb();

            var read = _mapper.Map<IcModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _ICService.IcExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _ICService.Delete(Stock_Id);
            _ICService.SaveToDb();
            return NoContent();

        }

    }
}
