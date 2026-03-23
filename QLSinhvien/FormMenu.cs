using System;
using System.Windows.Forms;

namespace QLSinhvien
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        // 🔹 Chuyển sang FormSinhVien
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            FormSinhVien f = new FormSinhVien();
            f.Show();
            this.Hide(); // ẩn menu
        }

        // 🔹 Chuyển sang FormLopHoc
        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            FormLopHoc f = new FormLopHoc();
            f.Show();
            this.Hide();
        }

        // 🔹 Đăng xuất → trở về FormLogin
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }
    }
}