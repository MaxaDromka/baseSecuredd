using System;
using System.Windows.Forms;

namespace dataBase
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            string expectedUsername = "postgres";
            string expectedPassword = "123456";

            string restrictedUsername = "user";
            string restrictedPassword = "123";

            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username == expectedUsername && password == expectedPassword)
            {
                // Если данные верны, переходим на Form1
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else if (username == restrictedUsername && password == restrictedPassword)
            {
                Form1 form1 = new Form1();
                form1.buttonGetRevenue.Hide();
                form1.button1.Enabled = false;
                form1.button3.Enabled = false;
                form1.button7.Enabled = false;
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }
    }
}
