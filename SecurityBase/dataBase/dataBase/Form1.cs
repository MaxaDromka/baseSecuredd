using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace dataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonGetRevenue.Click += buttonGetRevenue_Click;
            button1.Click += button1_Click;
        }

        private NpgsqlConnection connection;
      
        private void button2_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2(connection); // Создание экземпляра формы Form2
            form2.Show(); // Отображение формы Form2

            Form3 form3 = new Form3(connection);//
            form3.SelectedId = dataGridView1.CurrentRow.Cells["id"].Value.ToString();//
        }

        public void Fill()
        {
            string sql = "SELECT id,full_name,position,hire_date,salary,education FROM  employees;";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0];

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            string sql1 = "SELECT * FROM secured_objects;";

            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(sql1, connection);
            DataSet dataset1 = new DataSet();
            adapter1.Fill(dataset1);
            dataGridView2.DataSource = dataset1.Tables[0];

            string sql2 = "SELECT * FROM contracts;";

            NpgsqlDataAdapter adapter2 = new NpgsqlDataAdapter(sql2, connection);
            DataSet dataset2 = new DataSet();
            adapter2.Fill(dataset2);
            dataGridView3.DataSource = dataset2.Tables[0];

            string sql3 = "SELECT * FROM revenue;";

            NpgsqlDataAdapter adapter3 = new NpgsqlDataAdapter(sql3, connection);
            DataSet dataset3 = new DataSet();
            adapter3.Fill(dataset3);
            dataGridView4.DataSource = dataset3.Tables[0];


            string sql4 = "SELECT * FROM service_type;";

            NpgsqlDataAdapter adapter4 = new NpgsqlDataAdapter(sql4, connection);
            DataSet dataset4 = new DataSet();
            adapter4.Fill(dataset4);
            dataGridView5.DataSource = dataset4.Tables[0];

            string sql5 = "SELECT * FROM customers;";

            NpgsqlDataAdapter adapter5 = new NpgsqlDataAdapter(sql5, connection);
            DataSet dataset5 = new DataSet();
            adapter5.Fill(dataset5);
            dataGridView6.DataSource = dataset5.Tables[0];
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Проверяем, что выбрана хотя бы одна строка в dataGridView1
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем выбранный employee_id
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                // Получаем путь к файлу с фотографией сотрудника из базы данных по employee_id
                string tempImagePath = SaveImageFromDatabase(id);

                if (!string.IsNullOrEmpty(tempImagePath))
                {
                    pictureBox1.Image = null;
                    try
                    {
                        // Создаем изображение из массива байтов
                        using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(tempImagePath)))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message);
                    }
                }
            }
        }

       
        private string SaveImageFromDatabase(int employeeId)
        {
            string sql = "SELECT foto FROM employees WHERE id = @id";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", employeeId);

            try
            {
                byte[] imageData = (byte[])command.ExecuteScalar();
                if (imageData != null)
                {
                    string tempImagePath = Path.GetTempFileName();
                    File.WriteAllBytes(tempImagePath, imageData);
                    return tempImagePath;
                }
                else
                {
                    // Если поле с фото пустое, возвращаем null
                    return null;
                }
            }
            catch (InvalidCastException)
            {
                // Если возникает исключение InvalidCastException, возвращаем null
                return null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowId = (int)dataGridView1.SelectedRows[0].Cells["id"].Value; // получаем id выбранной записи
                string tableName = "employees"; // замените на имя выбранной таблицы

                string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres"; // замените на вашу строку подключения
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowId);
                        command.ExecuteNonQuery();
                    }
                } 
           
                Fill(); // замените на метод,который загружает данные в gridView
            }
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedRowId = (int)dataGridView2.SelectedRows[0].Cells["id"].Value; // получаем id выбранной записи
                string tableName = "secured_objects"; // замените на имя выбранной таблицы

                string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres"; // замените на вашу строку подключения
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowId);
                        command.ExecuteNonQuery();
                    }
                }
                // обновляем данные в gridVie
                Fill(); // замените на метод, который загружает данные в gridView для таблицы Secured Objects
            }
            
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedRowId = (int)dataGridView3.SelectedRows[0].Cells["id"].Value; // получаем id выбранной записи
                string tableName = "contracts"; // замените на имя выбранной таблицы

                string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres"; // замените на вашу строку подключения
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowId);
                        command.ExecuteNonQuery();
                    }
                }
                // обновляем данные в gridView
                Fill(); // замените на метод, который загружает данные в gridView для таблицы Secured Objects
            }

            if (dataGridView4.SelectedRows.Count > 0)
            {
                int selectedRowId = (int)dataGridView4.SelectedRows[0].Cells["id"].Value; // получаем id выбранной записи
                string tableName = "revenue"; // замените на имя выбранной таблицы

                string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres"; // замените на вашу строку подключения
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowId);
                        command.ExecuteNonQuery();
                    }
                }
                // обновляем данные в gridView
                Fill(); // замените на метод, который загружает данные в gridView для таблицы Secured Objects
            }

            if (dataGridView5.SelectedRows.Count > 0)
            {
                int selectedRowId = (int)dataGridView5.SelectedRows[0].Cells["id"].Value; // получаем id выбранной записи
                string tableName = "service_type"; // замените на имя выбранной таблицы

                string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres"; // замените на вашу строку подключения
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowId);
                        command.ExecuteNonQuery();
                    }
                }
                // обновляем данные в gridView
                Fill(); // замените на метод, который загружает данные в gridView для таблицы Secured Objects
            }

            if (dataGridView6.SelectedRows.Count > 0)
            {
                int selectedRowId = (int)dataGridView6.SelectedRows[0].Cells["id"].Value; // получаем id выбранной записи
                string tableName = "customers"; // замените на имя выбранной таблицы

                string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres"; // замените на вашу строку подключения
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedRowId);
                        command.ExecuteNonQuery();
                    }
                }
                // обновляем данные в gridView
                Fill(); // замените на метод, который загружает данные в gridView для таблицы Secured Objects
            }
        }

        private void DeleteRecord(string id)
        {
            string sql = "DELETE FROM employees WHERE id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value=int.Parse(id);
            command.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(connection); // Создание экземпляра формы Form2
            form3.Show(); // Отображение формы Form2
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Open_Click(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres";
            connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Соединение с базой данных открыто.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии базы данных: " + ex.Message);
            }
        }

        public void buttonGetRevenue_Click(object sender, EventArgs e)
        {
            decimal revenue = GetRevenue();
            textBox1.Text = "Количество выручки составляет: "+ revenue.ToString();

        }

        public decimal GetRevenue()
        {
            string sql = "SELECT SUM(amount) AS revenue FROM revenue;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            decimal revenue = (decimal)command.ExecuteScalar();
            return revenue;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            long contracts = GetContracts();
            textBox2.Text = "Количество заключенных договоров составляет составляет: " + contracts.ToString();
        }

        private long GetContracts()
        {
            string sql = "SELECT COUNT(*)  FROM contracts";
            NpgsqlCommand command = new NpgsqlCommand( sql, connection);
            long contracts = (long)command.ExecuteScalar();
            return contracts;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetStatistics();
        }

        private void GetStatistics()
        {
            string sql = @"SELECT Employees.full_name AS ""Employee Name"",
            COUNT(Contracts.id) AS ""Total Contracts"" FROM Employees
            LEFT JOIN Secured_Objects ON Employees.id = secured_objects.employee_id
            LEFT JOIN Contracts ON Secured_Objects.id = contracts.object_id
            GROUP BY Employees.full_name;";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            StringBuilder output = new StringBuilder();

            while (reader.Read())
            {
                string employeeName = reader["Employee Name"].ToString();
                int totalContracts = Convert.ToInt32(reader["Total Contracts"]);

                output.AppendLine($"ФИО сотрудника: {employeeName}");
                output.AppendLine($"Количество договоров: {totalContracts}\n");
            }

            reader.Close();

            textBox3.Text = output.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();
            employees.Show();
        }
    }
}
