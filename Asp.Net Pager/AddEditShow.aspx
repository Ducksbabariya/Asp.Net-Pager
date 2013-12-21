<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddEditShow.aspx.cs" Inherits="TechTracking.Admin.AddEditShow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .none {
            display: none;
        }

        .ui-widget-overlay {
            position: fixed !important;
            top: 0 !important;
            left: 0 !important;
        }

        .chzn-results {
            max-height: 130px !important;
        }
    </style>
    <script type="text/javascript">


        function TravelDaysPopup(src, title, height, width) {
            $("#ifPopup").load(function () {
                $("#loading").hide();
            });
            $("#ifPopup").removeClass("none");
            $("#loading").show();
            $("#ifPopup").attr("src", src);
            $("#dvConfirmMessage").attr("class", "none");
            $("#<%=lblPopupTitle.ClientID %>").html(title);
            $("#boxpopup").dialog({
                modal: true,
                height: height,
                width: width,
                closeText: '',
                resizable: false
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
                $("#boxpopup").dialog('destroy');
                return false;
            });
            $(".ui-dialog-titlebar").hide();
            return false;
        }
        function refreashPage() {
            $("#boxpopup").dialog('close');
            $("#boxpopup").dialog('destroy');
            $("#btnConfirmedTechs").click();
        }
        function ShowTravelDays() {

            $("#boxpopup").dialog('close');
            $("#boxpopup").dialog('destroy');
            $("#btnTravelDaysBinder").click();
            //document.getElementById("btnTravelDaysBinder").click();
        }

    </script>
    <link href="../Scripts/Counter/jquery.countdown.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Counter/jquery.countdown.js" type="text/javascript"></script>
    <style type="text/css">
        #defaultCountdown {
            width: 240px;
            height: 45px;
        }

        .ui-widget-overlay {
            position: fixed !important;
            top: 0 !important;
            left: 0 !important;
        }
    </style>
    <script type="text/javascript">
        function countDown() {
            if ($("#hdnStartDate").val() != '' && $("#hdnStartDate").val() != undefined) {

                var d = $("#hdnStartDate").val();
                var month = d.substring(0, 2);
                var day = d.substring(3, 5);
                var year = d.substring(6, 10);
                var austDay = new Date(year, month - 1, day);
                austDay.setDate(austDay.getDate() + 1);
                $('#defaultCountdown').countdown({ until: austDay });
            }
        }
    </script>
    <script type="text/javascript">
        function initializeDatePickers() {

            var sdate = ($("#txtStartDate").val() == undefined || $("#txtStartDate").val() == null || $("#txtStartDate").val() == '') ? 0 : convertDate($("#txtStartDate").attr('value'));
            if (sdate !== 0 && sdate !== "Invalid Date") {
                if (sdate > new Date()) {
                    sdate = new Date();
                }
            }

            $("#txtStartDate").datepicker({
                nextprev: true,
                numberOfMonths: 2,
                minDate: sdate,
                onClose: function (selectedDate) {
                    $("#hdnsStartDate").attr('value', selectedDate);
                    $("#txtEndDate").datepicker("option", "minDate", selectedDate);
                    $("#txtBidStartdate").datepicker("option", "maxDate", selectedDate);
                    $("#txtBidEndDate").datepicker("option", "maxDate", selectedDate);
                    //$("#txtBidStartdate").val('');
                    //$("#txtBidEndDate").val('');
                }
            });

            $("#txtEndDate").datepicker({
                nextprev: true,
                numberOfMonths: 2,
                onClose: function (selectedDate) {
                    //$("#hdnSEDate").attr('value', selectedDate);
                }

            });
            $("#txtBidStartdate").datepicker({
                nextprev: true,
                numberOfMonths: 2,
                minDate: ($("#txtBidStartdate").val() == undefined || $("#txtBidStartdate").val() == null || $("#txtBidStartdate").val() == '') ? 0 : convertDate($("#txtBidStartdate").attr('value')),
                onClose: function (selectedDate) {
                    $("#txtBidEndDate").datepicker("option", "minDate", selectedDate);
                }
            });
            //alert(($("#txtBidStartdate").val() == undefined || $("#txtBidStartdate").val() == null) ? 0 : convertDate($("#txtBidStartdate").attr('value')));
            $("#txtBidEndDate").datepicker({
                minDate: ($("#txtBidStartdate").val() == undefined || $("#txtBidStartdate").val() == null || $("#txtBidStartdate").val() == '') ? 0 : convertDate($("#txtBidStartdate").attr('value')),
                nextprev: true,
                numberOfMonths: 2
            });

            $("#txtBidStartdate").datepicker("option", "maxDate", convertDate($("#hdnsStartDate").attr('value')));
            $("#txtBidEndDate").datepicker("option", "maxDate", convertDate($("#hdnsStartDate").attr('value')));
        }

        function setArrivalDates() {
            var bsdate = $("#hdnSSDate").val() === '' ? 0 : convertDate($("#hdnSSDate").attr('value'));
            if (bsdate !== 0 && bsdate !== "Invalid Date") {
                if (bsdate > new Date()) {
                    bsdate = new Date();
                }
            }

            $(".Arrival").datepicker({
                nextprev: true,
                numberOfMonths: 2,
                minDate: bsdate,
                onClose: function (selectedDate) {
                    $(".Leave").datepicker("option", "minDate", selectedDate);
                }
            });

            $(".Leave").datepicker({
                nextprev: true,
                numberOfMonths: 2
            });
            $(".ArrivalAll").datepicker({
                nextprev: true,
                numberOfMonths: 2,
                minDate: bsdate,
                onClose: function (selectedDate) {
                    $(".leaveall").datepicker("option", "minDate", selectedDate);
                }
            });
            $(".leaveall").datepicker({
                nextprev: true,
                numberOfMonths: 2,
                minDate: bsdate

            });

            $(".Arrival").datepicker("option", "minDate", bsdate);
            $(".Arrival").datepicker("option", "maxDate", '+2y');
        }
        function convertDate(date) {

            var _date = date;
            var yy = _date.substr(6, 4);
            var newdate = yy + '-' + _date.substr(0, 2) + '-' + _date.substr(3, 2);
            var selectedDate = new Date(newdate);
            var msecsInADay = 86400000;
            msecsInADay = 0;
            var _dDate = new Date(selectedDate.getTime() + msecsInADay);
            return _dDate;
        }
    </script>
    <script src="../Choosen/chosen.jquery.js" type="text/javascript"></script>
    <link href="../Choosen/chosen.css" rel="stylesheet" />
    <script type="text/javascript">
        function setChoosen() {
            $('.chzn-select').chosen({ no_results_text: "Oops, nothing found!", width: "95%" }).change(function (e, args) {
                if (args.deselected !== undefined) {
                    $('#ddlByLocations option[value="' + args.deselected + '"]').removeAttr('selected');
                }
                else if (args.selected !== undefined) {
                    $('#ddlByLocations option[value="' + args.selected + '"]').attr('selected', 'selected');
                }
                var selectedids = $('#ddlByLocations option:selected').map(function () {
                    return this.value;
                }).get().join();
                $("#selectedLocations").attr('value', selectedids);
                var selectednames = $('#ddlByLocations option:selected').map(function () {
                    return this.text;
                }).get().join();
                $("#selectedTechsNames").attr('value', selectednames);
                $("#container").css("height", ($('.content').height() + 170));
            });

            $('.chzn').chosen({ no_results_text: "Oops, nothing found!", width: "95%" }).change(function (e, args) {
                if (args.deselected !== undefined) {
                    $('#ddlByTechs option[value="' + args.deselected + '"]').removeAttr('selected');
                }
                else if (args.selected !== undefined) {
                    $('#ddlByTechs option[value="' + args.selected + '"]').attr('selected', 'selected');
                }
                var selectedids = $('#ddlByTechs option:selected').map(function () {
                    return this.value;
                }).get().join();
                $("#selectedTechs").attr('value', selectedids);
                $("#container").css("height", ($('.content').height() + 170));
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="boxpopup" class="backside-box" style="background: white">
        <div class="hd">
            <h2>
                <asp:Label ID="lblPopupTitle" Text="" runat="server" />
            </h2>
            <a href="javascript:void(0);" class="close-btn">
                <img src="../App_Themes/Default/images/ic_close.png" alt="" /></a>
        </div>
        <div class="box-cont-dash">
            <p>
                <div id="loading" align="center">
                    <img src="../App_Themes/Default/images/ajax-loader.gif" />
                </div>
                <iframe id="ifPopup" src="" frameborder="0"></iframe>
            </p>
        </div>
    </div>
    <asp:HiddenField ID="hdnSSDate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSEDate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnsStartDate" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel runat="server" UpdateMode="Always" ID="updpnlAddEditShow">
        <ContentTemplate>
            <div class="shadow-box large">
                <asp:HiddenField ID="hdnStartDate" runat="server" ClientIDMode="Static" />
                <asp:Button Text="" ID="btnTravelDaysBinder" ClientIDMode="Static" OnClick="btnTravelDaysBinder_Click"
                    runat="server" Style="display: none" />

                <asp:Button Text="" ID="btnConfirmedTechs" runat="server" ClientIDMode="Static" OnClick="btnConfirmedTechs_Click"
                    runat="server" Style="display: none" />
                <div class="box-hd">
                    <h2>
                        <asp:Label Text="Add Show" ID="lblHeaderLabel" runat="server" />
                    </h2>
                </div>
                <div class="filter-panel">
                    <span class="headeing" style="float: left; margin: 0px">Please Input Show Information
                    </span>
                    <div class="filter-right">
                        <asp:LinkButton ID="lnkBack" runat="server" CssClass="view-btn" OnClick="lnkBack_Click"> <span class="back">Back</span> </asp:LinkButton>
                    </div>
                </div>
                <div style="margin: 5px; opacity: 1">
                    <uc:MessageBox runat="server" ID="MessageBox1" />
                </div>
                <div class="box-cont-common">
                    <div class="tabs-box">
                        <div class="tabs-nav">
                            <a runat="server" id="ahBasicInfo" onserverclick="ahBasicInfo_Click">Basic Info</a>
                            <a runat="server" id="ahPositionNeeds" onserverclick="ahPositionNeeds_Click">Positions
                                Needs</a> <a runat="server" id="aBiddingRates" onserverclick="aBiddingRates_Click">Bidding
                                    Rates </a><a runat="server" id="ahTravelDays" onserverclick="ahTravelDays_Click">Travel
                                        Days </a><a runat="server" id="ahTechView" onserverclick="ahTechView_Click">Tech View</a>
                            <a runat="server" id="ahPublish" onserverclick="ahPublish_Click">Publish</a>
                            <a runat="server" id="aConfirmedTechs" style="float: right" onserverclick="aConfirmedTechs_Click" visible="false">Techs</a>
                        </div>
                        <uc:MessageBox runat="server" ID="ucMessageBox" />
                        <asp:Panel runat="server" ID="pnlShow">
                            <asp:MultiView ActiveViewIndex="0" ID="mlvShow" runat="server">
                                <asp:View runat="server" ID="vwBasicInfo">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <p class="headeing">
                                                    Please Input the Shows Information.
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="420" valign="top">
                                                            <table width="100%" border="0" cellspacing="3" cellpadding="2">
                                                                <tr>
                                                                    <td align="right">Title:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txtTitle" runat="server" Width="160px" MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">Nearest Airport:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txtNearestAirport" runat="server" Width="160px" MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvNearestAirport" runat="server" ControlToValidate="txtNearestAirport"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">City of Location:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txtCity" runat="server" Width="160px" MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">Start Date:
                                                                    </td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="txtStartDate" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                                                                        <%--<asp:CalendarExtender ID="calStartDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                        Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgStartDate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                        Style="position: relative; top: 5px" />--%>
                                                                        <%--<asp:CompareValidator ID="cvStartDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate"
                                                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="" ValidationGroup="vwBasicInfo"></asp:CompareValidator>--%>
                                                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="cvStartDate" runat="server" Display="Static"
                                                                            ErrorMessage="MM/dd/yyyy" Text="" ControlToValidate="txtStartDate" SetFocusOnError="true"
                                                                            ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                        </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">End Date:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txtEndDate" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                                                                        <%--<asp:CalendarExtender ID="calEndDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                        Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEndDate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                        Style="position: relative; top: 5px" />--%>
                                                                        <%-- <asp:CompareValidator ID="cvEndDate" runat="server" Display="Dynamic" ControlToValidate="txtEndDate"
                                                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="" ValidationGroup="vwBasicInfo"></asp:CompareValidator>--%>
                                                                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="cvEndDate" runat="server" Display="Static" ErrorMessage="MM/dd/yyyy"
                                                                            Text="" ControlToValidate="txtEndDate" SetFocusOnError="true" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                        </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">Client:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:DropDownList runat="server" ID="ddlCustomer" Width="168px" CssClass="year">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlCustomer"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" InitialValue="0" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="trTravelDays" visible="false">
                                                                    <td align="right" valign="top">Travel Days:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:LinkButton Text="Select" ID="lnkTravelDays" OnClick="lnkTravelDays_Click" runat="server"
                                                                            Visible="false" />
                                                                        <div style="text-align: left; color: #3077A4">
                                                                            <asp:Literal Text="" ID="ltrTravelDays" runat="server" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">Email:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txtEmail" runat="server" Width="160px" MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                                                                            ErrorMessage="Not valid" ValidationGroup="vwBasicInfo" ValidationExpression="^([A-Za-z0-9_\-\.\'])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,6})$" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" valign="top">Description:
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txtDescription" runat="server" Width="160px" TextMode="MultiLine"
                                                                            Rows="4" Columns="33" MaxLength="50" Style="float: left"></asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator ID="rfvPosition" runat="server" ControlToValidate="txtDescription"
                                                                        Style="float: left; padding: 4px" ErrorMessage="*" ForeColor="Red" Display="Dynamic"
                                                                        ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td align="left" valign="top">Project Manager:
                                                                                </td>
                                                                                <td align="left" valign="top">
                                                                                    <div style="height: 112px; width: 190px; overflow: auto; border: 1px solid #D4E1E8">
                                                                                        <asp:CheckBoxList runat="server" ID="chklstProjectManagers">
                                                                                        </asp:CheckBoxList>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <p class="frm-title">
                                                                            Duration of Bid:
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtBidStartdate" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                                                                        <%--<asp:CalendarExtender ID="calBidStartdate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                        Format="MM/dd/yyyy" TargetControlID="txtBidStartdate" PopupButtonID="imgBidStartDate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Image ID="imgBidStartDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                        Style="position: relative; top: 5px" />--%>
                                                                        <asp:RegularExpressionValidator ID="cvBidStartDate" runat="server" Display="Static"
                                                                            ErrorMessage="" Text="" ControlToValidate="txtBidStartdate" SetFocusOnError="true"
                                                                            ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator ID="rfvBidStartDate" runat="server" ControlToValidate="txtBidStartdate"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:TextBox ID="txtBidEndDate" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                                                                        <%--<asp:CalendarExtender ID="calBidEndDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                        Format="MM/dd/yyyy" TargetControlID="txtBidEndDate" PopupButtonID="imgBidEndDate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Image ID="imgBidEndDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                        Style="position: relative; top: 5px" />--%>
                                                                        <asp:RegularExpressionValidator ID="cvBidEndDate" runat="server" Display="Static"
                                                                            ErrorMessage="" Text="" ControlToValidate="txtBidEndDate" SetFocusOnError="true"
                                                                            ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator ID="rfvBidEndDate" runat="server" ControlToValidate="txtBidEndDate"
                                                                            ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vwBasicInfo"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <div style="float: left;">
                                                                                    <p>
                                                                                        Logo Image: <span class="opt">(optional)</span>
                                                                                    </p>
                                                                                    <div class="clear">
                                                                                    </div>
                                                                                    <div style="float: left">
                                                                                        <asp:FileUpload ID="fluLogoImage" runat="server" />
                                                                                    </div>
                                                                                    <div style="float: left; margin-left: 10px">
                                                                                        <asp:Button Text="Add Logo" runat="server" ID="btnAddLogo" CssClass="input-button"
                                                                                            ValidationGroup="vwLogo" OnClick="btnAddLogo_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <div style="float: left; padding-top: 5px; padding-left: 10px">
                                                                                    <img src="" id="imgLogoImage" runat="server" height="60" width="60" onerror="(this.src = '../App_Themes/Default/images/upload-photo.jpg')" />
                                                                                    <asp:LinkButton ID="lnkRemoveImage" runat="server" Visible="false" ToolTip="Remove Image"
                                                                                        CausesValidation="false" OnClick="lnkRemoveImage_Click" Style="cursor: pointer; position: relative; top: -25px"> <img src="../App_Themes/Default/images/cancel.png" alt="" /></asp:LinkButton>
                                                                                </div>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="rfvLogo" runat="server" ControlToValidate="fluLogoImage"
                                                                                    ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vwLogo"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator runat="server" ID="revFluImage" ControlToValidate="fluLogoImage"
                                                                                    ErrorMessage="Image Files Only (.jpg, .bmp, .png, .gif)" ValidationGroup="vwLogo"
                                                                                    ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$" />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="btnAddLogo" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:UpdatePanel ID="updImportantDocs" runat="server">
                                                                            <ContentTemplate>
                                                                                <p>
                                                                                    Important Documents: <span class="opt">(optional)</span>
                                                                                </p>
                                                                                <asp:FileUpload ID="fluImportantDoc" runat="server" />
                                                                                <asp:Button Text="Add Documents" runat="server" ID="btnAddFile" CssClass="input-button"
                                                                                    ValidationGroup="vwDoc" OnClick="btnAddFile_Click" Style="margin-left: 8px" />
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="rfvImportantDoc" runat="server" ControlToValidate="fluImportantDoc"
                                                                                    ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" ValidationGroup="vwDoc"></asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator runat="server" ID="revfluImportantDoc" ControlToValidate="fluImportantDoc"
                                                                                    ErrorMessage="Files Only (.doc, .pdf, .txt, .docx)" ValidationGroup="vwDoc" ValidationExpression="^.*\.(doc|DOC|pdf|PDF|txt|TXT|Docx|DOCX)$" />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="btnAddFile" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel runat="server" Visible="false" ID="pnlImportantDoc">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                                                <ContentTemplate>
                                                                                    <h4>Important Documents</h4>
                                                                                    <table class="imp-doc" width="67%" cellspacing="0" cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <asp:Repeater runat="server" ID="rptImportantDoc" OnItemCommand="rptImportantDoc_ItemCommand">
                                                                                                <ItemTemplate>
                                                                                                    <td align="left">
                                                                                                        <asp:Label Text='<%# Container.DataItem %>' ID="lblDocPath" runat="server" />
                                                                                                    </td>
                                                                                                    <td align="right">
                                                                                                        <asp:LinkButton ID="LinkButton1" CommandName="Remove" CommandArgument='<%# Container.DataItem %>'
                                                                                                            Text="Remove" runat="server" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                                                                                    </td>
                                                                                                </ItemTemplate>
                                                                                                <SeparatorTemplate>
                                                                                                    <tr style="line-height: 0 !important;">
                                                                                                        <td colspan="2" style="padding: 0 !important; border: none !important"></td>
                                                                                                    </tr>
                                                                                                </SeparatorTemplate>
                                                                                            </asp:Repeater>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View runat="server" ID="vwPositionNeeds">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <p class="headeing">
                                                    Positions Needs:
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td valign="top" colspan="2" align="center">
                                                            <asp:DataList ID="dlPositions" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                                                <ItemTemplate>
                                                                    <table border="0" cellpadding="3" cellspacing="2" width="100%">
                                                                        <tr>
                                                                            <td align="right" style="color: #3086b3">
                                                                                <asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "PositionName") %>' ID="lblPositionName"
                                                                                    runat="server" />
                                                                                <asp:CheckBox Text="" ID="chkPosition" runat="server" />
                                                                                <asp:HiddenField ID="hdnPositionID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PKPositionID") %>' />
                                                                            </td>
                                                                            <td width="62" align="right">
                                                                                <asp:Label Text="Quantity" ID="lblQuantity" runat="server" />
                                                                            </td>
                                                                            <td width="60" align="left" valign="top">
                                                                                <asp:TextBox runat="server" ID="txtQuantity" Width="35px" MaxLength="3" />
                                                                                <asp:FilteredTextBoxExtender ID="ftbeQuantity" TargetControlID="txtQuantity" FilterType="Numbers"
                                                                                    runat="server">
                                                                                </asp:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                        <td width="15%" valign="top"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View runat="server" ID="vwBiddingRates">
                                    <div style="width: 100%">
                                        <p class="headeing">
                                            Max Bid Rates
                                        </p>
                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td colspan="2" align="center" style="width: 60%">
                                                            <asp:CheckBox Text="Would you like to set a maximum bid rate?" ID="chkMaximumRate"
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                    <asp:DataList ID="dlMaxBidrates" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                                        Width="60%">
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <p class="headeing">
                                                                        Select their max bid rate
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="right" valign="middle" width="50%">
                                                                    <asp:Label Text='<%# Eval("PositionName")%>' ID="lblPositionName" runat="server" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:HiddenField ID="hdnPositionID" runat="server" Value='<%# Eval("FKPositionID") %>' />
                                                                </td>
                                                                <td align="left" width="50%">
                                                                    <asp:TextBox ID="txtMaxBidRate" Text='<%# Eval("Maxbidrate") %>' runat="server" Width="100px"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="ftbeMaxBidRate" TargetControlID="txtMaxBidRate"
                                                                        FilterType="Numbers" runat="server">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View runat="server" ID="vwTravelDays">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <p class="headeing">
                                                    Travel Days
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" width="100%">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td valign="top" colspan="2" align="center">
                                                            <table border="0" cellpadding="5" cellspacing="5">
                                                                <asp:DataList ID="dlTravelDays" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                                                    Width="60%">
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <td align="left" width="33%">
                                                                                <p class="headeing">
                                                                                    Position
                                                                                </p>
                                                                            </td>
                                                                            <td align="left" width="33%">
                                                                                <p class="headeing">
                                                                                    Arrival
                                                                                </p>
                                                                            </td>
                                                                            <td align="left" width="33%">
                                                                                <p class="headeing">
                                                                                    Leave
                                                                                </p>
                                                                            </td>
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td align="left" width="33%" valign="middle">
                                                                                <asp:Label Text='<%# Eval("PositionName")%>' ID="lblPositionName" runat="server" />
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:HiddenField ID="hdnPositionID" runat="server" Value='<%# Eval("FKPositionID") %>' />
                                                                            </td>
                                                                            <td width="33%" align="left">
                                                                                <asp:TextBox ID="txtArrivalDate" CssClass="Arrival" runat="server" Width="100px"></asp:TextBox>
                                                                                <%-- <asp:CalendarExtender ID="calArrivalDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                                Format="MM/dd/yyyy" TargetControlID="txtArrivalDate" PopupButtonID="imgArrivalDate">
                                                                            </asp:CalendarExtender>
                                                                            <asp:Image ID="imgArrivalDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                                Style="position: relative; top: 5px" />
                                                                            <asp:CompareValidator ID="cvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtArrivalDate"
                                                                                Operator="DataTypeCheck" Type="Date" ErrorMessage="" ValidationGroup="travelDays"></asp:CompareValidator>--%>
                                                                                <asp:RegularExpressionValidator ID="cvArrivalDate" runat="server" Display="Static"
                                                                                    ErrorMessage="" Text="" ControlToValidate="txtArrivalDate" SetFocusOnError="true"
                                                                                    ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                            <td width="33%" align="left" valign="top">
                                                                                <asp:TextBox runat="server" ID="txtLeaveDate" CssClass="Leave" Width="100px" />
                                                                                <%--<asp:CalendarExtender ID="caltxtLeaveDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                                Format="MM/dd/yyyy" TargetControlID="txtLeaveDate" PopupButtonID="imgLeaveDate">
                                                                            </asp:CalendarExtender>
                                                                            <asp:Image ID="imgLeaveDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                                Style="position: relative; top: 5px" />
                                                                            <asp:CompareValidator ID="cvLeaveDate" runat="server" Display="Dynamic" ControlToValidate="txtLeaveDate"
                                                                                Operator="DataTypeCheck" Type="Date" ErrorMessage="" ValidationGroup="travelDays"></asp:CompareValidator>--%>
                                                                                <asp:RegularExpressionValidator ID="cvLeaveDate" runat="server" Display="Static"
                                                                                    ErrorMessage="" Text="" ControlToValidate="txtLeaveDate" SetFocusOnError="true"
                                                                                    ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="60%">
                                                                            <tr>
                                                                                <td align="left" width="33%">
                                                                                    <p class="headeing">
                                                                                        All Positions
                                                                                    </p>
                                                                                </td>
                                                                                <td align="left" width="33%">
                                                                                    <p class="headeing">
                                                                                        Arrival
                                                                                    </p>
                                                                                </td>
                                                                                <td align="left" width="33%">
                                                                                    <p class="headeing">
                                                                                        Leave
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" width="33%" valign="top" style="padding-top: 6px;">All Selected Positions
                                                                                </td>
                                                                                <td width="33%" align="left" valign="top">
                                                                                    <asp:TextBox ID="txtArrivalDate" CssClass="ArrivalAll" ClientIDMode="Static" runat="server"
                                                                                        Width="100px"></asp:TextBox>&nbsp;
                                                                                <%--<asp:CalendarExtender ID="calArrivalDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                        Format="MM/dd/yyyy" TargetControlID="txtArrivalDate" PopupButtonID="imgArrivalDate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Image ID="imgArrivalDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                        Style="position: relative; top: 5px" />&nbsp;&nbsp;
                                                                    <asp:CompareValidator ID="cvArrivalDate" runat="server" Display="Dynamic" ControlToValidate="txtArrivalDate"
                                                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="" ValidationGroup="travelDays"></asp:CompareValidator>--%>
                                                                                    <asp:RegularExpressionValidator ID="cvArrivalDate" runat="server" Display="Static"
                                                                                        ErrorMessage="" Text="" ControlToValidate="txtArrivalDate" SetFocusOnError="true"
                                                                                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                                <td width="33%" align="left" valign="top">
                                                                                    <asp:TextBox runat="server" ID="txtLeaveDate" CssClass="leaveall" ClientIDMode="Static"
                                                                                        Width="100px" />&nbsp;
                                                                                <%--<asp:CalendarExtender ID="calLeaveDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                                        Format="MM/dd/yyyy" TargetControlID="txtLeaveDate" PopupButtonID="imgLeaveDate">
                                                                    </asp:CalendarExtender>
                                                                    <asp:Image ID="imgLeaveDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                                        Style="position: relative; top: 5px" />
                                                                    <asp:CompareValidator ID="cvLeaveDate" runat="server" Display="Dynamic" ControlToValidate="txtLeaveDate"
                                                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="" ValidationGroup="travelDays"></asp:CompareValidator>--%>
                                                                                    <asp:RegularExpressionValidator ID="cvLeaveDate" runat="server" Display="Static"
                                                                                        ErrorMessage="" Text="" ControlToValidate="txtLeaveDate" SetFocusOnError="true"
                                                                                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table border="0" cellpadding="5" cellspacing="5" id="tabAllSelected" runat="server"
                                                                width="100%" align="center" visible="true">
                                                                <tr>
                                                                    <td align="center">
                                                                        <table border="0" cellpadding="5" cellspacing="5">
                                                                            <tr>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td id="Td1" width="15%" valign="top" runat="server" visible="false">
                                                            <asp:CheckBox Text="All positions have the same travel dates" ID="chkAllPosition"
                                                                OnCheckedChanged="chkAllPosition_CheckedChanged" AutoPostBack="true" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View runat="server" ID="vwTechView">
                                    <table width="100%" border="0" cellspacing="20" cellpadding="0" class="nobrd-view">
                                        <tr>
                                            <td width="214" align="left" valign="top">
                                                <a href="#">
                                                    <img id="imgShowLogoPreview" runat="server" onerror='(this.src="../App_Themes/Default/images/location.jpg")'
                                                        alt="" width="172" height="152" /></a> <a id="A1" runat="server" onserverclick="ahBasicInfo_Click"
                                                            class="edit-link">Edit</a>
                                                <br />
                                                <h4 style="padding-top: 10px">
                                                    <ExtendedLabel:ExtendedLabel ID="lblShowTitle" runat="server" MaxLength="20" Text=""></ExtendedLabel:ExtendedLabel>
                                                </h4>
                                                <p>
                                                    <ExtendedLabel:ExtendedLabel ID="lblShowLocation" runat="server" MaxLength="20" Text=""></ExtendedLabel:ExtendedLabel>
                                                </p>
                                                <asp:Label Text="" ID="lblShowYear" runat="server" />
                                                <br />
                                                <h4 style="padding-top: 10px">
                                                    <asp:Label Text="Time Remaining To Bid:" ID="lbldispTimeRemains" runat="server" />
                                                </h4>
                                                <div id="defaultCountdown">
                                                    <span class="orange-txt"></span><a id="A2" runat="server" onserverclick="ahBasicInfo_Click"
                                                        class="edit-link">Edit</a><br />
                                                </div>
                                                <br />
                                                <h4 style="padding-top: 10px">
                                                    <asp:Label ID="lblConfirmedDate" Text="My confirmed hourly rate:" runat="server" /></h4>
                                                <p class="dayrate">
                                                    <asp:TextBox runat="server" ID="txtRate" MaxLength="6" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvConfirmeddate" runat="server" ControlToValidate="txtRate"
                                                    Font-Size="16px" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vldRate"></asp:RequiredFieldValidator>--%>
                                                    <asp:CompareValidator ID="cmpRate" runat="server" ControlToValidate="txtRate" ForeColor="Red"
                                                        Font-Size="16px" Operator="DataTypeCheck" Type="Double" ErrorMessage="*" ValidationGroup="vldRate"></asp:CompareValidator>
                                                </p>
                                            </td>
                                            <td width="230" align="left" valign="top">
                                                <uc:Calender runat="server" ID="ucCalender" ShowNextPrevMonth="true" EnableViewState="true"
                                                    DayStyleHeight="16" CellPadding="4" CellSpacing="0" />
                                                <br />
                                                <br />
                                                <h4>Show Length:</h4>
                                                <p>
                                                    <span runat="server" id="spnShowDays" class="orange-txt"></span>show days<br />
                                                    <span class="orange-txt" runat="server" id="spnTravelDays"></span>travel days<br />
                                                    <asp:Label Text="" ID="lblShowDates" runat="server" />
                                                </p>
                                                <a id="A3" runat="server" onserverclick="ahBasicInfo_Click" class="edit-link">Edit</a><br />
                                            </td>
                                            <td align="left" valign="top">
                                                <h4>
                                                    <asp:Label ID="lblShowDescription" Text="Show Description: " runat="server" />
                                                </h4>
                                                <p runat="server" id="ltrShowDescription" style="word-wrap: break-word;">
                                                </p>
                                                <a id="A4" runat="server" class="edit-link" onserverclick="ahBasicInfo_Click">Edit</a><br />
                                                <br />
                                                <h4>
                                                    <asp:Label ID="lblDispImportantDocs" Text="Important Documents: " runat="server" />
                                                </h4>
                                                <table border="0" cellpadding="0" cellspacing="0" class="imp-doc" width="100%">
                                                    <tr>
                                                        <asp:Repeater runat="server" ID="rptViewImportantDoc">
                                                            <ItemTemplate>
                                                                <td align="left">
                                                                    <asp:Label Text='<%# Container.DataItem %>' ID="lblDocPath" runat="server" />
                                                                </td>
                                                                <td align="right">
                                                                    <a id="A5" runat="server" href='<%# "~/Download.aspx?ATTACH=YES&Path="+Convert.ToString(Container.DataItem)%>'>Download</a>
                                                                </td>
                                                            </ItemTemplate>
                                                            <SeparatorTemplate>
                                                                <tr style="line-height: 0 !important;">
                                                                    <td colspan="2" style="padding: 0 !important; border: none !important"></td>
                                                                </tr>
                                                            </SeparatorTemplate>
                                                        </asp:Repeater>
                                                    </tr>
                                                </table>
                                                <asp:Label Text="No Documents Found" ID="lblNoRecordsTechView" runat="server" ForeColor="Red"
                                                    Visible="true" />
                                                <a id="A6" runat="server" class="edit-link" onserverclick="ahBasicInfo_Click">Edit</a><br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View runat="server" ID="vwPublish">
                                    <table width="100%" border="0" cellspacing="5" cellpadding="0">
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="130" class="custom-grp">Custom Group:
                                                        </td>
                                                        <td valign="top">
                                                            <div class="search-box">
                                                                <asp:TextBox ID="txtSearch" runat="server" Text="Search" CssClass="search-input"
                                                                    MaxLength="50"></asp:TextBox>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvSearchPublish" runat="server" ControlToValidate="txtSearch"
                                                                ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="vldAddEdit"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="go-btn" Text="Search" ValidationGroup="vldAddEdit" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="300" valign="top">
                                                            <span class="bluetxt">Select from following:</span>
                                                            <div class="recieve-inv">
                                                                <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:CheckBoxList runat="server" ID="chklstSelection">
                                                                                <asp:ListItem Text="All qualified techs" Value="0"></asp:ListItem>
                                                                                <asp:ListItem Text="All available techs" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Only access techs" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Only techs with passport" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Techs willing to share room" Value="4"></asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td valign="top">
                                                            <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:LinkButton Text="" ID="lnkEmail" Style="float: right" runat="server" OnClick="lnkEmail_Click"> <img src="../App_Themes/Default/images/IcoEmail.png" /></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox Text="Select by location" ID="chkByLocation" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="ddlByLocations" ClientIDMode="Static" data-placeholder="Choose locations..."
                                                                            class="chzn-select" multiple Style="width: 350px;">
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="selectedLocations" runat="server" ClientIDMode="Static" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox ID="chkByTechs" Text="Select specific people" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="ddlByTechs" ClientIDMode="Static" data-placeholder="Choose peoples..."
                                                                            class="chzn" multiple Style="width: 350px;">
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField ID="selectedTechs" runat="server" ClientIDMode="Static" />
                                                                        <asp:HiddenField ID="selectedTechsNames" runat="server" ClientIDMode="Static" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox ID="chkByRating" Text="Select by rating" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float: left">
                                                                            <asp:SliderExtender ID="SliderExPrompt" runat="server" BoundControlID="lblPrompt"
                                                                                TargetControlID="txtPrompt" Minimum="0" Maximum="10" Steps="11">
                                                                            </asp:SliderExtender>
                                                                        </div>
                                                                        <div style="float: left">
                                                                            <asp:Label Text="" ID="lblPrompt" ClientIDMode="Static" runat="server" Style="float: right; position: relative; left: 25px" />
                                                                            <asp:TextBox runat="server" ID="txtPrompt" ClientIDMode="Static" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Techs must be at or above the selected rating
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                </asp:View>
                            </asp:MultiView>
                            <a runat="server" id="aNext" onserverclick="aNext_Click" class="nxt-btn"><span runat="server"
                                id="spnNext" class="next">Next</span></a>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlConfirmedTechs" Visible="false">
                            <div class="grid" style="margin-top: 15px">
                                <asp:GridView ID="gvConfirmedTechs" runat="server" OnRowCommand="gvConfirmedTechs_RowCommand"
                                    OnRowDataBound="gvConfirmedTechs_RowDataBound">
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
                                                    <%# Eval("Rank")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-Width="8%">
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
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="14%" ItemStyle-Width="14%">
                                            <HeaderTemplate>
                                                <span>
                                                    <asp:LinkButton Text="Confirmed Position" ID="lnkPosition" runat="server" CommandArgument="Position"
                                                        CommandName="Sort">                                            
                                                    </asp:LinkButton>
                                                    <asp:PlaceHolder ID="pcPosition" runat="server"></asp:PlaceHolder>
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Eval("Position") %>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <HeaderTemplate>
                                                <span>
                                                    <asp:LinkButton Text="Bid Price" ID="lnkBid" runat="server" CommandArgument="BidRate" CommandName="Sort">                                            
                                                    </asp:LinkButton>
                                                    <asp:PlaceHolder ID="pcBid" runat="server"></asp:PlaceHolder>
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    $<%# Eval("BidRate") %><asp:LinkButton ID="lnkUpdateBidRate" PostBackUrl="javascript:void(0)" runat="server" ToolTip="Click to Update Bid"
                                                        Style="cursor: pointer; float: right"> 
                                                   <img src="../App_Themes/Default/images/triangle-black.png" alt="" /></asp:LinkButton>
                                                </p>
                                                <asp:HiddenField ID="hdnTechid" runat="server" ClientIDMode="Static" Value='<%# Eval("PKTechnicianID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <div style="width: 100%; text-align: center">
                                                    Score
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<a id="A1Score" runat="server" class="detail-btn" style="width: 45px" href='<%# "ShowEvalListing.aspx?"+ base.SetQueryString("TechnicianID",Convert.ToString(Eval("PKTechnicianID")))%>'>Score </a>--%>
                                                <a id="A1Score" runat="server" class="detail-btn" style="width: 45px" href='<%# "ScoreToTech.aspx?ShowEvalList=False&IsAllTechnician=0&"+ base.SetQueryString("TechnicianID",Convert.ToString(Eval("PKTechnicianID"))) +"&"+ base.SetQueryString("ShowID", Convert.ToString(_ShowID))%>'>Score </a>

                                                <%--"ScoreToTech.aspx?ShowEvalList=True&ShowID=&TechnicianID=MTIy-/jhiDHgiBaM=&IsAllTechnician=1"--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <HeaderTemplate>
                                                <div style="width: 100%; text-align: center">
                                                    Remove
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Remove" ID="lnkRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("PKBidID") %>' CssClass="detail-btn"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
