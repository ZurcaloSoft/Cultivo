<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Almacen.aspx.cs" Inherits="SGF.Site.Administracion.Frm_Almacen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_AlmacenID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE ALMACEN" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscar" runat="server">
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
        <fieldset runat="server" id="fiel_Buscar">
            <legend>BUSQUEDA</legend>
            <table width="100%">
                <tr>
                    <td width="150px" align="center">Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="300px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">&nbsp;
                    </td>
                    <td colspan="2" align="right">
                        <telerik:RadButton ID="btn_Buscar" runat="server" Text="Buscar" OnClick="btn_Buscar_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btn_limpiar" runat="server" Text="Limpiar" OnClick="btn_limpiar_Click"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <fieldset>
            <legend>Empresas</legend>
            <telerik:RadGrid ID="gv_Almacen" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Almacen_ItemCommand" OnNeedDataSource="gv_Almacen_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="CO_ALM_COD">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="CO_ALM_COD" FilterControlAltText="Filter CO_ALM_COD column"
                            HeaderText="Código" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_EMP_RUC" FilterControlAltText="Filter CO_EMP_RUC column"
                            HeaderText="Empresa" UniqueName="column2" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_ALM_NOM" FilterControlAltText="Filter CO_ALM_NOM column"
                            HeaderText="Nombre" UniqueName="column3" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Telefono" FilterControlAltText="Filter Telefono column"
                            HeaderText="Teléfono" UniqueName="column2" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Direccion" FilterControlAltText="Filter Direccion column"
                            HeaderText="Direccion" UniqueName="column4" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("CO_ALM_COD") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
            </telerik:RadGrid>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_Datos" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnButtonClick="tool_principal_ButtonClick">
            <Items>
                <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText"
                    CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                    CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/borrar.png" ImagePosition="AboveText"
                    CommandName="Eliminar" Text="Eliminar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend>DATOS</legend>
            <table width="100%">
                <tr>
                    <td>Empresa:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Empresa" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Código:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Nombre" runat="server" Width="300px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Teléfono:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Teléfono" runat="server" Width="150px"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Dirección:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Direccion" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
