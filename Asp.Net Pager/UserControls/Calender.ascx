<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calender.ascx.cs" Inherits="TechTracking.UserControls.Calender" %>
<asp:Calendar ID="calTechview" runat="server" Width="100px" SelectionMode="None" DayStyle-Font-Size="11px" DayStyle-Height="20" DayHeaderStyle-Height="25"
    DayStyle-CssClass="tblBorderAll" DayHeaderStyle-CssClass="tblBorderAll" NextPrevStyle-ForeColor="Black" 
    TitleStyle-BorderColor="#D5DEE3" TitleStyle-BorderStyle="Solid" TitleStyle-BorderWidth="1px"
    FirstDayOfWeek="Sunday" TitleStyle-CssClass="roundbar-grey" TitleStyle-Height="22px" 
    BorderColor="#D5DEE3" TitleFormat="MonthYear" TitleStyle-BackColor="#DAEBF5"
    TitleStyle-VerticalAlign="Middle" TodayDayStyle-BackColor="Transparent"
    OnDayRender="calTechview_DayRender" NextPrevFormat="CustomText"
    ToolTip="" OtherMonthDayStyle-ForeColor="Gray"></asp:Calendar>
