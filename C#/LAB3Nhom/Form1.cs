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
    public partial class Form1 : Form
    {
        string nghenghiep = null;
        public Form1()
        {
            InitializeComponent();
        }
        public static string Username;
        public static string Password;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-J3BGHBTJ;Initial Catalog=lab3test;Integrated Security=True;Application Name=LAB3Nhom");
            string username = txttk.Text;
            string password = txtmk.Text;
            try
            {
                con.Open();
               
                using (SqlCommand command = new SqlCommand("SP_LOG_IN", con))
                    {
                        
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TENDN",username );
                        command.Parameters.AddWithValue("@MATKHAU",password);
                        

                        int result = (int)command.ExecuteScalar();
                        if (result == 1)
                        {
                            MessageBox.Show("Đăng Nhập Thành Công");
                            if (nghenghiep == "QLNV")
                            {
                                Username = txttk.Text;
                                Password = txtmk.Text;
                                Form2 f2 = new Form2(Username, Password);
                                f2.Show();
                            }
                            else if (nghenghiep == "QLLH")
                            {
                                Form3 f3 = new Form3();
                                f3.Show();
                            }
                            else if(nghenghiep == "QLSV")
                            {
                                Username = txttk.Text;
                                Form4 f4 = new Form4(Username);
                                f4.Show();
                            }
                            else if (nghenghiep == "QLD")
                            {
                                Password = txtmk.Text;
                                Username = txttk.Text;
                                Form5 f5 = new Form5(Username, Password);
                                f5.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Đăng Nhập Thất Bại");
                        }
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Kết Nối: ");
            }
     
        }
        static string CalculateSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            if (selectedValue == "Quản Lý Lớp Học")
                nghenghiep = "QLLH";
            else if (selectedValue == "Quản Lý Nhân Viên")
                nghenghiep = "QLNV";
            else if(selectedValue == "Quản Lý Sinh Viên")
                nghenghiep = "QLSV";
            else if (selectedValue == "Quản Lý Điểm")
                nghenghiep = "QLD";

        }

        private void txttk_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
