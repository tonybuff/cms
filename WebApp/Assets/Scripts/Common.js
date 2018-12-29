jQuery.sw = function (type, title, callback) {
    if (type == 'success-message') {
        swal({
            title: title,
            buttonsStyling: false,
            confirmButtonClass: "btn btn-info",
            type: "success"
        }).then((value) => {
            if (callback != null) {
                callback();
            }
        })
    } else if (type == 'error-message') {
        swal({
            title: title,
            buttonsStyling: false,
            confirmButtonClass: "btn btn-info",
            type: "error"
        }).then(function () {
            if (callback != null) {
                callback();
            }
        })
    }
}