$(function () {
    let anmimate = {};
    let $sliders = $('.rightSlider img');
    $sliders.each(function (index) {
        let $img = $($sliders[index]);
        let top=150+(index*25);
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
                        top: 150
                });
                anmimate["animate"] = $(this);
            });
    });
});