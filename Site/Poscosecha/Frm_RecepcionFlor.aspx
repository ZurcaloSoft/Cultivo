﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_RecepcionFlor.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="SGF.Site.Poscosecha.Frm_RecepcionFlor" %>

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
            background-color: LightGreen; /* Dorado */
            color: black; /* Blanco */
            font-weight: bold;
            border: 1px solid #FF8C00; /* Naranja oscuro */
            text-align: center;
            border-radius: 0%; /* Círculo */
        }

        .special-day-envio {
            background-color: gold; /* cyan*/
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
    <asp:HiddenField ID="hdn_ProveedorID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="RECEPCIÓN DE FLOR" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png" OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend><strong>VISUALIZAR MAPA DE ENVÍOS</strong></legend>
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
                <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText"
                    CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/restart.png" ImagePosition="AboveText"
                    CommandName="Refrescar" Text="Refrescar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Aprobar.png" ImagePosition="AboveText"
                    CommandName="Aprobar" Text="Aprobar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                    CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend><strong>INFORMACION</strong> </legend>
            <asp:Panel ID="pnl_ListadoProyecciones" runat="server" GroupingText="PROYECCIONES RECIBIDAS" Visible="false">
                <table width="100%" heigth="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grid_DetalleProyeccion" runat="server" AllowSorting="false" Visible="true"
                                AutoGenerateColumns="False" CellSpacing="0" GridLines="None"
                                GroupingEnabled="False" OnItemCommand="grid_DetalleProyeccion_ItemCommand" OnItemDataBound="grid_DetalleProyeccion_ItemDataBound">
                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView NoMasterRecordsText="No existen registros." DataKeyNames="ProyeccionID">
                                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <%--                                            <telerik:GridTemplateColumn HeaderText="Nro." ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Nro" runat="server" Text='<%# NumeradorColumna().ToString() %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn DataField="ProveedorNombre" FilterControlAltText="Filter column1 column"
                                            HeaderText="Proveedor" UniqueName="column">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalTallos" FilterControlAltText="Filter column2 column"
                                            HeaderText="Nro. Talos" UniqueName="column">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalMalla" FilterControlAltText="Filter column3 column"
                                            HeaderText="Nro. Mallas" UniqueName="column">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalSobrantes" FilterControlAltText="Filter column4 column"
                                            HeaderText="Nro. Tallos Sueltos" UniqueName="column">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FechaProyeccion" FilterControlAltText="Filter column5 column"
                                            HeaderText="Fecha Proyección" UniqueName="column" DataFormatString="{0:dd-MM-yyyy}">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Ver Detalle" Groupable="false" ItemStyle-Width="1px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDetalle" runat="server" CausesValidation="false" CommandName="editar"
                                                    TabIndex="1" CommandArgument='<%# Eval("ProyeccionID") %>' ImageUrl="~/Images/Revisar2.png"
                                                    ToolTip="Visualizar Detalle de la Proyección" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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
            <asp:Panel ID="pnl_Cabecera" runat="server" GroupingText="RESUMEN DE PROYECCIÓN" Visible="false">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <telerik:RadLabel ID="lbl_Mensaje" runat="server" Text="PROYECCIÓN DE " Font-Bold="true" Font-Size="Larger"></telerik:RadLabel>
                        </td>
                    </tr>
                </table>
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
                            <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true" Width="250px"></telerik:RadTextBox>
                        </td>
                        <td>Observaciones:</td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_Observaciones" runat="server" TextMode="MultiLine" Height="30px" Width="400px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btn_AprobarRecepcion" runat="server" Text="Aprobar Recepción" Height="40px"
                                OnClientClicked="bloquearPantalla" Visible="true" Style="text-align: right" OnClick="btn_AprobarRecepcion_Click">
                                <Icon PrimaryIconUrl="~/Images/grabar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                            </telerik:RadButton>

                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnl_Detalle" runat="server" GroupingText="<strong>DETALLE DE PROYECCIÓN</strong>" Visible="false" s>
                <table>
                    <tr>
                        <td>Proveedor: </td>
                        <td>
                            <telerik:RadComboBox ID="cmb_Proveedor" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="cmb_Proveedor_SelectedIndexChanged"></telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadButton ID="btn_Grabar" runat="server" Text="Grabar Recepción" Height="40px" OnClick="btn_Grabar_Click"
                                OnClientClicked="bloquearPantalla" Visible="true" Style="text-align: right">
                                <Icon PrimaryIconUrl="~/Images/grabar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <table id="table_ProyeccionGeneral" runat="server" visible="false" width="100%">
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
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
