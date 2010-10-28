<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='<%= ResolveUrl("~/Scripts/jquery-1.4.2.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Scripts/jquery.base64.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Scripts/jquery-ui.min.js") %>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Scripts/json2.js") %>'></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs();

            $("#tabsCollection").tabs();

            $('#tabsSearch').tabs();
        });
    </script>
    <style type="text/css">
        .ui-tabs .ui-tabs-hide
        {
            display: none;
        }
    </style>
</head>
<body>
    <table>
        <tr>
            <td>
                login
            </td>
            <td>
                <input type="text" id="login" />
            </td>
        </tr>
        <tr>
            <td>
                password
            </td>
            <td>
                <input type="text" id="pwd" />
            </td>
        </tr>
    </table>
    <h2>
        Item methods</h2>
    <div id="tabs">
        <ul>
            <li><a href="#getUsersId"><span>GET /users/{id}</span></a></li>
            <li><a href="#postUsers"><span>POST /users</span></a></li>
            <li><a href="#getEventsId"><span>GET /events/{id}</span></a></li>
            <li><a href="#postInvite"><span>POST /invites</span></a></li>
            <li><a href="#updateMessage"><span>PUT /messages/{id}</span></a></li>
            <li><a href="#createEventDiv"><span>POST /events</span></a></li>
            <li><a href="#createFollow"><span>POST /users/{id}/followed</span></a></li>
        </ul>
        <div id="getUsersId">
            <div>
                user ID:
                <input type="text" id="getUsersId_userID" /></div>
            <input type="button" value="Send" onclick="getUsersId();" />
            <div id="getUsersId_resultDiv">
            </div>
            <script type="text/javascript">
                function getUsersId() {
                    $('#getUsersId_resultDiv').html('');

                    $.ajax({
                        url: '<%=Url.Action("Users", "API") %>' + $('#getUsersId_userID').val(),
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.id) {
                                var s = '';
                                for (var prop in result) {
                                    s += prop + ' = ' + result[prop] + '<br/>';
                                }
                                $('#getUsersId_resultDiv').html(s);
                            }
                            else
                                alert('not');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
    
            </script>
        </div>
        <div id="postUsers">
            <table>
                <tr>
                    <td>
                        first name:
                    </td>
                    <td>
                        <input id="postUsers_firstName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        last name:
                    </td>
                    <td>
                        <input id="postUsers_lastName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        email:
                    </td>
                    <td>
                        <input id="postUsers_email" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        password:
                    </td>
                    <td>
                        <input id="postUsers_password" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        gender:
                    </td>
                    <td>
                        <input id="postUsers_gender" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        home town:
                    </td>
                    <td>
                        <input id="postUsers_homeTown" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        birthday:
                    </td>
                    <td>
                        <input id="postUsers_birthday" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        country id:
                    </td>
                    <td>
                        <input id="postUsers_country" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        language id:
                    </td>
                    <td>
                        <input id="postUsers_language" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        timezone id:
                    </td>
                    <td>
                        <input id="postUsers_timezone" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        profile text:
                    </td>
                    <td>
                        <input id="postUsers_profile" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        image:
                    </td>
                    <td>
                        <input id="postUsers_image" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        imageThumbnail:
                    </td>
                    <td>
                        <input id="postUsers_imageThumbnail" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        imagePreview:
                    </td>
                    <td>
                        <input id="postUsers_imagePreview" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Add" onclick="postUsers()" />
                    </td>
                </tr>
            </table>
            <div id="postUsers_resultDiv">
            </div>
            <script type="text/javascript">
                function postUsers() {

                    $('#postUsers_resultDiv').html('');

                    var userDetails =
                    {
                        email: $('#postUsers_email').val(),
                        password: $('#postUsers_password').val(),
                        /*if($('#postUsers_firstName').val().length>0) */firstName: $('#postUsers_firstName').val(),
                        /*if($('#postUsers_lastName').val().length>0)*/lastName: $('#postUsers_lastName').val(),
                        gender: $('#postUsers_gender').val(),
                        /*if($('#postUsers_homeTown').val().length>0)*/homeTown: $('#postUsers_homeTown').val(),
                        /*if($('#postUsers_birthday').val().length>0)*/birthday: $('#postUsers_birthday').val(),
                        country: $('#postUsers_country').val(),
                        language: $('#postUsers_language').val(),
                        timezone: $('#postUsers_timezone').val(),
                        /*if($('#postUsers_profile').val().length>0)*/profile: $('#postUsers_profile').val(),
                        /*if($('#postUsers_image').val().length>0)*/image: $('#postUsers_image').val(),
                        /*if($('#postUsers_imageThumbnail').val().length>0) */imageThumbnail: $('#postUsers_imageThumbnail').val(),
                        /*if($('#postUsers_imagePreview').val().length>0) */imagePreview: $('#postUsers_imagePreview').val()
                    }


                    $.ajax({
                        url: '<%=Url.Action("Users", "API") %>',
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(userDetails),
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.id) {
                                var s = '';
                                for (var prop in result) {
                                    s += prop + ' = ' + result[prop] + '<br/>';
                                }
                                $('#postUsers_resultDiv').html(s);
                            }
                            else
                                alert('not');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
        <div id="postInvite">
            <table>
                <tr>
                    <td>
                        eventId:
                    </td>
                    <td>
                        <input id="postInvite_eventId" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        inviteAdditionalText:
                    </td>
                    <td>
                        <input id="postInvite_inviteAdditionalText" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        inviteEmailSent:
                    </td>
                    <td>
                        <input id="postInvite_inviteEmailSent" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        inviteEmailSentEmailAddress:
                    </td>
                    <td>
                        <input id="postInvite_inviteEmailSentEmailAddress" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        inviteEmailSentDate:
                    </td>
                    <td>
                        <input id="postInvite_inviteEmailSentDate" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Add" onclick="postInvite()" />
                    </td>
                </tr>
            </table>
            <div id="postInvite_resultDiv">
            </div>
            <script type="text/javascript">
                function postInvite() {

                    $('#postUsers_resultDiv').html('');

                    var userDetails =
                    {
                        eventId: $('#postInvite_eventId').val(),
                        inviteAdditionalText: $('#postInvite_inviteAdditionalText').val(),
                        inviteEmailSent: $('#postInvite_inviteEmailSent').val(),
                        inviteEmailSentEmailAddress: $('#postInvite_inviteEmailSentEmailAddress').val(),
                        inviteEmailSentDate: $('#postInvite_inviteEmailSentDate').val(),
                    }


                    $.ajax({
                        url: '<%=Url.Action("Invites", "API") %>',
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(userDetails),
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.id) {
                                var s = '';
                                for (var prop in result) {
                                    s += prop + ' = ' + result[prop] + '<br/>';
                                }
                                $('#postInvite_resultDiv').html(s);
                            }
                            else
                                alert('not :' + result );
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
        <div id="getEventsId">
            <table>
                <tr>
                    <td>
                        event id
                    </td>
                    <td>
                        <input type="text" id="getEventsId_eventId" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Send" onclick="getEventsId();" />
                    </td>
                </tr>
            </table>
            <div id="getEventsId_resultDiv">
            </div>
            <script type="text/javascript">
                function getEventsId() {
                    $('#getEventsId_resultDiv').html('');

                    $.ajax({
                        url: '<%=Url.Action("Events", "API") %>' + $('#getEventsId_eventId').val(),
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.id) {
                                var s = '';
                                for (var prop in result) {
                                    s += prop + ' = ' + result[prop] + '<br/>';
                                }
                                $('#getEventsId_resultDiv').html(s);
                            }
                            else
                                alert('not');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
    
            </script>
        </div>
        <div id="updateMessage">
            <input type="text" id="messageId"  />
            <input type="button" value="Mark as read" onclick="updateMessage()"/>
            <div id="messageResult">
            </div>
            <script type="text/javascript">
                function updateMessage() {

                    $('#messageResult').html('');

                    var data =
                    {
                        id: $('#messageId').val(),
                    }


                    $.ajax({
                        url: '<%=Url.Action("Messages", "API") %>',
                        dataType: "json",
                        type: "PUT",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(data),
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.id) {
                                var s = '';
                                for (var prop in result) {
                                    s += prop + ' = ' + result[prop] + '<br/>';
                                }
                                $('#messageResult').html(s);
                            }
                            else
                                alert('not :' + result );
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
        <div id="createEventDiv">
            <table>
                <tr>
                    <td>
                        id (userId int):
                    </td>
                    <td>
                        <input id="event_userId" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        created (datetime):
                    </td>
                    <td>
                        <input id="created" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        updated (datetime):
                    </td>
                    <td>
                        <input id="updated" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        name (string):
                    </td>
                    <td>
                        <input id="name" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        venue (string):
                    </td>
                    <td>
                        <input id="venue" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        description (string):
                    </td>
                    <td>
                        <input id="description" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        mustDo (bool):
                    </td>
                    <td>
                        <input id="mustDo" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        dateType (nchar(1)):
                    </td>
                    <td>
                        <input id="dateType" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        start (datetime):
                    </td>
                    <td>
                        <input id="start" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        rangeStart (datetime):
                    </td>
                    <td>
                        <input id="rangeStart" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        rangeEnd (datetime):
                    </td>
                    <td>
                        <input id="rangeEnd" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        beforeBirthday (int):
                    </td>
                    <td>
                        <input id="beforeBirthday" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        privateEvent (bool):
                    </td>
                    <td>
                        <input id="privateEvent" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        category (int):
                    </td>
                    <td>
                        <input id="category" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        createdFromEvent (int):
                    </td>
                    <td>
                        <input id="createdFromEvent" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        timeZone (int):
                    </td>
                    <td>
                        <input id="timeZone" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Add" onclick="createNewEvent()" />
                    </td>
                </tr>
            </table>
            <div id="createEventResult">
            </div>
            <script type="text/javascript">
                function createNewEvent() {

                    $('#createEventResult').html('');

                    var data =
                    {
                        user: $('#event_userId').val(),
                        created: $('#created').val(),
                        updated: $('#updated').val(),
                        name: $('#name').val(),
                        venue: $('#venue').val(),
                        description: $('#description').val(),
                        mustDo: $('#mustDo').val(),
                        dateType: $('#dateType').val(),
                        start: $('#start').val(),
                        rangeStart: $('#rangeStart').val(),
                        rangeEnd: $('#rangeEnd').val(),
                        beforeBirthday: $('#beforeBirthday').val(),
                        private: $('#privateEvent').val(),
                        category: $('#category').val(),
                        createdFromEvent: $('#createdFromEvent').val(),
                        timeZone: $('#timeZone').val()
                    }


                    $.ajax({
                        url: '<%=Url.Action("Events", "API") %>',
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(data),
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined') {
                                $('#createEventResult').html(JSON.stringify(result));
                            } else {
                                alert('not :' + result);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
        <div id="createFollow">
            <table>
                <tr>
                    <td>
                        userId:
                    </td>
                    <td>
                        <input id="userId" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        eventId:
                    </td>
                    <td>
                        <input id="eventId" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        showOnTimeline:
                    </td>
                    <td>
                        <input id="showOnTimeline" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        joinPending:
                    </td>
                    <td>
                        <input id="joinPending" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Add" onclick="createFollow()" />
                    </td>
                </tr>
            </table>
            <div id="createFollowResult">
            </div>
            <script type="text/javascript">
                function createFollow() {

                    $('#createFollowResult').html('');

                    var data =
                    {
                        eventId: $('#eventId').val(),
                        showOnTimeline: $('#showOnTimeline').val(),
                        joinPending: $('#joinPending').val()
                    }


                    $.ajax({
                        url: '<%=Url.Action("Users", "API") %>/' + $('#userId').val() + '/followed/',
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(data),
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined') {
                                $('#createFollowResult').html(JSON.stringify(result));
                            } else {
                                alert('not :' + result);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
    </div>
    <h2>
        Collection methods</h2>
    <div id="tabsCollection">
        <ul>
            <li><a href="#getUsersIdConsumption"><span>GET /users/{id}/consumption</span></a></li>
            <li><a href="#getEventsIdComments"><span>GET /events/{id}/comments</span></a></li>
            <li><a href="#getUsersIdEvents"><span>GET /users/{id}/events</span></a></li>
            <li><a href="#getUsersIdAchieved"><span>GET /users/{id}/achieved</span></a></li>
            <li><a href="#getUsersIdFollowed"><span>GET /users/{id}/followed</span></a></li>
            <li><a href="#postEventsIdComments"><span>POST /events/{id}/comments</span></a></li>
        </ul>
        <div id="getUsersIdConsumption">
            <table>
                <tr>
                    <td>
                        user id
                    </td>
                    <td>
                        <input type="text" id="getUsersIdConsumption_userId" />
                    </td>
                </tr>
                <tr>
                    <td>
                        start
                    </td>
                    <td>
                        <input type="text" id="cf_start" />
                    </td>
                </tr>
                <tr>
                    <td>
                        count
                    </td>
                    <td>
                        <input type="text" id="cf_count" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Send" onclick="getUsersIdConsumption();" />
                    </td>
                </tr>
            </table>
            <div id="getUsersIdConsumption_resultDiv">
            </div>
            <script type="text/javascript">
                function getUsersIdConsumption() {
                    $('#getUsersIdConsumption_resultDiv').html('');
                    var start = $('#cf_start').val();
                    var count = $('#cf_count').val();

                    var filter = '';
                    if (start != "" || count != "") {
                        filter = "?";
                        if (start != "") {
                            filter += "start=" + start;
                            filter += "&";
                        }
                        if (count != "") {
                            filter += "count=" + count;
                        }
                    }

                    $.ajax({
                        url: '<%=Url.Action("Users", "API")%>/' + $('#getUsersIdConsumption_userId').val() + '/consumption' + filter,
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var message = result[i];
                                    s += '<p>';
                                    for (var prop in message) {
                                        s += prop + ' = ' + message[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#getUsersIdConsumption_resultDiv').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
    
            </script>
        </div>
        <div id="getEventsIdComments">
            <table>
                <tr>
                    <td>
                        event id
                    </td>
                    <td>
                        <input type="text" id="getEventsIdComments_eventId" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="send" onclick="getEventsIdComments();" />
                    </td>
                </tr>
            </table>
            <div id="getEventsIdComments_resultDiv">
            </div>
            <script type="text/javascript">
                function getEventsIdComments() {
                    $('#getEventsIdComments_resultDiv').html('');

                    $.ajax({
                        url: '<%=Url.Action("Events", "API")%>/' + $('#getEventsIdComments_eventId').val() + '/comments/',
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var comment = result[i];
                                    s += '<p>';
                                    for (var prop in comment) {
                                        s += prop + ' = ' + comment[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#getEventsIdComments_resultDiv').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
        <div id="getUsersIdEvents">
            <table>
                <tr>
                    <td>
                        user id
                    </td>
                    <td>
                        <input type="text" id="getUsersIdEvents_userId" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="search" onclick="getUsersIdEvents();" />
                    </td>
                </tr>
            </table>
            <div id="getUsersIdEvents_result">
            </div>
            <script type="text/javascript">
                function getUsersIdEvents() {
                    $('#getUsersIdEvents_result').html('');

                    $.ajax({
                        url: '<%=Url.Action("Users", "API")%>/' + $('#getUsersIdEvents_userId').val() + '/events/',
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var event = result[i];
                                    s += '<p>';
                                    for (var prop in event) {
                                        s += prop + ' = ' + event[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#getUsersIdEvents_result').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }    
            </script>
        </div>
        <div id="getUsersIdAchieved">
            <table>
                <tr>
                    <td>
                        user id
                    </td>
                    <td>
                        <input type="text" id="getUsersIdAchieved_userId" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="search" onclick="getUsersIdAchieved();" />
                    </td>
                </tr>
            </table>
            <div id="getUsersIdAchieved_result">
            </div>
            <script type="text/javascript">
                function getUsersIdAchieved() {
                    $('#getUsersIdAchieved_result').html('');

                    $.ajax({
                        url: '<%=Url.Action("Users", "API")%>/' + $('#getUsersIdAchieved_userId').val() + '/achieved/',
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var event = result[i];
                                    s += '<p>';
                                    for (var prop in event) {
                                        s += prop + ' = ' + event[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#getUsersIdAchieved_result').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }    
            </script>
        </div>
        <div id="getUsersIdFollowed">
            <table>
                <tr>
                    <td>
                        user id
                    </td>
                    <td>
                        <input type="text" id="getUsersIdFollowed_userId" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="search" onclick="getUsersIdFollowed();" />
                    </td>
                </tr>
            </table>
            <div id="getUsersIdFollowed_result">
            </div>
            <script type="text/javascript">
                function getUsersIdFollowed() {
                    $('#getUsersIdFollowed_result').html('');

                    $.ajax({
                        url: '<%=Url.Action("Users", "API")%>/' + $('#getUsersIdFollowed_userId').val() + '/followed/',
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var event = result[i];
                                    s += '<p>';
                                    for (var prop in event) {
                                        s += prop + ' = ' + event[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#getUsersIdFollowed_result').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }    
            </script>
        </div>
        <div id="postEventsIdComments">
            <table>
                <tr>
                    <td>
                        event id:
                    </td>
                    <td>
                        <input id="postEventsIdComments_eventId" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        text:
                    </td>
                    <td>
                        <input id="postEventsIdComments_text" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        user id:
                    </td>
                    <td>
                        <input id="postEventsIdComments_user" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        image:
                    </td>
                    <td>
                        <input id="postEventsIdComments_image" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        imagePreview:
                    </td>
                    <td>
                        <input id="postEventsIdComments_imagePreview" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        video:
                    </td>
                    <td>
                        <input id="postEventsIdComments_video" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        video thumbnail:
                    </td>
                    <td>
                        <input id="postEventsIdComments_videoThumbnail" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        link:
                    </td>
                    <td>
                        <input id="postEventsIdComments_link" type="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Add" onclick="postEventsIdComments()" />
                    </td>
                </tr>
            </table>
            <div id="postEventsIdComments_resultDiv">
            </div>
            <script type="text/javascript">
                function postEventsIdComments() {

                    $('#postEventsIdComments_resultDiv').html('');

                    var comment =
                    {
                        text: $('#postEventsIdComments_text').val(),
                        user: $('#postEventsIdComments_user').val(),
                        image: $('#postEventsIdComments_image').val(),
                        imagePreview: $('#postEventsIdComments_imagePreview').val(),
                        video: $('#postEventsIdComments_video').val(),
                        videoThumbnail: $('#postEventsIdComments_videoThumbnail').val(),
                        link: $('#postEventsIdComments_link').val()
                    }


                    $.ajax({
                        url: 'http://nikita/sedogo/api/events/' + $("#postEventsIdComments_eventId").val() + '/comments',
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(comment),
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.id) {
                                var s = '';
                                for (var prop in result) {
                                    s += prop + ' = ' + result[prop] + '<br/>';
                                }
                                $('#postEventsIdComments_resultDiv').html(s);
                            }
                            else
                                alert('not');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
            </script>
        </div>
    </div>
    <h2>
        Search</h2>
    <div id="tabsSearch">
        <ul>
            <li><a href="#searchText"><span>Text</span></a></li>
            <li><a href="#searchLocation"><span>Location</span></a></li>
            <li><a href="#searchRandom"><span>Random</span></a></li>
        </ul>
        <div id="searchText">
            <table>
                <tr>
                    <td>
                        query:
                    </td>
                    <td>
                        <input type="text" id="searchText_query" />
                    </td>
                </tr>
                <tr>
                    <td>
                        start:
                    </td>
                    <td>
                        <input type="text" id="searchText_start" />
                    </td>
                </tr>
                <tr>
                    <td>
                        count:
                    </td>
                    <td>
                        <input type="text" id="searchText_count" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Search" onclick="searchText();" />
                    </td>
                </tr>
            </table>
            <div id="searchText_result">
            </div>
            <script type="text/javascript">
                function searchText() {
                    $('#searchText_result').html('');
                    var params = '';
                    params += 'type=text';
                    params += '&query=' + $('#searchText_query').val();
                    params += '&start=' + $('#searchText_start').val();
                    params += '&count=' + $('#searchText_count').val();
                    $.ajax({
                        url: 'http://nikita/sedogo/api/search?' + params,
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var event = result[i];
                                    s += '<p>';
                                    for (var prop in event) {
                                        s += prop + ' = ' + event[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#searchText_result').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
    
            </script>
        </div>
        <div id="searchLocation">
            <table>
                <tr>
                    <td>
                        query:
                    </td>
                    <td>
                        <input type="text" id="searchLocation_query" />
                    </td>
                </tr>
                <tr>
                    <td>
                        start:
                    </td>
                    <td>
                        <input type="text" id="searchLocation_start" />
                    </td>
                </tr>
                <tr>
                    <td>
                        count:
                    </td>
                    <td>
                        <input type="text" id="searchLocation_count" />
                    </td>
                </tr>
                <tr>
                    <td>
                        lat:
                    </td>
                    <td>
                        <input type="text" id="searchLocation_lat" />
                    </td>
                </tr>
                <tr>
                    <td>
                        lng:
                    </td>
                    <td>
                        <input type="text" id="searchLocation_lng" />
                    </td>
                </tr>
                <tr>
                    <td>
                        radius:
                    </td>
                    <td>
                        <input type="text" id="searchLocation_radius" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Search" onclick="searchLocation();" />
                    </td>
                </tr>
            </table>
            <div id="searchLocation_result">
            </div>
            <script type="text/javascript">
                function searchLocation() {
                    $('#searchLocation_result').html('');
                    var params = '';
                    params += 'type=location';
                    params += '&query=' + $('#searchLocation_query').val();
                    params += '&start=' + $('#searchLocation_start').val();
                    params += '&count=' + $('#searchLocation_count').val();
                    params += '&lat=' + $('#searchLocation_lat').val();
                    params += '&lng=' + $('#searchLocation_lng').val();
                    params += '&radius=' + $('#searchLocation_radius').val();
                    $.ajax({
                        url: 'http://nikita/sedogo/api/search?' + params,
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var event = result[i];
                                    s += '<p>';
                                    for (var prop in event) {
                                        s += prop + ' = ' + event[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#searchLocation_result').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
    
            </script>
        </div>
        <div id="searchRandom">
            <table>
                <tr>
                    <td>
                        count:
                    </td>
                    <td>
                        <input type="text" id="searchRandom_count" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="Search" onclick="searchRandom()" />
                    </td>
                </tr>
            </table>
            <div id="searchRandom_result">
            </div>
            <script type="text/javascript">
                function searchRandom() {
                    $('#searchRandom_result').html('');
                    var params = '';
                    params += 'type=random';
                    params += '&count=' + $('#searchRandom_count').val();
                    $.ajax({
                        url: 'http://nikita/sedogo/api/search?' + params,
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result != null && typeof (result) != 'undefined' && result.length > 0) {
                                var s = '';

                                for (var i = 0; i < result.length; i++) {
                                    var event = result[i];
                                    s += '<p>';
                                    for (var prop in event) {
                                        s += prop + ' = ' + event[prop] + '<br/>';
                                    }
                                    s += '</p>';

                                }
                                $('#searchRandom_result').html(s);
                            }
                            else
                                alert('nothing returned');
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var result = eval('(' + XMLHttpRequest.responseText + ')');
                            if (result.error) {
                                alert(result.error);
                            }
                        }
                    });
                }
    
            </script>
        </div>
    </div>
</body>
</html>
