function AddEventList($links) {
    $links.each(function (index) {
        let $this = $(this);
        let href = $this.attr('href');
        console.log(href);
        $(this).on('click',
            function (e) {
                e.preventDefault();
                AjaxRequest(href);
            });
    });
}

function AjaxRequest(href) {
    let Datas = [];
    $.ajax({
        type: "Get",
        url: href,
        beforeSend: function (e) {
            let $views = $('.views');
            $views.append('<div class="error"><p>加载中。。。。</p></div>');
        },
        success: function (data, e) {
            let json = JSON.parse(data);
            $('.error').remove();
            CreateTable(json,href);//创建表格（根据数据创建表格）但目前先搭起整个框架的样子，之后在使用Json数据创建表格
        },
        error: function(e) {
            console.log(e);
            //向DOM添加节点以提示用户添加失败
            $('.error').remove();
            alert("提交失败");
        }
    });
}
let $link = $('.redict');
AddEventList($link);