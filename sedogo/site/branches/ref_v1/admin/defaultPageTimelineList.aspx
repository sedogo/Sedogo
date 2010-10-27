<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" 
    CodeFile="defaultPageTimelineList.aspx.cs" Inherits="admin_defaultPageTimelineList" Theme="AdminTheme" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="GridContainer">
    <div class="GridContainerHeader"><span>Goals list</span></div>

    <ComponentArt:Grid
        id="goalsListGrid"
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
        EmptyGridText="&nbsp;<br/>There are no goals<br/>&nbsp;"
        runat="server" 
        OnSelectCommand="onRowSelect">
        <Levels>
            <ComponentArt:GridLevel
                AllowGrouping="false"
                DataKeyField="EventID"
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
                    <ComponentArt:GridColumn DataField="EventName" HeadingText="Name" />
                    <ComponentArt:GridColumn DataField="FirstName" HeadingText="First name" />
                    <ComponentArt:GridColumn DataField="LastName" HeadingText="Last name" />
                    <ComponentArt:GridColumn DataField="EmailAddress" HeadingText="Email" />
                    <ComponentArt:GridColumn DataField="ShowOnDefaultPage" HeadingText="ShowOnDefaultPage" />
                        
                    <ComponentArt:GridColumn DataCellServerTemplateId="ShowOnDefaultPageTemplate"
                        HeadingText="Show on default" AllowEditing="false" AllowSorting="False" Width="80" />
                    <ComponentArt:GridColumn DataCellServerTemplateId="PrivateTemplate"
                        HeadingText="Private" AllowEditing="false" AllowSorting="False" Width="50" />
                    <ComponentArt:GridColumn DataCellServerTemplateId="EventPicThumbnailTemplate"
                        HeadingText="Thumbnail" AllowEditing="false" AllowSorting="False" Width="70" />
                        
                    <ComponentArt:GridColumn DataField="EventID" Visible="false" />
                    <ComponentArt:GridColumn DataField="PrivateEvent" Visible="false" />
                    <ComponentArt:GridColumn DataField="ShowOnDefaultPage" Visible="false" />
                    <ComponentArt:GridColumn DataField="EventPicThumbnail" Visible="false" />
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
                    Page <b>## DataItem.PageIndex + 1 ##</b> of <b>## goalsListGrid.PageCount ##</b>
                  </td>
                </tr>
                </table>
            </ComponentArt:ClientTemplate>
        </ClientTemplates>
        <ServerTemplates>
            <ComponentArt:GridServerTemplate ID="PrivateTemplate">
                <Template>
                </Template>
            </ComponentArt:GridServerTemplate>
            <ComponentArt:GridServerTemplate ID="ShowOnDefaultPageTemplate">
                <Template>
                </Template>
            </ComponentArt:GridServerTemplate>
            <ComponentArt:GridServerTemplate ID="EventPicThumbnailTemplate">
                <Template>
                </Template>
            </ComponentArt:GridServerTemplate>
        </ServerTemplates>
    </ComponentArt:Grid>

    </div>
    
    <div class="ButtonsContainer">
    
        <div class="FooterButton">
            <asp:HyperLink ID="adminMenuLink" NavigateUrl="~/admin/main.aspx" runat="server" 
                Text="Back to admin menu" />
        </div>
    
    </div>

</asp:Content>

