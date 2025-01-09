<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site_Gen.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SGF.Site.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center">
        <tr>
            <td>Nombre de Usuario:
            </td>
            <td>
                <telerik:radtextbox id="txt_Username" runat="server" labelwidth="64px"
                    resize="None" width="160px">
                </telerik:radtextbox>
            </td>
        </tr>
        <tr>
            <td>Contraseña:
            </td>
            <td>
                <telerik:radtextbox id="txt_Password" runat="server" textmode="Password">
                </telerik:radtextbox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <telerik:radbutton id="btn_Ingresar" runat="server" text="Ingresar" OnClick="btn_Ingresar_Click">
                </telerik:radbutton>
            </td>
        </tr>
    </table>
</asp:Content>
