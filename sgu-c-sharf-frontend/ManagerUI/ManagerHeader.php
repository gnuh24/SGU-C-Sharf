<style>
    .StaffHeader_wrapper__IQw-U {
        z-index: 9;
        background-color: rgb(50, 49, 49);
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        padding: 2rem;
        display: flex;
        justify-content: space-between;
    }

    .StaffHeader_title__QxjW4 {
        font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
        font-size: 3rem;
        font-weight: 700;
        color: #ffffff;
    }

    .StaffHeader_signOut__i2pcu {
        cursor: pointer;
        border: none;
        background-color: transparent;
    }
</style>

<div class="StaffHeader_wrapper__IQw-U">
    <p class="StaffHeader_title__QxjW4">Dekanta</p>
    <button id="logoutButton" class="StaffHeader_signOut__i2pcu">
        <svg aria-hidden="true" focusable="false" data-prefix="fas" data-icon="arrow-right-from-bracket" class="svg-inline--fa fa-arrow-right-from-bracket" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" style="width: 2rem; height: 2rem; color: white">
            <path fill="currentColor" d="M502.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L402.7 224 192 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l210.7 0-73.4 73.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l128-128zM160 96c17.7 0 32-14.3 32-32s-14.3-32-32-32L96 32C43 32 0 75 0 128L0 384c0 53 43 96 96 96l64 0c17.7 0 32-14.3 32-32s-14.3-32-32-32l-64 0c-17.7 0-32-14.3-32-32l0-256c0-17.7 14.3-32 32-32l64 0z"></path>
        </svg>
    </button>
</div>

<!-- Thư viện jQuery: Hỗ trợ thao tác DOM, sự kiện, Ajax -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>  

<!-- Thư viện SweetAlert2: Tạo popup thông báo đẹp -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<!-- File cấu hình của hệ thống: Chứa API endpoint, biến môi trường, v.v. -->
<script src="/SGU-Enterprise-Information-System-EIS/eis-frontend/config.js"></script>

<!-- File chứa các hàm định dạng dữ liệu đầu ra -->
<script src="/SGU-Enterprise-Information-System-EIS/eis-frontend/utils/formatOutput.js"></script>

<!-- Thư viện Font Awesome: Cung cấp các icon đẹp cho giao diện -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">

<!-- Thư viện Pagination.js: Hỗ trợ tạo phân trang dữ liệu -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/paginationjs/2.1.5/pagination.min.js"></script>

<!-- File CSS của Pagination.js: Giúp hiển thị phân trang đẹp hơn -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/paginationjs/2.1.5/pagination.css" />


<script>
    document.getElementById("logoutButton").addEventListener("click", () => {
        Swal.fire({
            title: 'Xác nhận đăng xuất',
            text: 'Bạn có chắc chắn muốn đăng xuất?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Hủy'
        }).then((result) => {
            if (result.isConfirmed) {
                sessionStorage.removeItem('id');
                sessionStorage.removeItem('role');

                // Chuyển hướng về trang đăng nhập
                window.location.href = "../../MemberUI/Login/AdminLoginUI.php";

            }
        });
    });
</script>