using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.IO;
using UserLibrary.ViewModel;

namespace UserLibrary.BLL
{
    class EmailLogic
    {
        List<string> errors = new List<string>();

        public static Boolean SendMail(EmailViewModel view)
        {
            if (view.FromEmail == null || view.FromEmail == string.Empty)
                view.Errors.Add("EmailAddress was not found as a AppSetting Key.");

            if (view.FromName == null || view.FromName == string.Empty)
                view.Errors.Add("EmailName was not found as a AppSetting Key.");

            if (view.FromPassword == null || view.FromPassword == string.Empty)
                view.Errors.Add("EmailPasscode was not found as a AppSetting Key.");

            if (view.Body == null || view.Body == string.Empty)
                view.Errors.Add("Please Provide Content for the Email Body");
            if (view.Subject == null || view.Subject == string.Empty)
                view.Errors.Add("Please Provide Content for the Email Subject");

            if (view.Errors.Count > 0)
                return false;

            var fromAddress = new MailAddress(view.FromEmail, view.FromName);
            var toAddress = new MailAddress(view.ToEmail, view.ToName);

            view.smtp.Credentials = new NetworkCredential(fromAddress.Address, view.FromPassword);

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = view.Subject,
                Body = view.Body,
                IsBodyHtml = true
            })
            {
                view.smtp.Send(message);
            }

            return true;
        }

        public static Boolean SendVerificationCode(EmailViewModel view)
        {
            foreach (string[] s in view.BodyParamaters)
            {
                view.Body = view.Body.Replace(s[0].ToString(), s[1].ToString());
            }

            try
            {
                return SendMail(view);
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean SendPasswordResetEmail(EmailViewModel view)
        {
            foreach (string[] s in view.BodyParamaters)
            {
                view.Body = view.Body.Replace(s[0].ToString(), s[1].ToString());
            }

            try
            {
                return SendMail(view);
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }

        public static Boolean GetBodyFromHTML(string path, EmailViewModel view)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    view.Body = reader.ReadToEnd();
                }
                return true;
            }
            catch (Exception ex)
            {
                view.Errors.Add(ex.Message);
                return false;
            }
        }
    }
}
