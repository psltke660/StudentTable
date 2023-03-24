using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StudentTable
{
    public partial class Form1 : Form
    {
        private static SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-PL29PE4;Initial Catalog=StudentDatabase;Integrated Security=True");
        private static SqlDataAdapter adapter;
        private static DataSet dataSet;

        public Form1()
        {
            InitializeComponent();
            // 创建数据适配器和数据集
            adapter = new SqlDataAdapter("SELECT * FROM Students", connection);
            dataSet = new DataSet();

            // 填充数据集
            adapter.Fill(dataSet);

            // 将数据集绑定到DataGridView控件
            dataGridView1.DataSource = dataSet.Tables[0];


            // 设置 DataGridView 控件的位置和大小
            //dataGridView1.Location = new Point(10, 10);
            //dataGridView1.Size = new Size(500, 300);

            // 将 DataGridView 控件添加到窗体中
            Controls.Add(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            lstStudentIds.Items.Clear();

            // 打开连接
            connection.Open();

            string query = "SELECT * FROM Students WHERE Name = @name";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", txtName.Text);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                lstStudentIds.Items.Add(reader["StudentID"].ToString());
            }

            // 关闭连接
            connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {     
        }

        private void lstStudentIds_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Addbutton_Click(object sender, EventArgs e)
        {


            try {

                string name = nameBox.Text;
                string Class_name = Class_nameBox.Text;
                string gender = genderBox.Text;
                int age = int.Parse(ageBox.Text);
                string college = collegeBox.Text;
                string sql = "INSERT INTO Students(Name, Class, Gender, Age, College) VALUES (@name, @class, @gender, @age, @college)";


                // 打开连接
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    // 添加参数
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@College", college);
                    command.Parameters.AddWithValue("@Class", Class_name);

                    // 执行 SQL 语句并返回受影响的行数
                    int affectedRows = command.ExecuteNonQuery();

                    // 如果受影响的行数大于 0，则添加数据成功
                    if (affectedRows > 0)
                    {
                        MessageBox.Show("添加数据成功");
                    }
                    else
                    {
                        MessageBox.Show("添加数据失败");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("添加数据失败");
            }

         

            //刷新DataGridView 控件数据
            {
                // 创建数据适配器和数据集
                adapter = new SqlDataAdapter("SELECT * FROM Students", connection);
                dataSet = new DataSet();

                // 填充数据集
                adapter.Fill(dataSet);

                // 将数据集绑定到DataGridView控件
                dataGridView1.DataSource = dataSet.Tables[0];

                // 将 DataGridView 控件添加到窗体中
                Controls.Add(dataGridView1);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
