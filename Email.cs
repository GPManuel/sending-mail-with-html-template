public void EnviarNotificación(string usuario, string mensaje)
{
    string body = string.Empty;
    
    //using streamreader for reading my htmltemplate
    var assembly = typeof(App).GetTypeInfo().Assembly;
    Stream stream = assembly.GetManifestResourceStream("EspacioDeNombres.Carpeta.EmailTemplate.html");
    using (StreamReader reader = new StreamReader(stream))
    {
        body = reader.ReadToEnd();
    }

    body = body.Replace("{UserName}", System.Environment.UserName);
    body = body.Replace("{message}", mensaje);

    SendEmail("ASUNTO DEL EMAIL", body);
}

public void SendEmail(string asunto, string mensaje)
{            
      try
      {
          MailMessage mail = new MailMessage();
          SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

          mail.From = new MailAddress("emisor@mail.com");
          mail.To.Add("receptor@mail.com");
          mail.Subject = asunto;
          mail.Body = mensaje;
          mail.IsBodyHtml = true;

          SmtpServer.Port = 587;
          SmtpServer.Host = "smtp.office365.com";
          SmtpServer.EnableSsl = true;
          SmtpServer.UseDefaultCredentials = false;
          SmtpServer.Credentials = new System.Net.NetworkCredential("emisor@mail.com", "password");

          SmtpServer.Send(mail);
      }
      catch (Exception ex)
      {
          DisplayAlert("Faild", ex.Message, "OK");  
      }
}
