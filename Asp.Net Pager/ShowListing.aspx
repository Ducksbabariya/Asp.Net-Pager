<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowListing.aspx.cs" Inherits="TechTracking.Admin.ShowListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="../App_Themes/Default/css/style.css" />
    <script type="text/javascript">

        function ConfirmDelete(e) {
            var data = "Are you sure want to delete?";
            $.msgbox(data, { type: "confirm" });
        }
    </script>
    <script type="text/javascript">


        function MakeBidPopup(src, title, height, width) {

            $("#ifPopup").load(function () {
                $("#loading").hide();
            });
            $("#loading").show();
            $("#ifPopup").attr("src", src);
            $("#<%=lblPopupTitle.ClientID %>").html(title);
            $("#boxpopup").dialog({
                modal: true,
                height: height,
                width: width,
                closeText: '',
                resizable: false,
                open: function () {
                    $("div.bounceOutDown").removeClass('bounceOutDown');
                },
                beforeClose: function (e, ui) {
                    var browser = $.browser;
                    if (browser.msie && browser.version == "9.0")
                        $(this).hide('slow');
                    else
                        $(this).addClass('bounceOutDown');
                }
            });
            $("#boxpopup").dialog('open');
            var top = ($(window).height() - height) / 2;
            var left = ($(window).width() - width) / 2;
            $('#boxpopup').css({
                position: 'fixed',
                top: top,
                left: left,
                height: height,
                width: width,
                zIndex: 10002
            });
            $('#ifPopup').css({
                height: height - 70,
                width: width - 20
            });
            $('.ui-dialog').css({
                display: 'block',
                position: 'fixed',
                top: 0,
                left: 0,
                height: $(window).height(),
                width: $(window).width(),
                zIndex: 9999
            });
            $(".close-btn").click(function () {
                $("#boxpopup").dialog('close');
                return false;
            });
            $(".ui-dialog-titlebar").hide();
            return false;
        }

        function CloseMakeBid() {
            $("#boxpopup").dialog('close');
        }

        function closePopup() {
            $("#boxpopup").dialog('close');
            $("#ifPopup").attr("src", "");
            window.location.href = window.location.href;
            return false;
        };
    </script>
    <style type="text/css">
        .ui-widget-overlay
        {
            position: fixed !important;
            top: 0 !important;
            left: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="boxpopup" class="backside-box" style="background: white">
        <div class="hd">
            <h2>
                <asp:Label ID="lblPopupTitle" Text="" runat="server" />
            </h2>
            <a href="javascript:void(0);" class="close-btn">
                <img src="../App_Themes/Default/images/ic_close.png" alt="" /></a></div>
        <div class="box-cont-dash">
            <p>
                <div id="loading" align="center">
                    <img src="../App_Themes/Default/images/ajax-loader.gif" />
                </div>
                <iframe id="ifPopup" src="" frameborder="0" height="230" width="280"></iframe>
            </p>
        </div>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="shadow-box large">
                <div class="box-hd">
                    <h2>
                        Shows</h2>
                </div>
                <div class="filter-panel">
                    <div class="search-box">
                        <asp:TextBox runat="server" ID="txtSearch" CssClass="search-input" MaxLength="50" />
                    </div>
                    <div class="search-box" style="width: 100px">
                        <asp:DropDownList runat="server" ID="ddlSortBy" Style="float: left;
                            padding: 5px;" > <%--AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged"--%>
                            <%--<asp:ListItem Text="Sort By" Value="StartDate" />--%>
                            <asp:ListItem Text="Title" Value="Title" />
                            <asp:ListItem Text="Client Name" Value="ClientName" />
                        </asp:DropDownList>
                    </div>
                    <asp:Button Text="Search" ID="btnSearch" OnClick="btnSearch_Click" CssClass="go-btn"
                        ValidationGroup="vldListing" runat="server" />
                    <div class="filter-right">
                        <a href="AddEditShow.aspx" class="view-btn"><span class="add">Add</span></a>
                    </div>
                </div>
                <uc:MessageBox runat="server" ID="ucMessageBox" />
                <div class="box-cont-full">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="table-layout: fixed;">
                        <asp:Repeater ID="rptShowList" runat="server" OnItemCommand="rptShowList_ItemCommand"
                            OnItemDataBound="rptShowList_OnItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td width="172" align="left" valign="top" class="location-img">
                                        <%-- <asp:LinkButton Text="" PostBackUrl="javascript:void(0)" ID="lnkStatusImage" runat="server">--%><%--</asp:LinkButton>--%>
                                        <img id="imgLogo" runat="server" src='<%# "~/Photo/"+ Eval("LogoPath") %>' alt=""
                                            width="172" height="152" onerror='(this.src = "../App_Themes/Default/images/location.jpg")'
                                            style="cursor: pointer" />
                                    </td>
                                    <td width="130" align="left" valign="top" style="max-width: 130px; width: 130px">
                                        <h4 style="max-width: 130px">
                                            <ExtendedLabel:ExtendedLabel ID="lblTitle" runat="server" MaxLength="20" Text='<%# Eval("Title")%>'>
                                            </ExtendedLabel:ExtendedLabel>
                                        </h4>
                                        <p style="max-height: 52px; max-width: 140px; overflow: hidden; text-overflow: ellipsis;">
                                            <ExtendedLabel:ExtendedLabel ID="lblDescription" runat="server" MaxLength="50" Text='<%# Eval("Description")%>'>
                                            </ExtendedLabel:ExtendedLabel>
                                        </p>
                                        <h4 style="max-width: 130px">
                                            Positions Needs:
                                        </h4>
                                        <p class="position">
                                            <asp:LinkButton CssClass="orange-txt" Text='<%# Eval("Positions")%>' ID="lnkMore"
                                                runat="server" Style="cursor: text" />
                                            <%--<ExtendedLabel:ExtendedLabel ID="lblPositions" runat="server" MaxLength="50" Text='<%# Eval("Positions")%>'></ExtendedLabel:ExtendedLabel>--%>
                                            <%--<asp:Label Text='<%# Eval("Positions")%>' ID="lblPositions" runat="server" />
                                            <asp:LinkButton CssClass="orange-txt" Text="..." ID="lnkMore" Visible="false" runat="server" />--%>
                                        </p>
                                    </td>
                                    <td width="190" align="left" valign="top">
                                        <uc:Calender runat="server" ID="ucCalender" ShowNextPrevMonth="false" EnableViewState="true"
                                            DayStyleHeight="18" CellPadding="3" CellSpacing="0" />
                                    </td>
                                    <td width="140" align="left" valign="top">
                                        <h4>
                                            General Info:</h4>
                                        <p>
                                            <span class="orange-txt">
                                                <%# Eval("ShowDates")%></span> show days<br />
                                            <span class="orange-txt">
                                                <%# Eval("ShowDays")%></span> show days
                                            <br>
                                            <span class="orange-txt">
                                                <%# Eval("TravelDays")%></span> travel days
                                        </p>
                                    </td>
                                    <td width="80" align="left" valign="top">
                                        <h4>
                                            Bids Info:</h4>
                                        <p>
                                            <span class="orange-txt">
                                                <%# Eval("BidCount")%></span> Bids<br />
                                            <span class="orange-txt">
                                                <%# Eval("PositionCount")%></span> Positions</p>
                                    </td>
                                    <td width="75" align="left" valign="top">
                                        <table width="100%" border="0" cellspacing="4" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <%-- <a runat="server" href='<%# "AddEditShow.aspx?"+ base.SetQueryString("ShowID",Convert.ToString(Eval("PKShowID"))) %>'
                                                        class="detail-btn">Edit</a>--%>
                                                    <asp:HiddenField ID="hdbBidStartDate" runat="server" Value='<%# Eval("BidStartDate")%>' />
                                                    <asp:LinkButton Text="Edit" CssClass="detail-btn" ID="lnkEdit" CommandName="Edit"
                                                        CommandArgument='<%# Eval("PKShowID") %>' runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton Text="View" CssClass="detail-btn" ID="lnkView" CommandName="View"
                                                        CommandArgument='<%# Eval("PKShowID") %>' runat="server" />
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    <a href="javascript:void(0)" class="detail-btn">Expand</a>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton Text="Delete" CssClass="detail-btn" ID="lnkDelete" CommandName="Delete"
                                                        CommandArgument='<%# Eval("PKShowID") %>' runat="server" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <% if (rptShowList.Items.Count == 0)
                                   {%>
                                <tr>
                                    <td align="center" class="orange-txt" width="100%" style="text-align: center !important;">
                                        No records found
                                    </td>
                                </tr>
                                <%}%>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <uc:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
