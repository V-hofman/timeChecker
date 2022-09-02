using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace timeChecker.Utils
{
    internal class NotificationManager
    {


        public void CreateNotification(int amount)
        {
            if(amount == 0)
            {
                return;
            }
            string desc = amount + " producten gaan vandaag over de datum!";
            var Notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = desc,
                Title = "LET OP!",
                NotificationId = 1253,
                Schedule =
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                    RepeatType = NotificationRepeat.TimeInterval,
                    NotifyRepeatInterval = TimeSpan.FromHours(1),
                }

            };

            LocalNotificationCenter.Current.Show(Notification);
        }

    }
}
