$(window).load(function () {
    redirect(5);
});

function redirect(count) {
    if (count == 0) {
        location.replace("/yops/Default.aspx");
    }
    else {
        count--;
        $("#Timer").text(count);
        setTimeout(function () {
            redirect(count)
        }, 1000);
    }
}