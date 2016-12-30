$(document).ready(function () {
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    //Drop Down Start
    $("select[SearchSelect='true']").select2();
    //Drop Down End
    //Date Picker Start
    $('input[calendar="date"]').datepicker({format:'yyyy/mm/dd'});
    //Date Picker End
})
function showMessage(msg, bool) {
    $('#divmsgSuccess').hide().delay(0);
    $('#divmsgError').hide().delay(0);

    if (bool === true) {
        $('#divmsgSuccess').show('slow').find('span').text(msg).parent();

    }
    else {
        $('#divmsgError').show('slow').find('span').text(msg).parent();
    }

}
function Cancel_Click() {
    window.location = window.location.href;
}