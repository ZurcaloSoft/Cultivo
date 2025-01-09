<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SGF.Site.Default" %>

<%@ MasterType VirtualPath="~/SGF_Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_UsrID" runat="server" />
    <table style="display: inline-table;" border="0" cellpadding="0" cellspacing="0"
        width="90%">
        <tr>
            <td colspan="6" align="center">
                <div style="width: 830px;">
                    <%--                    <img src="../images/working.jpg" alt="Fondo" style="width: 100%; height: 100%;">--%>
                    <div style="float: left" id="div_Cultivo" runat="server">
                        <a href="_Blank.aspx?Type=D0DF07F1-01B8-44C3-B472-B6AB7110FD76">
                            <img name="BotonesHome_r2_c2" src="Images/botonCultivo.png" width="272" height="186"
                                border="0" id="Img1" alt="" /></a>
                    </div>
                    <div style="float: left" id="div_Poscosecha" runat="server">
                        <a href="_Blank.aspx?Type=259A1102-7C1B-4917-BDEA-60059A3D468F">
                            <img name="BotonesHome_r2_c2" src="Images/botonPoscosecha.png" width="272" height="186"
                                border="0" id="Img2" alt="" /></a>
                    </div>
                    <%--                    <div style="float: left" id="div_Empaque" runat="server" >
                        <a id="lnkEmpaque" runat="server" >
                            <img name="BotonesHome_r2_c2" src="Images/botonEmpaque.png" width="272" height="186"
                                border="0" id="Img3" alt="" /></a>
                    </div>--%>
                    <div style="float: left" id="div_Empaque" runat="server">
                        <a href="_Blank.aspx?Type=34A4F66C-5F98-47FA-A9AB-97855093E20D">
                            <img name="BotonesHome_r2_c2" src="Images/botonEmpaque.png" width="272" height="186"
                                border="0" id="Img3" alt="" /></a>
                    </div>

                    <div style="float: left" id="div_Comercial" runat="server">
                        <a href="_Blank.aspx?Type=A7FD68C3-D7E5-4A80-997F-C576A618A3F6">
                            <img name="BotonesHome_r2_c2" src="Images/botonComercial.png" width="272" height="186"
                                border="0" id="Img4" alt="" /></a>
                    </div>
                    <div style="float: left" id="div_Recepcion" runat="server">
                        <a href="_Blank.aspx?Type=0A007EA0-36C2-43A4-AA67-20457436BFA2">
                            <img name="BotonesHome_r2_c2" src="Images/botonRecepcion.png" width="272" height="186"
                                border="0" id="Img5" alt="" /></a>
                    </div>
                    <div style="float: left" id="div_TH" runat="server">
                        <a href="_Blank.aspx?Type=6DDB5385-8090-4D61-AD8F-62961BBA294F">
                            <img name="BotonesHome_r2_c2" src="Images/botonTH.png" width="272" height="186"
                                border="0" id="Img6" alt="" /></a>
                    </div>
                    <div style="float: left" id="div_Administracion" runat="server">
                        <a href="_Blank.aspx?Type=B44C1F71-21EC-4FD7-AD4C-D6C0F3434CE0">
                            <img name="BotonesHome_r2_c2" src="Images/btncartera.png" width="272" height="186"
                                border="0" id="Img7" alt="" /></a>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
