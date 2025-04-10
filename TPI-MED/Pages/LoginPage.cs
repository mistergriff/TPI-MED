using System;
using Wisej.Web;

    public partial class LoginPage : Page
{
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string identifiant = txtIdentifiant.Text;
            string motDePasse = txtMotDePasse.Text;

            UtilisateurDAO dao = new UtilisateurDAO();
            Utilisateur utilisateur = dao.GetByNomOuEmail(identifiant);

            if (utilisateur == null)
            {
                MessageBox.Show("Utilisateur ou mot de passe incorrect.");
                return;
            }

            if (!utilisateur.EstValide)
            {
                MessageBox.Show("Veuillez valider votre compte par e-mail.");
                return;
            }

            string hash = PasswordHelper.HashPassword(motDePasse, utilisateur.Sel);
            if (hash == utilisateur.MotDePasse)
            {
                AlertBox.Show("Connexion réussie !", icon: MessageBoxIcon.Information);
                Application.MainPage = new HomePage();
            }
            else
            {
                MessageBox.Show("Utilisateur ou mot de passe incorrect.");
            }
        }

        private void btnCreerCompte_Click(object sender, EventArgs e)
        {
            Application.MainPage = new RegisterPage();
        }
    }
