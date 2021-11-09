$('#Question_Title').change(function () {
    var selectedValue = $('#Question_Title option:selected').attr('data-id');
    $('#loadpage').show();
    if (selectedValue != '-1') {
        $.ajax({
            url: '/Sınav/GetTextContent',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            data: { id: selectedValue },
            success: function (result) {
                $('#loadpage').hide();
                $('#Question_Content').val('');
                $('#Question_Content').val(result);
            },
            error: function (hata) {
                $('#loadpage').hide();
                alert('Hata Oluştu.');
            }
        });
    }
});