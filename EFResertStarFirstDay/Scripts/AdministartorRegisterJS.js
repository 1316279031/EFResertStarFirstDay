$(function () {
    let $ajaxUpdate = $('#ajaxUpdate');
    let $Register = $('#register');
    if (!$ajaxUpdate) {
        return;
    }
    $Register.on('submit',
        function (e) {
            let $this = $(this);
            let values = $this.serialize();
            console.log(values);
            e.preventDefault();
            $.ajax({
                type: "post",
                url: 'AdministartorsRegister',
                timeout:2000,
                beforeSend: function(e) {
                    let $span = $('#next').siblings('span');
                        $span.removeClass('error');
                        $span.text("加载中");
                },
                success: function(data) {
                    $this.remove();
                    console.log(data);
                    $ajaxUpdate.html(data).hide().fadeIn(300);
                },
                fail: function(e) {
                    console.log(e.status);
                    let $span = $('#next').siblings('span');
                        $span.addClass('error');
                        $span.text("请求出现了错误");
                }
            });
        });
});