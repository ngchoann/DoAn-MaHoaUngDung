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
    public partial class Form5 : Form
    {
        string Username;
        string Password;
        string hp = "";
        public Form5(string username, string password)
        {
            InitializeComponent();
            Username = username;
            Password = password;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Cleartext()
        {
            txtmasv.Text = "";
            txtmahp.Text = "";
            txtdiem.Text = "";



        }
        private void LoadData()
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_SEL_BANGDIEM'" + Username + "','" + Password + "'", connection);
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
            txtmahp.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtdiem.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_INS_BANGDIEM";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TK", SqlDbType.VarChar).Value = Username;
                    command.Parameters.Add("@MASV", SqlDbType.NVarChar).Value = txtmasv.Text;
                    command.Parameters.Add("@MAHP", SqlDbType.VarChar).Value = txtmahp.Text;
                    command.Parameters.Add("@DIEMTHI", SqlDbType.Float).Value = txtdiem.Text;



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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_UPD_BANGDIEM";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TK", SqlDbType.VarChar).Value = Username;
                    command.Parameters.Add("@MASV", SqlDbType.NVarChar).Value = txtmasv.Text;
                    command.Parameters.Add("@MAHP", SqlDbType.VarChar).Value = txtmahp.Text;
                    command.Parameters.Add("@DIEMTHI", SqlDbType.Float).Value = txtdiem.Text;
                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Thêm thành công");
                        LoadData();
                        Cleartext();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void txtmasv_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
