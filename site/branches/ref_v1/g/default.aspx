<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="g_default"
    MasterPageFile="~/Main.master" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="title">
    Create your future and connect with others to make it happen
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="content">
    <div class="three-col">
        <div>
            Sedogo directory
        </div>
        <div>
            Browse by goal name<br />
            <table border="0" cellpadding="2" cellspacing="4">
                <tr>
                    <td>
                        <asp:HyperLink ID="letterALink" runat="server" NavigateUrl="/g/?L=A" Text="A" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterBLink" runat="server" NavigateUrl="/g/?L=B" Text="B" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterCLink" runat="server" NavigateUrl="/g/?L=C" Text="C" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterDLink" runat="server" NavigateUrl="/g/?L=D" Text="D" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterELink" runat="server" NavigateUrl="/g/?L=E" Text="E" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterFLink" runat="server" NavigateUrl="/g/?L=F" Text="F" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterGLink" runat="server" NavigateUrl="/g/?L=G" Text="G" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterHLink" runat="server" NavigateUrl="/g/?L=H" Text="H" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterILink" runat="server" NavigateUrl="/g/?L=I" Text="I" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterJLink" runat="server" NavigateUrl="/g/?L=J" Text="J" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterKLink" runat="server" NavigateUrl="/g/?L=K" Text="K" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterLLink" runat="server" NavigateUrl="/g/?L=L" Text="L" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterMLink" runat="server" NavigateUrl="/g/?L=M" Text="M" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterNLink" runat="server" NavigateUrl="/g/?L=N" Text="N" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterOLink" runat="server" NavigateUrl="/g/?L=O" Text="O" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterPLink" runat="server" NavigateUrl="/g/?L=P" Text="P" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterQLink" runat="server" NavigateUrl="/g/?L=Q" Text="Q" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterRLink" runat="server" NavigateUrl="/g/?L=R" Text="R" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterSLink" runat="server" NavigateUrl="/g/?L=S" Text="S" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterTLink" runat="server" NavigateUrl="/g/?L=T" Text="T" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterULink" runat="server" NavigateUrl="/g/?L=U" Text="U" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterVLink" runat="server" NavigateUrl="/g/?L=V" Text="V" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterWLink" runat="server" NavigateUrl="/g/?L=W" Text="W" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterXLink" runat="server" NavigateUrl="/g/?L=X" Text="X" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterYLink" runat="server" NavigateUrl="/g/?L=Y" Text="Y" />
                    </td>
                    <td>
                        <asp:HyperLink ID="letterZLink" runat="server" NavigateUrl="/g/?L=Z" Text="Z" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="goalByLetterDiv" runat="server">
            <asp:PlaceHolder ID="goalPlaceHolder" runat="server" />
            <div id="noGoalsWithThisLetterDiv" runat="server">
                <p>
                    There are no goals with this letter</p>
            </div>
        </div>
    </div>
</asp:Content>
