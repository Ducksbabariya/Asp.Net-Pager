<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="References.ascx.cs"
    Inherits="TechTracking.UserControls.References" %>
<table border="0" cellpadding="2" cellspacing="3">
    <tr>
        <td>
            <asp:Label Text="First Name" ID="lblFirstName" runat="server" />
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtFirstName" />
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                Text="*" Visible="true" CssClass="Dntsaw"  ErrorMessage="*" ForeColor="Red"
                Display="Static" ValidationGroup="vldReference"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label Text="Last Name" ID="lblLastName" runat="server" />
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtLastName" />
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                Text="*" Visible="true"  CssClass="Dntsaw"  ErrorMessage="*" ForeColor="Red"
                Display="Static" ValidationGroup="vldReference"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label Text="Email" ID="lblEmail" runat="server" />
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtEmail" />
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                Text="*" Visible="true"  CssClass="Dntsaw"  ErrorMessage="*" ForeColor="Red"
                Display="Static" ValidationGroup="vldReference"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                Display="Static" ErrorMessage="Not valid" ValidationGroup="vldReference" ValidationExpression="^([A-Za-z0-9_\-\.\'])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,6})$" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label Text="Phone Number" ID="lblPhoneNumber" runat="server" />
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtPhoneNumber" MaxLength="15" />
        </td>
        <td>
            <asp:HiddenField ID="hdnPKTechnicianReferenceID" runat="server" />
            <asp:HiddenField ID="hdnFKTechnicianID" runat="server" />
        </td>
    </tr>
</table>
