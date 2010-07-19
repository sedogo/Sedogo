<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<script type="text/javascript">
    $(document).ready(function () {
        $("#tabs").tabs();
    });
  </script>
  <style>
  .ui-tabs .ui-tabs-hide {
     display: none;
    }
  </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr><td>login</td><td><input type="text" id="login" /></td></tr>
        <tr><td>password</td><td><input type="text" id="pwd" /></td></tr>
    </table>
    
    <div id="tabs">
        <ul>
            <li><a href="#getUsersId"><span>GET /users/{id}</span></a></li>
            <li><a href="#postUsers"><span>POST /users</span></a></li>
            <li><a href="#fragment-3"><span>Three</span></a></li>
        </ul>
        <div id="getUsersId">
            <div>user ID: <input type="text" id="getUsersId_userID" /></div>
            <input type="button" value="Send" onclick="getUsersId();" />
            <div id="getUsersId_resultDiv"></div>
            <script type="text/javascript">
                function getUsersId() {
                    $('#getUsersId_resultDiv').html('');

                    $.ajax({
                        url: 'http://nikita/sedogo/api/users/' + $('#getUsersId_userID').val(),
                        dataType: "json",
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result.id) {
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
                <tr><td>first name:</td><td><input id="postUsers_firstName" type="text" /></td></tr>
                <tr><td>last name:</td><td><input id="postUsers_lastName" type="text" /></td></tr>
                <tr><td>email: </td><td><input id="postUsers_email" type="text" /></td></tr>
                <tr><td>password:</td><td><input id="postUsers_password" type="text" /></td></tr>
                <tr><td>gender:</td><td><input id="postUsers_gender" type="text" /></td></tr>
                <tr><td>home town:</td><td><input id="postUsers_homeTown" type="text" /></td></tr>
                <tr><td>birthday:</td><td><input id="postUsers_birthday" type="text" /></td></tr>
                <tr><td>country id:</td><td><input id="postUsers_country" type="text" /></td></tr>
                <tr><td>language id:</td><td><input id="postUsers_language" type="text" /></td></tr>
                <tr><td>timezone id:</td><td><input id="postUsers_timezone" type="text" /></td></tr>
                <tr><td>profile text:</td><td><input id="postUsers_profile" type="text" /></td></tr>
                <tr><td>image:</td><td><input id="postUsers_image" type="text" /></td></tr>
                <tr><td>imageThumbnail:</td><td><input id="postUsers_imageThumbnail" type="text" /></td></tr>
                <tr><td>imagePreview:</td><td><input id="postUsers_imagePreview" type="text" /></td></tr>
                <tr><td></td><td><input type="button" value="Add" onclick="postUsers()" /></td></tr>
            </table>
            <div id="postUsers_resultDiv">
            
            </div>
            <script type="text/javascript">
                function postUsers() {

                    $('#postUsers_resultDiv').html('');

                    var userDetails = 
                    {
                        email : $('#postUsers_email').val(),
                        password : $('#postUsers_password').val(),
                        /*if($('#postUsers_firstName').val().length>0) */firstName : $('#postUsers_firstName').val(),
                        /*if($('#postUsers_lastName').val().length>0)*/ lastName : $('#postUsers_lastName').val(),
                        gender : $('#postUsers_gender').val(),
                        /*if($('#postUsers_homeTown').val().length>0)*/ homeTown : $('#postUsers_homeTown').val(),
                        /*if($('#postUsers_birthday').val().length>0)*/ birthday : $('#postUsers_birthday').val(),
                        country : $('#postUsers_country').val(),
                        language : $('#postUsers_language').val(),
                        timezone : $('#postUsers_timezone').val(),
                        /*if($('#postUsers_profile').val().length>0)*/ profile : $('#postUsers_profile').val(),
                        /*if($('#postUsers_image').val().length>0)*/ image : $('#postUsers_image').val(),
                        /*if($('#postUsers_imageThumbnail').val().length>0) */imageThumbnail : $('#postUsers_imageThumbnail').val(),
                        /*if($('#postUsers_imagePreview').val().length>0) */imagePreview: $('#postUsers_imagePreview').val()
                    }


                    $.ajax({
                        url: 'http://nikita/sedogo/api/users/',
                        dataType: "json",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(userDetails),
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader("Authorization", "Basic " + $.base64Encode($('#login').val() + ":" + $('#pwd').val()))
                        },
                        success: function (result) {
                            if (result.id) {
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
        <div id="fragment-3">
            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
            Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
        </div>
    </div>

    


</asp:Content>
