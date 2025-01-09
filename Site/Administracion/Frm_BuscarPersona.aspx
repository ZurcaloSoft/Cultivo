<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_BuscarPersona.aspx.cs" Inherits="SGF.Administracion.Frm_BuscarPersona" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <title>Búsqueda de Persona</title>
    <style type="text/css">
        html, body, form {
            padding: 0;
            margin: 0;
            height: 100%;
            background: #f2f2de;
        }

        body {
            font: normal 11px Arial, Verdana, Sans-serif;
        }

        fieldset {
            height: 150px;
        }

        * + html fieldset {
            height: 154px;
            width: 268px;
        }
    </style>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function returnToParent() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            oArg.PersonaID = document.getElementById('hdn_PersonaID').value;
            oArg.PersonaNombre = document.getElementById('hdn_PersonaNombre').value;
            oArg.PersonaIdentificacion = document.getElementById('hdn_PersonaIdentificacion').value;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            //Close the RadWindow and send the argument to the parent page
            if (!oArg.PersonaID) {
                alert("Debe seleccionar una Persona");
            }
            else {
                oWnd.close(oArg);
            }
        }
    </script>
</head>
<body>
  <form id="form1" runat="server">
        <telerik:RadScriptManager ID="manager" runat="server">
        </telerik:RadScriptManager>
        <asp:HiddenField ID="hdn_TypeID" runat="server" />
        <asp:HiddenField ID="hdn_PersonaID" runat="server" />
        <asp:HiddenField ID="hdn_PersonaNombre" runat="server" />
        <asp:HiddenField ID="hdn_PersonaIdentificacion" runat="server" />
       <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label runat="server" ID="lbl_Titulo" Text="BUSCAR PERSONA"
                        Font-Size="Large" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="620px">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left">Buscar por:
                                <telerik:RadRadioButtonList ID="rbt_Parametro" runat="server" Direction="Horizontal">
                                    <Items>
                                        <telerik:ButtonListItem Text="Cédula / RUC" Value="CODIGO" Selected="true" />
                                        <telerik:ButtonListItem Text="Nombre Persona" Value="NOMBRE" />
                                    </Items>
                                </telerik:RadRadioButtonList>
                            </td>
                            <td align="left">
                                <telerik:RadTextBox ID="txt_Filtro" runat="server" Width="250px">
                                </telerik:RadTextBox>
                                <telerik:RadButton ID="btn_Buscar" runat="server" Text="Buscar" OnClick="btn_Buscar_Click">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
<%--                <a href="Frm_Empresa.aspx.designer.cs">Frm_Empresa.aspx.designer.cs</a>--%>
                <td>
                    <telerik:RadGrid ID="grid_Persona" runat="server" Width="100%" Height="350px" AllowSorting="false"
                        AutoGenerateColumns="False" CellSpacing="0" GridLines="None" 
                        GroupingEnabled="False" OnSelectedIndexChanged="grid_Persona_SelectedIndexChanged">
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="No existen registros." DataKeyNames="PersonaID, Identificacion, NombrePersona">
                            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                            <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Identificacion" FilterControlAltText="Filter column column"
                                    HeaderText="Cédula" UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NombrePersona" FilterControlAltText="Filter column column"
                                    HeaderText="Nombre" UniqueName="column">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoPersonaNombre" FilterControlAltText="Filter column column"
                                    HeaderText="Tipo Persona" UniqueName="column">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu EnableImageSprites="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <%-- <tr>
            <td align="right">
                <telerik:RadButton ID="btn_OK" runat="server" OnClientClicking="returnToParent" Text="OK">
                </telerik:RadButton>
            </td>
        </tr>--%>
        </table>
        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
            AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
        </telerik:RadNotification>
    </form>
</body>
</html>
