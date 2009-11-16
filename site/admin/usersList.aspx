<%@ Page Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="usersList.aspx.cs" Inherits="admin_usersList" Theme="AdminTheme" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="GridContainer">
    <div class="GridContainerHeader"><span>Users list</span></div>

    <ComponentArt:Grid
        id="usersListGrid"
        EnableViewState="true"
        RunningMode="Client" 
        CssClass="Grid" 
        ShowHeader="false"
        FooterCssClass="GridFooter" 
        PageSize="23" 
        PagerStyle="Slider" 
        PagerTextCssClass="GridFooterText"
        PagerButtonWidth="41"
        PagerButtonHeight="22"
        SliderHeight="15"
        SliderWidth="150" 
        SliderGripWidth="9" 
        SliderPopupOffsetX="100"
        SliderPopupClientTemplateId="SliderTemplate" 
        ImagesBaseUrl="../images/gridImages/"
        PagerImagesFolderUrl="../images/gridImages/pager/"
        LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
        LoadingPanelPosition="MiddleCenter"
        AutoPostBackOnSelect="true"
        Width="900"
        Height="500"
        EmptyGridText="&nbsp;<br/>There are no users<br/>&nbsp;"
        runat="server" 
        OnSelectCommand="onRowSelect">
        <Levels>
            <ComponentArt:GridLevel
                AllowGrouping="false"
                DataKeyField="UserID"
                ShowTableHeading="false" 
                TableHeadingCssClass="GridHeader"
                RowCssClass="Row" 
                HoverRowCssClass="HoverRow"
                AlternatingRowCssClass="AlternatingRow"
                ColumnReorderIndicatorImageUrl="reorder.gif"
                DataCellCssClass="DataCell" 
                HeadingCellCssClass="HeadingCell" 
                HeadingCellHoverCssClass="HeadingCellHover" 
                HeadingCellActiveCssClass="HeadingCellActive" 
                HeadingRowCssClass="HeadingRow" 
                HeadingTextCssClass="HeadingCellText"
                SelectedRowCssClass="SelectedRow"
                SortedDataCellCssClass="SortedDataCell" 
                SortAscendingImageUrl="asc.gif" 
                SortDescendingImageUrl="desc.gif" 
                SortImageWidth="10" 
                SortImageHeight="19" 
                >
                <Columns>
                    <ComponentArt:GridColumn DataField="EmailAddress" HeadingText="Email" />
                    <ComponentArt:GridColumn DataField="FirstName" HeadingText="First Name" />
                    <ComponentArt:GridColumn DataField="LastName" HeadingText="Last Name" />
                    <ComponentArt:GridColumn DataField="LoginEnabled" HeadingText="Enabled" />
                    <ComponentArt:GridColumn DataField="FailedLoginCount" HeadingText="Failed Logins" />
                    <ComponentArt:GridColumn DataField="LastLoginDate" HeadingText="Last Login Date" />
                    <ComponentArt:GridColumn DataField="CreatedDate" HeadingText="Created Date" />
                        
                    <ComponentArt:GridColumn DataField="UserID" Visible="false" />
                </Columns>
            </ComponentArt:GridLevel>
        </Levels>

        <ClientTemplates>
            <ComponentArt:ClientTemplate Id="LoadingFeedbackTemplate">
                <table height="378" width="990"><tr><td valign="center" align="center">
                <table cellspacing="0" cellpadding="0" border="0">
                <tr>
                <td style="font:bold 12px Arial,Helvetica,sans-serif;">Loading...&nbsp;</td>
                <td><img src="../images/gridImages/spinner.gif" width="16" height="16" border="0"></td>
                </tr>
                </table>
                </td></tr></table>
            </ComponentArt:ClientTemplate>

            <ComponentArt:ClientTemplate Id="SliderTemplate">
                <table class="SliderPopup" width="200" style="background-color:#ffffff" cellspacing="0" cellpadding="0" border="0">
                <tr>
                  <td style="padding:10px;" valign="center" align="center">
                    Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## usersListGrid.PageCount ##</b>
                  </td>
                </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ServerTemplates>
        <ComponentArt:GridServerTemplate ID="RoleTemplate">
            <Template>
            </Template>
        </ComponentArt:GridServerTemplate>
        </ServerTemplates>
    </ComponentArt:Grid>

    </div>
    
    <div class="ButtonsContainer">
    
        <div class="FooterButton">
            <asp:HyperLink ID="addUserLink" NavigateUrl="~/admin/addUser.aspx" runat="server" 
                Text="Create user" />
        </div>
        <div class="FooterButton">
            <asp:HyperLink ID="adminMenuLink" NavigateUrl="~/admin/main.aspx" runat="server" 
                Text="Back to admin menu" />
        </div>
    
    </div>

</asp:Content>

