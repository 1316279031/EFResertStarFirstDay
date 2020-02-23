var getValidate = {
    IsRequest: false
};
$(function () {
    let anmimate = {};
    let $sliders = $('.rightSlider img');
    $sliders.each(function (index) {
        let $img = $($sliders[index]);
        let top=(index*25);
        $img.css({
            top: top + "px"
        });
        $img.on('click',
            function () {
                let $this = $(this);
                if ("animate" in anmimate) {
                    if (anmimate["animate"]) {
                        anmimate["animate"].css({
                            "z-index": 0,
                            top: top
                        });
                        anmimate["animate"] = null;
                        return;
                    }
                }
                $this.css({
                        "z-index": 1000,
                        top: 0
                });
                anmimate["animate"] = $(this);
            });
    });
    let $scriptTime = $('#time');
    let $getCode = $('.getvalidateCode');
    let href = $getCode.attr('href');
    let text = $getCode.text();
    let $login = $("#Login");
    let QueryString="";
    $getCode.on('click', function (e) {
        QueryString = $login.serialize();
        console.log(QueryString);
        e.preventDefault();
        console.log(getValidate.IsRequest);
        if (getValidate.IsRequest !== false) {
            return;
        }
        $.ajax({
            type: "Get",
            url: href,
            data:QueryString,
            success: function (data) {
                if (data === "") {
                    getValidate.IsRequest = false;
                } else {
                    getValidate.IsRequest = true;
                }
                $scriptTime.html(data);
            },
            error: function(e) {
                console.log(e.statusText);
            }
        });
    });
});