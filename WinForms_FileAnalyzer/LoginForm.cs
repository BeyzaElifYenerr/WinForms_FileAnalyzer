using System;
using System.Linq;
using System.Windows.Forms;
using WinForms_FileAnalyzer.Data;     // DbContext için
using WinForms_FileAnalyzer.Models;   // User modeli için

namespace WinForms_FileAnalyzer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShow.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            var username = txtUserName.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblStatus.Text = "Fill in all fields.";
                return;
            }

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblStatus.Text = "Invalid username or password.";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            // boş
        }

        private void lnkCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var reg = new RegisterForm())
            {
                if (reg.ShowDialog() == DialogResult.OK)
                {
                    var newUser = reg.Tag as string;
                    if (!string.IsNullOrEmpty(newUser))
                    {
                        txtUserName.Text = newUser;
                        txtPassword.Focus();
                    }
                }
            }
        }
    }
}
