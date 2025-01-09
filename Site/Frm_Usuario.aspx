<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Usuario.aspx.cs" Inherits="SGF.Site.Frm_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td><strong>USUARIO</strong></td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Busqueda" runat="server">
        <fieldset>
            <legend></legend>
            <table>
                <tr>
                    <td>Código:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" Width="100px"></telerik:RadTextBox>
                    </td>
                    <td>Cédula:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Identificacion" runat="server" Width="100px"></telerik:RadTextBox>
                    </td>
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Nombre" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Estado Civil</td>
                    <td></td>
                    <td>Género</td>
                    <td></td>
                    <td>Fecha Nacimiento:</td>
                    <td></td>
                    <td></td>
                    <td></td>

                </tr>
                <tr>
                    <td>Email:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Email" runat="server" Width="100px"></telerik:RadTextBox>
                    </td>
                    <td>Telf. Convencional:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Convencional" runat="server" Width="100px"></telerik:RadTextBox>
                    </td>
                    <td>Telf. Celular:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Celular" runat="server" Width="100px"></telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
