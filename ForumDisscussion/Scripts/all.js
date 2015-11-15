

/*User Account Update  Code*/

$(function () {
    $(".btn-info").css('color', 'white').css('font-size', '17px').css('height', 'auto');


    $("#logDialog").dialog({
        show: { effect: "blind", direction: "up" },
        opacity: 175,
        modal: true,


        width: 430, draggable: false, resizable: false, title: "<p id='dtitl'>Account Update Error</p>", buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });

    $("#catReq").dialog({
        show: { effect: "blind", direction: "up" },
        opacity: 175,
        modal: true,
        width: 430, draggable: false, resizable: false, title: "<p id='dtitl'>Error Dialog</p>", buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });


    $("#acUpDialog").dialog({
        title: "<p id='dtitl'>Account Update Pannel</p>"
    ,
        position: [370, 220],
        show: { effect: 'drop', direction: "up", duration: 100 },
        opacity: 175,
        modal: true,
        width: 450,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        }
    });

      //$(".kk").click(function () {

      //    var fn = $("#Fname").val();
      //    var ln = $("#Lname").val();
      //    var e = $("#EmailId").val();
      //    var op = $("#OldPassword").val();
      //    var np = $("#NewPassword").val();
      //    var cnp = $("#ConfirmNewPassword").val();
      //    var email=document.getElementById('EmailId');
      //    var filter=/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

      //    if (fn != "" && ln != "" && e != "" ) {
             
      //      if (filter.test(email.value)) {
               
      //          JavascriptFunction();

      //      }
                
      //    }



      //    if (fn!= "" && ln!= "" && e!= "" && op!= "" && np!= "" && cnp!= "") {
          
               
      //            if (filter.test(email.value)) {

      //                JavascriptFunction();

      //            }
            
      //    }

      //  });

    /*Login   code*/

      $("#loginFDialog").dialog({
          show: { effect: "blind", direction: "up" },
          opacity: 175,
          modal: true,


          width: 430, draggable: false, resizable: false, title: "<p id='dtitl'>Login Failed Pannel</p>", buttons: {
              "OK": function () {
                  $(this).dialog("close");
              }
          }
      });

      $(".btn-primary").click(function () {
          JavascriptFunction();

      });

      $("#loginButton").click(function () {
          var na = $("#UserName").val();
          var pa = $("#Password").val();

          if (na != "" && pa != "") {
              JavascriptFunction();
          }
      });


      $(function () {
          $(".btn").css('color', 'white');
      });

    /*Home Post Answer Code*/

      $(".btn-info").css('color', 'white');

      var op = $("#operationId").val();
      var c = $("#checkCrontroller").val();
      if (op == 1) {
          $(".bakToHom").hide();
      }
      if (c == 1) {
          $(".comlink").hide();
      }
      if (c == 2) {
          $(".areaLink").hide();
      }
      if (op == 2) {
          $(".backToArQusPag").hide();
      }


      var url = "";
      $("#dialog-edit").dialog({
          title: 'Post A Comment',
          autoOpen: false,
          resizable: false,
          width: 600,

          position: [250, 150],
          show: { effect: 'drop', direction: "up" },
          modal: true,
          draggable: false,
          open: function (event, ui) {
              $(".ui-dialog-titlebar-close").hide();
              $(this).load(url);
          }
      });

      $(".btn-inverse").live("click", function (e) {
          url = $(this).attr('href');
          $("#dialog-edit").dialog('open');

          return false;
      });
      $("#btnCancel").live("click", function (e) {
          $("#dialog-edit").dialog("close");
          return false;
      });

      var r = $("#gm").val();
      if (r == 5 || r == 6 || r == 1) {
          $("#t").dialog({
              title: "<p id='dtitl'>Answer Post Pannel</p>"
              ,
              width: 450,
              height: 220,
              position: [370, 90],
              show: { effect: 'drop', direction: "up" }

          });
      }
      if (r == 4) {
          $(".ansBox").empty();

      }

      $(".redLogLink").click(function () {
          JavascriptFunction();
      });


      $("#mesPostButton").click(function () {
          var ansBox = $("#msgBox").val();


          if (ansBox != "") {
              JavascriptFunction();
          }

      });
      $(".comlink").click(function () {

          JavascriptFunction();

      });

    /*Commrny Code*/

      $("#t").hide();
      var c = $("#check").val();
      if (c == 1) {
          $("#backButton2").hide();
      } else {
          $("#backButton1").hide();
      }
      $(".backLink").css('color', 'white');


      var r = $("#gm").val();
      if (r == 5 || r == 6) {
          $("#t").dialog({
              title: "<span id='dtitl'>" + "Comment Pannel" + "</span>"
              ,
              width: 450,
              height: 220,
              position: [370, 90],
              show: { effect: 'drop', direction: "up" }

          });
      }
      $(".redLogLink").click(function () {
          JavascriptFunction();
      });

});







