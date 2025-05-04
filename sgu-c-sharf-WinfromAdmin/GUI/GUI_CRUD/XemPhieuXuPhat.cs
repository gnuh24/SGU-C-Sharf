using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sgu_c_sharf_WinfromAdmin.Models;

namespace sgu_c_sharf_WinfromAdmin.GUI.GUI_CRUD
{
    public partial class FormXemPhieuXuphat : Form
    {
        public FormXemPhieuXuphat()
        {
            InitializeComponent();
        }
        public FormXemPhieuXuphat(PhieuXuPhatDetailDTO phieuXuPhat)
        {
            InitializeComponent();
            _phieuXuPhat = phieuXuPhat;
            LoadPhieuXuPhatDetails();
        }
        private void LoadPhieuXuPhatDetails()
        {
            // Hiển thị thông tin phiếu xử phạt lên các control
            txtMaPhieu.Text = _phieuXuPhat.Id.ToString();
            txtMoTa.Text = _phieuXuPhat.MoTa;
            txtMucPhat.Text = _phieuXuPhat.MucPhat?.ToString() ?? "0";
            txtMaThanhVien.Text = _phieuXuPhat.IdThanhVien.ToString();
            txtTenThanhVien.Text = _phieuXuPhat.TenThanhVien;
            dataTimeViPham.Value = _phieuXuPhat.NgayViPham;
            cbbTrangThai.SelectedItem = _phieuXuPhat.TrangThai.ToString();
        }
        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSuaThanhVien_Load(object sender, EventArgs e)
        {

        }

        private void btnThemDauThietBi_Click(object sender, EventArgs e)
        {
            ThemDauThietBi form = new ThemDauThietBi();
            form.ShowDialog();
        }
    }
}
