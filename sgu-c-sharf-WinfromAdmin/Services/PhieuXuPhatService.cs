using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using sgu_c_sharf_WinfromAdmin.Models;

namespace sgu_c_sharf_WinfromAdmin.Services
{
    public class PhieuXuPhatService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "http://localhost:5244/api/phieu-xu-phat";

        public PhieuXuPhatService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Lấy danh sách phiếu xử phạt không phân trang.
        /// </summary>
        public async Task<List<PhieuXuPhatDetailDTO>> GetAllNoPagingAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/nopaging");
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(json))
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<PhieuXuPhatDetailDTO>>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse?.Data ?? new List<PhieuXuPhatDetailDTO>();
                }

                return new List<PhieuXuPhatDetailDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<PhieuXuPhatDetailDTO>();
            }
        }

        /// <summary>
        /// Lấy danh sách phiếu xử phạt có phân trang.
        /// </summary>
        public async Task<object> GetAllWithPagingAsync(int page = 1, int limit = 10, TrangThaiPhieuXuPhatEnum? trangThai = null, DateTime? fromDate = null, DateTime? toDate = null, string? keyword = null)
        {
            try
            {
                string query = $"?page={page}&limit={limit}";

                if (trangThai.HasValue)
                    query += $"&trangThai={trangThai.Value}";

                if (fromDate.HasValue)
                    query += $"&fromDate={fromDate.Value:yyyy-MM-dd}";

                if (toDate.HasValue)
                    query += $"&toDate={toDate.Value:yyyy-MM-dd}";

                if (!string.IsNullOrEmpty(keyword))
                    query += $"&keyword={Uri.EscapeDataString(keyword)}";

                HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/paging{query}");
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(json))
                {
                    var apiResponse = JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy phiếu xử phạt theo ID.
        /// </summary>
        public async Task<PhieuXuPhatDetailDTO?> GetByIdAsync(uint id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{BASE_URL}/{id}");
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(json))
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<PhieuXuPhatDetailDTO>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse?.Data;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Thêm mới phiếu xử phạt.
        /// </summary>
        public async Task<bool> AddAsync(PhieuXuPhatCreateDTO newPhieuXuPhat)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(newPhieuXuPhat);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(BASE_URL, httpContent);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(json))
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse != null && apiResponse.Status == 200;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cập nhật phiếu xử phạt.
        /// </summary>
        public async Task<bool> UpdateAsync(uint id, PhieuXuPhatUpdateDTO updatedPhieuXuPhat)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(updatedPhieuXuPhat);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{BASE_URL}/{id}", httpContent);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(json))
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse != null && apiResponse.Status == 200;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa phiếu xử phạt theo ID.
        /// </summary>
        public async Task<bool> DeleteAsync(uint id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{BASE_URL}/{id}");
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(json))
                {
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return apiResponse != null && apiResponse.Status == 200;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
