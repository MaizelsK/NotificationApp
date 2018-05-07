using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NotificationApp
{
    public class NotificationContext : DbContext
    {
        public NotificationContext() : base("NotificationDb")
        {
            Database.SetInitializer(new NotificationDbInitializer());
        }

        public DbSet<Notification> Notifications { get; set; }
    }

    public class NotificationDbInitializer : CreateDatabaseIfNotExists<NotificationContext>
    {
        protected override void Seed(NotificationContext context)
        {
            List<Notification> notifications = new List<Notification>
            {
                new Notification
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    NotificationText = "Напоминание 1",
                    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                              DateTime.Now.Hour, DateTime.Now.Minute + 2, 0)
                },
                new Notification
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    NotificationText = "Напоминание 2",
                    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                              DateTime.Now.Hour, DateTime.Now.Minute + 1, 0)
                }
            };

            context.Notifications.AddRange(notifications);
            base.Seed(context);
        }
    }
}
