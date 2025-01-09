<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_ProveedorPrecio.aspx.cs" Inherits="SGF.Site.Compras.Frm_ProveedorPrecio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function clientbuttonclick(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "Eliminar")
                args.set_cancel(!confirm('Está seguro que desea eliminar el registro?'));
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
    <style type="text/css">
        .rlbItem
        {
            float:left !important;
        }
        .rlbGroup, .RadListBox
        {
            width:auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_ProveedorPrecioID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="CONFIGURAR PRECIOS DEL PROVEEDOR"
                    Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <fieldset>
            <legend>Criterios de Búsqueda</legend>
            <table width="100%">
                <tr>
                    <td width="150px" align="left">Proveedor:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarProveedor" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="150px" align="left">Variedad:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarVariedad" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                    </td>
                </tr>
            </table>

        </fieldset>
    </asp:Panel>
    <telerik:RadTabStrip ID="tab_Container" runat="server" MultiPageID="tab_pages" SelectedIndex="0" Width="100%">
        <Tabs>
            <telerik:RadTab runat="server" Text="Valor de Negociación" PageViewID="tab_variedad" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Valor Establecido" PageViewID="tab_eliminado">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="tab_pages" runat="server" SelectedIndex="0" BorderColor="Gray"
        BorderStyle="Solid" BorderWidth="1px">
        <telerik:RadPageView ID="tab_variedad" runat="server">
            <br />
            <asp:Panel ID="pnl_Datos" runat="server" Width="100%">
                <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png" OnClick="btn_Nuevo_Click" />
                <telerik:RadGrid ID="gv_Temporada" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Temporada_ItemCommand" OnNeedDataSource="gv_Temporada_NeedDataSource">
                    <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                        ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                        <Scrolling AllowScroll="True" />
                    </ClientSettings>
                    <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="ProveedorPrecioID">
                        <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                            ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Nro." ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Nro" runat="server" Text='<%# NumeradorColumna().ToString() %>' />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="EmpresaNombre" FilterControlAltText="Filter EmpresaNombre column"
                                HeaderText="Empresa" UniqueName="column1" ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProveedorNombre" FilterControlAltText="Filter ProveedorNombre column"
                                HeaderText="Proveedor" UniqueName="column1" ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VariedadNombre" FilterControlAltText="Filter VariedadNombre column"
                                HeaderText="Variedad" UniqueName="column2" ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaRegistro" FilterControlAltText="Filter FechaRegistro column"
                                HeaderText="Fecha Registro" UniqueName="column3" ItemStyle-Width="150px" DataFormatString="{0:dd-MM-yyyy}">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                        TabIndex="1" CommandArgument='<%# Eval("ProveedorPrecioID") %>' ImageUrl="~/Images/edit.png"
                                        ToolTip="Editar este registro" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
                </telerik:RadGrid>
            </asp:Panel>
            <asp:Panel ID="pnl_Variedad" runat="server" Visible="false">
                <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnButtonClick="tool_principal_ButtonClick"
                    OnClientButtonClicking="clientbuttonclick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText"
                            CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton ImageUrl="~/Images/Borrar.png" ImagePosition="AboveText"
                            CommandName="Eliminar" Text="Eliminar" CausesValidation="false">
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                            CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
                <%--<fieldset>
                    <legend><strong>INFORMACIÓN DE LA VARIEDAD</strong>            </legend>
                   
                </fieldset>--%>
                <asp:Panel ID="pnl_InformacionPrecio" runat="server" GroupingText="<strong>INFORMACIÓN DE LA VARIEDAD</strong>" Width="100%">
                    <table width="100%">
                        <tr>
                            <td class="auto-style1">Empresa:</td>
                            <td class="auto-style1">
                                <telerik:RadTextBox ID="txt_EmpresaRUC" runat="server" MaxLength="13" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td class="auto-style1">Proveedor:</td>
                            <td class="auto-style1">
                                <telerik:RadComboBox ID="cmb_Proveedor" runat="server" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td class="auto-style1">Variedad:</td>
                            <td class="auto-style1">
                                <telerik:RadComboBox ID="cmb_Variedad" runat="server" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Precio Negociación:</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_PrecioNegociacion" runat="server" MinValue="0" Value="0">
                                    <NumberFormat DecimalDigits="2" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>Observaciones:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_Observaciones" runat="server" TextMode="MultiLine" Height="30px" Width="300px"></telerik:RadTextBox></td>
                            <td>Estado:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnl_Detalle" runat="server" GroupingText="<strong>DETALLE DE PRECIOS</strong>">
                    <table>
                        <tr>
                            <td colspan="2">
                                <telerik:RadCheckBox ID="chk_Calidad" runat="server" Text="Calidad" OnClick="chk_Calidad_Click" AutoPostBack="true"></telerik:RadCheckBox>
                                <asp:Panel ID="pnl_Calidad" runat="server" GroupingText="<strong>Tipo de Calidad</strong>" Visible="true" Enabled="false">
                                    <telerik:RadListBox ID="rlb_TipoCalidad" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="false" Width="200px"
                                        Height="50px" ToolTip="Tipo de Calidad">
                                    </telerik:RadListBox>
                                </asp:Panel>
                            </td>
                            <td colspan="2">
                                <telerik:RadCheckBox ID="chk_Longitud" runat="server" Text="Longitud" OnClick="chk_Longitud_Click" AutoPostBack="true"></telerik:RadCheckBox>
                                <asp:Panel ID="pnl_Longitud" runat="server" GroupingText="<strong>Longitud en cm</strong>" Visible="true" Enabled="false">
                                    <telerik:RadListBox ID="rlb_Longitud" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="false" Width="200px"
                                        Height="50px" ToolTip="Longitud">
                                    </telerik:RadListBox>
                                </asp:Panel>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <%--  <telerik:RadMultiPage ID="tab_pages" runat="server" SelectedIndex="0" BorderColor="Gray"
        BorderStyle="Solid" BorderWidth="1px">
       
          
        </telerik:RadPageView>
        <%-- <telerik:RadPageView ID="tab_eliminado" runat="server">
            <br />
            <asp:Panel ID="pnl_DatosEliminado" runat="server">
                <telerik:RadGrid ID="gv_TemporadaEliminado" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_TemporadaEliminado_ItemCommand" OnNeedDataSource="gv_TemporadaEliminado_NeedDataSource">
                    <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                        ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                        <Scrolling AllowScroll="True" />
                    </ClientSettings>
                    <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="ProveedorVariedadID">
                        <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                            ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Nro." ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Nro" runat="server" Text='<%# NumeradorColumna().ToString() %>' />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="ProveedorNombre" FilterControlAltText="Filter ProveedorNombre column"
                                HeaderText="Proveedor" UniqueName="column1" ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VariedadNombre" FilterControlAltText="Filter VariedadNombre column"
                                HeaderText="Variedad" UniqueName="column2" ItemStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaRegistro" FilterControlAltText="Filter FechaRegistro column"
                                HeaderText="Fecha Registro" UniqueName="column3" ItemStyle-Width="150px" DataFormatString="{0:dd-MM-yyyy}">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                        TabIndex="1" CommandArgument='<%# Eval("ProveedorVariedadID") %>' ImageUrl="~/Images/edit.png"
                                        ToolTip="Editar este registro" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
                </telerik:RadGrid>
            </asp:Panel>
            <asp:Panel ID="pnl_VariedadEliminada" runat="server" Visible="false">
                <telerik:RadToolBar ID="tool_principal_eliminar" runat="server" Width="100%"
                    OnClientButtonClicking="clientbuttonclick" OnButtonClick="tool_principal_eliminar_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton ImageUrl="~/Images/Complete.png" ImagePosition="AboveText"
                            CommandName="Activar" Text="Activar" ValidationGroup="Grupo1">
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                            CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
                <fieldset>
                    <legend><strong>INFORMACIÓN DE LA VARIEDAD ELIMINADA</strong>            </legend>
                    <table>
                        <tr>
                            <td>Empresa:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_EliminadoEmpresa" runat="server" MaxLength="13" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Proveedor:</td>
                            <td>
                                <telerik:RadComboBox ID="cmb_EliminadoProveedor" runat="server" Width="300px" Enabled="false">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Variedad:</td>
                            <td>
                                <telerik:RadComboBox ID="cmb_EliminadoVariedad" runat="server" Width="300px" Enabled="false">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Observaciones:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_EliminadoObservaciones" runat="server" TextMode="MultiLine" Height="30px" Width="400px" ReadOnly="true"></telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td>Estado:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_EliminadoEstado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </telerik:RadPageView>--%>

    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
