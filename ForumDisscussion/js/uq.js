       //$("#mesPostButton").click(function (){
       // var msbox = $("#msgBox").val();
       // if (msbox != "") {
       
       //     JavascriptFunction();
          
       // }

       

       // });
       // function ClearMsgBox() {
       //     $("#msgBox").empty();
       // }
       // $("#op").ready(function () {
       //     ClearMsgBox();
       // });
       // $(function() {
       //     $("#ansPng").hide();
       // } );


       // $(function() {

       //     var r = $("#gm").val();
       //     if (r == 3) {
       //         $("#d").dialog({
       //             title: '<p id="dtitl">Answer Post Pannel</p>',
       //             width: 450,
       //             height: 220,
       //             position: [370, 90],
       //             show: { effect: 'drop', direction: "up" }

       //         });
       //     }
       //     $('.redLogLink').click(function () {

       //         JavascriptFunction();
       //     });
// });
$(document).ready(function () {
    $(".hi").hide();
    var url = "";

    if ('@TempData["msg"]' != "") {
        $("#dialog-alert").dialog('open');
    }

    $("#dialog-edit").dialog({
        title: 'Create User',
        autoOpen: false,
        resizable: false,
        width: 400,
        position: [370, 20],
        show: { effect: 'drop', direction: "up" },
        modal: true,
        draggable: false,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide();
            $(this).load(url);
        }
    });

    $("#dialog-confirm").dialog({
        autoOpen: false,
        title: ' Delete Pannel',
        resizable: false,
        height: 210,
        width: 350,
        position: [370, 120],
        show: { effect: 'drop', direction: "up" },
        modal: true,
        draggable: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide();

        },
        buttons: {
            "OK": function () {
                $(this).dialog("close");
                window.location.href = url;
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $("#dialog-detail").dialog({
        title: 'Customer Details',
        autoOpen: false,
        resizable: false,
        width: 500,
        position: [370, 20],
        show: { effect: 'drop', direction: "up" },
        modal: true,
        draggable: false,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide();
            $(this).load(url);
        },
        buttons: {
            "Close": function () {
                $(this).dialog("close");
            }
        }
    });



    $(".lnkEdit").live("click", function (e) {
        // e.preventDefault(); use this or return false
        url = $(this).attr('href');
        $(".ui-dialog-title").html("Update Customer");
        $("#dialog-edit").dialog('open');

        return false;
    });

    $(".lnkDelete").live("click", function (e) {
        // e.preventDefault(); use this or return false

        url = $(this).attr('href');
        $("#dialog-confirm").dialog('open');

        return false;
    });

    $(".lnkDetail").live("click", function (e) {
        // e.preventDefault(); use this or return false
        url = $(this).attr('href');
        $("#dialog-detail").dialog('open');

        return false;
    });

    $("#btnClose").live("click", function (e) {
        $("#dialog-edit").dialog("close");
        return false;
    });
});