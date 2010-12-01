<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bannerAddFindControl.ascx.cs" 
    Inherits="components_bannerAddFindControl" %>

<div class="three-col">

    <script language="JavaScript" type="text/javascript">
    function loginRedirect(eventID)
    {
        window.location = "/login.aspx?EID=" + eventID;
    }
    function doLogin()
    {
        var form = document.forms[0];
        window.location = "/login.aspx?Redirect=AddEvent&Details=" + form.bannerAddFindControl_what.value;
    }
    function doAddEvent()
    {
        var form = document.forms[0];
        openModal("/addEvent.aspx?Name=" + form.bannerAddFindControl_what.value);
    }
    function checkAddButtonEnter(e)
    {
        var characterCode;
        if (e && e.which) // NN4 specific code
        {
            e = e;
            characterCode = e.which;
        }
        else
        {
            e = event;
            characterCode = e.keyCode; // IE specific code
        }
        if (characterCode == 13) //// Enter key is 13
        {
            e.returnValue = false;
            e.cancelBubble = true;
            <asp:Literal id="keyPressActionScript" runat="server" />
            return false;
        }
        else
        {
        }
    }
    function clearWhat()
    {
        var form = document.forms[0];
        form.bannerAddFindControl_what.value = "";
    }
    function clearWhat2()
    {
        var form = document.forms[0];
        form.bannerAddFindControl_what2.value = "";
    }
    </script>

    <table border="0" cellspacing="10" cellpadding="0" width="100%" class="add-find">
        <tr>
            <td><h3 class="blue"><asp:HyperLink ID="addLink" runat="server" Text="Add goal" /></h3>
                <p class="blue">to my timeline</p>
                <asp:Panel ID="Panel1" DefaultButton="" runat="server">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td valign="top"><asp:TextBox ID="what" runat="server" 
                                MaxLength="1000" Text="name your goal" /></td>
                            <td valign="top" style="padding-top:4px"><asp:HyperLink runat="server"
                                ID="addButtonLink"><asp:Image ID="addButtonImage" runat="server" 
                                ImageUrl="~/images/addButton.png" /></asp:HyperLink>
                                </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td><h3 class="blue"><asp:LinkButton ID="findButton" runat="server" Text="Find people" OnClick="searchButton2_click" /></h3>
                <p class="blue">with my goals</p>
                <asp:Panel ID="Panel2" DefaultButton="searchButton2" runat="server">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="top"><asp:TextBox ID="what2" runat="server" MaxLength="1000" 
                                    ValidationGroup="what2Group" Text="search goals or people" /></td>
                                <td valign="top" style="padding-top:4px"><asp:ImageButton 
                                    ID="searchButton2" runat="server" OnClick="searchButton2_click" 
                                    ImageUrl="~/images/searchButton.png" /></td>
                            </tr>
                        </table>
                        <asp:RegularExpressionValidator 
                            ID="what2Validator" runat="server" 
                            ErrorMessage="Goal name must have at least 2 characters" 
                            ControlToValidate="what2" ValidationGroup="what2Group" 
                            ValidationExpression="[\S\s]{2,200}" />
                </asp:Panel>
            </td>
        </tr>    
    </table>
</div>
