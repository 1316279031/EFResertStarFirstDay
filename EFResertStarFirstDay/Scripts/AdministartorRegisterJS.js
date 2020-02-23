$(function () {
    let $ajaxUpdate = $('#ajaxUpdate');
    let $Register = $('#register');
    $('span').remove();
    function AddEvent() {

        $Register.on('submit',
            function (e) {
                let $this = $(this);
                let values = $this.serialize();
                console.log(values);
                e.preventDefault();
                $.ajax({
                    type: "Post",
                    url: 'AdministartorsRegister',
                    data: values,
                    timeout: 2000,
                    beforeSend: function (e) {
                        let $span = $('#next').siblings('span');
                        $span.removeClass('error');
                        $span.text("加载中");
                    },
                    success: function (data) {
                        $this.remove();
                        if ($(data).attr('name') === 'DetialsRegister') {
                            $ajaxUpdate.html($(data)).hide().fadeIn(300);
                            return;
                        }
                        $ajaxUpdate.html($(data).find('form')).hide().fadeIn(300);
                        $Register = $(data);
                        $Register=$ajaxUpdate.find('form');
                        $('#next').attr("disabled", false);
                        AddEvent();
                    },
                    fail: function (e) {
                        let $span = $('#next').siblings('span');
                        $span.addClass('error');
                        $span.text("请求出现了错误");
                    }
                });
            });
    }
    AddEvent();
});