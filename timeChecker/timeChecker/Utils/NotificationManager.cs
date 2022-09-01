using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace timeChecker.Utils
{
    internal class NotificationManager
    {


        public void CreateNotification()
        {
            var Notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "7 producten gaan vandaag over de datum!",
                Title = "LET OP!",
                ReturningData = "Data",
                NotificationId = 1253,
                Schedule =
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                    RepeatType = NotificationRepeat.TimeInterval,
                    NotifyRepeatInterval = TimeSpan.FromSeconds(5),
                }

            };

            LocalNotificationCenter.Current.Show(Notification);
        }
    }
}
