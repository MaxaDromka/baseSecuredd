using System;
using Npgsql;
using System.Windows.Forms;
using System.IO;

namespace dataBase
{
    public partial class Form3 : Form
    {
        private NpgsqlConnection connection;

        public string SelectedId;
        

        public Form3(NpgsqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
        }


        private void Form3_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif";


            if(openFileDialog.ShowDialog() == DialogResult.OK )
            {
                string imagePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                string sql = "UPDATE employees SET full_name = @full_name, position = @position, hire_date = @hire_date::date, salary = @salary, education = @education,foto = @foto WHERE id = @id;";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@full_name", textBox1.Text);
                command.Parameters.AddWithValue("@position", comboBox1.Text);
                command.Parameters.AddWithValue("@hire_date", textBox3.Text);
                command.Parameters.AddWithValue("@salary", Convert.ToInt32(textBox2.Text));
                command.Parameters.AddWithValue("@education", comboBox2.Text);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox18.Text));
                command.Parameters.AddWithValue("@foto", imageBytes);

                command.ExecuteNonQuery();

                this.Close(); // Закрытие формы Form3

                Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
                form1.Fill(); // Обновление данных в GridView на форме Form1
            }          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE revenue SET date = @date::date, amount = @amount, contract_id = @contract_id WHERE id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            // Указываем значение для параметра "@date"
            command.Parameters.AddWithValue("@date", textBox4.Text);

            int amount;
            if (int.TryParse(textBox5.Text, out amount))
            {
                // Указываем значение для параметра "@amount"
                command.Parameters.AddWithValue("@amount", amount);
            }
            else
            {
                MessageBox.Show("Указано неверное значение для поля 'amount'");
                return;
            }

            int contractId;
            if (int.TryParse(textBox6.Text, out contractId))
            {
                // Указываем значение для параметра "@contract_id"
                command.Parameters.AddWithValue("@contract_id", contractId);
            }
            else
            {
                MessageBox.Show("Указано неверное значение для поля 'contract_id'");
                return;
            }

            int id;
            if (int.TryParse(textBox7.Text, out id))
            {
                // Указываем значение для параметра "@id"
                command.Parameters.AddWithValue("@id", id);
            }
            else
            {
                MessageBox.Show("Указано неверное значение для поля 'id'");
                return;
            }

            command.ExecuteNonQuery();
            this.Close(); // Закрытие формы Form3

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE secured_objects SET address = @address, account_number = @account_number, representative_full_name = @representative_full_name, phone = @phone, service_type = @service_type,employee_id = @employee_id, service_type_id = @service_type_id  WHERE id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);


            // Указываем значение для параметра "@address"
            command.Parameters.AddWithValue("@address", textBox8.Text);

            // Указываем значение для параметра "@account_number"
            command.Parameters.AddWithValue("@account_number", textBox9.Text);

            // Указываем значение для параметра "@representative_full_name"
            command.Parameters.AddWithValue("@representative_full_name", textBox10.Text);

            

            // Указываем значение для параметра "@service_type"
            command.Parameters.AddWithValue("@service_type", textBox12.Text);

            if (int.TryParse(textBox13.Text, out int employeeId))
            {
                // Указываем значение для параметра "@employee_id"
                command.Parameters.AddWithValue("@employee_id", employeeId);
            }
            else
            {
                MessageBox.Show("Указано неверное значение для поля 'employee_id'");
                return;
            }

            if (int.TryParse(textBox14.Text, out int serviceTypeId))
            {
                // Указываем значение для параметра "@service_type_id"
                command.Parameters.AddWithValue("@service_type_id", serviceTypeId);
            }
            else
            {
                MessageBox.Show("Указано неверное значение для поля 'service_type_id'");
                return;
            }

            if (int.TryParse(textBox15.Text, out int id))
            {
                // Указываем значение для параметра "@id"
                command.Parameters.AddWithValue("@id", id);
            }
            else
            {
                MessageBox.Show("Указано неверное значение для поля 'id'");
                return;
            }

            command.ExecuteNonQuery();
            this.Close(); // Закрытие формы Form3

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Assuming textBox16 is for name, textBox17 is for cost, and textBox19 is for the ID
            string sql = "UPDATE service_type SET name = @name, cost = @cost WHERE id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", textBox16.Text);
            command.Parameters.AddWithValue("@cost", Convert.ToDecimal(textBox17.Text));  // Assuming cost is represented with decimal or numeric type
            command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox19.Text));

            try
            {
                command.ExecuteNonQuery();
                this.Close(); // Закрытие формы Form3

                Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
                form1.Fill(); // Обновление данных в GridView на форме Form1
            }
            catch (Exception ex)
            {
                // В случае возникновения ошибки выводим сообщение на русском языке
                MessageBox.Show("Произошла ошибка при обновлении данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE contracts SET contract_number = contract_number, date_of_conclusion = " +
                "@date_of_conclusion ::date, completion_date = @completion_date :: date," +
                "contract_amount = @contract_amount, object_id = @object_id, " +
                "customer_id=@customer_id WHERE id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox20.Text));
            command.Parameters.AddWithValue("@contract_number", textBox21.Text);
            command.Parameters.AddWithValue("@date_of_conclusion", textBox22.Text);
            command.Parameters.AddWithValue("@completion_date", textBox23.Text);
            command.Parameters.AddWithValue("@contract_amount", Convert.ToInt32(textBox24.Text) );
            command.Parameters.AddWithValue("@object_id", Convert.ToInt32(textBox25.Text));
            command.Parameters.AddWithValue("@customer_id", Convert.ToInt32(textBox26.Text));

            command.ExecuteNonQuery();

            this.Close(); // Закрытие формы Form3

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE customers SET customer_full_name = customer_full_name, address = @address, account_number = @account_number, phone = @phone WHERE id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@customer_full_name", textBox27.Text);
            command.Parameters.AddWithValue("@address", textBox28.Text);
            command.Parameters.AddWithValue("@account_number", Convert.ToInt32(textBox29.Text));
            command.Parameters.AddWithValue("@phone", Convert.ToInt32(textBox30.Text));
            command.Parameters.AddWithValue("@id", Convert.ToInt32(textBox31.Text));

            command.ExecuteNonQuery();

            this.Close(); // Закрытие формы Form3

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }
    }
}
