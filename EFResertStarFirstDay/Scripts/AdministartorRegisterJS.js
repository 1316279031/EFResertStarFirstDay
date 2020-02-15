$(function () {
    let $ajaxUpdate = $('#ajaxUpdate');
    let $Register = $('#register');
    function  addEvent($Register) {
        $Register.on('submit',
            function (e) {
                let $this = $(this);
                let values = $this.serialize();
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
                        $ajaxUpdate.html($(data).find('#register')).hide().fadeIn(300);
                        $Register.off('submit');
                        $Register = $('#register');
                        addEvent($Register);
                    },
                    fail: function (e) {
                        console.log(e.status);
                        let $span = $('#next').siblings('span');
                        $span.addClass('error');
                        $span.text("请求出现了错误");
                    }
                });
            });
    }

    addEvent($Register);
});