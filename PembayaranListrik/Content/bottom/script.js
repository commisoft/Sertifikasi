// Get all the tabs
const tabs = document.querySelectorAll('.tab');

tabs.forEach(clickedTab => {
    // Add onClick event listener on each tab
    clickedTab.addEventListener('click', () => {
        // Remove the active class from all the tabs (this acts as a "hard" reset)
        tabs.forEach(tab => {
            tab.classList.remove('active');
        });

        // Add the active class on the clicked tab
        clickedTab.classList.add('active');
		const clickedTabBGColor = getComputedStyle(clickedTab).getPropertyValue('color');
		console.log(clickedTabBGColor);
		document.body.style.background = clickedTabBGColor;
    });
});
// $('.add-to-cart').on('click', function () {
//     var cart = $('.fa-shopping-cart');
//     var imgtodrag = $(".g").eq(0);
//     if (imgtodrag) {
//         var imgclone = imgtodrag.clone()
//             .offset({
//             top: imgtodrag.offset().top,
//             left: imgtodrag.offset().left
//         })
//             .css({
//             'opacity': '0.5',
//                 'position': 'absolute',
//                 'height': '150px',
//                 'width': '150px',
//                 'z-index': '100'
//         })
//             .appendTo($('body'))
//             .animate({
//             'top': cart.offset().top + 10,
//                 'left': cart.offset().left + 10,
//                 'width': 75,
//                 'height': 75
//         }, 1000, 'easeInOutExpo');
        
//         setTimeout(function () {
//             cart.effect("shake", {
//                 times: 2
//             }, 200);
//         }, 1500);

//         imgclone.animate({
//             'width': 0,
//                 'height': 0
//         }, function () {
//             $(this).detach()
//         });
//     }
// });