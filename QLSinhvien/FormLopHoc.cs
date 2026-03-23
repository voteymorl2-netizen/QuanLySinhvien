using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSinhvien
{
    public partial class FormLopHoc : Form
    {
        string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSinhVien;Integrated Security=True";

        string selectedID = ""; // lưu MaLop khi chọn

        public FormLopHoc()
        {
            InitializeComponent();
        }

        private void FormLopHoc_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Load dữ liệu lớp học
        void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM LopHoc", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // Click DataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMaClass.Text = dataGridView1.Rows[i].Cells["MaLop"].Value.ToString();
                txtTenLop.Text = dataGridView1.Rows[i].Cells["TenLop"].Value.ToString();
                selectedID = label3.Text;
            }
        }

        // Thêm lớp
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaClass.Text.Trim() == "" || txtTenLop.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop,@TenLop)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLop", txtMaClass.Text.Trim());
                cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text.Trim());

                try { cmd.ExecuteNonQuery(); }
                catch (Exception ex) { MessageBox.Show("Lỗi thêm lớp: " + ex.Message); }

                conn.Close();
            }

            LoadData();
        }

        // Sửa lớp
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedID == "") return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE LopHoc SET TenLop=@TenLop WHERE MaLop=@MaLop";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLop", selectedID);
                cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text.Trim());
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadData();
        }

        // Xóa lớp
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedID == "") return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "DELETE FROM LopHoc WHERE MaLop=@MaLop";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaLop", selectedID);

                try { cmd.ExecuteNonQuery(); }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa lớp: " + ex.Message); }

                conn.Close();
            }

            LoadData();
        }

        // Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaClass.Clear();
            txtTenLop.Clear();
            selectedID = "";
            LoadData();
        }

        // Tìm kiếm
        private void btnTim_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM LopHoc WHERE 1=1";

                if (!string.IsNullOrEmpty(txtMaClass.Text))
                    sql += " AND MaLop LIKE '%" + label3.Text.Trim() + "%'";

                if (!string.IsNullOrEmpty(txtTenLop.Text))
                    sql += " AND TenLop LIKE N'%" + txtTenLop.Text.Trim() + "%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // Chuyển sang FormSinhVien
        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            FormSinhVien f = new FormSinhVien();
            f.Show();
            this.Hide();
        }

        // Nút đang ở FormLopHoc
        private void btnLopHoc_Click(object sender, EventArgs e)
        {
            // không làm gì
        }

        // Đăng xuất
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            FormLogin f = new FormLogin();
            f.Show();
            this.Hide();
        }

        
       

        // TextChanged Tên lớp
        private void txtTenLop_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "Bạn vừa nhập Tên lớp: " + txtTenLop.Text;
        }

        private void txtMaClass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenLop_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}