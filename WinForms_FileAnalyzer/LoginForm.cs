using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinForms_FileAnalyzer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

            var user = txtUserName.Text.Trim();
            var pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                lblStatus.Text = "Fill in all fields.";
                return;
            }

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.txt");
            if (!File.Exists(path))
            {
                lblStatus.Text = "No users found. Please create an account.";
                return;
            }

            bool ok = File.ReadAllLines(path)
                          .Any(l => l.Trim().Equals($"{user}:{pass}", StringComparison.Ordinal));
            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblStatus.Text = "Invalid username or password.";
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

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
