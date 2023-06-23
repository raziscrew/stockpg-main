using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Diode;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{
    // [Authorize(Roles = "Admin")]
    [Route("api/Diode")]
    [ApiController]
    public class DiodeController : ControllerBase
    {
        private IDiodeService _DiodeService;

        private IMapper _mapper;


        public DiodeController(
           IDiodeService DiodeService,
            IMapper mapper
           )
        {
            _DiodeService = DiodeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllDiodes(DataSourceLoadOptions loadOptions)
        {
            var Diode = _DiodeService.GetAllDiodes();
            var model = _mapper.Map<IList<DiodeModels>>(Diode);
            //return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        //[AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Diode = _DiodeService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<DiodeModels>>(Diode);


            if (Diode == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, DiodeUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _DiodeService.DiodeExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _DiodeService.UpdateQuantity(id, Stock_Id);
                _DiodeService.SaveToDb();
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
        public ActionResult Update(int id, DiodeUpdateModels update)
        {
            var Stock_Id = _DiodeService.DiodeExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _DiodeService.UpdateQuantity(id, Stock_Id);
                _DiodeService.SaveToDb();
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
        public ActionResult<DiodeAddModels> Add(DiodeAddModels DiodeAddModels)
        {

            var add = _mapper.Map<Stock>(DiodeAddModels);


            _DiodeService.Add(add);
            _DiodeService.SaveToDb();

            var read = _mapper.Map<DiodeModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _DiodeService.DiodeExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _DiodeService.Delete(Stock_Id);
            _DiodeService.SaveToDb();
            return NoContent();

        }

    }
}
