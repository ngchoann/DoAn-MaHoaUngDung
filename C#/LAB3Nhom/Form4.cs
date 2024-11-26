using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace LAB3Nhom
{
    
    public partial class Form4 : Form
    {
        string Username;
        public Form4(string username)
        {
            InitializeComponent();
            Username = username;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void Cleartext()
        {
            txtmasv.Text = "";
            txthoten.Text = "";
            txtngaysinh.Text = "";
            txtdiachi.Text = "";
            txtmalop.Text = "";
            txttendn.Text = "";
            txtmk.Text = "";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_INS_SINHVIEN";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MASV", SqlDbType.NVarChar).Value = txtmasv.Text;
                    command.Parameters.Add("@HOTEN", SqlDbType.NVarChar).Value = txthoten.Text;
                    command.Parameters.Add("@NGAYSINH", SqlDbType.Date).Value = txtngaysinh.Text;
                    command.Parameters.Add("@DIACHI", SqlDbType.NVarChar).Value = txtdiachi.Text;
                    command.Parameters.Add("@MALOP", SqlDbType.VarChar).Value = txtmalop.Text;
                    command.Parameters.Add("@TENDN", SqlDbType.NVarChar).Value = txttendn.Text;
                    command.Parameters.Add("@MATKHAU", SqlDbType.VarChar).Value = txtmk.Text;
                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Thêm thành công");
                        LoadData();
                        Cleartext();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_SEL_SINHVIEN'" + Username + "'", connection);
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtmasv.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txthoten.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtngaysinh.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtdiachi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtmalop.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SuaThongTinSinhVien";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MASV", SqlDbType.NVarChar).Value = txtmasv.Text;
                    command.Parameters.Add("@HOTEN", SqlDbType.NVarChar).Value = txthoten.Text;
                    command.Parameters.Add("@NGAYSINH", SqlDbType.Date).Value = txtngaysinh.Text;
                    command.Parameters.Add("@DIACHI", SqlDbType.NVarChar).Value = txtdiachi.Text;
                    command.Parameters.Add("@TENDNGV", SqlDbType.NVarChar).Value = Username;
                    
                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Sửa thành công");
                        LoadData();
                        Cleartext();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_DEL_SINHVIEN";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MASV", SqlDbType.NVarChar).Value = txtmasv.Text;
                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Xoa thành công");
                        LoadData();
                        Cleartext();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
