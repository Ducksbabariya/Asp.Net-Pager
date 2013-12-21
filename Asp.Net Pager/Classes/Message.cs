using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
namespace TechTracking.Classes
{
    public enum NotificationType
    {
        Error,
        Information,
        Success
    }
    public static class Message
    {
        /// <summary>
        /// Shows the errors.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="text">The text.</param>
        public static void ShowError(this Page page, string text)
        {

            ShowNotification(page, NotificationType.Error, text, false);
        }

        /// <summary>
        /// Shows the information.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="text">The text.</param>
        public static void ShowInformation(this Page page, string text)
        {
            ShowNotification(page, NotificationType.Information, text, true);
        }

        /// <summary>
        /// Shows the errors.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="text">The text.</param>
        public static void ShowNotification(this Page page, NotificationType notificationType, string text, bool autoClose)
        {
            string className = null;
            switch (notificationType)
            {
                case NotificationType.Error:
                    className = "fail";
                    break;
                case NotificationType.Information:
                    className = "notification";
                    break;
                case NotificationType.Success:
                    className = "success";
                    break;
            }

            string notification = "jQuery('body').showMessage({'thisMessage':['" + text.Replace(Environment.NewLine, "','") + "'],'className':'" + className + "','autoClose':" + autoClose.ToString().ToLower() + ",'delayTime':4000,'displayNavigation':" + (!autoClose).ToString().ToLower() + ",'useEsc':" + (!autoClose).ToString().ToLower() + "});";

            //if (ScriptManager.GetCurrent(page) != null)
            //{
            //    //ScriptManager.GetCurrent(page).ResponseScripts.Add(notification);
            //}
            //else
            //{
            if (ScriptManager.GetCurrent(page) != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(),
                                                    "notification",
                                                    notification,
                                                    true);
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(),
                                                        "notification",
                                                        notification,
                                                        true);
            }
            //}
        }

        /// <summary>
        /// Shows the notifications.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="text">The text.</param>
        public static void ShowSuccess(this Page page, string text)
        {
            ShowNotification(page, NotificationType.Success, text, true);
        }

    }
}