<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SliderBar.ascx.cs" Inherits="TechTracking.UserControls.SliderBar" %>
<link rel="stylesheet" type="text/css" href="../App_Themes/Default/css/jquery-ui.css" />
<script src="../Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
<%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />--%>
<%--<script src="http://code.jquery.com/jquery-1.8.3.js" type="text/javascript"></script>
<script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js" type="text/javascript"></script>--%>
<%--<link rel="stylesheet" href="/resources/demos/style.css" />--%>
<link href="../App_Themes/Default/css/style.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #slider
    {
        text-align: center !important; /*  width: 300px !important;*/
    }
    .ui-state-default
    {
        background: url("../App_Themes/Default/images/paging_h.png") repeat-x scroll 50% 50% #E6E6E6 !important;
        width: 15px !important;
    }
    .ui-widget-header
    {
        background: none;
    }
</style>
<script type="text/javascript">
    // $(function () {
    //        $('.contSlider').each(function (i, obj) {
    //            var currentValue = $(obj).find('.currentValue')
    //            var slider = $(obj).find('.slider')
    //            slider.slider({
    //                max: 10,
    //                value: $("#hdnVal").attr("value"),
    //                min: 0,
    //                slide: function (event, ui) {
    //                    $(this).parent().parent().parent().find("#lblVal").text(ui.value);
    //                    $(this).parent().parent().parent().find("#hdnVal").attr("value", ui.value);
    //                    alert($("#hdnVal").attr("value"));
    //                }
    //            });
    //        });

    // });

    function setSliderValue(val) {
        debugger;
        //$(document).ready(function () {
        $('.pnlslider').slider({

            range: "min",
            value: $("#hdnVal").attr("value"),
            min: 1,
            max: 10,
            slide: function (event, ui) {
                $(this).parent().parent().parent().find("#lblVal").text(ui.value);
                $(this).parent().parent().parent().find("#hdnVal").attr("value", ui.value);
            }
        });

    }
    //    function setSliderValue(val) {
    //        $('.contSlider').each(function (i, obj) {
    //            var currentValue = $(obj).find('.currentValue')
    //            var slider = $(obj).find('.slider')
    //            slider.slider({
    //                max: 10,
    //                value: val,
    //                min: 0,
    //                slide: function (event, ui) {
    //                    $(this).parent().parent().parent().find("#lblVal").text(ui.value);
    //                    $(this).parent().parent().parent().find("#hdnVal").attr("value", ui.value);
    //                   // alert($("#hdnVal").attr("value"));
    //                }
    //            });
    //        });
    //    }
  
</script>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" CssClass="SliderControl" ID="search_bar">
            <%-- <table cellpadding="0" cellspacing="0" border="0" width="100%" class="contSlider">
        <tr>
            <td valign="top" width="180">
                <div class="slider">
                </div>
            </td>
            <td align="center" valign="top" width="25">
                <asp:Label ID="lblVal" runat="server" CssClass="currentValue" ClientIDMode="Static"></asp:Label>
                <asp:HiddenField ID="hdnVal" runat="server" ClientIDMode="Static" />
            </td>
        </tr>
    </table>--%>
            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="contSlider">
                <tr>
                    <td valign="top" width="180">
                        <div class="pnlslider">
                        </div>
                    </td>
                    <td align="center" valign="top" width="25">
                        <asp:Label ID="lblVal" runat="server" CssClass="currentValue" ClientIDMode="Static"></asp:Label>
                        <asp:HiddenField ID="hdnVal" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
