var cart = {
    init: function ()
    { cart.regEvents(); },
    regEvents: function ()
    {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuanlity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.pop({
                    quantity: $(this).val(),
                    ID: $(item).data('id')
                });
            });
            $.ajax({
                url: 'Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true)
                    { window.location.href = "/gio-hang";}
                    else {}
                }
            })
        });
    }
}
cart.init();