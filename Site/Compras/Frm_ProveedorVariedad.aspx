<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_ProveedorVariedad.aspx.cs" Inherits="SGF.Site.Compras.Frm_ProveedorVariedad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function clientbuttonclick(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "Eliminar")
                args.set_cancel(!confirm('Está seguro que desea eliminar el registro?'));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_ProveedorVariedadID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="CONFIGURAR VARIEDADES DEL PROVEEDOR"
                    Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <fieldset>
            <legend>Criterios de Búsqueda</legend>
            <table>
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
    <telerik:RadTabStrip ID="tab_Container" runat="server" MultiPageID="tab_pages" SelectedIndex="1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Variedades Asignadas" PageViewID="tab_variedad" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Variedades Eliminadas" PageViewID="tab_eliminado" Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="tab_pages" runat="server" SelectedIndex="0" BorderColor="Gray"
        BorderStyle="Solid" BorderWidth="1px">
        <telerik:RadPageView ID="tab_variedad" runat="server">
            <br />
            <asp:Panel ID="pnl_Datos" runat="server">
                <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png" OnClick="btn_Nuevo_Click" Visible="false" />
                <asp:LinkButton ID="lnkConfigurar" runat="server" ToolTip="Configurar Proveedores con Variedad" Visible="true" CommandName="configurarPV" OnClick="lnkConfigurar_Click">
                    <asp:Image ID="btn_Configurar" runat="server" ImageUrl="~/Images/lista.png" BorderStyle="None"
                        BorderColor="Transparent" BorderWidth="0px" />
                    <div style="display: inline; vertical-align: middle;">
                        Configurar Proveedores con Variedad
                    </div>
                </asp:LinkButton>
                <telerik:RadGrid ID="gv_Temporada" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Temporada_ItemCommand" OnNeedDataSource="gv_Temporada_NeedDataSource">
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
                            <telerik:GridBoundColumn DataField="ProveedorID" FilterControlAltText="Filter ProveedorID column"
                                HeaderText="Cód. Proveedor" UniqueName="column1" ItemStyle-Width="150px">
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
                            <telerik:GridTemplateColumn HeaderText="Borrar" Groupable="false" ItemStyle-Width="1px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                        TabIndex="1" CommandArgument='<%# Eval("ProveedorVariedadID") %>' ImageUrl="~/Images/Borrar.png"
                                        ToolTip="Eliminar este registro" />
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
                <fieldset>
                    <legend><strong>INFORMACIÓN DE LA VARIEDAD</strong></legend>
                    <table>
                        <tr>
                            <td>Empresa:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_EmpresaRUC" runat="server" MaxLength="13" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>Observaciones:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_Observaciones" runat="server" TextMode="MultiLine" Height="30px" Width="400px"></telerik:RadTextBox></td>
                            <td>Estado:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:RadioButton ID="rbt_General" GroupName="Typo" runat="server" ValidationGroup="tipo" Text="Configuración General" AutoPostBack="true" OnCheckedChanged="rbt_General_CheckedChanged" Visible="false" />
                                <asp:RadioButton ID="rbt_Detallada" GroupName="Typo" runat="server" ValidationGroup="tipo" Text="Configuración Individual" AutoPostBack="true" OnCheckedChanged="rbt_Detallada_CheckedChanged" Visible="false" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <table id="table_Detallado" runat="server" visible="true">
                        <tr>
                            <td>
                                <asp:DataList ID="dl_InfProVar" runat="server" RepeatDirection="Vertical" BorderStyle="Double" OnItemCommand="dl_InfProVar_ItemCommand" OnItemDataBound="dl_InfProVar_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_ProveedorID" runat="server" />
                                        <asp:HiddenField ID="hdn_EmpresaID" runat="server" />
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadCheckBox runat="server" ID="chk_Proveedor" Text="" Font-Bold="true" CommandName="seleccionar" CommandArgument='<%# Eval("CO_PRO_COD") %>'></telerik:RadCheckBox>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txt_filtro" EmptyMessage="Filtro Variedad"></telerik:RadTextBox></td>
                                                <td>
                                                    <asp:DataList ID="dl_Variedades" runat="server" RepeatDirection="Horizontal" BorderStyle="Dashed" OnItemCommand="dl_Variedades_ItemCommand" OnItemDataBound="dl_Variedades_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdn_ProvID" runat="server" />
                                                            <asp:HiddenField ID="hdn_VariedadID" runat="server" />
                                                            <asp:HiddenField ID="hdn_EmpID" runat="server" />
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadCheckBox runat="server" ID="chk_Variedad" Text=""></telerik:RadCheckBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <SeparatorTemplate>
                                                            <hr />
                                                        </SeparatorTemplate>
                                                    </asp:DataList>
                                                </td>
                                                <td>
                                                    <telerik:RadImageButton ID="btn_Grabar" runat="server" Height="30px" Width="50px" CommandName="grabar" CommandArgument='<%# Eval("CO_PRO_COD") %>'
                                                        SingleClick="true" SingleClickText="Grabar Proveedor con Variedad..." Font-Bold="true" CssClass="custom-border" Visible="false">
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
                    <table id="table_General" runat="server" visible="false">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Proveedor</legend>
                                    <telerik:RadListBox ID="rlb_Proveedor" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="true" Width="300px"
                                        Height="400px" ToolTip="Proveedores">
                                    </telerik:RadListBox>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset>
                                    <legend>Variedades</legend>
                                    <telerik:RadListBox ID="rlb_Variedad" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="false" Width="300px"
                                        Height="400px" ToolTip="Variedades">
                                    </telerik:RadListBox>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </telerik:RadPageView>
        <telerik:RadPageView ID="tab_eliminado" runat="server" Visible="false">
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
                            <td>Observaciones:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_EliminadoObservaciones" runat="server" TextMode="MultiLine" Height="30px" Width="400px" ReadOnly="true"></telerik:RadTextBox>
                            </td>
                            <td>Estado:</td>
                            <td>
                                <telerik:RadTextBox ID="txt_EliminadoEstado" runat="server" ReadOnly="true"></telerik:RadTextBox>
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
                    </table>
                </fieldset>
            </asp:Panel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
