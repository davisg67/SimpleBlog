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


    //Loop all elements that have the data-slug attribute.
    $("[data-slug]").each(function () {
        //Locate the text field to get the slug value.
        var $this = $(this);
        var $sendSlugFrom = $($this.data("slug"));

        //Key press event handler attached.
        $sendSlugFrom.keyup(function () {
            var slug = $sendSlugFrom.val();
            slug = slug.replace(/[^a-zA-Z0-9\s]/g, ""); //replace all special characters with empty string.
            slug = slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-"); //Replace all spaces with a -

            //If final character is a dash we trim it off.
            if (slug.charAt(slug.length - 1) === "-")
            {
                slug = slug.substr(0, slug.length - 1);
            }
                
            $this.val(slug); //Update value to the formatted slug.
        });
    });



});