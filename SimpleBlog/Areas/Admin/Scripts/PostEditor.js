$(document).ready(function () {

    var $tagEditor = $(".post-tag-editor");

    $tagEditor
        .find(".tag-select")
        //Attaches a click event on every list item.
        .on("click", "> li > a", function(e) {
            e.preventDefault();  //Ignore the href.

            var $this = $(this);
            var $tagParent = $this.closest("li");
            $tagParent.toggleClass("selected");  //Toggles the selected class on the parent li.
            
            var selected = $tagParent.hasClass("selected");
            $tagParent.find(".selected-input").val(selected); //Updates the hidden form field that dtermines if the tag is selected.
        });


    //ADD TAGS
    var $addTagButton = $tagEditor.find(".add-tag-button");
    var $newTagName = $tagEditor.find(".new-tag-name");

    $addTagButton.click(function (e) {
        console.log("add");
        e.preventDefault();
        addTag($newTagName.val());
    });

    $newTagName
        .keyup(function () {
            if($newTagName.val().trim().length > 0)
                $addTagButton.prop("disabled", false);
            else 
                $addTagButton.prop("disabled", true);
        })
        .keydown(function (e) {
            if (e.which != 13)
                return;

            e.preventDefault();
            addTag($newTagName.val());
        });

    function addTag(name) {
        var newIndex = $tagEditor.find(".tag-select > li").length-1;

        $tagEditor
            .find(".tag-select > li.template")
            .clone()
            .removeClass("template")
            .addClass("selected")
            .find(".name").text(name).end()
            .find(".name-input").val(name).attr("name", "Tags[" + newIndex + "].Name").end()
            .find(".selected-input").attr("name", "Tags[" + newIndex + "].IsChecked").val(true).end()
            .appendTo($tagEditor.find(".tag-select"));

        $newTagName.val("");
        $addTagButton.prop("disabled", true);
    }

});