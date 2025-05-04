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
    public partial class FormSuaPhieuXuphat : Form
    {
        private PhieuXuPhatDetailDTO _phieuXuPhat; // Khai báo field cho đối tượng phiếu xử phạt

        public FormSuaPhieuXuphat()
        {
            InitializeComponent();
        }

        public FormSuaPhieuXuphat(PhieuXuPhatDetailDTO phieuXuPhat)
        {
            InitializeComponent();
            _phieuXuPhat = phieuXuPhat ?? throw new ArgumentNullException(nameof(phieuXuPhat));
            LoadPhieuXuPhatDetails();
        }

        // Sự kiện để thông báo khi phiếu xử phạt được cập nhật
        public event EventHandler<PhieuXuPhatDetailDTO> UpdatedPhieuXuPhat;

        private void LoadPhieuXuPhatDetails()
        {
            if (_phieuXuPhat == null) return;

            // Hiển thị thông tin phiếu xử phạt lên các control
            txtMaPhieu.Text = _phieuXuPhat.Id.ToString();
            txtMoTa.Text = _phieuXuPhat.MoTa ?? string.Empty;
            txtMucPhat.Text = _phieuXuPhat.MucPhat?.ToString() ?? "0";
            txtMaThanhVien.Text = _phieuXuPhat.IdThanhVien.ToString();
            // Thay txtTenThanhVien bằng cbbThanhVien vì không có txtTenThanhVien trong designer
            cbbThanhVien.SelectedItem = _phieuXuPhat.TenThanhVien ?? string.Empty;
            dataTimeViPham.Value = _phieuXuPhat.NgayViPham ?? DateTime.Now;
            dataTimeXuPhat.Value = _phieuXuPhat.NgayXuPhat ?? DateTime.Now; // Giả sử có NgayXuPhat
            cbbTrangThai.SelectedItem = _phieuXuPhat.TrangThai.ToString() ?? "Chưa xử lý";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_phieuXuPhat == null) return;

            // Cập nhật dữ liệu từ control
            _phieuXuPhat.MoTa = txtMoTa.Text;
            _phieuXuPhat.MucPhat = uint.TryParse(txtMucPhat.Text, out uint mucPhat) ? mucPhat : 0;
            _phieuXuPhat.NgayViPham = dataTimeViPham.Value;
            _phieuXuPhat.NgayXuPhat = dataTimeXuPhat.Value; // Giả sử có thuộc tính NgayXuPhat
            _phieuXuPhat.TrangThai = Enum.TryParse<TrangThaiPhieuXuPhatEnum>(cbbTrangThai.SelectedItem?.ToString(), out var trangThai) ? trangThai : TrangThaiPhieuXuPhatEnum.CHOXULY;
            _phieuXuPhat.TenThanhVien = cbbThanhVien.SelectedItem?.ToString() ?? string.Empty;

            // Gọi sự kiện cập nhật
            UpdatedPhieuXuPhat?.Invoke(this, _phieuXuPhat);

            MessageBox.Show("Phiếu xử phạt đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnThemDauThietBi_Click(object sender, EventArgs e)
        {
            using (ThemDauThietBi form = new ThemDauThietBi())
            {
                form.ShowDialog();
                // Có thể thêm logic để cập nhật dữ liệu sau khi thêm thiết bị
            }
        }

        // Sửa tên phương thức Load cho đúng với form
        private void FormSuaPhieuXuphat_Load(object sender, EventArgs e)
        {
            if (_phieuXuPhat != null)
            {
                LoadPhieuXuPhatDetails();
            }
        }
    }

    // Giả định enum TrangThaiPhieuXuPhatEnum (nếu chưa có trong model)
    public enum TrangThaiPhieuXuPhatEnum
    {
        CHOXULY,
        DAXULY
    }
}