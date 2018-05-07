using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace NotificationApp
{
    public partial class MainWindow : Window
    {
        private ICollection<Timer> Timers { get; set; }

        public MainWindow()
        {
            Timers = new List<Timer>();

            UpdateTimers();
            InitializeComponent();
            UpdateNotificationList();
        }

        // Обновить список напоминаний
        public void UpdateNotificationList()
        {
            using (var context = new NotificationContext())
            {
                NotificationList.ItemsSource = context.Notifications
                            .OrderByDescending(x => x.CreateTime).ToList();
            }
        }

        // Обновить таймеры для напоминаний
        public void UpdateTimers(Notification newNotification = null)
        {
            if (newNotification == null)
            {
                using (var context = new NotificationContext())
                {
                    foreach (var ntf in context.Notifications.Where(n => n.NotifyTime > DateTime.Now))
                    {
                        Timers.Add(new Timer(new TimerCallback(Notify), ntf.NotificationText,
                                    GetMilliseconds(ntf.NotifyTime), Timeout.Infinite));
                    }
                }
            }
            else
            {
                Timers.Add(new Timer(new TimerCallback(Notify), newNotification.NotificationText,
                                    GetMilliseconds(newNotification.NotifyTime), Timeout.Infinite));
            }
        }

        public int GetMilliseconds(DateTime time)
        {
            double diff = (time - DateTime.Now).TotalMilliseconds;

            if (diff < 0)
            {
                return 0;
            }

            return (int)diff;
        }

        // Показать напоминание
        public void Notify(object notification)
        {
            MessageBox.Show(notification.ToString());
        }

        // Удалить
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (NotificationList.SelectedIndex == -1)
                return;

            Notification notificationToDelete = NotificationList.Items[NotificationList.SelectedIndex] as Notification;

            using (var context = new NotificationContext())
            {
                context.Entry(notificationToDelete).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            UpdateNotificationList();
        }

        // Создать
        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            Window createWindow = new CreateNotification(this);
            createWindow.ShowDialog();
        }
    }
}