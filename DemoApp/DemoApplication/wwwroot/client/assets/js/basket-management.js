let addBtn = document.querySelectorAll(".add-product-to-basket-btn")

addBtn.forEach(a => a.addEventListener("click", function (e) {
    e.preventDefault();



    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);
        })


}))



$(document).on('click', ".remove-product-to-basket-btn", function (e) {


    e.preventDefault();
    fetch(e.target.href)
        .then(response => response.text())
        .then(data => {
            $('.cart-block').html(data);

        })


})