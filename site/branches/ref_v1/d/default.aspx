<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="d_default"
    MasterPageFile="~/Main.master" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content runat="server" ContentPlaceHolderID="title">
    Create your future and connect with others to make it happen
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="content">
    <div class="three-col">
        <div>
            Sedogo directory
        </div>
        <div>
            Browse by last name<br />
            <table border="0" cellpadding="2" cellspacing="4">
                <tr>
                    <td>
                        <asp:HyperLink ID="letterALink" runat="server" NavigateUrl="/d/?L=A" Text="A" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterBLink" runat="server" NavigateUrl="/d/?L=B" Text="B" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterCLink" runat="server" NavigateUrl="/d/?L=C" Text="C" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterDLink" runat="server" NavigateUrl="/d/?L=D" Text="D" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterELink" runat="server" NavigateUrl="/d/?L=E" Text="E" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterFLink" runat="server" NavigateUrl="/d/?L=F" Text="F" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterGLink" runat="server" NavigateUrl="/d/?L=G" Text="G" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterHLink" runat="server" NavigateUrl="/d/?L=H" Text="H" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterILink" runat="server" NavigateUrl="/d/?L=I" Text="I" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterJLink" runat="server" NavigateUrl="/d/?L=J" Text="J" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterKLink" runat="server" NavigateUrl="/d/?L=K" Text="K" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterLLink" runat="server" NavigateUrl="/d/?L=L" Text="L" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterMLink" runat="server" NavigateUrl="/d/?L=M" Text="M" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterNLink" runat="server" NavigateUrl="/d/?L=N" Text="N" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterOLink" runat="server" NavigateUrl="/d/?L=O" Text="O" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterPLink" runat="server" NavigateUrl="/d/?L=P" Text="P" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterQLink" runat="server" NavigateUrl="/d/?L=Q" Text="Q" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterRLink" runat="server" NavigateUrl="/d/?L=R" Text="R" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterSLink" runat="server" NavigateUrl="/d/?L=S" Text="S" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterTLink" runat="server" NavigateUrl="/d/?L=T" Text="T" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterULink" runat="server" NavigateUrl="/d/?L=U" Text="U" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterVLink" runat="server" NavigateUrl="/d/?L=V" Text="V" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterWLink" runat="server" NavigateUrl="/d/?L=W" Text="W" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterXLink" runat="server" NavigateUrl="/d/?L=X" Text="X" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterYLink" runat="server" NavigateUrl="/d/?L=Y" Text="Y" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterZLink" runat="server" NavigateUrl="/d/?L=Z" Text="Z" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="nameByLastNameLetterDiv" runat="server" style="padding-top: 10px">
            <asp:PlaceHolder ID="peopleNamePlaceHolder" runat="server" />
            <div id="noUsersWithThisLastNameLetterDiv" runat="server">
                <p>
                    There are no users with this letter</p>
            </div>
        </div>
    </div>
</asp:Content>
