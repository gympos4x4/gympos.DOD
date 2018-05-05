using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace MailSender {
	class Program {

		private static MailAddress _senderAddress;
		private static SmtpClient _smtpClient;

		static void Main(string[] args) {
			_senderAddress = new MailAddress("columba@gympos.sk", "Columba Racing");
			_smtpClient = new SmtpClient("smtp.gympos.sk", 465) {
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("columba", "4X4/pse"),
				DeliveryMethod = SmtpDeliveryMethod.Network
			};
			
			var reciever = new MailAddress(args[0]);
			
			var mail = new MailMessage(_senderAddress, reciever)
			{
				HeadersEncoding = Encoding.UTF8,
				SubjectEncoding = Encoding.UTF8,
				BodyEncoding = Encoding.UTF8,
				Subject = Config.MailSubject,
			};

			var pic = new Attachment(args[1], new ContentType("image/jpeg"))
			{
				Name = Config.MailPhotoName,
				ContentDisposition = {Inline = true}
			};

			var plainTextView = AlternateView.CreateAlternateViewFromString(Config.MailPlainContent, Encoding.UTF8,
				MediaTypeNames.Text.Plain);
			var htmlView = AlternateView.CreateAlternateViewFromString(Config.MailHtmlContent, Encoding.UTF8,
				MediaTypeNames.Text.Html);

			var inline = new LinkedResource(args[1], MediaTypeNames.Image.Jpeg) {ContentId = "photo"};
			htmlView.LinkedResources.Add(inline);

			mail.AlternateViews.Add(plainTextView);
			mail.AlternateViews.Add(htmlView);

			mail.Attachments.Add(pic);

			_smtpClient.Send(mail);
		}
	}
}
