$(".logLink").click(function () {
    $("#t").dialog('close');
    $("#UnAuthUserErr").dialog('close');
    JavascriptFunction();
});
  /*User Account Update Code*/
 
$(function () {

    $(".kk").click(function () {

        var fn = $("#Fname").val();
        var ln = $("#Lname").val();
        var e = $("#EmailId").val();
        var op = $("#OldPassword").val();
        var np = $("#NewPassword").val();
        var cnp = $("#ConfirmNewPassword").val();
        var email = document.getElementById('EmailId');
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (fn != "" && ln != "" && e != "") {

            if (filter.test(email.value)) {

                JavascriptFunction();

            }

        }


        if (fn != "" && ln != "" && e != "" && op != "" && np != "" && cnp != "") {


            if (filter.test(email.value)) {

                JavascriptFunction();

            }

        }

    });

    $(document).ready(function () {
        $("#logDialog").dialog({
            show: { effect: "blind", direction: "up" },
            opacity: 175,
            modal: true,


            width: 430, draggable: false, resizable: false, title: 'Login Failed Pannel', buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#catReq").dialog({
            show: { effect: "blind", direction: "up" },
            opacity: 175,
            modal: true,
            width: 430, draggable: false, resizable: false, title: 'Error Dialog', buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#acUpDialog").dialog({
            title: "Account Update Pannel"
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
    });



});


	/*Post Anwer for Answered Page*/
	

  $(function() {
      $(".comlink").css('background', 'none');
  });

  $(function () {
      $(".comlink").css('background', 'none');


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
              title: "Answer Post Pannel"
              ,
              width: 450,
              height: 250,
              position: [370, 90],
              show: { effect: 'drop', direction: "up" },buttons:{"Close":function() {
                  $(this).dialog('close');
              }}

          });
      }
      if (r == 4) {
          $(".ansBox").empty();

      }

   


      $("#mesPostButton").click(function () {
          var ansBox = $("#msgBox").val();


          if (ansBox != "") {
              JavascriptFunction();
          }

      });
      $(".comlink").click(function () {

          JavascriptFunction();

      });

});

/*Comment pag Code*/
 
$(function () {
  
      $("#t").hide();
      var c = $("#check").val();
      if (c == 1) {
          $("#backButton2").hide();
      } else {
          $("#backButton1").hide();
      }
     // $(".backLink").css('color', 'white');


      var r = $("#gm").val();
      if (r == 5 || r == 6) {
          $("#t").dialog({
              title: "Answer Post Warning Dialog "
              ,
              width: 450,
              height: 220,
              position: [370, 90],
              show: { effect: 'drop', direction: "up" }

          });
      }

});

/*Unans Question Code*/


$("#mesPostButton").click(function () {
    var msbox = $("#msgBox").val();
    if (msbox != "") {

        JavascriptFunction();

    }



});
function ClearMsgBox() {
    $("#msgBox").empty();
}
$("#op").ready(function () {
    ClearMsgBox();
});
$(function () {
    $("#ansPng").hide();
});


$(function () {

    var r = $("#gm").val();
    if (r == 3) {
        $("#d").dialog({
            title: 'Answer Post Pannel',
            width: 450,
            height: 220,
            position: [370, 90],
            show: { effect: 'drop', direction: "up" }


            , buttons: {
                "Close": function () {
                    $(this).dialog('close');
                }
            }

        });
    }
    $('.redLogLink').click(function () {

        JavascriptFunction();
    });


    $(".butClass").hover(function () {
        $(".butClass").css('background', 'burlywood');
    });
    $(".butClass").mouseleave(function () {
        $(".butClass").css('background', 'white');
    });
    $(".butClass").click(function () {
        $(".butClass").css('background', 'dodgerblue');
    });
});


/*Layout Page Code*/

        function JavascriptFunction() {
            var url = '@Url.Action("Index", "Home")';
            
            $('#divLoading').modal({ modal: false });

            $.post(url, null,
                    function (data) {
                        $("#PID")[0].innerHTML = data;
                        $("#divLoading").hide();
                    });
        }

 


        $("#Hlink").click(function () {
            JavascriptFunction();
        });
        $("#link").click(function () {
            JavascriptFunction();
        });

        $(".username").click(function () {
            JavascriptFunction();
            
        });

  $(function() {
      $(".username").css('color','white')
       
  });
        $("#reglink").click(function () {
            JavascriptFunction();
        });
        $("#uansLi").click(function () {
            JavascriptFunction();
        });
        $("#ansLi").click(function () {
            JavascriptFunction();
        });

        $('.backLink').click(function () {
            JavascriptFunction();
        });
        function ClearMsgBox() {

            $(".txt").empty();
        }
        $("#op").ready(function () {
            ClearMsgBox();
        });

 

/*Unanswered Question*/

        $(".lnkEdit").click(function () {
            JavascriptFunction();
        });
 
/*Home Index Code*/

        $("#searchButton").click(function () {
            var checkSearchBox = $("#serbox").val();
            if (checkSearchBox == "") {

            } else {
                JavascriptFunction();
            }
        });


        $(".ViewLnk").click(function () {
            JavascriptFunction();

        });
        $("#pagLink").click(function () {
            JavascriptFunction();
        });
        $(".h").click(function () {
            JavascriptFunction();
        });
    

        $(function () {

           
            $("#searchDialog").dialog({
                title: "<p id='dtitl'>Search  Error</p>",

                resizable: false,
                width: 530,
                position: [370, 220],
                show: { effect: 'drop', direction: "up", duration: 100 },
                modal: true,
                draggable: false,

                buttons: {
                    "OK": function () {
                        $(this).dialog("close");
                    }
                }
            });

           
            $("#aUn").css('color', 'fuchsia');
            $(".h").css('color', 'white').css('font-size', '24px');

            $(".h").hover(function () {
                $(".h").css('color', 'gold').css('font-size', '24px');
            });
            $(".h").mouseleave(function () {
                $(".h").css('color', 'white').css('font-size', '24px');
            });
            $("#searchButton").css('color', 'black');
            $("#logOffButton").css('color', 'black');
            $("#logOffButton").hover(function() {
                 $("#logOffButton").css('color', 'white').css('background','burlywood');
            });
            $("#logOffButton").mouseleave(function () { $("#logOffButton").css('color', 'black').css('background','white'); });
            $("#searchButton").hover(function () {

                $("#searchButton").css('background', '#7f977f');
            });
            $("#searchButton").mouseleave(function () {

                $("#searchButton").css('background', 'white');
            });
            $("#searchButton").click(function () {

                $("#searchButton").css('background', 'black');
            });
        });



/*Question Post Page*/
$(function () {

    //var c = $("#logC").val();
    //if (c == null) {

    //    $("#d").dialog({
    //        title: '<p id="dtitl">Login Warning.</p>',
    //        width: 450,
    //        height: 220,
    //        position: [370, 90],
    //        show: { effect: 'drop', direction: "up" }


    //    });

    //}

        var erroec = $("#authChech").val();

        if (erroec == 402) {




            $("#quesPostButton").click(function () {



                $("#UnAuthUserErr").dialog({
                    title: '<p id="dtit">Login Warning</p>',
                    width: 450,
                    height: 250,
                    position: [370, 90],
                    draggable: true,
                    show: { effect: 'drop', direction: "up" },
                    buttons: {
                        "Close": function () {
                            $(this).dialog('close');
                        }
                    }
                }
                );

            });

        } else {

            $(function () {
                $("form").kendoValidator();
            });


        }




    $("#quesPostButton").hover(function () {
        $("#quesPostButton").css('background', '#FEF5CA').css('color', 'black');
    });
    $("#quesPostButton").mouseleave(function () {
        $("#quesPostButton").css('background', '#33b5e5').css('color', 'white');
    });
    if (c == 4) {
        $(".emptyQuestionBox").empty();
    }



});


        $("#quesPostButton").click(function () {
            $("#ScrollPositionX").val($(document).scrollTop());
        });

        $("#ScrollPositionX").each(function () {
            var val = parseInt($(this).val(), 0);
            if (!isNaN(val))
                $(document).scrollTop(val);
        });


        function ClearMsgBox() {

            $("#msgBox").empty();

        }
        // $("#op").ready(function () {
        if ($("#logError").val() == 4) { ClearMsgBox(); }

        // });

        if ($("#logError").val() == 402) {
            $("#quesPostButton").attr('disabled', true);
        }

        if ($("#logError").val() == 402) {

            $("#UnAuthUserErr").dialog({
                title: '<p id="dtit">Login Warning</p>',
                width: 450,
                height: 250,
                position: [370, 90],
                draggable: true,
                show: { effect: 'drop', direction: "up" },
                buttons: {
                    "Close": function () {
                        $(this).dialog('close');
                    }
                }
            }
            );
        }


        $(function () {




         





            $("#quesPostButton").hover(function () {
                $("#quesPostButton").css('background', '#FEF5CA').css('color', 'black');
            });
            $("#quesPostButton").mouseleave(function () {
                $("#quesPostButton").css('background', '#33b5e5').css('color', 'white');
            });
            if (c == 4) {
                $(".emptyQuestionBox").empty();
            }



        });

