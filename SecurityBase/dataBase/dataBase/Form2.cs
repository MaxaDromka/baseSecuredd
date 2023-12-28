using Npgsql;
using System;
using System.IO;
using System.Windows.Forms;

namespace dataBase
{
    public partial class Form2 : Form
    {
        private NpgsqlConnection connection;
        public string SelectedId { get; set; }
        public Form2(NpgsqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Преобразование изображения в массив байтов
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Загрузка изображения в базу данных
                string sql = "INSERT INTO employees (full_name, position, hire_date, salary, education, foto) VALUES (:full_name, :position, :hire_date::date, :salary, :education, :foto)";

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue(":full_name", textBox1.Text);
                command.Parameters.AddWithValue(":position", comboBox1.Text);
                command.Parameters.AddWithValue(":hire_date", textBox3.Text);
                command.Parameters.AddWithValue(":salary", Convert.ToDecimal(textBox2.Text));
                command.Parameters.AddWithValue(":education", comboBox2.Text);
                command.Parameters.AddWithValue(":foto", imageBytes);

                command.ExecuteNonQuery();

                //this.Close(); // Закрытие формы Form2

                Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
                form1.Fill(); // Обновление данных в GridView на форме Form1
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) // Проверка на пустое поле "full_name"
            {
                MessageBox.Show("Пожалуйста, введите полное имя."); // Вывод сообщения об ошибке
                return; // Прерывание выполнения метода
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text)) // Проверка на пустое поле "HireDate"
            {
                MessageBox.Show("Пожалуйста, введите корректную дату "); // Вывод сообщения об ошибке
                return; // Прерывание выполнения метода
            }

            if (!DateTime.TryParse(textBox3.Text, out DateTime hireDate)) // Проверка на правильный формат даты
            {
                MessageBox.Show("Пожалуйста, введите действительную дату  в формате ММ/ДД/ГГГГ.\""); // Вывод сообщения об ошибке
                return; // Прерывание выполнения метода
            }

            if (!decimal.TryParse(textBox2.Text, out decimal salary)) // Проверка на числовое значение в поле "salary"
            {
                MessageBox.Show("Пожалуйста, введите  зарплату."); // Вывод сообщения об ошибке
                return; // Прерывание выполнения метода
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text)) // Проверка на пустое поле "position"
            {
                MessageBox.Show("Пожалуйста,заполните поле должность"); // Вывод сообщения об ошибке
                return; // Прерывание выполнения метода
            }

            if (string.IsNullOrWhiteSpace(comboBox2.Text)) // Проверка на пустое поле "education"
            {
                MessageBox.Show("Пожалуйста,заполните поле образование"); // Вывод сообщения об ошибке
                return; // Прерывание выполнения метода
            }
            else
            {
                MessageBox.Show("Данные введены корректно");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO secured_objects (address, account_number, representative_full_name, service_type,employee_id,service_type_id ) VALUES (:address, :account_number, :representative_full_name,  :service_type, :employee_id, :service_type_id);";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue(":address", textBox4.Text);
            command.Parameters.AddWithValue(":account_number", textBox5.Text);
            command.Parameters.AddWithValue(":representative_full_name", textBox6.Text);
            command.Parameters.AddWithValue(":service_type", (textBox8.Text));
            command.Parameters.AddWithValue(":employee_id", Convert.ToInt32(textBox9.Text));
            command.Parameters.AddWithValue(":service_type_id", Convert.ToInt32(textBox10.Text));
            command.ExecuteNonQuery();

            this.Close(); // Закрытие формы Form2

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }

        private void button5_Click(object sender, EventArgs e)
        {
            decimal cost;
            if(decimal.TryParse(textBox12.Text, out cost))
            {
                string sql = "INSERT INTO service_type (name, cost ) VALUES (:name,:cost);";
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue(":name", textBox11.Text);
                command.Parameters.AddWithValue(":cost", cost);

                command.ExecuteNonQuery();
                //this.Close(); // Закрытие формы Form2

                Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
                form1.Fill(); // Обновление данных в GridView на форме Form1
            }
            else
            {
                MessageBox.Show("Данные введены корректно");
            }

            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO contracts (contract_number, date_of_conclusion, completion_date, contract_amount,object_id,customer_id) VALUES (:contract_number, :date_of_conclusion::date,:completion_date::date, :contract_amount,:object_id,:customer_id);";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue(":contract_number", textBox17.Text);
            command.Parameters.AddWithValue(":date_of_conclusion", textBox18.Text);
            command.Parameters.AddWithValue(":completion_date", textBox19.Text);
            command.Parameters.AddWithValue(":contract_amount",  Convert.ToInt32(textBox20.Text));
            command.Parameters.AddWithValue(":object_id", Convert.ToInt32(textBox21.Text));
            command.Parameters.AddWithValue(":customer_id", Convert.ToInt32(textBox22.Text));
            command.ExecuteNonQuery();

           // this.Close(); // Закрытие формы Form2

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO customers (customer_full_name, address, account_number, phone) VALUES (:customer_full_name, :address,:account_number, :phone);";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue(":customer_full_name", textBox13.Text);
            command.Parameters.AddWithValue(":address", textBox14.Text);
            command.Parameters.AddWithValue(":account_number", textBox15.Text);
            command.Parameters.AddWithValue(":phone",textBox16.Text);
            command.ExecuteNonQuery();

           // this.Close(); // Закрытие формы Form2

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO revenue (date, amount, contract_id) VALUES (:date, :amount, :contract_id);";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add(new NpgsqlParameter(":date", NpgsqlTypes.NpgsqlDbType.Date)).Value = DateTime.Parse(textBox23.Text);
            command.Parameters.AddWithValue(":amount", Convert.ToInt32(textBox24.Text));
            command.Parameters.AddWithValue(":contract_id", Convert.ToInt32(textBox25.Text));
            command.ExecuteNonQuery();

            this.Close(); // Закрытие формы Form2

            Form1 form1 = (Form1)Application.OpenForms["Form1"]; // Получение ссылки на открытую форму Form1
            form1.Fill(); // Обновление данных в GridView на форме Form1
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) ||
       string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox8.Text) ||
       string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                
            }

            int employeeId;
            if (!int.TryParse(textBox9.Text, out employeeId))
            {
                MessageBox.Show("Идентификационный номер сотрудника должен быть номерным");
                
            }

            int serviceTypeId;
            if (!int.TryParse(textBox10.Text, out serviceTypeId))
            {
                MessageBox.Show("Идентификатор типа услуги должен быть номерным");
               
            }
            else
            {
                MessageBox.Show("Данные введены корректно");
            }         
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Проверка корректности введенных данных
            if (string.IsNullOrWhiteSpace(textBox17.Text) || string.IsNullOrWhiteSpace(textBox18.Text) || string.IsNullOrWhiteSpace(textBox19.Text) || string.IsNullOrWhiteSpace(textBox20.Text) || string.IsNullOrWhiteSpace(textBox21.Text) || string.IsNullOrWhiteSpace(textBox22.Text))
            {
                // Если хотя бы одно из полей не заполнено, выводим сообщение об ошибке
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!DateTime.TryParse(textBox18.Text, out _))
            {
                MessageBox.Show("Некорректный формат даты заключения контракта. Пожалуйста, введите дату в формате ГГГГ-ММ-ДД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!DateTime.TryParse(textBox19.Text, out _))
            {
                MessageBox.Show("Некорректный формат даты окончания контракта. Пожалуйста, введите дату в формате ГГГГ-ММ-ДД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox20.Text, out _) || !int.TryParse(textBox21.Text, out _) || !int.TryParse(textBox22.Text, out _))
            {
                MessageBox.Show("Поле 'Сумма контракта', 'ID объекта' и 'ID заказчика' должны быть числовыми значениями.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Данные введены корректно");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox12.Text, out _))
            {
                MessageBox.Show("Некорректное значение стоимости. Пожалуйста, введите число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Данные введены корректно");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Проверка корректности введенных данных
            if (!DateTime.TryParse(textBox23.Text, out _))
            {
                // Введите дату в формате ГГГГ-ММ-ДД (например, 2023-12-31)
                MessageBox.Show("Некорректный формат даты. Пожалуйста, введите дату в формате ГГГГ-ММ-ДД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox24.Text, out _))
            {
                MessageBox.Show("Поле 'Сумма' должно быть числовым значением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox25.Text, out _))
            {
                MessageBox.Show("Поле 'ID контракта' должно быть числовым значением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Данные корректны");
                return;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox13.Text) || string.IsNullOrWhiteSpace(textBox14.Text) || string.IsNullOrWhiteSpace(textBox15.Text) || string.IsNullOrWhiteSpace(textBox16.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректными данными.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox16.Text, out _))
            {
                MessageBox.Show("Поле 'Номер счета' должно быть числовым значением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBox16.Text, out _))
            {
                MessageBox.Show("Поле 'Телефон' должно быть числовым значением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
    
}
