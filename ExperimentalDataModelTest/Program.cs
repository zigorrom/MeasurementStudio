using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalDataModelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //IVDataModel model = new IVDataModel();
            //RawDataFormatter fmt = new RawDataFormatter();
            //using(Stream s = new FileStream(Directory.GetCurrentDirectory()+"\\text.txt",FileMode.Create,FileAccess.Write,FileShare.None))
            //{
            //    fmt.Serialize(s, model);
            //}

            MailMessage msg = new MailMessage(new MailAddress("i.zadorozhnyi@fz-juelich.de", "a"), new MailAddress("zigorrom@gmail.com", "z"));
            SmtpClient cli = new SmtpClient();
            cli.Port = 25;
            cli.DeliveryMethod = SmtpDeliveryMethod.Network;
            cli.UseDefaultCredentials = false;
            cli.Host = "smtp.fz-juelich.de";
            msg.Subject = "this is a test mail.";
            msg.Body = "body";
            cli.Send(msg);
        }
    }
}
