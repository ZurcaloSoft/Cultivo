<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Persona.aspx.cs" Inherits="SGF.Site.Genérico.Frm_Persona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnl_Buscar" runat="server">
        <fieldset>
            <legend><strong>BUSCAR PERSONA</strong></legend>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_Datos" runat="server">
        <table>
            <tr>
                <td>Tipo de Persona:</td>
                <td>
                    <telerik:RadComboBox ID="cmb_TipoPersona" runat="server">
                        <Items>
                            <telerik:RadComboBoxItem Text="Empleado" Value="1" />
                            <telerik:RadComboBoxItem Text="Cliente" Value="2" />
                            <telerik:RadComboBoxItem Text="Proveedor" Value="3" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>Código:</td>
                <td>
                    <telerik:RadTextBox ID="txt_Codigo" runat="server"></telerik:RadTextBox></td>
            </tr>
            <tr>
                <td>Tipo Identificación:</td>
                <td></td>
                <td>Identificación:</td>
                <td>
                    <telerik:RadTextBox ID="txt_Identificacion" runat="server" MaxLength="10"></telerik:RadTextBox></td>
                <td>Nombre:</td>
                <td>
                    <telerik:RadTextBox ID="txt_Nombre" runat="server" Width="250px"></telerik:RadTextBox></td>
            </tr>
            <tr>
                <td>Email:</td>
                <td>
                    <telerik:RadTextBox ID="txt_Email" runat="server"></telerik:RadTextBox></td>
                <td>Teléfono</td>
                <td>
                    <telerik:RadTextBox ID="txt_Telefono" runat="server" EmptyMessage="(02)1123456"></telerik:RadTextBox></td>
                <td>Celular:</td>
                <td>
                    <telerik:RadTextBox ID="txt_Celular" runat="server"></telerik:RadTextBox></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
