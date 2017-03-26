$(function () {

    $(".daTime").datetimepicker();

    $(".date").datetimepicker({
        format: 'MM/DD/YYYY'
    });

    $(".expDate").datetimepicker({
        format: 'MM/YY'
    });

    $('#chkBilling').hide();
    $('#chkBillingAddress').change(function () {
        if (this.checked) {
            $('#chkBilling').hide();
            $('#billStreet').val($('#shipStreet').val());
            $('#billCity').val($('#shipCity').val());
            $('#billPostalCode').val($('#shipPostalCode').val());
            $('#billProvinceCode').val($('#shipProvinceCode').val());
        }
        else {
            $('#chkBilling').show();
        }
    });

    $('#chkEditBillingAddress').change(function () {
        if (this.checked) {
            $('#chkEditBilling').hide();
            $('#billStreet').val($('#shipStreet').val());
            $('#billCity').val($('#shipCity').val());
            $('#billPostalCode').val($('#shipPostalCode').val());
            $('#billProvinceCode').val($('#shipProvinceCode').val());
        }
        else {
            $('#chkEditBilling').show();
        }
    });

    $('#divQoh').hide();
    $('#phyCopy').change(function () {
        if (this.checked) {
            $('#divQoh').show();
        }
        else {
            $('#divQoh').hide();
            $("#qoh").val(null);
        }
    });

});

function SubmitAddress() {
    if ($('#chkBillingAddress').is(':checked')) {
        $('#billStreet').val($('#shipStreet').val());
        $('#billCity').val($('#shipCity').val());
        $('#billPostalCode').val($('#shipPostalCode').val());
        $('#billProvinceCode').val($('#shipProvinceCode').val());
    }
}

function SubmitEditAddress() {
    if ($('#chkEditBillingAddress').is(':checked')) {
        $('#billStreet').val($('#shipStreet').val());
        $('#billCity').val($('#shipCity').val());
        $('#billPostalCode').val($('#shipPostalCode').val());
        $('#billProvinceCode').val($('#shipProvinceCode').val());
    }
}