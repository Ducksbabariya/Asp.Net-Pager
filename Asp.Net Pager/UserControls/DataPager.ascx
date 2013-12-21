<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPager.ascx.cs" Inherits="TechTracking.UserControls.DataPager" %>
<div class="paging">
    <table cellpadding="0" cellspacing="0" width="100%" border="0" class="PagingContainer">
        <tr class="GridFooter" style="height: 25px;" runat="server" >
            <td align="left" style="width: 25%" class="PagingShowingRecord" runat="server" visible="false">
                <asp:Label runat="server" ID="lblTotalRecord">Showing 00 of 00 record(s)</asp:Label>
                <asp:Label runat="server" ID="lblTotalPage" Visible="false">Showing 00 of 00 record(s)</asp:Label>
            </td>
            <td align="center" style="width: 50%">
                <table cellpadding="0" cellspacing="0" border="0" class="PagingNavigation">
                    <tr>
                        <td>
                            <ul>
                                <li>
                                    <asp:LinkButton runat="server" ToolTip="Move First" ID="btnMoveFirst" Text="« First"
                                        CommandArgument="First" OnCommand="btnMove_Click" />
                                </li>
                                <li>
                                   
                                    <asp:LinkButton Text="« Prev" runat="server" ToolTip="Move Previous" ID="btnMovePrevious"
                                        CommandArgument="Previous" OnCommand="btnMove_Click" />
                                </li>
                                <li style="padding-left:6px">
                                    <asp:Repeater ID="Rptpager" runat="server" OnItemCommand="Rptpager_ItemCommand" OnItemDataBound="Rptpager_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandArgument='<%# Eval("intPageNo") %>' Text='<%# Eval("intPageNo") %>'
                                                CommandName="Paging" ID="link" runat="server"></asp:LinkButton>
                                            &nbsp;
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ToolTip="Move Next" ID="btnMoveNext" Text="Next »"
                                        CommandArgument="Next" OnCommand="btnMove_Click" />
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ToolTip="Move Last" ID="btnMoveLast" Text="Last »"
                                        CommandArgument="Last" OnCommand="btnMove_Click" />
                                </li>
                            </ul>
                        </td>
                    </tr>
                    <table runat="server" visible="false" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox runat="server" AutoPostBack="true" CssClass="CurrentPage" ID="txtPage"
                                    OnTextChanged="txtPage_TextChanged" Width="40" Height="12"></asp:TextBox>
                            </td>
                            <td>
                              
                            </td>
                        </tr>
                    </table>
                </table>
            </td>
            <td align="right" style="width: 25%" runat="server" visible="false" >
                <table cellpadding="0" cellspacing="0" class="PagingRecords">
                    <tr>
                        <td>
                            Display records
                        </td>
                        <td>
                            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlRecords" OnSelectedIndexChanged="ddlRecords_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
