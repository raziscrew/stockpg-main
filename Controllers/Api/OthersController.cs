using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Others;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    // [Authorize(Roles = "Admin")]
    [Route("api/Others")]
    [ApiController]
    public class OthersController : ControllerBase
    {
        private IOthersService _OthersService;

        private IMapper _mapper;


        public OthersController(
           IOthersService OthersService,
            IMapper mapper
           )
        {
            _OthersService = OthersService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllOtherss(DataSourceLoadOptions loadOptions)
        {
            var Optocouplers = _OthersService.GetAllOtherss();
            var model = _mapper.Map<IList<OthersModels>>(Optocouplers);
            // return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Otherss = _OthersService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<OthersModels>>(Otherss);


            if (Otherss == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, OthersUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _OthersService.OthersExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _OthersService.UpdateQuantity(id, Stock_Id);
                _OthersService.SaveToDb();
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
        public ActionResult Update(int id, OthersUpdateModels update)
        {
            var Stock_Id = _OthersService.OthersExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _OthersService.UpdateQuantity(id, Stock_Id);
                _OthersService.SaveToDb();
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
        public ActionResult<OthersAddModels> Add(OthersAddModels OthersAddModels)
        {

            var add = _mapper.Map<Stock>(OthersAddModels);


            _OthersService.Add(add);
            _OthersService.SaveToDb();

            var read = _mapper.Map<OthersModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _OthersService.OthersExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _OthersService.Delete(Stock_Id);
            _OthersService.SaveToDb();
            return NoContent();

        }

    }
}
