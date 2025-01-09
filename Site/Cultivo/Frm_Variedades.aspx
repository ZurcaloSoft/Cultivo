<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Variedades.aspx.cs" Inherits="SGF.Site.Cultivo.Frm_Variedades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AbrirModalPersona(_id) {
            debugger;
            var oManager = window.radopen("Frm_BuscarProveedor.aspx?type=" + _id, "wnd_Persona");
            oManager.setSize(650, 500); //Width, Height
            oManager.center();
            oManager.SetActive();
            //return false;
        }
        function OnClientClose(oWnd, args) {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                document.getElementById('ContentPlaceHolder1_hdn_PersonaID').value = arg.PersonaID;
                document.getElementById('ContentPlaceHolder1_hdn_PersonaNombre').value = arg.PersonaNombre;
                document.getElementById('ContentPlaceHolder1_hdn_PersonaIdentificacion').value = arg.PersonaIdentificacion;
                //                document.getElementById('ctl00_ContentPlaceHolder1_txt_Observaciones').value = arg.ActividadSecundariaID;
                //                alert(arg.NombreActividad);
                __doPostBack('ctl00$ContentPlaceHolder1$btn_RetornoVentana', '');
            }
        }
        function clientbuttonclick(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "Eliminar")
                args.set_cancel(!confirm('Está seguro que desea eliminar el registro?'));
        }
    </script>
    <style>
        .customRadListBox .rlbList {
            display: flex; /* Establece un contenedor flexible */
            flex-direction: row; /* Alinea los elementos en fila */
            gap: 10px; /* Espaciado entre elementos */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_PersonaID" runat="server" />
    <asp:HiddenField ID="hdn_PersonaNombre" runat="server" />
    <asp:HiddenField ID="hdn_PersonaIdentificacion" runat="server" />
    <asp:HiddenField ID="hdn_VariedadID" runat="server" />
    <telerik:RadWindowManager ID="wnd_manager" runat="server" ShowContentDuringLoad="false"
        VisibleStatusbar="false" ReloadOnShow="true" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="wnd_Persona" runat="server" Width="650" Height="500" NavigateUrl="~/Frm_BuscarProveedor.aspx" Modal="true"
                OnClientClose="OnClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="VARIEDADES" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend>Criterios de Búsqueda</legend>
            <table width="100%">
                <tr>
                    <td width="150px" align="left">Código:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarCodigo" runat="server" Width="80px"></telerik:RadTextBox>
                    </td>
                    <td width="150px" align="left">Nombre Variedad:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <%--                    <td width="150px" align="left">Tipo Usuario:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarTipoUsuario" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>--%>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                    </td>
                </tr>

            </table>
            <telerik:RadGrid ID="gv_Variedad" runat="server" AutoGenerateColumns="false" OnItemCommand="gv_Variedad_ItemCommand" OnNeedDataSource="gv_Variedad_NeedDataSource" ShowGroupPanel="true" AllowFilteringByColumn="True">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="VariedadID" GroupLoadMode="Client">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Codigo" FilterControlAltText="Filter Codigo column"
                            HeaderText="Código" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NombreVariedad" FilterControlAltText="Filter NombreVariedad column"
                            HeaderText="Nombre Variedad" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TipoFlor" FilterControlAltText="Filter TipoFlor column" AllowFiltering="False"
                            HeaderText="Tipo de Flor" UniqueName="column3" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter Color column" AllowFiltering="False"
                            HeaderText="Color" UniqueName="column4" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Calidad" FilterControlAltText="Filter Calidad column" AllowFiltering="False"
                            HeaderText="Calidad" UniqueName="column5" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LongitudMax" FilterControlAltText="Filter LongitudMax column" AllowFiltering="False"
                            HeaderText="Longitud Máx." UniqueName="column6" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LongitudMin" FilterControlAltText="Filter LongitudMin column" AllowFiltering="False"
                            HeaderText="Longitud Min." UniqueName="column7" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px" AllowFiltering="False">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("VariedadID") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
            </telerik:RadGrid>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_DatosVariedad" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnButtonClick="tool_principal_ButtonClick"
            OnClientButtonClicking="clientbuttonclick">
            <Items>
                <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText"
                    CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                    CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend><strong>DATOS DE LA VARIEDAD</strong></legend>
            <table width="100%">
                <tr>
                    <td>Código:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server"></telerik:RadTextBox>
                    </td>
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_NombreVariedad" runat="server" Width="200px"></telerik:RadTextBox>
                    </td>
                    <td>Obtentor</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_ObtentorCedula" runat="server" Width="100px" ReadOnly="true"></telerik:RadTextBox>-
                    <telerik:RadTextBox ID="txt_ObtentorNombre" runat="server" Width="200px" ReadOnly="true"></telerik:RadTextBox>
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="btn_RetornoVentana" runat="server" OnClick="btn_RetornoVentana_Click"
                            ImageUrl="~/Images/dot_clear.gif" />
                        <asp:HyperLink ID="btn_MostrarPersona" runat="server" ToolTip="Visualizar datos de Recepción" NavigateUrl="javascript:AbrirModalPersona('3');"
                            ImageUrl="~/Images/BuscarModal.png" Height="20px" />
                    </td>
                </tr>
                <tr>
                    <td>Color:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Color" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Calidad:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Calidad" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Tipo de Flor:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoFlor" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Rotación:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_Rotacion" runat="server" MinValue="0" Value="0">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Nro. Tallos por Malla</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_NroTalloPorMalla" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Longitud Mínima:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_LongitudMinima" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Longitud Máxima:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_LongitudMaxima" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>Tamaño de Botón:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_TamanoBoton" runat="server" MinValue="0" Value="0" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator=",">
                            <NumberFormat DecimalDigits="2" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Indice Prod. Mensual:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_IndiceProdMensual" runat="server" MinValue="0" Value="0" NumberFormat-DecimalSeparator="." NumberFormat-GroupSeparator=",">
                            <NumberFormat DecimalDigits="2" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>Nro. Pétalos:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroPetalos" runat="server" MinValue="0" Value="0">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>Ciclo:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_Ciclo" runat="server" MinValue="0" Value="0">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>Días Florero:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_DiasFlorero" runat="server" MinValue="0" Value="0">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Tallos por Malla:</td>
                    <td colspan="5">
                        <fieldset>
                            <legend><strong>Tallos por Malla Adicionales</strong></legend>
                            <telerik:RadListBox ID="rlb_TallosPorMalla" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="500px" CssClass="customRadListBox"
                                Height="50px" ToolTip="Tallos Por Malla Adicionales">
                            </telerik:RadListBox>
                            <br />
                        </fieldset>
                    </td>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" Enabled="false"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Observaciones:</td>
                    <td colspan="7">
                        <telerik:RadTextBox ID="txt_Observaciones" runat="server" Width="600px" Height="50px" TextMode="MultiLine"></telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend><strong>DATOS DEL PRODUCTO</strong></legend>
            <table width="100%">
                <tr>
                    <td>
                        <telerik:RadCheckBox ID="chk_CrearProducto" runat="server" Text="CREAR PRODUCTO" Checked="false" AutoPostBack="true" Font-Bold="True" ForeColor="Blue" OnClick="chk_CrearProducto_Click"></telerik:RadCheckBox>
                        <br />
                        <telerik:RadCheckBox ID="chk_Mercado" runat="server" Text="Agregar Mercado" Checked="false" AutoPostBack="true" Font-Bold="True" OnClick="chk_Mercado_Click" Visible="false"></telerik:RadCheckBox>
                        <telerik:RadCheckBox ID="chk_Pais" runat="server" Text="Agregar País" Checked="false" AutoPostBack="true" Font-Bold="True" OnClick="chk_Pais_Click" Visible="false"></telerik:RadCheckBox>
                        <telerik:RadButton ID="btn_GenerarProducto" runat="server" Text="Generar Productos" Height="40px" Visible="false" OnClick="btn_GenerarProducto_Click">
                            <Icon PrimaryIconUrl="~/Images/Cotizar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnl_DatosProducto" runat="server" GroupingText="Configurar Productos" Visible="false">
                <table width="100%">
                    <tr>
                        <td width="60%">
                            <fieldset>
                                <legend>PARÁMETROS OBLIGATORIOS</legend>
                                <table width="100%">
                                    <tr>
                                        <td width="33%" align="center">
                                            <fieldset>
                                                <legend><strong>Tipo de Calidad</strong> de Calidad</legend>
                                                <telerik:RadListBox ID="rlb_TipoCalidad" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="true" Width="200px"
                                                    Height="300px" ToolTip="Tipo de Calidad">
                                                </telerik:RadListBox>
                                                <br />
                                            </fieldset>
                                        </td>
                                        <td></td>
                                        <td width="33%" align="center" valign="top">
                                            <fieldset>
                                                <legend><strong>Longitud en cm</strong></legend>
                                                <telerik:RadListBox ID="rlb_Longitud" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="true" Width="200px"
                                                    Height="300px" ToolTip="Longitud">
                                                </telerik:RadListBox>
                                                <br />
                                            </fieldset>
                                        </td>
                                        <td></td>
                                        <td width="33%" valign="top" align="center">
                                            <fieldset>
                                                <legend><strong>Nro. de Tallos</strong></legend>
                                                <telerik:RadListBox ID="rlb_Tallos" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" Width="200px"
                                                    Height="300px" ToolTip="Nro. de Tallos">
                                                </telerik:RadListBox>
                                                <br />
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td width="40%">
                            <asp:Panel ID="pnl_ParametrosAdicionales" runat="server" GroupingText="PARÁMETROS OPCIONALES" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td id="td_Mercado1" runat="server" visible="false"></td>
                                        <td width="50%" valign="top" id="td_Mercado" runat="server" visible="true" align="center">
                                            <fieldset>
                                                <legend><strong>Mercado</strong></legend>
                                                <telerik:RadListBox ID="rlb_Mercado" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="180px" RenderMode="Lightweight" AutoPostBack="true"
                                                    Height="300px" OnSelectedIndexChanged="rlb_Mercado_SelectedIndexChanged" ToolTip="Mercado">
                                                </telerik:RadListBox>
                                                <br />
                                            </fieldset>
                                        </td>
                                        <td id="td_Pais1" runat="server" visible="false"></td>
                                        <td width="50%" valign="top" id="td_Pais" runat="server" visible="true" align="center">
                                            <fieldset>
                                                <legend><strong>País</strong></legend>
                                                <telerik:RadListBox ID="rlb_Pais" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="180px" RenderMode="Lightweight"
                                                    Height="300px" ToolTip="Países">
                                                </telerik:RadListBox>
                                                <br />
                                            </fieldset>
                                        </td>

                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnl_DetalleProducto" runat="server" GroupingText="Detalle de Productos" Visible="true">
                <table width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="gv_Producto" runat="server" AutoGenerateColumns="false" ShowGroupPanel="true" AllowFilteringByColumn="True" OnItemCommand="gv_Producto_ItemCommand" OnNeedDataSource="gv_Producto_NeedDataSource">
                                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                                    ReorderColumnsOnClient="True">
                                    <Selecting AllowRowSelect="True" />
                                    <Scrolling AllowScroll="True" />
                                </ClientSettings>
                                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="ProductoID" GroupLoadMode="Client">
                                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Codigo" FilterControlAltText="Filter Codigo column"
                                            HeaderText="Código" UniqueName="column1" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter Nombre column"
                                            HeaderText="Nombre Producto" UniqueName="column2" ItemStyle-Width="250px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Calidad" FilterControlAltText="Filter Calidad column" AllowFiltering="False"
                                            HeaderText="Calidad" UniqueName="column3" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Longitud" FilterControlAltText="Filter Longitud column" AllowFiltering="False"
                                            HeaderText="Longitud " UniqueName="column4" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Tallo" FilterControlAltText="Filter Tallo column" AllowFiltering="False"
                                            HeaderText="Tallo" UniqueName="column5" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Mercado" FilterControlAltText="Filter Mercado column" AllowFiltering="False"
                                            HeaderText="Mercado" UniqueName="column6" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pais" FilterControlAltText="Filter Pais column" AllowFiltering="False"
                                            HeaderText="País" UniqueName="column7" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Estado" FilterControlAltText="Filter Estado column" AllowFiltering="False"
                                            HeaderText="Estado" UniqueName="column7" ItemStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <%--                                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px" AllowFiltering="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                                    TabIndex="1" CommandArgument='<%# Eval("ProductoID") %>' ImageUrl="~/Images/edit.png"
                                                    ToolTip="Editar este registro" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>

</asp:Content>
