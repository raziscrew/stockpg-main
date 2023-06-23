using JPGStockServer.Entities;
using JPGStockServer.Entities.Notification;
using JPGStockServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JPGStockServer.Services
{


    public interface INotificationService
    {
        IEnumerable<NotificationRequest> GetAllNotifications();
        IEnumerable<NotificationRequest> GetUnreadNotifecation();

        void UpdateStatus(long id, NotificationRequest Status);

        void Update(long id, NotificationRequest update);

        void Add(NotificationRequest Add);

        void Delete(NotificationRequest Delete);

        NotificationRequest NotificationExists(long id);


        bool SaveToDb();
    }
    public class NotificationService : INotificationService
    {
        private DataContext _context;
        public NotificationService(DataContext context)
        {
            _context = context;
        }

        public void Add(NotificationRequest Add)
        {

            if (Add == null)

            {
                throw new ArgumentNullException(nameof(Add));
            }
            _context.Notification.Add(Add);



        }

        public NotificationRequest NotificationExists(long id)
        {
            return _context.Notification.FirstOrDefault(e => e.NOTIFICATIONS_ID == id);
        }


        public void Delete(NotificationRequest Delete)
        {
            if (Delete == null)

            {
                throw new ArgumentNullException(nameof(Add));
            }
            _context.Notification.Remove(Delete);
        }

        public IEnumerable<NotificationRequest> GetAllNotifications()
        {

            var result = _context.Notification;
            return result;
        }

        public IEnumerable<NotificationRequest> GetUnreadNotifecation()
        {
            var result = _context.Notification.Where(q => q.READ == 1).OrderBy(on => on.NOTIFICATIONS_ID);
            return result;
        }

        public bool SaveToDb()
        {
            return (_context.SaveChanges() > 0);
        }

       

        public void Update(long id, NotificationRequest update)
        {
            //Noting
        }

        public void UpdateStatus(long id, NotificationRequest Status)
        {
            //Noting
        }
    }
}
