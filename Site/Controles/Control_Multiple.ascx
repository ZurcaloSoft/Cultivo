<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control_Multiple.ascx.cs" Inherits="SGF.Site.Controles.Control_Multiple" %>

<asp:CheckBoxList ID="chk_Clasificadores" runat="server" RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="10" RepeatColumns="7" >
</asp:CheckBoxList>

<asp:RadioButtonList ID="rbt_Clasificador" runat="server" CellPadding="10" CellSpacing="30"
    RepeatDirection="Horizontal" AutoPostBack="true" Visible="true">
</asp:RadioButtonList>