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
using System.Xml;
namespace LAB3Nhom
{
    
    public partial class Form2 : Form
    {
        string Username;
        string Password;
        string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";
        SqlConnection sqlcon = null;
        public Form2(string username,string password)
        {
            InitializeComponent();
            Username = username;
            Password = password;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtnv.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txthoten.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtemail.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtluong.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtdn.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            txtmk.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
           

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void Cleartext()
        {
            txtnv.Text = "";
            txthoten.Text = "";
            txtemail.Text = "";
            txtluong.Text = "";
            txtdn.Text = "";
            txtmk.Text = "";
            
        }
        private void LoadData()
        {

            if (sqlcon==null)
            {
                sqlcon = new SqlConnection(connectionString);
            }
            string sql = "select * from NHANVIEN";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql, connectionString);
            DataSet ds = new DataSet();
            adapter1.Fill(ds);
            nhanvien.DataSource = ds.Tables[0];
            nhanvien.ValueMember = "TENDN";
            nhanvien.DisplayMember = "MANV";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_UPD_NHANVIEN";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MANV", SqlDbType.VarChar).Value = txtnv.Text;
                    command.Parameters.Add("@HOTEN", SqlDbType.NVarChar).Value = txthoten.Text;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";
            


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_INS_PUBLIC_NHANVIEN";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MANV", SqlDbType.VarChar).Value = txtnv.Text;
                    command.Parameters.Add("@HOTEN", SqlDbType.NVarChar).Value = txthoten.Text;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemail.Text;
                  
                    command.Parameters.Add("@LUONG", SqlDbType.Int).Value =txtluong.Text;
                    command.Parameters.Add("@TENDN", SqlDbType.NVarChar).Value = txtdn.Text;
                    command.Parameters.Add("@MATKHAU", SqlDbType.NVarChar).Value = txtmk.Text;



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
        
        
        

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_DEL_NHANVIEN";

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MANV", SqlDbType.VarChar).Value = txtnv.Text;
                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Xóa thành công");
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
        

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void txtnv_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void nhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nv = nhanvien.SelectedValue.ToString();
            if(Username==nv)
            {
                string sql = "SP_SEL_PUBLIC_NHANVIEN '" + nv + "','" + Password + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                string sql = "select * from NHANVIEN where TENDN='"+nv+"'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            
        }
    }
}
