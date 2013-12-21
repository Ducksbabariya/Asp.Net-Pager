<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true"
    CodeBehind="TechnicianListing.aspx.cs" Inherits="TechTracking.Admin.TechnicianListing"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .highLight
        {
            background: #3E78FD !important;
        }

            .highLight a
            {
                color: black !important;
            }

        .none
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
           // $("#loading").show();
            if ('<%=TechTracking.Classes.ProjectSession.LevelID %>' == 1) {
               // alert("admin");
                $("#aPending").removeClass("none");
            }
            if ('<%=TechTracking.Classes.ProjectSession.LevelID%>' == 3) {
               // alert('pm');
                $("#aPending").addClass("none");
            }
        }
        function manageMultiple() {
            $("#hdnSelected").attr('value', $('.chkTOdeleteTechs input:checked').length);
            if ($('.chkTOdeleteTechs input:checked').length > 0) {
                $('.disDLinks').attr('disabled', '');
                $('.disDLinks').attr("onclick", "javascript:return confirm('Are you sure to delete multiple technician?');");
                $('.disDLinks').html('Delete Now').closest('th').attr('class', 'highLight');
            }
            else {
                $('.disDLinks').attr('disabled', 'disabled');
                $('.disDLinks').attr('onclick', 'return false;');
                $('.disDLinks').html('  Delete  ').closest('th').attr('class', '');

            }
            if ($('.chkTOshareTechs input:checked').length > 0) {
                $('.disSLinks').attr('disabled', '');
                $('.disSLinks').attr('onclick', '');
                $('.disSLinks').html('Share Now').closest('th').attr('class', 'highLight');

                //$("#hdnShared").attr('value', $('.chkTOshareTechs input:checked').length);
            }
            else {
                $('.disSLinks').attr('disabled', 'disabled');
                $('.disSLinks').attr('onclick', 'return false;');
                $('.disSLinks').html(' Share ').closest('th').attr('class', '');

            }
        }
        function opPopup(src, height, width, title) {

            $("#ifPopup").load(function () {
                $("#loading").hide();
            });

            $('.close-btn').click(function () {
                closePopup();
                return false;
            });
            var left = ($(window).width() - width) / 2;
            var top = ($(window).height() - height) / 2;
            //if ($('.chkTOshareTechs input:checked').length > 1) {
            //    title = "Share Multiple";
            //}
            $("#lblPopupTitle").html(title);
            $("#loading").show();
            $("#ifPopup").attr("src", src);
            $("#boxpopup").dialog({
                modal: true,
                height: 350,
                width: 'auto',
                closeText: '',
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
            $('#boxpopup').css({
                position: 'fixed',
                top: top,
                left: left,
                width: width + 'px',                 // adjust width
                height: height + 'px',               // adjust height,
                zIndex: '99999'
            });
            $('#ifPopup').css({

                width: (width - 25) + 'px',                 // adjust width
                height: (height - 80) + 'px'              // adjust height,

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
            $("#boxpopup").dialog('open');
            $(".ui-dialog-titlebar").hide();
            return false;
        }

        function closePopup() {
            $("#boxpopup").dialog('close');
            $("#ifPopup").attr("src", "");
            return false;
        };


    </script>
    <style type="text/css">
        .grid td
        {
            padding: 5px 3px !important;
        }

        .grid th
        {
            padding: 7px 3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="boxpopup" class="backside-box" style="background: white">
        <div class="hd">
            <h2>
                <asp:Label ID="lblPopupTitle" ClientIDMode="Static" Text="" runat="server" />
            </h2>
            <a href="javascript:void(0);" onclick="closePopup();" class="close-btn">
                <img src="../App_Themes/Default/images/ic_close.png" alt="" /></a>
        </div>
        <div class="box-cont-dash">
            <div id="loading" align="center">
                <img src="../App_Themes/Default/images/ajax-loader.gif" />
            </div>
            <iframe id="ifPopup" src="" frameborder="0" height="0" width="0"></iframe>
        </div>
    </div>
    <asp:UpdatePanel ID="upTechnicianList" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnSelected" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnShared" runat="server" ClientIDMode="Static" />
            <div class="shadow-box large">
                <div class="box-hd">
                    <h2>Technicians</h2>
                </div>
                <div class="filter-panel">
                    <div class="search-box" style="width: 80px; display: none">
                        <asp:DropDownList runat="server" ID="ddlStatus" Style="float: left; padding: 5px;"
                            Visible="false">
                            <asp:ListItem Text="All" Value="-2" Selected="True" />
                            <asp:ListItem Text="Approved" Value="1" />
                            <asp:ListItem Text="Pending" Value="0" />
                        </asp:DropDownList>
                    </div>
                    <div class="search-box">
                        <asp:TextBox runat="server" ID="txtSearch" CssClass="search-input" MaxLength="50"
                            Style="float: left" />
                        <%-- <asp:AutoCompleteExtender runat="server" ID="autoComplete1" TargetControlID="myTextBox"
                            ServiceMethod="GetCompletionList" ServicePath="TechnicianListing.aspx" MinimumPrefixLength="2"
                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            DelimiterCharacters=";, :" ShowOnlyCurrentWordInCompletionListItem="true">
                        </asp:AutoCompleteExtender>--%>
                    </div>
                    <asp:Button Text="Search" ID="btnSearch" OnClick="btnSearch_Click" CssClass="go-btn"
                        ValidationGroup="vldListing" runat="server" />
                    <div class="search-box" style="width: 80px; padding-left: 10px">
                        <asp:DropDownList runat="server" ID="ddlSortBy" AutoPostBack="true" Style="float: left; padding: 5px;"
                            OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="All" Selected="True" />
                            <asp:ListItem Text="First Name" Value="FirstName" />
                            <asp:ListItem Text="Last Name" Value="LastName" />
                            <asp:ListItem Text="Location" Value="Location" />
                            <asp:ListItem Text="Averager Bid" Value="AvgBid" />
                            <asp:ListItem Text="Positions" Value="Positions" />
                            <asp:ListItem Text="Rank" Value="Rank" />
                            <asp:ListItem Text="Rating" Value="Rating" />
                        </asp:DropDownList>
                    </div>
                    <div class="filter-right">
                        <a href="PendingPositionApprovalListing.aspx" id="aPending" class="view-btn"><span class="view-position">Pending Approval</span></a> <a href="AddEditTechnician.aspx" class="view-btn"><span
                            class="add">Add</span></a>
                    </div>
                </div>
                <div align="center" width="100%">
                    <div style="width: 98%; text-align: left">
                        <uc:MessageBox runat="server" ID="ucMessageBox" />
                    </div>
                </div>
                <div class="grid">
                    <asp:GridView ID="gvTechnicianListing" runat="server" OnRowCommand="gvTechnicianListing_RowCommand"
                        DataKeyNames="PKTechnicianID" OnRowDataBound="gvTechnicianListing_RowDataBound"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" SortExpression="FirstName" HeaderStyle-Width="13%"
                                ItemStyle-Width="13%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="First Name" ID="lnkName" runat="server" CommandArgument="FirstName"
                                            CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcName" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <ExtendedLabel:ExtendedLabel ID="exFirstName" runat="server" MaxLength="18" Text='<%#Eval("FirstName")%>'></ExtendedLabel:ExtendedLabel>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="13%" />
                                <ItemStyle Width="13%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="Last Name" ID="lnkLastName" runat="server" CommandArgument="LastName"
                                            CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcLastName" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <ExtendedLabel:ExtendedLabel ID="exLastName" runat="server" MaxLength="18" Text='<%#Eval("LastName")%>'></ExtendedLabel:ExtendedLabel>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="13%" />
                                <ItemStyle Width="13%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="11%" ItemStyle-Width="11%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="Location" ID="lnkLocation" runat="server" CommandArgument="Location"
                                            CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcLocation" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <ExtendedLabel:ExtendedLabel ID="exLocation" runat="server" MaxLength="18" Text='<%#Eval("Location")%>'></ExtendedLabel:ExtendedLabel>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="11%" />
                                <ItemStyle Width="11%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="Rank" ID="lnkRank" runat="server" CommandArgument="Rank" CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcRank" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# TechTracking.Classes.ConvertTo.Integer(Eval("Rank")) == 0 ? "N/A" : Eval("Rank")%>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="8%" />
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="Rating" ID="lnkRating" runat="server" CommandArgument="Rating"
                                            CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcRating" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <asp:Label Text='<%# Eval("Rating")%>' ID="lblRating" runat="server" />
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Width="9%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="24%" ItemStyle-Width="24%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="Approved Positions" ID="lnkPositions" runat="server" CommandArgument="Positions"
                                            CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcPositions" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Eval("Positions")%>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="24%" />
                                <ItemStyle Width="24%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Avg Bid" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                <HeaderTemplate>
                                    <span>
                                        <asp:LinkButton Text="Avg Bid" ID="lnlAvgBid" runat="server" CommandArgument="AvgBid"
                                            CommandName="Sort">                                            
                                        </asp:LinkButton>
                                        <asp:PlaceHolder ID="pcAvgBid" runat="server"></asp:PlaceHolder>
                                    </span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# TechTracking.Classes.ConvertTo.Decimal(Eval("AvgBid")).ToString("F2") %>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle Width="12%" />
                                <ItemStyle Width="12%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center" align="center">
                                        Interview
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="width: 100%; text-align: center" align="center">
                                        <asp:LinkButton ID="lnkInterviewtrue" runat="server" CommandName="interview_uncheck"
                                            Visible='<%#TechTracking.Classes.ConvertTo.Boolean(Eval("isInterviewed")) %>'
                                            CommandArgument='<%# Eval("PKTechnicianID") %>' ToolTip="Click to Uncheck" Style="cursor: pointer;"> 
                                <img src="../App_Themes/Default/images/chk.png" alt="" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkInterviewFalse" runat="server" CommandName="interview_check"
                                            Visible='<%#!TechTracking.Classes.ConvertTo.Boolean(Eval("isInterviewed")) %>'
                                            CommandArgument='<%# Eval("PKTechnicianID") %>' ToolTip="Click to Check" Style="cursor: pointer;">
                                <img src="../App_Themes/Default/images/cancel.png" alt="" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center" align="center">
                                        Passport
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="width: 100%; text-align: center" align="center">
                                        <asp:LinkButton ID="lnkPassporttrue" runat="server" CommandName="Passport_uncheck"
                                            CommandArgument='<%# Eval("PKTechnicianID") %>' Visible='<%#TechTracking.Classes.ConvertTo.Boolean(Eval("hasPassport")) %>'
                                            ToolTip="Click to Uncheck" Style="cursor: pointer;"> 
                                <img src="../App_Themes/Default/images/chk.png" alt="" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkPassportfalse" runat="server" CommandName="Passport_check"
                                            Visible='<%#!TechTracking.Classes.ConvertTo.Boolean(Eval("hasPassport")) %>'
                                            CommandArgument='<%# Eval("PKTechnicianID") %>' ToolTip="Click to Check" Style="cursor: pointer;">
                                <img src="../App_Themes/Default/images/cancel.png" alt="" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center" align="center">
                                        Access
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="width: 100%; text-align: center" align="center">
                                        <asp:LinkButton ID="lnkAccesstrue" runat="server" CommandName="Access_uncheck" ToolTip="Click to Uncheck"
                                            Visible='<%#TechTracking.Classes.ConvertTo.Boolean(Eval("isAccess")) %>' CommandArgument='<%# Eval("PKTechnicianID") %>'
                                            Style="cursor: pointer;"> 
                                <img src="../App_Themes/Default/images/chk.png" alt="" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkAccessFalse" runat="server" CommandName="Access_check" ToolTip="Click to Check"
                                            Visible='<%#!TechTracking.Classes.ConvertTo.Boolean(Eval("isAccess")) %>' CommandArgument='<%# Eval("PKTechnicianID") %>'
                                            Style="cursor: pointer;">
                                <img src="../App_Themes/Default/images/cancel.png" alt="" />
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center">
                                        Score
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id="A1Score" runat="server" class="detail-btn" style="width: 45px" href='<%# "ShowEvalListing.aspx?"+ base.SetQueryString("TechnicianID",Convert.ToString(Eval("PKTechnicianID")))%>'>Score </a>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center">
                                        Edit
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a id="A1" runat="server" class="detail-btn" style="width: 45px" href='<%# "AddEditTechnician.aspx?"+ base.SetQueryString("TechnicianID",Convert.ToString(Eval("PKTechnicianID")))%>'>Edit </a>
                                </ItemTemplate>
                                <HeaderStyle Width="6%" />
                                <ItemStyle Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center; white-space: nowrap">
                                        <asp:LinkButton Text="   Delete   " ID="lnkHeaderDeleteNow" CommandName="delNow"
                                            runat="server" CssClass="disDLinks" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap">
                                        <asp:LinkButton ID="lnkDelete" OnClientClick="javascript:return confirm('Are you sure to delete this technician?')"
                                            Text="" CssClass="detail-btn" runat="server" CommandName="del" Width="50" CommandArgument='<%# Eval("PKTechnicianID") %>'>
                                        Delete
                                        </asp:LinkButton>
                                        <asp:CheckBox Text="" ID="chkDelete" CssClass="chkTOdeleteTechs" runat="server" onclick="manageMultiple();" />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="6%" />
                                <ItemStyle Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                <HeaderTemplate>
                                    <div style="width: 100%; text-align: center; white-space: nowrap">
                                        <asp:LinkButton Text=" Share " ID="lnkHeaderShareNow" CommandName="ShareNow" runat="server"
                                            CssClass="disSLinks" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="white-space: nowrap">
                                        <asp:LinkButton ID="lnkShare" Text="" CssClass="detail-btn" runat="server" Width="45"
                                            CommandArgument='<%# Eval("PKTechnicianID") %>'>
                                        Share
                                        </asp:LinkButton>
                                        <asp:CheckBox Text="" ID="chkShare" CssClass="chkTOshareTechs" runat="server" onclick="manageMultiple();" />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle Width="6%" />
                                <ItemStyle Width="6%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <uc:DataPager ID="pagerApps" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
