using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Optocoupler;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //[Authorize(Roles = "Admin")]
    [Route("api/Optocoupler")]
    [ApiController]
    public class OptocouplerController : ControllerBase
    {
        private IOptocouplerService _OptocouplerService;

        private IMapper _mapper;


        public OptocouplerController(
           IOptocouplerService OptocouplerService,
            IMapper mapper
           )
        {
            _OptocouplerService = OptocouplerService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllOptocouplers(DataSourceLoadOptions loadOptions)
        {
            var Optocouplers = _OptocouplerService.GetAllOptocouplers();
            var model = _mapper.Map<IList<OptocouplerModels>>(Optocouplers);
            // return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");

        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Optocouplers = _OptocouplerService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<OptocouplerModels>>(Optocouplers);


            if (Optocouplers == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, OptocouplerUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _OptocouplerService.OptocouplerExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _OptocouplerService.UpdateQuantity(id, Stock_Id);
                _OptocouplerService.SaveToDb();
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
        public ActionResult Update(int id, OptocouplerUpdateModels update)
        {
            var Stock_Id = _OptocouplerService.OptocouplerExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _OptocouplerService.UpdateQuantity(id, Stock_Id);
                _OptocouplerService.SaveToDb();
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
        public ActionResult<OptocouplerAddModels> Add(OptocouplerAddModels OptocouplerAddModels)
        {

            var add = _mapper.Map<Stock>(OptocouplerAddModels);


            _OptocouplerService.Add(add);
            _OptocouplerService.SaveToDb();

            var read = _mapper.Map<OptocouplerModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _OptocouplerService.OptocouplerExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _OptocouplerService.Delete(Stock_Id);
            _OptocouplerService.SaveToDb();
            return NoContent();

        }

    }
}
