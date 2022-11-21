$(function () {

    var PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-bs-save="modal"]', function (event) {

        var formData = new FormData($('#modalForm').get(0));
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        $.ajax({
            url: "/Admin/" + actionUrl,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (data) {
                PlaceHolderElement.find('.modal').modal('hide');
            }
            
        })
        PlaceHolderElement.find('.modal').modal('hide');
    })

})


function Validate() {
    var password = document.getElementById("txtPassword").value;
    var confirmPassword = document.getElementById("txtConfirmPassword").value;
    if (password != confirmPassword) {
        alert("Passwords do not match.");
        return false;
    }
    return true;
}