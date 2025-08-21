using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.txt");
            if (!File.Exists(path)) File.WriteAllText(path, ""); 

          
            var exists = File.ReadAllLines(path)
                             .Select(l => l.Split(':'))
                             .Any(p => p.Length == 2 && p[0].Equals(username, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                lblStatus.Text = "Username already exists.";
                return;
            }

            File.AppendAllLines(path, new[] { $"{username}:{password}" });

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
