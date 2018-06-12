<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IntegratewithYammerWeb.Pages.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to integrate with Yammer from a provider hosted SharePoint application</title>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>
    <script src="https://assets.yammer.com/platform/yam.js"></script>
    <script src="/Scripts/Yammer.js"></script>
    <script type="text/javascript">
        var hostweburl;

        // Load the SharePoint resources.
        $(document).ready(function () {

            // Get the URI decoded app web URL.
            hostweburl = decodeURIComponent(getQueryStringParameter("SPHostUrl"));

            // The SharePoint js files URL are in the form:
            // web_url/_layouts/15/resource.js
            var scriptbase = hostweburl + "/_layouts/15/";

            // Load the js file and continue to the 
            //   success handler.
            $.getScript(scriptbase + "SP.UI.Controls.js", renderChrome);

            $("#btnConnect").click(function () {
                if ($.trim($("#yammerAppClientId").val()).length > 0 && $.trim($("#yammerAppGroupId").val()).length > 0) {
                    initYammerApp($.trim($("#yammerAppClientId").val()), $.trim($("#yammerAppGroupId").val()));
                }
                else {
                    alert("Please enter correct Client ID and Group ID");
                }
            });
        });

        // Function to prepare the options and render the control.
        function renderChrome() {

            // The Help, Account, and Contact pages receive the 
            // same query string parameters as the main page.
            var options = {
                "appTitle": "How to integrate with Yammer from a provider hosted SharePoint application"
            };

            var nav = new SP.UI.Controls.Navigation("SharePointChromeControl", options);
            nav.setVisible(true);
        }

        // Function to retrieve a query string value.
        // For production purposes you may want to use
        // a library to handle the query string.
        function getQueryStringParameter(paramToRetrieve) {
            var params = document.URL.split("?").length > 1 ?
                document.URL.split("?")[1].split("&") : [];
            var strParams = "";
            for (var i = 0; i < params.length; i = i + 1) {
                var singleParam = params[i].split("=");
                if (singleParam[0] == paramToRetrieve)
                    return singleParam[1];
            }
        }
    </script>
    <link href="/Styles/Style.css" rel="stylesheet" />
</head>
<body style="overflow:auto">
    <div id="SharePointChromeControl"></div>
    <div class="yammerapp-main">
        <p>
            <label>Client ID:</label>
            <input id="yammerAppClientId" type="text" />
            <label>Feed ID:</label>
            <input id="yammerAppGroupId" type="text" />
            <button id="btnConnect">Connect</button>
        </p>
        <!-- The chrome control also makes the SharePoint
          Website stylesheet available to your page -->
        <div class="yammer-app">
            <div id="YammerClientForm" style="display: none">
                <h3>Ask a question:</h3>
                <div>
                    <textarea id="txtQuestion"></textarea>
                </div>
                <div style="padding-top: 9px; margin-left: 501px">
                    <button id="btnPost">Submit</button>
                </div>
            </div>
            <div style="padding-left: 15px" class="ms-clear"></div>
            <br />
            <div style="left: 15px; right: 0px;" id="ms-feeddiv">
                <div id="ms-viewDescription" class="ms-microfeed-viewDescription ms-hide"></div>
                <div id="ms-feedthreadsdiv" class="ms-microfeed-threadsDiv">
                    <div id="YammerClientFeeds"></div>
                </div>
                <div class="ms-clear"></div>
            </div>
        </div>

        <!-- template -->
        <script id="threadTemplate" type="text/x-jquery-tmpl">
            <div class="ms-microfeed-thread">
                <div class="ms-microfeed-rootDiv">
                    <div class="ms-microfeed-message">
                        <div class="ms-microfeed-indentRootRef">
                            <div class="ms-microfeed-userThumbnailArea ms-microfeed-userThumbnailAreaRootPadding">
                                <div class="ms-table ms-core-tableNoSpace">
                                    <div class="ms-tableRow">
                                        <div class="ms-tableCell ms-verticalAlignTop">
                                            <div class="ms-peopleux-userImgDiv">
                                                <span class="ms-imnSpan">
                                                    <span style="width: 48px; height: 48px;" class="ms-peopleux-userImgWrapper">{{if source == "Y"}}
                                                    {{else}}     
                                                    {{/if}}
                                                    </span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ms-microfeed-userThumbnailArea ms-microfeed-userThumbnailAreaRootPadding">
                                <div class="ms-table ms-core-tableNoSpace">
                                    <div class="ms-tableRow">
                                        <div class="ms-tableCell ms-verticalAlignTop">
                                            <div class="ms-peopleux-userImgDiv">
                                                <span class="ms-imnSpan">
                                                    <a class="ms-subtleLink ms-listlink ms-peopleux-imgUserLink" href="${userUrl}">
                                                        <span style="width: 48px; height: 48px;" class="ms-peopleux-userImgWrapper">
                                                            <img style="cliptop: 0px; clipright: 48px; clipbottom: 48px; clipleft: 0px; min-height: 48px; min-width: 48px; max-width: 48px;" class="ms-peopleux-userImg" alt="${fullName}" src="${avatarUrl}">
                                                        </span>
                                                    </a>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ms-microfeed-rootBody">
                                <div class="ms-microfeed-text ms-microfeed-rootText">
                                    <a class="ms-microfeed-userName ms-textLarge ms-subtleLink" href="${userUrl}">${fullName}
                                    </a>
                                    <br>
                                    <span class="ms-textSmall">${message}
                                    </span>
                                </div>
                                <div class="ms-microfeed-messageFooter">
                                    <div class="ms-microfeed-likesIndicatorText ms-metadata ms-link ms-bold {{if likeTxt.length == 0}} ms-hide {{/if}}" id="likehdr_${messageId}">
                                        <span>
                                            <span id="likespan_${messageId}">${likeTxt}</span>
                                        </span>
                                    </div>
                                    <span class="ms-metadata">
                                        <span class="ms-microfeed-postedTime">${postTime} from ${clientType}
                                        </span>
                                    </span>
                                    <span class="ms-microfeed-linkSeparator ms-textSmall"></span>
                                    <span class="ms-microfeed-linkSeparator ms-textSmall"></span>
                                    <a class="ms-microfeed-button ms-textSmall ms-secondaryCommandLink ms-microfeed-footerButton" onclick="postLike('${messageId}');" href="#">Like
                                    </a>
                                    <span class="ms-microfeed-linkSeparator ms-textSmall"></span>
                                    <a class="ms-microfeed-button ms-textSmall ms-secondaryCommandLink ms-microfeed-footerButton" onclick="showReply('msgid_${rmId}');" href="#">Reply
                                    </a>
                                </div>
                                <div id="msgid_${rmId}" class="ms-metadata ms-textsmall ms-hide">
                                    <input type="text" id="reply_${rmId}" />
                                    <button onclick="return postReply('msgid_${rmId}');">post reply</button>
                                </div>
                                <div class="ms-clear"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="ms-microfeed-replyArea ms-microfeed-repliesDiv" id="repliesdiv_${messageId}">
                    {{tmpl(replies) "#replyTemplate"}}
                </div>

                <div class="ms-clear"></div>
            </div>
        </script>
        <script id="replyTemplate" type="text/x-jquery-tmpl">
            <div class="ms-microfeed-message ms-microfeed-replyMessage">
                <div>
                    <div class="ms-microfeed-userThumbnailArea ms-microfeed-userThumbnailAreaReplyPadding">
                        <div class="ms-table ms-core-tableNoSpace">
                            <div class="ms-tableRow">
                                <div class="ms-tableCell ms-verticalAlignTop">
                                    <div class="ms-peopleux-userImgDiv">
                                        <span class="ms-imnSpan">
                                            <a class="ms-subtleLink ms-listlink ms-peopleux-imgUserLink" onclick="GoToLinkOrDialogNewWindow(this);return false;" href="${userUrl}">
                                                <span style="width: 48px; height: 36px;" class="ms-peopleux-userImgWrapper"></span>
                                            </a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="ms-microfeed-userThumbnailArea ms-microfeed-userThumbnailAreaReplyPadding">
                        <div class="ms-table ms-core-tableNoSpace">
                            <div class="ms-tableRow">
                                <div class="ms-tableCell ms-verticalAlignTop">
                                    <div class="ms-peopleux-userImgDiv">
                                        <span class="ms-imnSpan">
                                            <a class="ms-subtleLink ms-listlink ms-peopleux-imgUserLink" onclick="GoToLinkOrDialogNewWindow(this);return false;" href="${userUrl}">
                                                <span style="width: 36px; height: 36px;" class="ms-peopleux-userImgWrapper">
                                                    <img style="cliptop: 0px; clipright: 36px; clipbottom: 36px; clipleft: 0px; min-height: 36px; min-width: 36px; max-width: 36px;" class="ms-peopleux-userImg" alt="${fullName}" src="${avatarUrl}">
                                                </span>
                                            </a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="ms-microfeed-replyBody">
                        <div class="ms-floatRight">
                            <div>
                                <div style="width: 24px; height: 24px;" class="ms-microfeed-deleteDiv"></div>
                                <div class="ms-microfeed-deleteDiv ms-hidden" fixedwidth="24px" fixedheight="24px" hashover="false" hasfocus="false">
                                    <button class="ms-microfeed-button  ms-microfeed-deleteButton" title="Delete this reply" type="button">
                                        <img class="ms-microfeed-deleteImg" src="Images/CancelGlyph.16x16x32.png">
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="ms-microfeed-text ms-microfeed-replyText">
                            <a class="ms-microfeed-userName ms-textSmall ms-bold ms-subtleLink" href="${userUrl}">${fullName}
                            </a>&nbsp; 
                        <span class="ms-textSmall">${message}
                        </span>
                        </div>
                        <div class="ms-microfeed-messageFooter">
                            <span class="ms-metadata">
                                <span class="ms-microfeed-postedTime">${postTime} from ${clientType}
                                </span>
                            </span>
                            <span class="ms-microfeed-linkSeparator ms-textSmall ms-hide"></span>
                            <span class="ms-textSmall {{if likeTxt.length == 0}} ms-hide {{/if}}" id="likehdr_${messageId}">
                                <a class="ms-bold ms-subtleLink" href="#">
                                    <span>
                                        <span id="likespan_${messageId}">${likeTxt}</span>
                                    </span>
                                </a>
                            </span>
                            <span class="ms-microfeed-linkSeparator ms-textSmall"></span>
                            <a class="ms-microfeed-button ms-textSmall ms-secondaryCommandLink ms-microfeed-footerButton" onclick="postLike('${messageId}');" href="#">Like
                            </a>
                        </div>
                        <div class="ms-clear"></div>
                    </div>
                </div>
            </div>
        </script>
    </div>
</body>
</html>
