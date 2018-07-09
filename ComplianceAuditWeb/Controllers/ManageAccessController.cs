﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ComplianceAuditWeb.Models;
using WebMatrix.WebData;

namespace ComplianceAuditWeb.Controllers
{
    public class ManageAccessController : Controller
    {
        // GET: ManageAccess
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccessViewModel accessmanagement)
        {
            AccessService.AccessServiceClient clientaccess = new AccessService.AccessServiceClient();
            //int UId = clientaccess.
            //if (UId != 0)
            //{
            //    return View("");
            //}
            //else
            //{
                return View();
            //}
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string EmailAddress) //can pass the viewmodel with property called emailID
        {
            if (ModelState.IsValid)
            {
                if (WebMatrix.WebData.WebSecurity.UserExists(EmailAddress))
                {
                    //string str = abc, dfg;

                    string  To = EmailAddress, UserId, UserPassword, SMTPPort, Host;
                    
                    string token = WebSecurity.GeneratePasswordResetToken(EmailAddress,1440);
                    if (token == null)
                    {
                        return View("");// if doesnt match 
                    }
                    else
                    {
                        var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = EmailAddress,
                            code = token }, "http") + "'>Reset Password</a>"; // creats url  with above token

                        // html template for send email
                        string subject = "Your Changed password";
                        string body = "<b> Please find the password reset link. </b><br/>" + lnkHref;

                        // get and set the appSettings using configuration manager.
                        EmailManager.AppSettings(out UserId, out UserPassword, out SMTPPort, out Host);
                        //call send email methods
                        EmailManager.SendEmail(UserId, subject, body,UserId, To, UserPassword, SMTPPort, Host);
                    }
                }
            }
            //AccessManagementServices.AccessManagementServicesClient clientaccess = new AccessManagementServices.AccessManagementServicesClient();
            //int UId =  clientaccess.GetPasswordData(management);
            //if(UId!= 0)
            //{
            //    return View("");
            //}
            //else
            //{
            //    return View("");
            //}
            return View();
        }
        public class EmailManager
        {
            public static void AppSettings(out string UserId,out string UserPassword, out string SMTPPort, out string Host)
            {
                UserId = ConfigurationManager.AppSettings.Get("UserId");
                UserPassword = ConfigurationManager.AppSettings.Get("UserPassword");
                SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
                Host = ConfigurationManager.AppSettings.Get("Host");
            }

            public static void SendEmail(string From, string Subject, string Body, string To, string UserId, string UserPassword, string SMTPPort,
                string Host)
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
               // MailMessage mailMessage = new MailMessage("mythriashok.aak@gmail.com", EmailAddress);
                mail.To.Add(To);
                mail.From = new System.Net.Mail.MailAddress(From);
                mail.Subject = Subject;
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.Port = Convert.ToInt16(SMTPPort);
                smtp.Credentials = new NetworkCredential(UserId, UserPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }

        public ActionResult ResetPassword(string code, string email)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ReturnToken = code;
            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if(ModelState.IsValid)
            {
                bool resetResponse = WebSecurity.ResetPassword(model.ReturnToken, model.Password);
                if(resetResponse)
                {
                    ViewBag.Message = "Successfully changed";
                }
                else
                {
                    ViewBag.Message = "went wrong";
                }
            }
            return View(model);
        }


        public ActionResult UpdateResult()
        {
            return View();
        }

        //public ActionResult UpdatePassword(string userName, string currentPassword, string newPassword)
        //{
        //   bool token= WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        //    if(token!= false)
        //    {
        //        return View("");
        //    }
        //    else
        //    {
        //        return View();
        //    }
            
            //AccessManagementServices.AccessManagementServicesClient clientaccess = new AccessManagementServices.AccessManagementServicesClient();
            //string result = clientaccess.updatePasswordData(viewmodel);
           // return View();
        }






    }

    
    
