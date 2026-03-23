using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSinhvien
{
    public partial class FormLogin : Form
    {
        //  Chuỗi kết nối SQL Server
        string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSinhVien;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Có thể để rỗng
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Kiểm tra TextBox có trống không
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql = "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@u", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", txtPass.Text.Trim());

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Đăng nhập thành công!");

                        // Chuyển sang FormMenu
                        FormMenu menu = new FormMenu();
                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}
