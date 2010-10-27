<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback"
    MasterPageFile="Main.master" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="meta">
    <meta http-equiv="pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="title">
    Feedback : Sedogo : Create your future and connect with others to make it happen
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="content">
    <div class="three-col">
        <h1>
            Feedback</h1>
        <p>
            Comments? Suggestions? We'd love to hear them.<br />
            &nbsp;</p>
        <fieldset>
            <ol>
                <li>
                    <label for="">
                        Your email address</label>
                    <asp:Label ID="yourEmailAddressLabel" runat="server" />
                </li>
                <li>
                    <asp:TextBox ID="emailAddressTextBox" runat="server" Width="250" MaxLength="200" />
                    <asp:RequiredFieldValidator ID="emailAddressTextBoxValidator" runat="server" ControlToValidate="emailAddressTextBox"
                        ErrorMessage="Your email address is required" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <br />
                </li>
                <li>
                    <label for="">
                        Feedback</label>
                </li>
                <li>
                    <asp:TextBox ID="feedbackTextBox" runat="server" TextMode="MultiLine" Width="250"
                        Rows="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="feedbackTextBoxValidator" runat="server" ControlToValidate="feedbackTextBox"
                        ErrorMessage="Some feedback text is required" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </li>
            </ol>
        </fieldset>
        <div class="buttonsfullpage">
            <asp:LinkButton ID="sendFeedbackButton" runat="server" Text="Send to Sedogo team"
                OnClick="sendFeedbackButton_click" CssClass="button-lrg"></asp:LinkButton>
        </div>
    </div>
</asp:Content>
