$(document).ready(function () {

    //console.log("This is my number: " + (x + y));

    var theForm = $("#theForm");
    theForm.hide();


    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });


    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text())
    });


    //zaczynamy nazwę zmiennej od $, żeby wiedzieć, że jest ona jakoś związana z jQuery, taka konwencja nazwenicza
    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.slideToggle(1000);
    });

    //może być $popupForm.toggle() albo $popupForm.toggle(1000) albo $popupForm.slideToggle(1000) albo $popupForm.fadeToggle(1000)


});

    




// aby uniknąć konfliktu nazwenictwa spowodowanego globalnym kontentem javascript zamykamy cały skrypt w anonimowej funkcji aby nasze zmienne nie były w global scopie
//ale używając jQuery jest to prostsze i używamy funkcji ready

//(function () {

//    var x = 30;
//    let y = 150;

//    //alert("wtf");
//    //alert(x+y);
//    console.log("This is my number: " + (x + y));

//    // używać jQuery można używając $ zamiast pisać jQuery gdy chcemy go użyć

//    //bez jQuery
//    //var theForm = document.getElementById("theForm");
//    //theForm.hidden = true;
//    var theForm = $("#theForm");
//    theForm.hide();

//    //bez jQuery
//    //var button = document.getElementById("buyButton");
//    //button.addEventListener("click", function () {
//    //    //alert("Buying Item");
//    //    console.log("Buying Item");
//    //});
//    var button = $("#buyButton");
//    button.on("click", function () {
//        //alert("Buying Item");
//        console.log("Buying Item");
//    });

//    //bez jQuery
//    //var productInfo = document.getElementsByClassName("product-props");
//    //var listItems = productInfo.item[0];

//    var productInfo = $(".product-props li");
//    productInfo.on("click", function () {
//        console.log("You clicked on " + $(this).text())
//    });

////gdyby nie używać jQuery to zamiast $(this).text() trzeba by było this.innerText ale może to w różnych przeglądarkach dawać różne efekty a z jQuery bezpieczniej

//}) ();



