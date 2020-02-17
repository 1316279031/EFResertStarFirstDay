$(function () {
    let $window = $('.modelWindow');
    function OpenWindow($window) {
        let left = 100 * ((1 - ($window.outerWidth() / $(window).width())) / 2);
        let top = 100 * ((1 - ($window.outerHeight() / $(window).height())) / 2);
        $window.css({
            left: left + "%",
            top:top+"%"
        });
        $window.fadeIn(300);
    }
    function CloseWindow($window) {
        $window.find('.close').on('click',
            function(e) {
                e.preventDefault();
                $window.fadeOut(200);
                $("#next").attr("disabled", false);
            });
    }
    OpenWindow($window);
    CloseWindow($window);
});