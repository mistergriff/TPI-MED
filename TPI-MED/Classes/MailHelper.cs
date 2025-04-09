using System.Net.Mail;
using System.Net;
using System;
using Wisej.Web;
using TPI_MED;
using Microsoft.Extensions.Configuration;

public static class MailHelper
{
    public static void EnvoyerMailValidation(string emailDestinataire, string token, string nom)
    {
        var lien = "https://dev.mediateur.mycpnv.ch/?token=" + token;

        var mail = new MailMessage();
        mail.To.Add(emailDestinataire);
        mail.Subject = "Validation de votre compte Journal de médiation";

        mail.Body = $@"
                Bonjour {nom},

                Merci pour votre inscription sur l'application Journal de médiation.

                Veuillez cliquer sur le lien suivant pour valider votre compte :
                {lien}

                À bientôt,
                L’équipe Journal de médiation
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