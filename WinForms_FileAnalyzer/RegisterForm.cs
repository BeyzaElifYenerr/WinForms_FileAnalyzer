using System;
using System.Linq;
using System.Windows.Forms;
using WinForms_FileAnalyzer.Data;     // DbContext için
using WinForms_FileAnalyzer.Models;   // User modeli için

namespace WinForms_FileAnalyzer
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirm))
            {
                lblStatus.Text = "Please fill in all fields.";
                return;
            }

            if (password.Length < 4)
            {
                lblStatus.Text = "Password must be at least 4 characters.";
                return;
            }

            if (password != confirm)
            {
                lblStatus.Text = "Passwords do not match.";
                return;
            }

            using (var db = new AppDbContext())
            {
                bool exists = db.Users.Any(u => u.Username == username);
                if (exists)
                {
                    lblStatus.Text = "Username already exists.";
                    return;
                }

                var newUser = new User
                {
                    Username = username,
                    Password = password
                };

                db.Users.Add(newUser);
                db.SaveChanges();
            }

            this.Tag = username;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShow.Checked;
            txtPassword.UseSystemPasswordChar = !show;
            txtConfirm.UseSystemPasswordChar = !show;
        }
    }
}
