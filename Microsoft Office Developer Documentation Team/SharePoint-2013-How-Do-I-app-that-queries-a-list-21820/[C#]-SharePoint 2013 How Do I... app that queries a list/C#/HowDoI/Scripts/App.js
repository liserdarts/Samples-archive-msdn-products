"use strict";
var WingtipToys = window.WingtipToys || {};

$(document).ready(function () {

    /* This routine checks to see if a list named "FAQ" exists
    /* in the app. If the list does not exist, then it is created
    /* and populated using a custom content type to define the
    /* schema.*/

    //Check to see if the list exists
    WingtipToys.Lists.read("FAQ")
    .then(function (list) {

        //List exists
        $("#appStatusDiv").hide();
        $("#mainDiv").show();

        //Show the list items
        WingtipToys.ViewModel.load("FAQ");
        ko.applyBindings(WingtipToys.ViewModel, document.getElementById("listTable"));


    },
    function (sender, args) {

        //List does not exist
        $("#appStatusDiv").show();
        $("#mainDiv").hide();

        $.when(

            //Add site columns to the app web
            WingtipToys.Lists.create_siteColumn("Question", "Text", "How Do I"),
            WingtipToys.Lists.create_siteColumn("Answer", "Text", "How Do I")

            )

       .done(function (x1, x2) {

           $("#step1Status").text("Done")

           //Create Content Type
           WingtipToys.Lists.create_contentType("HowDoI", "A content type for answering questions.", "0x01")

      .then(function (ctype) {

          $("#step2Status").text("Done")
          var id = ctype.get_id();

          //Add 'Question' column to content type
          WingtipToys.Lists.add_fieldLink(id, "Question")

     .then(function (fieldLink) {

         //Add 'Answer' column to content type
         WingtipToys.Lists.add_fieldLink(id, "Answer")

      .then(function (fieldLink) {

          $("#step3Status").text("Done");

          //Create a list named 'FAQ'
          WingtipToys.Lists.create("FAQ", "A list of frequently-asked questions", SP.ListTemplateType.genericList)

      .then(function (list) {

          $("#step4Status").text("Done");

          //Bind 'HowDoI' Content Type to 'FAQ' list
          WingtipToys.Lists.bind_ContentType(list.get_title(), ctype.get_id())

      .then(function (ctype) {

          $("#step5Status").text("Done");

          //Fill 'FAQ' List from XML file
          $.ajax({
              type: "GET",
              url: "../Content/Questions.xml",
              dataType: "xml",
              success: function (xml) {
                  $(xml).find("FAQ").each(function () {
                      var title = $(this).find("Title").text();
                      var question = $(this).find("Question").text();
                      var answer = $(this).find("Answer").text();
                      WingtipToys.ListItems.create(list.get_title(), title, question, answer);
                  });
                  $("#step6Status").text("Done");
                  $("#appStatusDiv").hide();
                  $("#mainDiv").show();
                  WingtipToys.ViewModel.load("FAQ");
                  ko.applyBindings(WingtipToys.ViewModel, document.getElementById("listTable"));
              },
              error: function (err) {
                  $("#step6Status").text("Error loading list.");
              }
          });

      },
          function (sender, args) { $("#step5Status").text(args.get_message()); });
      },
          function (sender, args) { $("#step4Status").text(args.get_message()); });
      },
          function (sender, args) { $("#step3Status").text(args.get_message()) });
     },
          function (sender, args) { $("#step3Status").text(args.get_message()) });
      },
          function (sender, args) { $("#step2Status").text(args.get_message()); });
       });
    });
});

