<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_ProyeccionProduccion.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="SGF.Site.Cultivo.Frm_ProyeccionProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function clientbuttonclick(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "Eliminar")
                args.set_cancel(!confirm('Está seguro que desea eliminar el registro?'));
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
    <style>
        /* Estilo para los días deshabilitados */
        .disabled-day {
            color: #ccc; /* Texto gris */
            background-color: #f9f9f9; /* Fondo más claro */
            cursor: not-allowed; /* Cambiar el cursor */
        }

        .special-day {
            background-color: #FFD700; /* Dorado */
            color: #FFFFFF; /* Blanco */
            font-weight: bold;
            border: 1px solid #FF8C00; /* Naranja oscuro */
            text-align: center;
            border-radius: 0%; /* Círculo */
        }

        .special-day-envio {
            background-color: cyan; /* cyan*/
            color: black; /* Negro*/
            font-weight: bold;
            border: 1px solid #00FFFF; /* cyan*/
            text-align: center;
            border-radius: 0%; /* Círculo */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_ProyeccionProduccionID" runat="server" />
    <asp:HiddenField ID="hdn_ProyeccionID" runat="server" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
    <asp:Panel ID="pnl_Buscador" runat="server">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label runat="server" ID="lbl_Titulo" Text="PROYECCION DE LA PRODUCCION"
                        Font-Size="Large" Font-Bold="true"></asp:Label>

                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png" OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend>Visualizar Mapa de Proyección</legend>
            <%--  <table width="100%">
                <tr>
                    <td width="150px" align="left">Proveedor:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarProveedor" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="150px" align="left">Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                    </td>
                </tr>
            </table>--%>
            <telerik:RadGrid ID="gv_Proyeccion" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Proyeccion_ItemCommand" OnNeedDataSource="gv_Proyeccion_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="ProyeccionProduccionID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Identificacion" FilterControlAltText="Filter Identificacion column"
                            HeaderText="Identificación" UniqueName="column2" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter NombrePersona column"
                            HeaderText="Nombre" UniqueName="column3" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"
                            HeaderText="Email" UniqueName="column5" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Celular" FilterControlAltText="Filter Celular column"
                            HeaderText="Celular" UniqueName="column6" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("ProyeccionProduccionID") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
            </telerik:RadGrid>

            <telerik:RadCalendar ID="cal_ProyeccionGeneral" runat="server" AutoPostBack="true" Skin="Outlook" Width="100%" Height="300px" Culture="es-ES"
                EnableEmbeddedSkins="true" EnableEmbeddedBaseStylesheet="true" EnableMonthYearFastNavigation="true" TitleFormat="MMMM yyyy" DayNameFormat="Full" HeaderStyle-Font-Bold="true" HeaderStyle-BorderWidth="1"
                ShowRowHeaders="false" ShowOtherMonthsDays="false" OnSelectionChanged="cal_ProyeccionGeneral_SelectionChanged" OnDayRender="cal_ProyeccionGeneral_DayRender"
                CellAlign="Center" DayStyle-HorizontalAlign="Center" DayOverStyle-HorizontalAlign="Center" CalendarTableStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                SelectedDayStyle-HorizontalAlign="Center" WeekendDayStyle-HorizontalAlign="Center" ViewSelectorStyle-HorizontalAlign="Center"
                CalendarTableStyle-BorderWidth="1" BorderWidth="1" DayStyle-BorderWidth="1" EnableMultiSelect="false">
            </telerik:RadCalendar>

        </fieldset>
    </asp:Panel>
    <!-- Overlay para bloquear la pantalla -->
    <div id="overlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.5); z-index: 1000; text-align: center;">
        <div style="position: relative; top: 50%; transform: translateY(-50%); color: white; font-size: 20px;">
            Procesando...   
            <br />
            <img src="../Images/Spinner.gif" alt="Procesando..." />
        </div>
    </div>
    <asp:Panel ID="pnl_Informacion" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnClientClicked="bloquearPantalla"
            OnButtonClick="tool_principal_ButtonClick">
            <Items>
                <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText" OnClientButtonClicking="bloquearPantalla"
                    CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/restart.png" ImagePosition="AboveText"
                    CommandName="Refrescar" Text="Refrescar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Aprobar.png" ImagePosition="AboveText" OnClientButtonClicking="bloquearPantalla"
                    CommandName="Aprobar" Text="Aprobar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                    CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend><strong>INFORMACION</strong> </legend>
            <fieldset>
                <asp:Panel ID="pnl_Cabecera" runat="server" GroupingText="CABECERA" Visible="false">
                    <table width="100%">
                        <tr>
                            <td>Proveedor: </td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txt_Proveedor" ReadOnly="true" Width="250px"></telerik:RadTextBox>
                            </td>
                            <td>Fecha de Proyección:</td>
                            <td>
                                <telerik:RadDateTimePicker ID="dt_FechaProyeccion" runat="server" Width="200px" Enabled="false" Font-Bold="true"
                                    DateInput-EnabledStyle-HorizontalAlign="Center" MinDate="2016-01-01">
                                </telerik:RadDateTimePicker>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Nro. de Tallos:
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_NroTallos" runat="server" MinValue="0" Value="0" Width="75px" ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>Nro. de Mallas:
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_NroMallas" runat="server" MinValue="0" Value="0" Width="75px" ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>Nro. de Tallos Sueltos:
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_NroTallosSueltos" runat="server" MinValue="0" Value="0" Width="75px" ReadOnly="true">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Estado:
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>Observaciones:</td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_Observaciones" runat="server" TextMode="MultiLine" Height="30px" Width="400px"></telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
            <fieldset>
                <asp:Panel ID="pnl_Detalle" runat="server" GroupingText="DETALLE DE PROYECCIÓN" Visible="false">
                    <table id="table_Radios" runat="server" visible="false" width="100%">
                        <tr>
                            <td>
                                <asp:RadioButton runat="server" ID="rbt_ProyeccionGeneral" Text="Proyección General" GroupName="proyeccion" OnCheckedChanged="rbt_ProyeccionGeneral_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton runat="server" ID="rbt_ProyeccionDetallada" Text="Proyección Detallada" GroupName="proyeccion" OnCheckedChanged="rbt_ProyeccionDetallada_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <table id="table_ProyeccionGeneral" runat="server" visible="true" width="100%">
                        <tr>
                            <td>
                                <asp:DataList ID="dl_ProyeccionGeneral" runat="server" RepeatDirection="Vertical" BorderStyle="Double" OnItemCommand="dl_ProyeccionGeneral_ItemCommand" OnItemDataBound="dl_ProyeccionGeneral_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_ProveedorID" runat="server" />
                                        <asp:HiddenField ID="hdn_ProveedorCodigo" runat="server" />
                                        <asp:HiddenField ID="hdn_EmpresaID" runat="server" />
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadCheckBox runat="server" ID="chk_Proveedor" Text="" Font-Bold="true" CommandName="seleccionar" CommandArgument='<%# Eval("ProveedorID") %>'></telerik:RadCheckBox>
                                                </td>
                                                <td>
                                                    <asp:DataList ID="dl_Variedades" runat="server" RepeatDirection="Vertical" BorderStyle="Dashed" OnItemCommand="dl_Variedades_ItemCommand" OnItemDataBound="dl_Variedades_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdn_ProvID" runat="server" />
                                                            <asp:HiddenField ID="hdn_VariedadID" runat="server" />
                                                            <asp:HiddenField ID="hdn_EmpID" runat="server" />
                                                            <asp:HiddenField ID="hdn_TallosXMalla" runat="server" />

                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadLabel ID="lbl_Variedad" runat="server"></telerik:RadLabel>
                                                                    </td>
                                                                    <td>
                                                                        <td>Nro. de Tallos:
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txt_NroTallos" runat="server" MinValue="0" Value="0" Width="75px" ReadOnly="false">
                                                                                <NumberFormat DecimalDigits="0" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <SeparatorTemplate>
                                                            <hr />
                                                        </SeparatorTemplate>
                                                    </asp:DataList>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <fieldset>
                                                        <legend>Días de Entrega Mensual</legend>
                                                        <telerik:RadCalendar ID="cal_DiasProyeccion" runat="server" AutoPostBack="true" Skin="Outlook" Width="350px" Height="100px"
                                                            EnableEmbeddedSkins="true" EnableEmbeddedBaseStylesheet="true" EnableMonthYearFastNavigation="false" EnableMultiSelect="true"
                                                            DayNameFormat="Short" ShowRowHeaders="false" ShowOtherMonthsDays="false" ShowNavigationButtons="false" ShowFastNavigationButtons="false">
                                                        </telerik:RadCalendar>
                                                    </fieldset>
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
                    </table>
                    <table id="table_ProyeccionDetalle" runat="server" visible="false" width="100%">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><b>DETALLE DE LA PROYECCIÓN: </b></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div align="right">
                                    <asp:LinkButton ID="lnk_AddProyeccionIndividual" runat="server" ToolTip="Añadir nueva proyección" OnClick="lnk_AddProyeccionIndividual_Click">
                                        <asp:Image ID="img_AddProyeccion" runat="server" ImageUrl="~/Images/addItem.png" BorderStyle="None"
                                            BorderColor="Transparent" BorderWidth="0px" />
                                        Agregar Proyección
                                    </asp:LinkButton>
                                </div>
                                <br />
                                <br />
                                <asp:DataList ID="dl_ProyeccionDetallada" runat="server" OnItemCommand="dl_ProyeccionDetallada_ItemCommand" OnItemDataBound="dl_ProyeccionDetallada_ItemDataBound">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>No. &nbsp;&nbsp;
                                            <telerik:RadLabel ID="lbl_Orden" runat="server" />
                                                    <asp:HiddenField ID="hdn_ProveedorID" runat="server" />
                                                    <asp:HiddenField ID="hdn_VariedadID" runat="server" />
                                                    <asp:HiddenField ID="hdn_EmpresaID" runat="server" />
                                                </td>
                                                <td>Variedad:</td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmb_Variedad" runat="server" Width="200px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>Nro. Total de Tallos: 
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_NroTallos" runat="server" Width="70px"
                                                        EnabledStyle-HorizontalAlign="Center" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>Fecha:
                                                </td>
                                                <td colspan="2">
                                                    <telerik:RadDateTimePicker ID="dt_FechaProyeccion" runat="server" Width="200px"
                                                        DateInput-EnabledStyle-HorizontalAlign="Center" MinDate="2016-01-01">
                                                    </telerik:RadDateTimePicker>
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
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnl_DetalleProyeccion" runat="server" GroupingText="DETALLE DE PROYECCIÓN" Visible="false">

                    <table id="table3" runat="server" visible="true" width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:DataList ID="dl_DetalleProyeccion" runat="server" OnItemCommand="dl_DetalleProyeccion_ItemCommand" OnItemDataBound="dl_DetalleProyeccion_ItemDataBound">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadLabel ID="lbl_Proveedor" runat="server" />
                                                    <asp:HiddenField ID="hdn_ProvID" runat="server" />
                                                    <asp:HiddenField ID="hdn_VarID" runat="server" />
                                                    <asp:HiddenField ID="hdn_EmpID" runat="server" />
                                                    <asp:HiddenField ID="hdn_ProyID" runat="server" />
                                                    <asp:HiddenField ID="hdn_ProyProdID" runat="server" />
                                                </td>
                                                <td>Variedad:</td>
                                                <td>
                                                    <telerik:RadLabel ID="lbl_Variedad" runat="server" />
                                                </td>
                                                <td>Nro. de Tallos: 
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_NroTallos" runat="server" Width="70px"
                                                        EnabledStyle-HorizontalAlign="Center" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>Nro. de Mallas: 
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_NroMallas" runat="server" Width="70px" ReadOnly="true"
                                                        EnabledStyle-HorizontalAlign="Center" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>Nro. de Tallos Sueltos: 
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_NroSueltos" runat="server" Width="70px" ReadOnly="true"
                                                        EnabledStyle-HorizontalAlign="Center" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadImageButton ID="btn_Grabar" runat="server" Height="30px" Width="50px" CommandName="update"
                                                        CommandArgument='<%# Eval("ProyeccionProduccionID") %>' OnClientClicked="bloquearPantalla"
                                                        SingleClick="true" SingleClickText="Actualizar Nro. de Tallos..." Font-Bold="true" CssClass="custom-border" Visible="true">
                                                        <Image Url="~/Images/save.png" />
                                                    </telerik:RadImageButton>
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
                    </table>
                </asp:Panel>
            </fieldset>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
