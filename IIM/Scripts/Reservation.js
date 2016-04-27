$(":button").click(function () {


    //Kijken op welke knop gedrukt werd en dan gepast doorsturen
    if ($(this).attr("id").substring(0, 9) === "btnExpand") {
        var id = $(this).attr("id").substring(9);
        expand_reservation(id);
    }else if ($(this).attr("id").substring(0, 9) === "btnDelete") {
        console.log("Delete res");
    } else {
        console.log("Onbekende knop");
    }
    


})

function expand_reservation(resId) {
    var res_divId = "#resDetail" + resId;
    var res_btnID = "#btnExpand" + resId;
    var res_span = res_btnID + " span:first";
    $(res_divId).toggleClass("collapse");

    if ($(res_divId).attr("class") === "collapse") {
        $(res_span).attr("class", "glyphicon glyphicon-chevron-down");
       } else {
        $(res_span).attr("class", "glyphicon glyphicon-chevron-up");
    }
}

function delete_reservation(redId) {
    
}

