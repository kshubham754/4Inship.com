$(document).ready(function () {
    try {
    /*DropDown Add SearchSelect='true' in control*/
        $("select[SearchSelect='true']").select2();
    } catch (e) {

    }
});

/*Mesage popup*/
function showMessage(msg, bool) {
   
    if (bool === true) {
        $('#div_msg').html('<div class="alert-success">' + msg + '</div>');
        $.pgwModal({
            target: '#div_msg',
            titleBar: false
        });
        $(".pm-content").css({ "background": "#DFF0D8" })
    }
    else {
        $('#div_msg').html('<div style="color: #dc0400;">' + msg + '</div>');
        $.pgwModal({
            target: '#div_msg',
            titleBar: false
        });
        $(".pm-content").css({ "background": "#ffebeb" })
    }
}