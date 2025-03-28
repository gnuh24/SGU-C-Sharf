<html lang="en">

<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
    <link rel="stylesheet" href="./HomePage.css" />
    <link rel="stylesheet" href="./login.css" />
    <link rel="stylesheet" href="MyOrderInDetail.css" />
    <link rel="stylesheet" href="CreateOrder.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="../HelperUI/formatOutput.js"></script>

    <title>Chi tiết đơn hàng</title>
</head>

<body>
    <?php require_once "./Header.php" ?>

    <section>
        <div class="center-text" style="margin-top: 20px;">
            <div class="title_section">
                <div class="bar"></div>
                <h2 class="center-text-share">Chi tiết đơn hàng</h2>
            </div>
        </div>
    </section>

    <section style="padding: 0 5%;">
        <div id="chiTietTrangThaiContent">
            <div id="circle-container1" class="circle-container">
                <div id="ele1" class="chiTietTrangThaiElement">
                    <i id="icon1" class="fa-solid fa-cart-shopping icon"></i>
                    <p class="trangThai">Chờ duyệt</p>
                    <p id="thoiGian1" class="thoiGian"></p>
                </div>
            </div>

            <div id="line1" class="line">_____________________</div>

            <div id="circle-container2" class="circle-container">
                <div id="ele2" class="chiTietTrangThaiElement">
                    <i id="icon2" class="fa-solid fa-circle-user icon"></i>
                    <p class="trangThai">Đã duyệt</p>
                    <p id="thoiGian2" class="thoiGian"></p>
                </div>
            </div>

            <div id="line2" class="line">_____________________</div>

            <div id="circle-container3" class="circle-container">
                <div id="ele3" class="chiTietTrangThaiElement">
                    <i id="icon3" class="fa-solid fa-truck-fast icon"></i>
                    <p class="trangThai">Đang giao</p>
                    <p id="thoiGian3" class="thoiGian"></p>
                </div>
            </div>

            <div id="line3" class="line">_____________________</div>

            <div id="circle-container4" class="circle-container">
                <div id="ele4" class="chiTietTrangThaiElement">
                    <i id="icon4" class="fa-solid fa-gift icon"></i>
                    <p class="trangThai">Giao thành công</p>
                    <p id="thoiGian4" class="thoiGian"></p>
                </div>
            </div>

            <div id="line4" class="line">_____________________</div>

            <div id="circle-container5" class="circle-container">
                <div id="ele5" class="chiTietTrangThaiElement">
                    <i id="icon5" class="fa-solid fa-ban icon"></i>
                    <p class="trangThai">Hủy</p>
                    <p id="thoiGian5" class="thoiGian"></p>
                </div>
            </div>
        </div>
    </section>

    <section>
        <div class="layout__wrapper">
            <div class="checkout__wrapper containerPage" style="margin-top: 30px;">
                <div class="payment_info__wrapper">
                    <div class="payment_info">
                        <div id='checkout_form'>
                            <div class='payment__wrapper'>
                                <label>Các sản phẩm đặt mua</label>
                                <div id="product-list"></div>

                            </div>



                            <p class='hotline'>
                                * Để được hỗ trợ trực tiếp và nhanh nhất vui lòng liên hệ THug88
                            </p>
                        </div>
                    </div>
                </div>
                <div class="order_info__wrapper" style="height: 500px;">
                    <div class="order_info" style="margin-top: 30px; padding: 0 20px;">
                        <p class="title" style="text-align: center;">Thông tin đơn hàng</p>
                        <div class="wrap">

                        </div>

                    </div>



                </div>
            </div>
        </div>
        </div>
    </section>

</body>


<?php require_once "./Footer.php" ?>

<script>
    // Hàm cập nhật màu sắc và thời gian cho container được chỉ định
    function setColorAndTime(trangThaiValue, thoiGianValue) {

        // Chọn các phần tử sử dụng jQuery
        const $line1 = $('#line1');
        const $line2 = $('#line2');
        const $line3 = $('#line3');
        const $line4 = $('#line4');


        const $thoiGian1 = $('#thoiGian1');
        const $thoiGian2 = $('#thoiGian2');
        const $thoiGian3 = $('#thoiGian3');
        const $thoiGian4 = $('#thoiGian4');
        const $thoiGian5 = $('#thoiGian5');

        const $icon1 = $('#icon1');
        const $icon2 = $('#icon2');
        const $icon3 = $('#icon3');
        const $icon4 = $('#icon4');
        const $icon5 = $('#icon5');

        const $circleContainer1 = $('#circle-container1');
        const $circleContainer2 = $('#circle-container2');
        const $circleContainer3 = $('#circle-container3');
        const $circleContainer4 = $('#circle-container4');
        const $circleContainer5 = $('#circle-container5');


        // Thực hiện thay đổi CSS sử dụng jQuery
        switch (trangThaiValue) {
            case 'ChoDuyet':
                $icon1.css("color", "green");
                $circleContainer1.css("border-color", "green");
                $thoiGian1.html(convertDateTimeFormat(thoiGianValue));
                break;
            case 'DaDuyet':
                $icon2.css("color", "green");
                $line1.css("color", "green");
                $circleContainer2.css("border-color", "green");
                $thoiGian2.html(convertDateTimeFormat(thoiGianValue));

                break;
            case 'DangGiao':
                $icon3.css("color", "green");
                $line2.css("color", "green");
                $circleContainer3.css("border-color", "green");
                $thoiGian3.html(convertDateTimeFormat(thoiGianValue));

                break;
            case 'GiaoThanhCong':
                $icon4.css("color", "green");
                $line3.css("color", "green");
                $circleContainer4.css("border-color", "green");
                $thoiGian4.html(convertDateTimeFormat(thoiGianValue));

                break;
            case 'Huy':

                $icon1.css("color", "rgb(146, 26, 26)");
                $circleContainer1.css("border-color", "rgb(146, 26, 26)");

                $icon2.css("color", "rgb(146, 26, 26)");
                $line1.css("color", "rgb(146, 26, 26)");
                $circleContainer2.css("border-color", "rgb(146, 26, 26)");

                $icon3.css("color", "rgb(146, 26, 26)");
                $line2.css("color", "rgb(146, 26, 26)");
                $circleContainer3.css("border-color", "rgb(146, 26, 26)");

                $icon4.css("color", "rgb(146, 26, 26)");
                $line3.css("color", "rgb(146, 26, 26)");
                $circleContainer4.css("border-color", "rgb(146, 26, 26)");

                $icon5.css("color", "rgb(146, 26, 26)");
                $line4.css("color", "rgb(146, 26, 26)");
                $circleContainer5.css("border-color", "rgb(146, 26, 26)");
                $thoiGian5.html(convertDateTimeFormat(thoiGianValue));

                break;
            default:
                break;
        }
    }

    function loadOrderData(maOrder) {
        $.ajax({
            url: '../../Controllers/OrderController.php', // Đường dẫn API lấy chi tiết đơn hàng
            method: 'GET',
            dataType: 'json',
            data: {
                idOrder: maOrder // Use 'Id' based on your response structure
            },
            success: function(response) {
                console.log(response.data);
                let totalPrice_Shipping = 0;
                let productListHtml = '';
                let html = '';
                // Xử lý từng sản phẩm trong kết quả trả về
                response.data.details.forEach(function(cartProduct) {
                    totalPrice_Shipping += cartProduct.Total;
                    const formattedPrice = formatCurrency(cartProduct.UnitPrice);
                    const formattedTotalPrice = formatCurrency(cartProduct.Total);

                    // Thêm sản phẩm vào danh sách HTML
                    productListHtml += `
                                            <div class='radio__wrapper'>
                                                <div>
                                                    <div class='cartItem' id='${cartProduct.ProductId}'>
                                                        <a href='#' class='img'><img class='img' src='../img/${cartProduct.Image}' /></a>
                                                        <div class='inforCart'>
                                                            <div class='nameAndPrice'>
                                                                <a href='#' class='nameCart'>${cartProduct.ProductName}</a>
                                                                <p class='priceCart'>${formattedPrice}</p>
                                                            </div>
                                                            <div class='quantity'>
                                                                <div class='txtQuantity'>${cartProduct.Quantity}</div>
                                                            </div>
                                                        </div>
                                                        <div class='wrapTotalPriceOfCart'>
                                                            <div class='totalPriceOfCart'>
                                                                <p class='lablelPrice'>Thành tiền</p>
                                                                <p class='valueTotalPrice'>${formattedTotalPrice}</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        `;
                });

                html = `
                        <div class="container mt-4">
                            <div class="info__wrapper order_info2">
                                <p><span class="span1">Mã đơn hàng:</span><span class="span2" id="id">${response.data.info.OrderId}</span></p>
                                <p><span class="span1">Thời gian đặt hàng:</span><span class="span2" id="orderTime">${convertDateTimeFormat(response.data.info.OrderTime)}</span></p>
                                <p><span class="span1">Ghi chú:</span><span class="span2" id="note">${response.data.info.Note==null?'':response.data.info.Note}</span></p>
                                <p><span class="span1">Tình trạng:</span><span class="span2" id="note">${response.data.info.isPaid==0?'Chưa thanh toán':'Đã thanh toán'}</span></p>
                                <p><span class="span1">Phương thức thanh toán:</span><span class="span2" id="note">${response.data.info.Payment}</span></p>

                            </div>

                            <div class="divider"></div>

                            <div class="total__info">
                                <p>Tạm tính</p>
                                <p id="totalPrice"></p>
                            </div>

                            <div class="total__info">
                                <p>Giảm giá</p>
                                <p id="totalPrice">${formatCurrency(totalPrice_Shipping - response.data.info.TotalPrice)}</p>
                            </div>

                            <div class="divider"></div>

                            <div class="total__info">
                                <p>Tổng cộng</p>
                                <p id="totalPrice">${formatCurrency(response.data.info.TotalPrice)}</p>
                            </div>
                        </div>`
                // Chèn danh sách sản phẩm vào phần tử HTML
                $('#product-list').html(productListHtml);
                $('.wrap').html(html)
                // Hiển thị tổng giá trị đơn hàng
                $('#totalPrice').text(formatCurrency(totalPrice_Shipping));
                var trangThaiDonHang = response.data.orderStatuses;

                // Lặp qua mỗi trạng thái trong mảng
                trangThaiDonHang.forEach(function(trangThai) {

                    // Lấy thông tin trạng thái và thời gian
                    var trangThaiValue = trangThai.Status;
                    var thoiGianValue = trangThai.UpdateTime;
                    setColorAndTime(trangThaiValue, thoiGianValue);
                });
            },
            error: function() {
                $('#product-list').html("<h1> Lỗi khi tải dữ liệu từ server </h1>");
            }
        });
    }
    $(document).ready(function() {
        const urlParams = new URLSearchParams(window.location.search);

        const maDonHang = urlParams.get('maDonHang');

        if (maDonHang) {
            loadOrderData(maDonHang);
        } else {
            console.error("Không tìm thấy mã đơn hàng trong URL");
        }
    });
</script>

</html>