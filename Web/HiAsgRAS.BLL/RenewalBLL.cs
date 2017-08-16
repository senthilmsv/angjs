using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.ViewModel;
using HiAsgRAS.DAL.Repositories;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.DAL.EntityModels;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace HiAsgRAS.BLL
{
    public class RenewalBLL : IRenewalBLL
    {
        private IRenewalRepository _IRenewalRepository;

        public RenewalBLL(IRenewalRepository renewalRepository)
        {
            _IRenewalRepository = renewalRepository;
        }


        public void UpdateRenewalRecord(RenewalViewModel renewalDetailModel)
        {
            _IRenewalRepository.UpdateRenewalRecord(renewalDetailModel);
        }

        public string composeMailBody(string appname, string renewalDate, string ownerPrimary, string mailPrimary, string ownerSecondary, string mailSecondary, int applicationId, string uniqueId)
        {
            string body = PopulateBody();
            body = body.Replace("{ApplicationID}", applicationId.ToString());
            body = body.Replace("{UniqueId}", uniqueId);
            body = body.Replace("{Application}", appname);
            body = body.Replace("{ApplicationRenewalDate}", renewalDate);
            body = body.Replace("{BAOwnerPrimary}", ownerPrimary);
            body = body.Replace("{BAEmailPrimary}", mailPrimary);
            body = body.Replace("{BAOwnerSecondary}", ownerSecondary);
            body = body.Replace("{BAEmailSecondary}", mailSecondary);
         
            return body;
        }


        public string PopulateBody()
        {

            string body = string.Empty;

            string pathfortemplate = string.Empty;

            pathfortemplate = ConfigurationManager.AppSettings["TemplatePath"];

            //StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm"))

            using (StreamReader reader = new StreamReader(pathfortemplate))
            {

                body = reader.ReadToEnd();

            }

            return body;

        }



        public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {

            //Calling first method
            using (MailMessage mailMessage = new MailMessage())
            {

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Sender"]);

                mailMessage.Subject = subject;


                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress(recepientEmail));


                SmtpClient smtp = new SmtpClient();

                smtp.Host = ConfigurationManager.AppSettings["Host"];

                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);




                //smtp.Port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString());

                smtp.Send(mailMessage);
                Console.WriteLine("Mail Sent\n");
            }

        }



        public RenewalViewModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(RenewalViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(RenewalViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(RenewalViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<RenewalViewModel> GetAll()
        {
            List<RenewalViewModel> renewalModel = new List<RenewalViewModel>();
            var results = _IRenewalRepository.GetAll().ToList();

            if (results != null)
            {
                renewalModel = MappingHelper.MappingHelper.MapRenewalEntitiesToModels(results);
            }

            return renewalModel;
        }

        public IQueryable<RenewalViewModel> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}