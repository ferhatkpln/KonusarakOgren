$('.list-group-item').click(function () {
    var baseId = "#" + $(this).parent().get(0).id + " a";
    $(baseId).removeClass('active');
    $(this).addClass('active');
});

$('#btnExamControl').click(function () {
    var answers = [];
    var isContinue = true;
    $('.exam .question .list-group').each(function (i) {
        var listId = "#" + $(this).attr('id') + " a";
        $(listId).each(function (i) {
            if ($(this).hasClass('active')) {
                var model = {
                    id: $(this).filter('.active').attr('data-id'),
                    answer: $(this).filter('.active').attr('data-answer')
                };
                answers.push(model);
            }
        });
    });

    if (Object.keys(answers).length != 4) {
        isContinue = false;
    }

    if (isContinue) {
        $('#loadpage').show();

        var _examId = $('#examId').val();
        $('#btnExamControl').prop('disabled', true);

        $.ajax({
            url: '/Sınav/SınavCevapControl',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json',
            data: { answerModels: answers, examId: _examId },
            success: function (result) {
                $('#loadpage').hide();

                $('.exam .question .list-group').each(function (i) {
                    var listId = "#" + $(this).attr('id') + " a";

                    $(listId).each(function (i) {
                        if ($(this).hasClass('active')) {
                            var answerLink = this;
                            var id = $(this).filter('.active').attr('data-id');

                            result.forEach(function (e, a) {
                                if (e.id == id) {
                                    if (e.isCorrectAnswer) {
                                        $(answerLink).css("background-color", "green");
                                        $(answerLink).css("border-color", "green");
                                    }
                                    else {
                                        $(answerLink).css("background-color", "red");
                                        $(answerLink).css("border-color", "red");

                                        var answerGroup = answerLink.closest('div');

                                        for (var i = 0; i < 4; i++) {
                                            if ($(answerGroup).children().get(i).attributes[1].nodeValue == e.correctAnswer) {
                                                
                                                $($(answerGroup).children().get(i)).css("border-color", "green").css("background-color", "green").css("color", "white");
                                            }
                                        }
                                    }
                                }
                            });
                        }
                    });
                });

               
            },
            error: function (hata) {
                $('#loadpage').hide();
                alert('Hata Oluştu');
            }
        });
    }
    else {
        Swal.fire('Sınavı tamamlamak için tüm sorular cevaplanmalıdır.')
    }
});