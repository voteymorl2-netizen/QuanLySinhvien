using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSinhvien
{
    public partial class FormSinhVien : Form
    {
        // 🔌 Chuỗi kết nối SQL Server
        string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSinhVien;Integrated Security=True";

        string selectedID = ""; // Lưu ID khi chọn từ DataGridView

        public FormSinhVien()
        {
            InitializeComponent();
        }

        private void FormSinhVien_Load(object sender, EventArgs e)
        {
            // Load dữ liệu lên DataGridView
            LoadData();

            // Cài đặt ComboBox giới tính
            cbGioiTinh.Items.Clear();
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
        }

        // 🔹 Load dữ liệu SinhVien
        void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SinhVien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // 🔹 Click vào DataGridView → đổ dữ liệu lên form
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMaSV.Text = dataGridView1.Rows[i].Cells["MaSV"].Value.ToString();
                txtTenSV.Text = dataGridView1.Rows[i].Cells["TenSV"].Value.ToString();
                cbGioiTinh.Text = dataGridView1.Rows[i].Cells["GioiTinh"].Value.ToString();
                dtNgaySinh.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["NgaySinh"].Value);
                txtLop.Text = dataGridView1.Rows[i].Cells["MaLop"].Value.ToString();

                selectedID = txtMaSV.Text;
            }
        }

        // 🔹 Nút Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "INSERT INTO SinhVien VALUES (@MaSV,@TenSV,@GioiTinh,@NgaySinh,@MaLop)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text.Trim());
                cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text.Trim());
                cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dtNgaySinh.Value);
                cmd.Parameters.AddWithValue("@MaLop", txtLop.Text.Trim());

                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadData();
        }

        // 🔹 Nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedID == "") return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "UPDATE SinhVien SET TenSV=@TenSV, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, MaLop=@MaLop WHERE MaSV=@MaSV";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaSV", selectedID);
                cmd.Parameters.AddWithValue("@TenSV", txtTenSV.Text.Trim());
                cmd.Parameters.AddWithValue("@GioiTinh", cbGioiTinh.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dtNgaySinh.Value);
                cmd.Parameters.AddWithValue("@MaLop", txtLop.Text.Trim());

                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadData();
        }

        // 🔹 Nút Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedID == "") return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "DELETE FROM SinhVien WHERE MaSV=@MaSV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", selectedID);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadData();
        }

        // 🔹 Nút Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            txtLop.Clear();
            cbGioiTinh.SelectedIndex = -1;
            dtNgaySinh.Value = DateTime.Now;

            selectedID = "";
            LoadData();
        }

        // 🔹 Nút Tìm kiếm
        private void btnTim_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM SinhVien WHERE 1=1";

                if (!string.IsNullOrEmpty(txtMaSV.Text))
                    sql += " AND MaSV LIKE '%" + txtMaSV.Text.Trim() + "%'";

                if (!string.IsNullOrEmpty(txtTenSV.Text))
                    sql += " AND TenSV LIKE N'%" + txtTenSV.Text.Trim() + "%'";

                if (!string.IsNullOrEmpty(cbGioiTinh.Text))
                    sql += " AND GioiTinh=N'" + cbGioiTinh.Text + "'";

                if (!string.IsNullOrEmpty(txtLop.Text))
                    sql += " AND MaLop LIKE '%" + txtLop.Text.Trim() + "%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        // 🔹 Nút chuyển sang FormSinhVien (đang ở đây → có thể bỏ)
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            // Không cần làm gì nếu đang ở FormSinhVien
        }

        // 🔹 Nút chuyển sang FormLopHoc
        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            FormLopHoc f = new FormLopHoc();
            f.Show();
            this.Hide();
        }

        // 🔹 Nút Đăng xuất
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }

        private void cbGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}