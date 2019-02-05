$(document).ready(function () {

    /*Using a hyperlink to post like a form.
    Click event handler for all links that have the data-post attribute.*/
    $("a[data-post]").click(function(e) {
        e.preventDefault();

        var $this = $(this);
        var message = $this.data("post"); //Confirmation prompt

        if (message && !confirm(message)) //No was selected in confirmation message.
            return;

        //Hidden form in admin _Layout.
        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        //Create form element and post.
        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href")) //The action set in Href.
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();
    });



});