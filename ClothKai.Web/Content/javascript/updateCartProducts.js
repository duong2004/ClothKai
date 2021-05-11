$(document).ready(function () {
    updateCartProducts();
});
function updateCartProducts() {
    var cartProducts;
    var existingCookieData = $.cookie('CartProducts');
    if (existingCookieData != undefined || existingCookieData != "" || existingCookieData != null) {
        cartProducts = existingCookieData.split('-');
    } else {
        cartProducts = [];
    }
    $("#cartProductsCount").html(cartProducts.length)
}