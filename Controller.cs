namespace YourNameSpace.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using System.Configuration;
    using System.Net.Mail;

    using YourNameSpace.Models;

    using Umbraco.Web;
    using Recaptcha.Web;
    using Recaptcha.Web.Mvc;
    using RJP.MultiUrlPicker.Models;

    public class ContactFormController : SurfaceController
    {
        [ChildActionOnly] //Prevent the action method from being invoked as a result of a user request i.e. by manipulation the URL. 
        public ActionResult ShowContactForm()
        {
            return this.PartialView("ContactForm", new ContactFormModel());
        }

        [HttpPost]
        public ActionResult SubmitContactForm(ContactFormModel model) //Use ActionResult to handle the user interaction
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            //Check if reCAPTCHA has a result
            if (string.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("reCAPTCHA", "Please complete the reCAPTCHA");
                return CurrentUmbracoPage();
            }
            else
            {
                //Check if reCAPTCHA has a success result
                RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
                if (recaptchaResult != RecaptchaVerificationResult.Success)
                {
                    ModelState.AddModelError("reCAPTCHA", "The reCAPTCHA is incorrect!");
                    return CurrentUmbracoPage();
                }
            }

            //Check if the data posted is valid
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            string managerEmail = CurrentPage.HasValue("notifyEmail") ? CurrentPage.GetPropertyValue<string>("notifyEmail") : string.Empty;

            //Send email to manager
            SendNotificationToManager(model, managerEmail);

            //Send an auto replied email back to the clients
            SendAutoResponder(model);

            //Check if redirectionPage Url is empty
            var redirectionPage = CurrentPage.GetPropertyValue<Link>("redirection");
            //If it is, then redirect page to the Home page
            if (string.IsNullOrWhiteSpace(redirectionPage?.Url))
            {
                return this.RedirectToUmbracoPage(CurrentPage.Site());
            }
            //Otherwise, redirect it to the redirection page
            return this.Redirect(redirectionPage.Url);

        }

        private void SendNotificationToManager(ContactFormModel model, string managerEmail)
        {
            //Create subject
            var subject = string.Format("New Contact message from website {0}.", this.Request.Url.Host);

            //Create message content
            var message = string.Format(
                @"First Name: {0}<br/>
                  Last Name: {1}<br/>
                  Email: {2}<br/>
                  {3}",
                model.FirstName,
                model.LastName,
                model.Email,
                model.Message
                );

            //Assemble as an email
            var email = new MailMessage
            {
                Subject = subject,
                From = new MailAddress(model.Email),
                Body = message,
                IsBodyHtml = true
            };

            //Set up developer email as a fallback option
            var developerEmail = ConfigurationManager.AppSettings["DeveloperEmail"];

            // configure the TO email address as manager Email, and fallback to developer's email - so we don't miss out any message
            email.To.Add(string.IsNullOrWhiteSpace(managerEmail) ? developerEmail : managerEmail);

            var isSendCopyToDev = bool.Parse(ConfigurationManager.AppSettings["SendEmailCopyToDev"]);

            //if SendEmailCopyToDev is True, then add Dev Email to Bcc
            if (isSendCopyToDev)
            {
                email.Bcc.Add(developerEmail);
            }

            //Send email
            try
            {
                //Connect to SMTP credentials set in web.config
                SmtpClient smtp = new SmtpClient();
                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SendAutoResponder(ContactFormModel model)
        {
            //Create subject
            var subject = string.Format("Contact form submission on {0}.", this.Request.Url.Host);

            //Create message content
            var message = @"Hi, <br/>
                  Thank you for reaching out. We'll get back to you within 72 hours.<br/>
                  Cheers,<br/>
                  Support team<br/>";

            //Assemble as an email
            var email = new MailMessage
            {
                Subject = subject,
                To = {model.Email},
                From = new MailAddress("noreply@tedxnewplymouth.co.nz"),
                Body = message,
                IsBodyHtml = true
            };

            //Set up developer email as a fallback option
            var developerEmail = ConfigurationManager.AppSettings["DeveloperEmail"];

            var isSendCopyToDev = bool.Parse(ConfigurationManager.AppSettings["SendEmailCopyToDev"]);

            //if SendEmailCopyToDev is True, then add Dev Email to Bcc
            if (isSendCopyToDev)
            {
                email.Bcc.Add(developerEmail);
            }

            //Send email
            try
            {
                //Connect to SMTP credentials set in web.config
                SmtpClient smtp = new SmtpClient();
                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
