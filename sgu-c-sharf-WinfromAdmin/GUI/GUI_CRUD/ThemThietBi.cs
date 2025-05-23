﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using sgu_c_sharf_WinfromAdmin.Models;
using sgu_c_sharf_WinfromAdmin.Services;
using sgu_c_sharf_WinfromAdmin.GUI.GUI_Menu;

namespace sgu_c_sharf_WinfromAdmin.GUI.GUI_CRUD
{
    public partial class FormThemThietBi : Form
    {
        private readonly ThietBiService _thietBiService;
        private readonly QuanLyThietBi _parentForm;
        private readonly LoaiThietBiService _loaiThietBiService;

        private string tenAnh = "";
        private string hinhAnhPath = "";

        public FormThemThietBi(QuanLyThietBi parentForm)
        {
            InitializeComponent();
            _thietBiService = new ThietBiService();
            _loaiThietBiService = new LoaiThietBiService();
            _parentForm = parentForm;
            LoadLoaiThietBi();
        }

        private async void LoadLoaiThietBi()
        {
            var loaiThietBiList = await _loaiThietBiService.GetAll();
            if (loaiThietBiList != null && loaiThietBiList.Count > 0)
            {
                cbbLoaiThietBi.DataSource = loaiThietBiList;
                cbbLoaiThietBi.DisplayMember = "TenLoaiThietBi";
                cbbLoaiThietBi.ValueMember = "Id";
            }
        }




        private async void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtTenThietBi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbbLoaiThietBi.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDauThietBi.Text) || !int.TryParse(txtDauThietBi.Text, out int soLuongDauThietBi) || soLuongDauThietBi < 1 || soLuongDauThietBi > 100)
            {
                MessageBox.Show("Số lượng đầu thiết bị phải là số nguyên từ 1 đến 100!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Copy ảnh vào thư mục đích (chỉ khi thêm)
            try
            {
                string targetFolder = @"C:\YayCode\CodeWithCSharp\ProjectCsharp\sgu-c-sharf-frontend\img";
                string targetPath = Path.Combine(targetFolder, tenAnh);

                File.Copy(hinhAnhPath, targetPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sao chép ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng ThietBiCreateDTO
            var thietBiCreate = new ThietBiCreateDTO
            {
                TenThietBi = txtTenThietBi.Text.Trim(),
                IdLoaiThietBi = (int)cbbLoaiThietBi.SelectedValue,
                SoLuongDauThietBi = soLuongDauThietBi,
                AnhMinhHoa = tenAnh

            };

            // Gọi API để thêm thiết bị
            try
            {
                var result = await _thietBiService.Add(thietBiCreate);
                if (result.Success)
                {
                    MessageBox.Show("Thêm thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _parentForm.RefreshDataGridView();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show($"Thêm thiết bị thất bại: {result.ErrorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh thiết bị";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                hinhAnhPath = openFileDialog.FileName;
                tenAnh = Path.GetFileName(hinhAnhPath);
                try
                {
                    hinhAnh.Image = Image.FromFile(hinhAnhPath);
                    hinhAnh.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể hiển thị ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}