$(document).ready(function () {

    /*Using a hyperlink to post like a form.
    Click event handler for all links that have the data-post attribute.*/
    $("a[data-post]").click(function(e) {
        e.preventDefault();

        var $this = $(this);
        var message = $this.data("post"); //Confirmation prompt

        if (message && !confirm(message)) //No was selected in confirmation message.
            return;

        //Create form element and post.
        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href")) //The action set in Href.
            .appendTo(document.body)
            .submit();
    });



});