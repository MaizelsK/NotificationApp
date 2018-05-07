using System;
using System.Windows;

namespace NotificationApp
{
    public partial class CreateNotification : Window
    {
        private MainWindow MainWindow { get; set; }

        public CreateNotification(MainWindow main)
        {
            this.MainWindow = main;
            InitializeComponent();
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (ValidateNotification())
            {
                Notification newNotification = new NotificationApp.Notification
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    NotificationText = Notification.Text
                                        .Substring(0, Notification.Text.Length - 2),
                    NotifyTime = (DateTime)NotifyDateTime.Value
                };

                using (var context = new NotificationContext())
                {
                    context.Notifications.Add(newNotification);
                    context.SaveChanges();
                }

                MainWindow.UpdateTimers(newNotification);
                MainWindow.UpdateNotificationList();
                this.Close();
            }
        }

        public bool ValidateNotification()
        {
            if (NotifyDateTime.Value == null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Выберите дату напоминания!");
                return false;
            }
            if (NotifyDateTime.Value < DateTime.Now)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Нельзя выбрать прошедшую дату");
                return false;
            }

            if (Notification.Text == "\r\n" || Notification.Text == "")
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Введите текст напоминания!");
                return false;
            }

            return true;
        }
    }
}