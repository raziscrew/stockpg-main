using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DevExtreme.AspNet.Mvc;
using JPGStockServer.Services;
using AutoMapper;
using JPGStockServer.Entities.Notification;
using JPGStockServer.Models.Notification;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace JPGStockServer.Controllers
{ // [Authorize(Roles = "Admin")]
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationService _NotificationService;

        private IMapper _mapper;


        public NotificationController(
          INotificationService NotificationService,
           IMapper mapper
          )
        {
            _NotificationService = NotificationService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllNotifications(DataSourceLoadOptions loadOptions)
        {
            var Notification = _NotificationService.GetAllNotifications().OrderBy(a=>a.READ).ToList();
           // var model = _mapper.Map<IList<NotificationRequest1>>(Notification);
            //return Ok(model);
            var result = DataSourceLoader.Load(Notification, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }


        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet("UnredNotifecation")]
        public IActionResult GetUnreadNotifecation(DataSourceLoadOptions loadOptions)
        {
            var Notification = _NotificationService.GetUnreadNotifecation();
            // var model = _mapper.Map<IList<NotificationRequest1>>(Notification);
            //return Ok(model);
            var result = DataSourceLoader.Load(Notification, loadOptions);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "application/json");
        }
        [AllowAnonymous]
        [HttpPut("UpdateStatus/{id}")]
        public ActionResult UpdateStatus(int id, NotificationStatusModels updateStatus)
        {
            var NotificationID = _NotificationService.NotificationExists(id);

            if (id != updateStatus.NOTIFICATIONS_ID)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(updateStatus, NotificationID);
                _NotificationService.UpdateStatus(id, NotificationID);
                _NotificationService.SaveToDb();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (NotificationID == null)
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
        [HttpPost]
        public ActionResult<NotificationAddModels> Add(NotificationAddModels NotificationAddModels)
        {

            var add = _mapper.Map<NotificationRequest>(NotificationAddModels);


            _NotificationService.Add(add);
            _NotificationService.SaveToDb();

            var read = _mapper.Map<NotificationRequestModels>(add);

            return Ok(read);
        }



       // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {

            var NotificationId = _NotificationService.NotificationExists(id);
            if (NotificationId == null)
            {
                return NotFound();
            }

            _NotificationService.Delete(NotificationId);
            _NotificationService.SaveToDb();
            return NoContent();

        }

    }
}
