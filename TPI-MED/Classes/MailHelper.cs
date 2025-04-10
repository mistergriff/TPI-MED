using System.Net.Mail;
using System.Net;
using System;
using Wisej.Web;
using TPI_MED;

public static class MailHelper
{
    public static void EnvoyerMailValidation(string emailDestinataire, string token, string nom)
    {
        var lien = "https://dev.mediateur.mycpnv.ch/?token=" + token;

        var mail = new MailMessage();
        mail.To.Add(emailDestinataire);
        mail.Subject = "Validation de votre compte Journal de médiation";

        mail.IsBodyHtml = true;

        mail.Body = $@"
<html>
  <body style='font-family:Segoe UI, sans-serif; background-color:#f6f8fa; padding:20px;'>
    <div style='max-width:600px; margin:auto; background-color:white; padding:30px; border-radius:8px; box-shadow:0 2px 10px rgba(0,0,0,0.05);'>

      <h2 style='color:#007ACC;'>Bienvenue sur l'application Journal de médiation 👋</h2>
      <p>Merci pour votre inscription.</p>
      <p>Pour finaliser la création de votre compte, veuillez cliquer sur le bouton ci-dessous :</p>

      <div style='text-align:center; margin:30px 0;'>
        <a href='{lien}' style='background-color:#007ACC; color:white; padding:14px 28px; text-decoration:none; border-radius:5px; font-weight:bold; display:inline-block;'>
          Valider mon compte
        </a>
      </div>

      <p style='color:#666;'>Si vous n’avez pas demandé cette inscription, vous pouvez ignorer ce message.</p>
      <hr style='border:none; border-top:1px solid #eee;' />
      <p style='font-size:12px; color:#999;'>© 2025 Journal de médiation</p>

    </div>
  </body>
</html>
";

        string smtpHost = Program.Configuration["Smtp:Host"];
        string smtpUser = Program.Configuration["Smtp:User"];
        string smtpPass = Program.Configuration["Smtp:Password"];


        mail.From = new MailAddress(smtpUser, "Validation de compte");

        var smtp = new SmtpClient(smtpHost, 587);
        smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
        smtp.EnableSsl = true;

        try
        {
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur lors de l'envoi du mail : " + ex.Message);
        }
    }
}