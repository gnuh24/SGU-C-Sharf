<style>
    /* Base Styles */
    #Home-img {
        width: 110px;
        height: 80px;
    }

    .input__wrapper {
        display: flex;
        align-items: center;
        position: relative;
        gap: 10px;
        justify-content: center;
    }

    .search-input {
        padding-left: 25px;
        /* background-image: url("../img/search.png");*/
        background-size: 20px;
        background-repeat: no-repeat;
        background-position: 5px center;
        border: 2px solid rgb(133, 6, 6);
        border-radius: 20px;
        width: 100%;
        max-width: 800px;
        height: 35px;
    }

    #search-button {
        background-color: white;
        color: rgb(146, 26, 26);
        padding: 10px 20px;
        border: none;
        cursor: pointer;
        border-radius: 5px;
        margin-right: 10px;
    }

    .header-option {
        cursor: pointer;
        font-size: 24px;
        /* Adjust icon size */
        display: flex;
        align-items: center;
        justify-content: center;
        width: 20%;
        height: 40px;
        background-color: white;
        color: rgb(146, 26, 26);
        padding: 5px 15px;
        border: none;
        border-radius: 5px;
        margin-left: 5px;
    }

    .header-option a {
        color: inherit;
        text-decoration: none;
    }



    /* Responsive Styles */
    @media (max-width: 768px) {

        /* Adjust logo size */
        #Home-img {
            width: 100px;
            height: auto;
        }

        /* Adjust search input */
        .search-input {
            width: 100%;
            max-width: none;
            height: 30px;
        }

        /* Stack buttons vertically */
        .header-option {
            padding: 5px;
        }
    }

    @media (max-width: 576px) {

        /* Logo center alignment on small screens */
        .col-md-2 {
            text-align: center;
        }

        /* Center the search form on small screens */
        .input__wrapper {
            flex-direction: column;
            align-items: stretch;
            gap: 5px;
        }

        /* Stack login options vertically */
        .col-md-4 {
            flex-direction: column;
            align-items: center;
            gap: 10px;
        }
    }
</style>
<header class="container-fluid bg-danger" style="background-color: rgb(146, 26, 26) !important;">
    <div id="Home-over-Header" class="row align-items-center justify-content-center" style="height: fit-content;">
        <!-- Logo -->
        <div class="col-12 col-md-2 text-center mb-2 mb-md-0">
            <img id="Home-img" src="./img/logoWine.jpg" alt="Logo" class="img-fluid mx-auto" />
        </div>

        <!-- Search Form
            <div class="col-12 col-md-6 mb-2 mb-md-0 d-flex justify-content-center">
                <form id="search" class="input__wrapper d-flex justify-content-center" method="post" action="SignedProduct.php" style="width:100%">
                    <input id="searchSanPham" name="searchFromAnotherPage" type="text" class="form-control me-2 search-input" placeholder="Tìm kiếm" style="width:90%" required="" />
                    <button id="search-button" class="btn btn-primary" type="submit" style="width:10%">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </button>
                </form>
            </div> -->


        <!-- Icons Section -->
        <div class="col-12 col-md-4 d-flex justify-content-center align-items-center gap-4" style="height: fit-content;">
            <!-- <div class="header-option" onclick="toCart()" style="height: fit-content;">
                <i class="fa-solid fa-cart-shopping"></i>
            </div> -->
            <!-- <div class="header-option  header-option-login" onclick="toMyOrder()" style="height: fit-content;">
                <i class="fa-solid fa-truck-fast"></i>
            </div> -->
            <div class="header-option  header-option-login" onclick="toProfile()" style="height: fit-content;">
                <i class="fa-solid fa-user"></i>
            </div>
            <div class="header-option  header-option-login" onclick="logout()" style="height: fit-content;">
                <i class="fa-solid fa-right-from-bracket"></i>
            </div>
            <div id="Home-login" class="col-6 col-md-2 text-end text-center">
                <button class="btn btn-light text-danger" style="color:rgb(146, 26, 26) !important;font-weight:bold;">Login</button>
            </div>
        </div>

    </div>
    
</header>

<script src=" https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

<script>
    const urlParams1 = new URLSearchParams(window.location.search);

    // Lấy giá trị của searchFromAnotherPage và gán vào search
    const searchFromAnotherPage1 = urlParams1.get('searchFromAnotherPage');
    if (searchFromAnotherPage1) {
        document.getElementById("searchSanPham").value = searchFromAnotherPage1;
    }
    // Kiểm tra xem 'role' có tồn tại trong Session Storage không
    if (sessionStorage.getItem('role')) {
        // Nếu role tồn tại, ẩn nút bằng cách thêm thuộc tính 'hidden'
        document.getElementById("Home-login").style.display = 'none';

    } else {
        // Nếu tồn tại, ẩn các nút bằng cách thiết lập display: none
        document.querySelectorAll('.header-option-login').forEach((element) => {
            element.style.display = 'none';
        });
    }

    // Lắng nghe sự kiện click vào id "Home-login"
    document.getElementById("Home-login").addEventListener("click", function() {
        window.location.href = './Login/LoginUI.php';
    });

    document.getElementById("Home-img").addEventListener("click", function() {
        // Chuyển hướng về trang chủ khi click vào hình ảnh
        window.location.href = "./HomePage.php";
    });

    // Sự kiện tìm kiếm search 
    document.getElementById("search-button").addEventListener("click", (event) => {
        event.preventDefault();
        const form = document.getElementById("search");
        const searchValue = document.getElementById("searchSanPham").value;
        if (searchValue != "") {
            form.action = `Product.php?searchFromAnotherPage=${searchValue}`;
            form.submit();
        } else {
            Swal.fire({
                title: 'Lỗi!',
                text: 'Bạn cần phải nhập gì đó vào thanh tìm kiếm trước khi ấn nút tìm kiếm.',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }

    });

    //Sự kiện giỏ hàng
    function toCart() {
        const form = document.getElementById("search");
        if (form) {
            const sessionStoragedata = JSON.parse(sessionStorage.getItem("id"));
            const maTaiKhoan = sessionStoragedata;



            // Thêm MaTaiKhoan vào action của form
            form.action = "Cart.php?maTaiKhoan=" + maTaiKhoan;
            // Gửi form đi
            form.submit();
        } else {
            console.error("Form not found!");
        }
    }

    //profile
    function toProfile() {
        const form = document.getElementById("search");
        if (form) {
            const sessionStorageData = JSON.parse(sessionStorage.getItem("id"));
            const maTaiKhoan = sessionStorageData;

            // Thêm MaTaiKhoan vào action của form
            form.action = "Profile.php?maTaiKhoan=" + maTaiKhoan;
            // Gửi form đi
            form.submit();
        } else {
            console.error("Form not found!");
        }
    }

    //Sự kiện đơn hàng cá nhân
    function toMyOrder() {
        const form = document.getElementById("search");
        if (form) {
            const sessionStorageData = JSON.parse(sessionStorage.getItem("id"));
            const maTaiKhoan = sessionStorageData;


            // Thêm MaTaiKhoan vào action của form
            form.action = "MyOrder.php?maTaiKhoan=" + maTaiKhoan;

            // Gửi form đi
            form.submit();
        } else {
            console.error("Form not found!");
        }
    }

    // Sự kiện đăng xuất
    function logout() {
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

                // Gọi API logout qua Ajax
                sessionStorage.removeItem("id");
                sessionStorage.removeItem("email");
                sessionStorage.removeItem("role");

                // Chuyển hướng về trang chủ khách
                window.location.href = "./HomePage.php";


            }
        });
    }
</script>