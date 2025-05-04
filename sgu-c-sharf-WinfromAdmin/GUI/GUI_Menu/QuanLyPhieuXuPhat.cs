using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using sgu_c_sharf_WinfromAdmin.GUI.GUI_CRUD;
using sgu_c_sharf_WinfromAdmin.Models;
using sgu_c_sharf_WinfromAdmin.Services;

namespace sgu_c_sharf_WinfromAdmin.GUI.GUI_Menu
{
    public partial class QuanLyPhieuXuPhat : Form
    {
        private readonly PhieuXuPhatService _phieuXuPhatService;
        private const string PlaceholderText = "Tìm kiếm";
        private readonly Color PlaceholderColor = Color.Gray;

        public QuanLyPhieuXuPhat()
        {
            InitializeComponent();
            _phieuXuPhatService = new PhieuXuPhatService();
            ConfigurePlaceholder();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var data = await _phieuXuPhatService.GetAllNoPagingAsync();
                if (data != null)
                {
                    DataGrid.DataSource = data;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurePlaceholder()
        {
            txtSearch.Text = PlaceholderText;
            txtSearch.ForeColor = PlaceholderColor;

            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == PlaceholderText)
                {
                    txtSearch.Text = string.Empty;
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = PlaceholderText;
                    txtSearch.ForeColor = PlaceholderColor;
                }
            };

            // Thêm sự kiện tìm kiếm khi người dùng nhấn Enter
            txtSearch.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Ngăn tiếng "ding" khi nhấn Enter
                    await SearchAsync();
                }
            };
        }

        private async Task SearchAsync()
        {
            if (txtSearch.Text == PlaceholderText || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadDataAsync();
                return;
            }

            try
            {
                var searchTerm = txtSearch.Text.Trim();
                var data = await _phieuXuPhatService.GetAllNoPagingAsync();
                var filteredData = data?.Where(p =>
                    p.TenThanhVien?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    p.MoTa?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                    p.Id.ToString().Contains(searchTerm)).ToList();

                DataGrid.DataSource = filteredData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXem_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xem!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedId = (uint)DataGrid.SelectedRows[0].Cells["Id"].Value;
            var detail = await _phieuXuPhatService.GetByIdAsync(selectedId);

            if (detail == null)
            {
                MessageBox.Show("Không tìm thấy phiếu xử phạt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new FormXemPhieuXuphat(detail))
            {
                form.ShowDialog();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedId = (uint)DataGrid.SelectedRows[0].Cells["Id"].Value;
            var detail = await _phieuXuPhatService.GetByIdAsync(selectedId);

            if (detail == null)
            {
                MessageBox.Show("Không tìm thấy phiếu xử phạt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new FormSuaPhieuXuphat(detail))
            {
                form.UpdatedPhieuXuPhat += async (s, updatedPhieuXuPhat) =>
                {
                    var updateDTO = new PhieuXuPhatUpdateDTO
                    {
                        TrangThai = updatedPhieuXuPhat.TrangThai,
                        NgayViPham = updatedPhieuXuPhat.NgayViPham,
                        MoTa = updatedPhieuXuPhat.MoTa,
                        ThoiHanXuPhat = updatedPhieuXuPhat.ThoiHanXuPhat,
                        MucPhat = updatedPhieuXuPhat.MucPhat,
                        IdThanhVien = updatedPhieuXuPhat.IdThanhVien,
                        NgayXuPhat = updatedPhieuXuPhat.NgayXuPhat
                    };

                    var result = await _phieuXuPhatService.UpdateAsync(selectedId, updateDTO);
                    if (result)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                form.ShowDialog();
            }
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            using (var form = new FormThemPhieuXuphat())
            {
                form.NewPhieuXuPhat += async (s, newPhieuXuPhat) =>
                {
                    var createDTO = new PhieuXuPhatCreateDTO
                    {
                        TrangThai = newPhieuXuPhat.TrangThai,
                        NgayViPham = newPhieuXuPhat.NgayViPham,
                        MoTa = newPhieuXuPhat.MoTa,
                        ThoiHanXuPhat = newPhieuXuPhat.ThoiHanXuPhat,
                        MucPhat = newPhieuXuPhat.MucPhat,
                        IdThanhVien = newPhieuXuPhat.IdThanhVien
                    };

                    var result = await _phieuXuPhatService.AddAsync(createDTO);
                    if (result)
                    {
                        MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                form.ShowDialog();
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedId = (uint)DataGrid.SelectedRows[0].Cells["Id"].Value;
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu xử phạt này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            var result = await _phieuXuPhatService.DeleteAsync(selectedId);
            if (result)
            {
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataAsync();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = PlaceholderText;
            txtSearch.ForeColor = PlaceholderColor;
            LoadDataAsync();
        }
    }
}