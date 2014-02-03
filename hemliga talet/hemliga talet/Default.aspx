<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="hemliga_talet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <title>Gissa det hemliga talet</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Gissa det hemliga talet</h1>
        <%-- Felmeddelande --%>
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett fel inträffade. Korrigera felet och gör ett nytt försök" CssClass="error" />
        </div>

        <%-- Skriv in tal --%>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label">Gissa på ett tal mellan 1 och 100</asp:Label>
        </div>

        <div>
            <asp:TextBox ID="Input" runat="server" autofocus="autofocus"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ett tal måste anges" ControlToValidate="Input"  CssClass="error" Text="*" Display="Dynamic" />
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Ange ett tal mellan 1-100" MaximumValue="100" MinimumValue="1" ControlToValidate="Input" Display="Dynamic"  CssClass="error" ClientIDMode="AutoID" Type="Integer" Text="*" />
        </div>
        <%-- Skicka talet --%>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>

        <asp:PlaceHolder ID="ResultPlaceHolder" runat="server" Visible="false">
            <asp:Label ID="HelpTextLable" runat="server" Text=""></asp:Label>
            <asp:Label ID="PrevGuessLabel" runat="server" Text=""></asp:Label>

        </asp:PlaceHolder>


    </div>
    </form>
</body>
</html>
