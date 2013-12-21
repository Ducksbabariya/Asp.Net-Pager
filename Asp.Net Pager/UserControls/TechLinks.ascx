<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TechLinks.ascx.cs" Inherits="TechTracking.UserControls.TechLinks" %>
<asp:Panel runat="server" CssClass="pnlTechLinks">

    <asp:LinkButton Text="TimeSheets" ID="lnkTimeSheets" runat="server" CssClass="TechLinks" />
    <asp:LinkButton Text="Discussion" ID="lnkDiscussion" runat="server" CssClass="TechLinks" />

    <asp:LinkButton Text="Documents" ID="lnkDocuments" runat="server" CssClass="TechLinks" />

    <asp:LinkButton Text="Shows" ID="lnkShows" CssClass="TechLinks" runat="server" />

    <asp:LinkButton Text="Profile" ID="lnkProfile" CssClass="TechLinks" runat="server" />
</asp:Panel>

