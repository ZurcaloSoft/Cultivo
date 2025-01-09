<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_ActividadesMapaCultivo.aspx.cs" Inherits="SGF.Cultivo.Frm_ActividadesMapaCultivo"
    MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ACTIVIDADES MAPA DE CULTIVO</title>
    <style type="text/css">
        html, body, form {
            padding: 0;
            margin: 0;
            height: 100%;
        }

        body {
            font: normal 11px Arial, Verdana, Sans-serif;
        }

        fieldset {
            height: 100%;
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

            //oArg.PersonaID = document.getElementById('hdn_PersonaID').value;
            //oArg.PersonaNombre = document.getElementById('hdn_PersonaNombre').value;
            //oArg.PersonaIdentificacion = document.getElementById('hdn_PersonaIdentificacion').value;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            //Close the RadWindow and send the argument to the parent page
            //if (!oArg.PersonaID) {
            //    alert("Debe seleccionar una Persona");
            //}
            //else {
            //    oWnd.close(oArg);
            //}
            oWnd.close(oArg);
        }
        //Funciones para bloquear la pantalla al momento de procesar
        function bloquearPantallaBloque() {
            // Mostrar el overlay para bloquear la pantalla
            document.getElementById("overlayPlant").style.display = "block";
        }
        function bloquearPantalla() {
            // Mostrar el overlay para bloquear la pantalla
            document.getElementById("overlay").style.display = "block";
        }

        function desbloquearPantalla() {
            // Ocultar el overlay (si es necesario desde el cliente)
            document.getElementById("overlay").style.display = "none";
        }
    </script>
    <style type="text/css">
        .horizontal .rlbItem {
            float: left;
            margin-right: 10px; /* Espacio entre los elementos */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="manager" runat="server">
        </telerik:RadScriptManager>
        <asp:HiddenField ID="hdn_TypeID" runat="server" />
        <asp:HiddenField ID="hdn_ItemCultivoID" runat="server" />
        <asp:HiddenField ID="hdn_PersonaID" runat="server" />
        <asp:HiddenField ID="hdn_PersonaNombre" runat="server" />
        <asp:HiddenField ID="hdn_PersonaIdentificacion" runat="server" />
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label runat="server" ID="lbl_Titulo" Text="REGISTRAR ACTIVIDADES DEL MAPA DE CULTIVO"
                        Font-Size="Large" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <telerik:RadTabStrip ID="tab_Container" runat="server" MultiPageID="tab_pages" SelectedIndex="3" OnTabClick="tab_Container_TabClick">
            <Tabs>
                <telerik:RadTab runat="server" Text="Información General" PageViewID="tab_Informacion">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Administrar Cultivo" PageViewID="tab_Siembra">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Registrar Actividades" PageViewID="tab_Actividades">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Distribución de Cuadrantes" PageViewID="tab_Distribucion" Selected="True">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Administrar Suelo" PageViewID="tab_Estados">
                </telerik:RadTab>
                <telerik:RadTab runat="server" Text="Actualizar Valores" PageViewID="tab_Valores">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="tab_pages" runat="server" SelectedIndex="3" BorderColor="Gray"
            BorderStyle="Solid" BorderWidth="1px">
            <telerik:RadPageView ID="tab_Informacion" runat="server" Selected="true" Height="100%" Width="100%">
                <br />
                <fieldset>
                    <legend><strong>INFORMACION GENERAL</strong></legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfNombreBloque" Text="Bloque:" Visible="false"></asp:Label>
                            </td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="cmb_InfBloque" runat="server" Width="250px" Visible="false" Enabled="false"></telerik:RadComboBox>
                            </td>
                            <td colspan="3">
                                <asp:ImageButton ID="btn_InfActualizaBloque" runat="server" ToolTip="Actualizar Nro. de Bloque" ImageUrl="~/Images/Grabar.png" Visible="false" OnClick="btn_InfActualizaBloque_Click" />
                                <asp:CheckBox ID="chk_InfActualizarBloque" runat="server" Text="Actualizar Nro. Bloque" AutoPostBack="true" RenderMode="Lightweight" Visible="false" OnCheckedChanged="chk_InfActualizarBloque_CheckedChanged" />

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfAream2" Text="Área m2:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfAream2" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfNroPlantas" Text="Cantidad Plantas:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfNroPlantas" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfInicioBloque" Text="Fecha Inicio Bloque:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfFechaInicioBloque" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfCamasSembradas" Text="Cantidad Camas Sembradas:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfCamasSembradas" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfCamasInjertadas" Text="Cantidad Camas Injertadas:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfCamasInjertadas" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfCamasLibres" Text="Cantidad Camas Libres:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfCamasLibres" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfFechaSiembra" Text="Fecha de Siembra:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfFechaSiembra" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfFechaInjerto" Text="Fecha de Injerto:"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_InfFechaInjerto" Width="100px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfVariedadSembrada" Text="Variedad Sembrada:"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:DataList ID="dl_InfVariedadSembrada" runat="server" RepeatDirection="Vertical" BorderStyle="Double" OnItemDataBound="dl_InfVariedadSembrada_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_ID" runat="server" />
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txt_InfNombreVariedad" Width="200px" ReadOnly="true"></telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txt_InfCamasVariedad" Width="400px" Height="40px" TextMode="MultiLine" ReadOnly="true"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <SeparatorTemplate>
                                        <hr />
                                    </SeparatorTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_InfResponsables" Text="Responsables:"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:DataList ID="dl_InfResponsable" runat="server" RepeatDirection="Vertical" OnItemDataBound="dl_InfResponsable_ItemDataBound" BorderStyle="Double">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_ID" runat="server" />
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txt_InfNombreResponsable" Width="200px" ReadOnly="true"></telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txt_InfCamasResponsable" Width="400px" Height="40px" TextMode="MultiLine" ReadOnly="true"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <%--                                    <SeparatorTemplate>
                                        <hr />
                                    </SeparatorTemplate>--%>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </telerik:RadPageView>

            <telerik:RadPageView ID="tab_Siembra" runat="server" Height="100%" Width="100%">

                <!-- Overlay para bloquear la pantalla -->
                <div id="overlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.5); z-index: 1000; text-align: center;">
                    <div style="position: relative; top: 50%; transform: translateY(-50%); color: white; font-size: 20px;">
                        Procesando...   
                        <br />
                        <img src="../Images/Spinner.gif" alt="Procesando..." />
                    </div>
                </div>
                <br />
                <asp:Panel ID="pnl_SiembraLibres" runat="server" GroupingText="<strong>SIEMBRA DEL PATRÓN</strong>" Visible="false">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_FechaSiembraLibres" Text="Fecha de Siembra: "> </asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtp_FechaSiembra" runat="server"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_ItemsCultivo" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal" OnItemCreated="rlb_ItemsCultivo_ItemCreated">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btn_Sembrar" runat="server" Text="Sembrar" Height="40px" OnClick="btn_Sembrar_Click" OnClientClicked="bloquearPantalla">
                                    <Icon PrimaryIconUrl="~/Images/garden.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea Sembrar las camas seleccionadas?" UseRadConfirm="false" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_SiembraPatron" runat="server" GroupingText="<strong>INJERTO DE VARIEDAD</strong>" Visible="false">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_TextoCultivo" Text="Variedad para Injertar"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmb_Variedad" runat="server" Width="200px"></telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_FechaInjerto" Text="Fecha de Injerto: "> </asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtp_FechaInjerto" runat="server"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_ItemsSembrar" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadButton ID="btn_Injertar" runat="server" Text="Injertar" Height="40px" OnClick="btn_Injertar_Click" OnClientClicked="bloquearPantalla">
                                    <Icon PrimaryIconUrl="~/Images/injerto.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea Injertar las camas seleccionadas?" UseRadConfirm="false" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_SiembraInjerto" runat="server" GroupingText="<strong>CIERRE DE SIEMBRA</strong>" Visible="false">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_ItemsInjertar" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadButton ID="btn_CierreSiembra" runat="server" Text="Cierre Siembra" Height="40px" OnClientClicked="bloquearPantalla" OnClick="btn_CierreSiembra_Click">
                                    <Icon PrimaryIconUrl="~/Images/construccion.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea ejecutar el Cierre de Siembra?" UseRadConfirm="false" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="tab_Actividades" runat="server" Height="100%" Width="100%">
                <br />
                <asp:Panel ID="pnl_RegistroActividades" runat="server" GroupingText="REGISTRO DE ACTIVIDADES" Width="100%" Height="100%">
                    <table width="100%" heigth="100%">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label2" Text="Fecha de Registro: "> </asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtp_FechaRegistro" runat="server"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label1" Text="Tipo de Actividad: "> </asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmb_TipoActividad" runat="server" Width="300px"></telerik:RadComboBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_TextoItemCultivo" Text="Descripción"> </asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_DescripcionActividad" TextMode="MultiLine" Width="300px" Height="40px"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btn_RegistrarActividad" runat="server" Text="Registrar Actividad" Height="40px" OnClick="btn_RegistrarActividad_Click">
                                    <Icon PrimaryIconUrl="~/Images/Agregar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%" heigth="100%">
                        <tr>
                            <td>
                                <telerik:RadGrid ID="grid_DetalleActividades" runat="server" AllowSorting="false" Visible="true"
                                    AutoGenerateColumns="False" CellSpacing="0" GridLines="None"
                                    GroupingEnabled="False">
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView NoMasterRecordsText="No existen registros." DataKeyNames="ActividadesMapaCultivoID">
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Nro." ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Nro" runat="server" Text='<%# NumeradorColumna().ToString() %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn DataField="ActividadNombre" FilterControlAltText="Filter column1 column"
                                                HeaderText="Nombre Actividad" UniqueName="column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Descripcion" FilterControlAltText="Filter column2 column"
                                                HeaderText="Descripción" UniqueName="column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaActividad" FilterControlAltText="Filter column3 column"
                                                HeaderText="Fecha Actividad" UniqueName="column" DataFormatString="{0:dd-MM-yyyy}">
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
                    </table>
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="tab_Distribucion" runat="server" Height="100%" Width="100%" Visible="false">
                <br />
                <asp:Panel ID="pnl_DistribucionCuadrantes" runat="server" GroupingText="DISTRIBUCIÓN DE CUADRANTES" Width="100%" Height="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_Responsable" Text="Responsable"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmb_Responsable" runat="server" Width="300px"></telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lbl_FechaAsignacion" Text="Fecha Asignación de Cuadrante:"> </asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dtp_FechaAsignacionCuadrante" runat="server"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_AsignacionCuadrante" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadButton ID="btn_AsignarResponsable" runat="server" Text="Asignar Responsable" Height="40px" OnClick="btn_AsignarResponsable_Click">
                                    <Icon PrimaryIconUrl="~/Images/Renovacion.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea Asignar la Distribución de Cuadrantes?" UseRadConfirm="false" />
                                </telerik:RadButton>
                                <telerik:RadButton ID="btn_ReasignarDistribucion" runat="server" Text="Reasignar Distribución" Height="40px" OnClick="btn_ReasignarDistribucion_Click">
                                    <Icon PrimaryIconUrl="~/Images/ReCargar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea Limpiar la Distribución?" UseRadConfirm="false" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="tab_Estados" runat="server" Height="100%" Width="100%" Visible="false">
                <br />
                <asp:Panel ID="pnl_Activos" runat="server" GroupingText="<strong>DESHABILITAR ESPACIO DE SUELO</strong>" Visible="true">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_ItemsActivos" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btn_Deshabilitar" runat="server" Text="Eliminar Espacio" Height="40px" OnClientClicked="bloquearPantalla" OnClick="btn_Deshabilitar_Click">
                                    <Icon PrimaryIconUrl="~/Images/construccion.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea Eliminar el espacio de suelo seleccionado?" UseRadConfirm="false" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnl_Pasivos" runat="server" GroupingText="<strong>HABILITAR ESPACIO DE SUELO</strong>" Visible="true">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_ItemsPasivos" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btn_Habilitar" runat="server" Text="Habilitar Espacio" Height="40px" OnClientClicked="bloquearPantalla" OnClick="btn_Habilitar_Click">
                                    <Icon PrimaryIconUrl="~/Images/carretilla.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                                    <ConfirmSettings ConfirmText="Está seguro que desea Habilitar el espacio de suelo seleccionado?" UseRadConfirm="false" />
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="tab_Valores" runat="server" Height="100%" Width="100%" Visible="false">
                <br />
                <asp:Panel ID="pnl_Valores" runat="server" GroupingText="<strong>ACTUALIZACIÓN DE VALORES</strong>" Visible="true">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadListBox ID="rlb_ItemsValores" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="100%" CssClass="horizontal">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </telerik:RadPageView>

        </telerik:RadMultiPage>
        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
            AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
        </telerik:RadNotification>
    </form>
</body>
</html>
