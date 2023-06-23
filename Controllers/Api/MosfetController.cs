using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Mosfet;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //[Authorize(Roles = "Admin")]
    [Route("api/Mosfet")]
    [ApiController]
    public class MosfetController : ControllerBase
    {
        private IMosfetService _MosfetService;

        private IMapper _mapper;


        public MosfetController(
           IMosfetService MosfetService,
            IMapper mapper
           )
        {
            _MosfetService = MosfetService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllMosfets(DataSourceLoadOptions loadOptions)
        {
            var Mosfets = _MosfetService.GetAllMosfets();
            var model = _mapper.Map<IList<MosfetModels>>(Mosfets);
            //return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Mosfets = _MosfetService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<MosfetModels>>(Mosfets);


            if (Mosfets == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, MosfetUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _MosfetService.MosfetExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _MosfetService.UpdateQuantity(id, Stock_Id);
                _MosfetService.SaveToDb();
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
        public ActionResult Update(int id, MosfetUpdateModels update)
        {
            var Stock_Id = _MosfetService.MosfetExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _MosfetService.UpdateQuantity(id, Stock_Id);
                _MosfetService.SaveToDb();
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
        public ActionResult<MosfetAddModels> Add(MosfetAddModels MosfetAddModels)
        {

            var add = _mapper.Map<Stock>(MosfetAddModels);


            _MosfetService.Add(add);
            _MosfetService.SaveToDb();

            var read = _mapper.Map<MosfetModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _MosfetService.MosfetExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _MosfetService.Delete(Stock_Id);
            _MosfetService.SaveToDb();
            return NoContent();

        }

    }
}
