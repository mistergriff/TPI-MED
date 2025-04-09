using System;
using Wisej.Web;

    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void btnInscription_Click(object sender, EventArgs e)
        {
            string nom = txtNom.Text;
            string email = txtEmail.Text;
            string motDePasse = txtMotDePasse.Text;
            string confirmation = txtConfirmation.Text;

            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(motDePasse))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            if (motDePasse != confirmation)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
                return;
            }

            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(motDePasse, salt);
            string token = Guid.NewGuid().ToString(); // token unique


        Utilisateur utilisateur = new Utilisateur
            {
                Nom = nom,
                Email = email,
                MotDePasse = hash,
                Sel = salt,
                TokenValidation = token,
                EstValide = false
        };

            UtilisateurDAO dao = new UtilisateurDAO();
            if (dao.CreerUtilisateur(utilisateur))
            {
                MessageBox.Show("Inscription réussie valider l'adresse mail !");
                Application.MainPage = new LoginPage();
            }
            else
            {
                MessageBox.Show("Erreur : cet utilisateur existe déjà.");
            }
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            Application.MainPage = new LoginPage();
        }
    }
