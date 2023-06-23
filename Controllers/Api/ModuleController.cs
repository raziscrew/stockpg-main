using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Entities;
using JPGStockServer.Models.Module;
using JPGStockServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JPGStockServer.Controllers
{

    //   [Authorize(Roles = "Admin")]
    [Route("api/Module")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private IModuleService _ModuleService;

        private IMapper _mapper;


        public ModuleController(
           IModuleService ModuleService,
            IMapper mapper
           )
        {
            _ModuleService = ModuleService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //  [Authorize(Roles = "Admin,rara")]
        [HttpGet]
        public IActionResult GetAllModules(DataSourceLoadOptions loadOptions)
        {
            var Modules = _ModuleService.GetAllModules();
            var model = _mapper.Map<IList<ModuleModels>>(Modules);
            // return Ok(model);
            var result = DataSourceLoader.Load(model, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("{Partnumbers}")]
        public ActionResult SearchByPartNumber(string Partnumbers)
        {
            var Modules = _ModuleService.SearchByPartNumber(Partnumbers);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _mapper.Map<IList<ModuleModels>>(Modules);


            if (Modules == null)
            {
                return NotFound();
            }



            return Ok(model);
        }

        [HttpPut("UpdateQuantity/{id}")]
        public ActionResult UpdateQuantity(int id, ModuleUpdateQuantityModels updateQuantity)
        {
            var Stock_Id = _ModuleService.ModuleExists(id);

            if (id != updateQuantity.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateQuantity, Stock_Id);
                _ModuleService.UpdateQuantity(id, Stock_Id);
                _ModuleService.SaveToDb();
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
        public ActionResult Update(int id, ModuleUpdateModels update)
        {
            var Stock_Id = _ModuleService.ModuleExists(id);

            if (id != update.STOCK_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(update, Stock_Id);
                _ModuleService.UpdateQuantity(id, Stock_Id);
                _ModuleService.SaveToDb();
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
        public ActionResult<ModuleAddModels> Add(ModuleAddModels ModuleAddModels)
        {

            var add = _mapper.Map<Stock>(ModuleAddModels);


            _ModuleService.Add(add);
            _ModuleService.SaveToDb();

            var read = _mapper.Map<ModuleModels>(add);

            return Ok(read);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var Stock_Id = _ModuleService.ModuleExists(id);
            if (Stock_Id == null)
            {
                return NotFound();
            }

            _ModuleService.Delete(Stock_Id);
            _ModuleService.SaveToDb();
            return NoContent();

        }

    }
}
