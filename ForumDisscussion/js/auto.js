  $(function () {

        $("#serbox").autocomplete({
            source: '@Url.Action("GetCategoryList")', minLength: 2
        });

    });



 
