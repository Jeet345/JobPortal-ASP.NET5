// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {


    $('.job-search-container .input-box .select-box').click(function () {
        $('.job-search-container .input-box .option').toggle();
    });

    $('.job-search-container .input-box .option a').click(function (e) {
        e.preventDefault();

        var option = $(this).text();

        $('.job-search-container .input-box .select-box .select').text(option);

        $('.job-search-container .input-box .option a').removeClass('selected');
        $(this).addClass('selected');

        $('.job-search-container .input-box .option').hide();

    });

    // outside click hide
    $(document).mouseup(function (e) {
        var container = $('.job-search-container .input-box .option');
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            container.hide();
        }
    });

});
