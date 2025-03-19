<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="../AdminHome.css" />
    <link rel="stylesheet" href="../QLLoaiSanPham/QLLoaiSanPham.css" />

    <title>Quản lý loại sản phẩm</title>
</head>
<style>
    .paginationjs {
        display: flex;
        justify-content: center;
        margin: 20px 0;
    }
</style>

<body>
    <div class="StaffLayout_wrapper__CegPk">
        <?php require_once "../ManagerHeader.php" ?>
        <div class="Manager_wrapper__vOYy">
            <?php require_once "../ManagerMenu.php" ?>

            <div style="padding-left: 16%; width: 100%; padding-right: 2rem">
                <div class="wrapper">
                    <div style="
                        display: flex;
                        padding-top: 1rem;
                        padding-bottom: 1rem;
                    ">
                        <h2>Loại Sản Phẩm</h2>
                        <button style="
                            margin-left: auto;
                            font-family: Arial;
                            font-size: 1.5rem;
                            font-weight: 700;
                            color: white;
                            background-color: rgb(65, 64, 64);
                            padding: 1rem;
                            border-radius: 0.6rem;
                            cursor: pointer;
                        " id="btnAddcategory">
                            <a href="./FromCreateLoaiSanPham.php"> Thêm Loại Sản Phẩm</a>
                        </button>
                    </div>
                    <br>
                    <div class="boxFeature">
                        <div style="position: relative">
                            <input style="border: 2px solid #333; height: 30px; padding: 0.5rem;" class="Admin_input__LtEE-" placeholder="Tìm kiếm loại sản phẩm" />
                            <button id="filter-button" style="cursor: pointer;"><i class="fa fa-search"></i></button>
                        </div>
                        <div style="margin-left: auto"></div>
                    </div>
                    <br>
                    <table class="Table_table__BWPy">
                        <thead class="Table_head__FTUog">
                            <tr>
                                <th style="width: 25%" class="Table_th__hCkcg" scope="col">Mã loại sản phẩm</th>
                                <th class="Table_th__hCkcg" scope="col">Loại sản phẩm</th>
                                <th style="width: 15%" class="Table_th__hCkcg" scope="col">Action</th>
                            </tr>
                        </thead>
                        <tbody id="tableBody">
                        </tbody>
                    </table>
                    <div id="pagination-container"></div>
                </div>
            </div>
        </div>
    </div>
</body>

</html>

<script>
    // Khởi tạo trang hiện tại
    fetchDataAndUpdateTable(currentPage, '');
    var currentPage = 1;
    var pageSizeGlobal = 5;
    var search = "";
    const userRole = sessionStorage.getItem('role');

    document.addEventListener('DOMContentLoaded', () => {
        // const adminButton = document.getElementById('btnAddcategory');
        // if (userRole != 'Manager') {
        //     adminButton.style.display = 'none';
        // } else {
        //     console.log('Phần tử adminButton không tồn tại.');
        // }
    });

    function clearTable() {
        var tableBody = document.querySelector('.Table_table__BWPy tbody');
        if (tableBody) {
            tableBody.innerHTML = ''; // Xóa nội dung trong tbody
        } else {
            console.error("Table body not found.");
        }
    }

    function getAllLoaiSanPham(page, search) {
        // Lấy userRole từ sessionStorage
        const userRole = sessionStorage.getItem('role');

        $.ajax({
            url: `${window.env.API_ROOT}/categories`,
            type: "GET",
            dataType: "json",
            data: {
                pageNumber: page,
                pageSize: pageSizeGlobal,
                search: search
            },
            success: function(response) {
                var data = response.data.content;
                var tableBody = document.getElementById("tableBody");
                var tableContent = "";

                // Duyệt qua mảng dữ liệu và tạo các hàng mới cho tbody
                if (data.length > 0) {
                    data.forEach(function(record, index) {
                        var trClass = (index % 2 !== 0) ? "Table_data_quyen_1" : "Table_data_quyen_2"; // Xác định class của hàng

                        var trContent = `
                        <form id="updateForm" method="post" action="FormUpdateLoaiSanPham.php">
                            <tr style="height: 20%; max-height: 20%;">
                                <td class="${trClass}">${record.id}</td>
                                <td class="${trClass}">${record.categoryName}</td>
                                <td class="${trClass}">`;

                        // Logic hiển thị nút dựa vào userRole
                        if (record.id == 1) {
                            trContent += `Mặc định`;
                        } else {
                            // if (userRole === "Manager") {
                                trContent += `
                                <button class="edit" onclick="updateLoaiSanPham(${record.id}, '${record.categoryName}')">Sửa</button>
                                <button class="delete" onclick="deleteLoaiSanPham(${record.id}, '${record.categoryName}')">Xoá</button>`;
                            // } else if (userRole === "Employee") {
                            //     trContent += `
                            //     <button class="edit" onclick="updateLoaiSanPham(${record.id}, '${record.categoryName}')">Xem chi tiết</button>`;
                            // }
                        }

                        trContent += `</td></tr></form>`;

                        // Nếu chỉ có ít hơn 5 phần tử và đã duyệt đến phần tử cuối cùng, thêm các hàng trống vào
                        if (data.length < 5 && index === data.length - 1) {
                            for (var i = data.length; i < 5; i++) {
                                var emptyTrClass = (i % 2 !== 0) ? "Table_data_quyen_1" : "Table_data_quyen_2";
                                trContent += `
                                <tr style="height: 20%; max-height: 20%;">
                                    <td class="${emptyTrClass}" style="width: 130px;"></td>
                                    <td class="${emptyTrClass}"></td>
                                    <td class="${emptyTrClass}"></td>
                                </tr>
                            `;
                            }
                        }

                        tableContent += trContent;
                    });
                } else {
                    tableContent = `<tr><td style="text-align: center;" colspan="7">Không có loại sản phẩm nào thỏa yêu cầu</td></tr>`;
                }

                tableBody.innerHTML = tableContent;

                // Tạo phân trang
                setupPagination(response.data.totalElements, page);
            },

            error: function(xhr, status, error) {
                if (xhr.status === 401) {
                    alert('Phiên đăng nhập của bạn đã hết hạn. Vui lòng đăng nhập lại.');
                    window.location.href = '/login';
                } else {
                    console.error('Lỗi khi gọi API: ', error);
                }
            }
        });
    }


    function fetchDataAndUpdateTable(page, search) {
        clearTable();
        getAllLoaiSanPham(page, search);
    }

    function setupPagination(totalElements, currentPage) {
        const totalPage = Math.ceil(totalElements / pageSizeGlobal);
        totalPage <= 1 ? $('#pagination-container').hide() : $('#pagination-container').show();
        $('#pagination-container').pagination({
            dataSource: Array.from({
                length: totalElements
            }, (_, i) => i + 1),
            pageSize: pageSizeGlobal,
            showPrevious: true,
            showNext: true,
            pageNumber: currentPage,

            callback: function(data, pagination) {
                if (pagination.pageNumber !== currentPage) {
                    currentPage = pagination.pageNumber;
                    fetchDataAndUpdateTable(currentPage, search);
                }
            }
        });
    }

    document.getElementById('filter-button').addEventListener('click', function() {
        var search = document.querySelector('.Admin_input__LtEE-').value;
        fetchDataAndUpdateTable(currentPage, search);
    });

    document.querySelector('.Admin_input__LtEE-').addEventListener('keypress', function(event) {
        if (event.key === 'Enter') {
            event.preventDefault();
            search = this.value;
            fetchDataAndUpdateTable(currentPage, search);
        }
    });

    function deleteLoaiSanPham(id, categoryName) {
        Swal.fire({
            title: `Bạn có muốn xóa ${categoryName} không?`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Hủy'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `${window.env.API_ROOT}/categories/${id}`,
                    type: 'DELETE',

                    success: function(response) {
                        Swal.fire('Thành công!', 'Xóa loại sản phẩm thành công !!',
                            'success');
                        fetchDataAndUpdateTable(currentPage, search); // Refresh table after delete
                    },
                    error: function(xhr, status, error) {
                        Swal.fire('Lỗi!', 'Không thể xóa loại sản phẩm. Vui lòng thử lại sau.', 'error');
                        console.error('Delete Error: ', error);
                    }
                });
            }
        });
    }

    const updateLoaiSanPham = (id, categoryName) => {
        window.location.href = `FormUpdateLoaiSanPham.php?id=${id}&categoryName=${categoryName}`;
    }

    // Initial data fetch
    fetchDataAndUpdateTable(currentPage, search);
</script>