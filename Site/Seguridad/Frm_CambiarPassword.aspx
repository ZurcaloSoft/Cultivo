<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_CambiarPassword.aspx.cs" Inherits="SGF.Site.Seguridad.Frm_CambiarPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlDatos" runat="server">
        <fieldset>
            <legend>DATOS</legend>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label runat="server" ID="lbl_Titulo" Text="CAMBIO DE CONTRASEÑA" Font-Size="Large" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="50%">
                <tr>
                    <td style="font-weight: 700; text-align: center"></td>
                </tr>
                <tr>
                    <td>Usuario:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtUsuario" runat="server" Enabled="false"
                            MaxLength="20">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Clave Actual:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtClaveAnt" runat="server" TextMode="Password">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Nueva Actual:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtClaveNueva" runat="server" TextMode="Password">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Repita Nueva Clave Actual:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtClaveNueva2" runat="server" TextMode="Password">
                        </telerik:RadTextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="text-align: center">
                        <telerik:RadButton ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" Skin="Glow"
                            Width="80">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>

</asp:Content>
