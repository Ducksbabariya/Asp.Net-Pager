<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddEditTechnician.aspx.cs" Inherits="TechTracking.Admin.AddEditTechnician" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style type="text/css">
        .tblBorderAll {
            border: none;
            font-size: 11px !important;
            padding: 2px 3px;
            height: 18px;
            text-align: center;
            vertical-align: middle;
        }

        .none {
            display: none;
        }

        .ui-widget-overlay {
            position: fixed !important;
            top: 0 !important;
            left: 0 !important;
        }

        #loading {
            position: relative;
            display: none;
            background-color: White;
            font-weight: bold;
            vertical-align: top;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function OnAirportSelected(source, eventArgs) {

            $("#txtNearestMajorCity").attr('value', eventArgs.get_value());
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


        function opPopupImage(height, width, title) {

            var imgupload = document.getElementById("<%= imgUpload.ClientID %>");
            $("#imgEnlarge").attr("src", imgupload.src);

            var left = ($(window).width() - width) / 2;
            var top = ($(window).height() - height) / 2;
            $("#lblPopupTitle1").html(title);
            $('#boxpopup1').css({
                position: 'fixed',
                top: top,
                left: left,
                width: width + 'px',                 // adjust width
                height: height + 'px',               // adjust height,
                zIndex: '99999'
            });
            $("#boxpopup1").show();
            $("#boxpopup1").dialog('open');

            return false;
        }
        function closePopup() {
            $("#ifPopup").attr("src", "");
            $("#boxpopup").hide();
            $("#boxpopup").dialog('close');
            $("#boxpopup").dialog('destroy');
            return false;
        };
        function closePopup1() {
            $("#ifPopup").attr("src", "");
            $("#boxpopup1").hide();
            $("#boxpopup1").dialog('close');
            $("#boxpopup1").dialog('destroy');
            return false;
        };
        $(document).ready(function () {
            $('.close-btn1').click(function () {
                $("#ifPopup").attr("src", "");
                $("#boxpopup1").dialog('close');
                $("#boxpopup1").dialog('destroy');
                $("#boxpopup1").hide();
                return false;

            });
            $(".ui-dialog-titlebar").hide();
            return false;

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="boxpopup1" class="backside-box" style="position: fixed; display: none">
        <div class="hd">
            <h2>
                <asp:Label ID="lblPopupTitle1" ClientIDMode="Static" Text="" runat="server" />
            </h2>
            <a href="javascript:void(0);" onclick="closePopup1();" class="close-btn">
                <img src="../App_Themes/Default/images/ic_close.png" alt="" /></a>
        </div>
        <div class="box-cont-dash">
            <div id="dvimg">
                <img id="imgEnlarge" src="" height="380" width="420" />
            </div>
        </div>
    </div>
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
    <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <asp:Panel runat="server" DefaultButton="btnSubmit">
        <div class="shadow-box large">
            <div class="box-hd">
                <h2>
                    <asp:Label Text="Add Technicians" ID="lblHeaderLabel" runat="server" />
                </h2>
                <uc:TechLinks ID="ucTechLinks" runat="server" />
            </div>
            <div class="filter-panel">
                <span class="headeing" style="float: left; margin: 0px">Please Input Technician Information
                </span>
                <div class="filter-right">
                    <a class="view-btn" href="TechnicianListing.aspx"><span class="back">Back</span></a>
                </div>
            </div>
            <div style="margin: 5px; opacity: 1">
                <uc:MessageBox runat="server" ID="ucMessageBox" />
            </div>
            <div class="box-cont-common" style="background-color: White">
                <div class="admin-cont">
                    <table border="0" cellpadding="10" cellspacing="10" width="100%" id="tblLogin">
                        <tr>
                            <td width="40%" align="left" valign="top">
                                <table border="0" cellspacing="3" cellpadding="2">
                                    <tr>
                                        <td align="right">First Name:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtName" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Last Name:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtLastName" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Address Line 1:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtAddressLine1" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvAddressLine1" runat="server" ControlToValidate="txtAddressLine1"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Address Line 2:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtAddressLine2" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvAddressLine2" runat="server" ControlToValidate="txtAddressLine2"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">City:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtCityOfLocation" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCityOfLocation"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">State:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtState" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Zipcode:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtZipCode" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvZipcode" runat="server" ControlToValidate="txtZipCode"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Username:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtUsername" runat="server" Width="185px" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Password:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtPassword" runat="server" Width="185px" TextMode="Password" autocomplete="off"
                                                MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Email:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtEmail" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                            <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                                                ErrorMessage="*" ValidationGroup="AddEditTechnician" ValidationExpression="^([A-Za-z0-9_\-\.\'])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,6})$" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Social Security Number:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtSSN" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Gender:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RadioButtonList runat="server" ID="rdblstGender" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Male" Selected="True" Value="0" />
                                                <asp:ListItem Text="Female" Value="1" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Approved:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:CheckBox Text="Approve" ID="chkApprove" runat="server" Height="21" Style="line-height: 21px; padding: 3px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">Applied Positions:
                                        </td>
                                        <td align="left" valign="top">
                                            <div style="height: 112px; width: 190px; overflow: auto; border: 1px solid #D4E1E8">
                                                <asp:CheckBoxList runat="server" ID="chklstAppliedPositions">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td align="right" valign="top">
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:HiddenField ID="hdnIsEmailSent" runat="server" />
                                            <asp:Button Text="Save" ID="btnSubmit" CssClass="input-button" runat="server" OnClick="btnSubmit_Click"
                                                ValidationGroup="AddEditTechnician" />
                                            &nbsp;
                                            <asp:Button Text="Save and Email" ID="btnSaveAndEmail" CssClass="input-button" runat="server"
                                                OnClick="btnSaveAndEmail_Click" ValidationGroup="AddEditTechnician" />
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                            <td width="60%" valign="top">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr>
                                        <td align="left" width="120px">
                                            <asp:CheckBox ID="chkbAccessTech" runat="server" Text=" Access tech " />
                                        </td>
                                        <td width="180px">
                                            <asp:CheckBox ID="chkbPassport" runat="server" Text=" Passport " />
                                        </td>
                                        <td rowspan="5" align="center">
                                            <asp:Image ID="imgUpload" Style="border: 1px solid #c3d5df;" runat="server" Height="140"
                                                Width="136" ImageUrl="~/App_Themes/Default/images/upload-photo.jpg" onerror="(this.src = '../App_Themes/Default/images/upload-photo.jpg')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:CheckBox ID="chkbInterview" runat="server" Text=" Interview" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkbSharehotelroom" runat="server" Text=" Share hotel room" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <p class="frm-title">
                                                Heard about Ovation:
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="120px">
                                            <asp:CheckBox ID="chkbFaceBook" runat="server" Text="Facebook" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkbLinkedin" runat="server" Text=" Linkedin" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:CheckBox ID="chkbFriend" runat="server" Text="Friend" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkbRecruiter" runat="server" Text="Recruiter" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Profile Image:
                                            <br>
                                            <asp:FileUpload ID="fluImage" runat="server" Width="200px" size="20" /><br />
                                            <asp:RegularExpressionValidator runat="server" ID="revFluImage" ControlToValidate="fluImage"
                                                ErrorMessage="Image Files Only (.jpg, .bmp, .png, .gif)" ValidationGroup="AddEditTechnician"
                                                ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G)|(b|B)(m|M)(p|P))$" />
                                        </td>
                                        <td align="center" valign="top">
                                            <a class="enlarge" id="a_ClickToEnlarge" runat="server" href="javascript:void(0);"
                                                visible="false" onclick="javascript:return opPopupImage('450','450','Image Enlarge View');">
                                                <img alt="" src="../App_Themes/Default/images/zoom.png">
                                                Click to Enlarge</a> <a href="" runat="server" id="aMailTO" class="enlarge" style="clear: left">
                                                    <img src="../App_Themes/Default/images/mail.png" height="14" width="14" alt="Email" />
                                                    Email </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">Resume:<br />
                                            <asp:FileUpload ID="fluResume" runat="server" />
                                            &nbsp;&nbsp; <a class="enlarge" style="display: inline !important;" id="a_download"
                                                visible="false" runat="server">
                                                <img alt="" src="../App_Themes/Default/images/ic-download.png">Download</a>
                                            &nbsp;&nbsp; <a href="" runat="server" visible="false" id="aLinkdeInURL" target="_blank">LinkedInURL</a>
                                            <br />
                                            <asp:RegularExpressionValidator runat="server" ID="revfluImportantDoc" ControlToValidate="fluResume"
                                                ErrorMessage="(.doc, .pdf, .txt Files Only)" ValidationGroup="AddEditTechnician"
                                                ValidationExpression="^.*\.(doc|DOC|pdf|PDF|txt|TXT|Docx|DOCX)$" />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Cell Phone:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtCellPhone" runat="server" Width="185px" MaxLength="15"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvCellPhone" runat="server" ControlToValidate="txtCellPhone"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">NickName:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtNickName" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNickName" runat="server" ControlToValidate="txtNickName"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Nearest Airport:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtNearestAirport" runat="server" Width="185px" MaxLength="50"></asp:TextBox>
                                            <asp:AutoCompleteExtender runat="server" ID="autoCompleteAirports" TargetControlID="txtNearestAirport"
                                                CompletionInterval="1" ServiceMethod="GetAirportList" ServicePath="AddEditTechnician.aspx"
                                                CompletionSetCount="20" MinimumPrefixLength="1" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                DelimiterCharacters=";" OnClientItemSelected="OnAirportSelected">
                                            </asp:AutoCompleteExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvNearestAirport" runat="server" ControlToValidate="txtNearestAirport"
                                                ErrorMessage="*" ForeColor="Red" Display="Static" ValidationGroup="AddEditTechnician">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label Text="Nearest Major City" runat="server" ID="lblNearestMajorCity" />
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox runat="server" ID="txtNearestMajorCity" Width="185px" MaxLength="50"
                                                ClientIDMode="Static" TabIndex="9" />
                                            <%--<asp:RequiredFieldValidator ID="rfvNearestMajorCity" runat="server" ControlToValidate="txtNearestMajorCity"
                                                Text="*" Display="Static" ErrorMessage="*" ForeColor="Red" ValidationGroup="AddEditTechnician"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">Date of Birth:
                                        </td>
                                        <td colspan="2" align="left" valign="top">
                                            <asp:TextBox ID="txtBirthdate" runat="server" Width="160px"></asp:TextBox>
                                            <asp:CalendarExtender ID="calBirthDate" runat="server" Enabled="True" FirstDayOfWeek="Monday"
                                                Format="MM/dd/yyyy" TargetControlID="txtBirthdate" PopupButtonID="imgBirthDate">
                                            </asp:CalendarExtender>
                                            <asp:Image ID="imgBirthDate" runat="server" ImageUrl="~/App_Themes/Default/images/calendar.gif"
                                                Style="position: relative; top: 5px" />
                                            <asp:CompareValidator ID="cvBirthDate" runat="server" Display="Dynamic" ControlToValidate="txtBirthdate"
                                                Operator="DataTypeCheck" Type="Date" ErrorMessage="*" ToolTip="Invalid Date"
                                                ValidationGroup="AddEditTechnician"></asp:CompareValidator>
                                            <asp:CustomValidator ID="custvalBirthdate" runat="server" ControlToValidate="txtBirthdate"
                                                OnServerValidate="checkdate" ValidationGroup="AddEditTechnician"> </asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">Approved Positions:
                                        </td>
                                        <td colspan="2" align="left" valign="top">
                                            <div style="width: 50%; float: left">
                                                <div style="height: 112px; width: 190px; overflow: auto; border: 1px solid #D4E1E8">
                                                    <asp:CheckBoxList runat="server" ID="chklstApprovedPositions">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="white-space: nowrap; padding: 0">
                                <table border="0" cellpadding="3" cellspacing="2">
                                    <tr>
                                        <td width="15%">&nbsp;
                                        </td>
                                        <td>Shirt Size:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdbShirtSize">
                                                <asp:ListItem Text="small (s)" Value="s" Selected="True" />
                                                <asp:ListItem Text="medium (m)" Value="m" />
                                                <asp:ListItem Text="large (l)" Value="l" />
                                                <asp:ListItem Text="extra large (XL)" Value="xl" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td colspan="2" style="padding: 5px">
                                        <b style="font-size: 20px">Technician References</b> &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton Text="Add More Reference" ID="lnkAddReference" OnClick="lnkAddReference_Click"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <uc:TReference ID="TReference1" runat="Server"></uc:TReference>
                                                </td>
                                                <td>
                                                    <uc:TReference ID="TReference2" runat="Server"></uc:TReference>
                                                </td>
                                                <td>
                                                    <uc:TReference ID="TReference3" runat="Server" Visible="false"></uc:TReference>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <uc:TReference ID="TReference4" runat="Server" Visible="false"></uc:TReference>
                                                </td>
                                                <td>
                                                    <uc:TReference ID="TReference5" runat="Server" Visible="false"></uc:TReference>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </table>
                </div>
                <asp:Button Text="Save and Email" ID="btnSaveAndEmail" CssClass="input-button" runat="server"
                    OnClick="btnSaveAndEmail_Click" ValidationGroup="AddEditTechnician" Style="border: none; cursor: pointer; float: right; margin-left: 10px" />
                &nbsp;
                <asp:HiddenField ID="hdnIsEmailSent" runat="server" />
                <asp:Button Text="Save" ID="btnSubmit" CssClass="input-button" runat="server" OnClick="btnSubmit_Click"
                    ValidationGroup="AddEditTechnician" Style="border: none; cursor: pointer; float: right; margin-left: 10px" />
                <asp:Button Text="Delete" ID="btnDelete" CssClass="input-button" runat="server" OnClick="btnDelete_Click"
                    Visible="false" Style="border: none; cursor: pointer; float: right; margin-left: 10px" />
                <asp:Button Text="Table" ID="btnTable" CssClass="input-button" runat="server" OnClick="btnTable_Click"
                    Visible="false" Style="border: none; cursor: pointer; float: right; margin-left: 10px" />
                <asp:LinkButton Text="Share" ID="btnShare" CssClass="input-button" runat="server"
                    Visible="false" Style="border: none; cursor: pointer; float: right; margin-left: 10px" />
            </div>
        </div>
    </asp:Panel>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
