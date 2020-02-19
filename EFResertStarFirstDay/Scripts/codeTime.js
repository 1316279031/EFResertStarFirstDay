let count = 300;
let href = $('.getvalidateCode').attr('href');
let text = $('.getvalidateCode').text();
clock();
let timeOut = setInterval(clock, 1000);
function clock() {
    $('.getvalidateCode').text(count);
    $('.getvalidateCode').attr('href', '#');
    if (count === 0) {
        ResterClock();
        return;
    }
    count--;
}

function ResterClock() {
    clearInterval(timeOut);
    $(".getvalidateCode").text(text);
    $(".getvalidateCode").attr('href', href);
}
