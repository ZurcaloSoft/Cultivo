<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Vendedor.aspx.cs" Inherits="SGF.Site.Comercial.Frm_Vendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_VendedorID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="VENDEDORES" Font-Size="Large" Font-Bold="true"></asp:Label>
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
                    <td>
                        <asp:RadioButton ID="rbt_CedulaRUC" runat="server" Text="Cédula / RUC" GroupName="Buscar" />
                        <asp:RadioButton ID="rbt_Nombre" runat="server" Text="Nombre Vendedor" GroupName="Buscar" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscNombre" runat="server" Width="400px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">&nbsp;
                    </td>
                    <td colspan="2" align="right">
                        <telerik:RadButton ID="btn_Buscar" runat="server" Text="Buscar" OnClick="btn_Buscar_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btn_limpiar" runat="server" Text="Limpiar" OnClick="btn_limpiar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <fieldset>
            <legend>Empresas</legend>
            <telerik:RadGrid ID="gv_Vendedor" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Vendedor_ItemCommand" OnNeedDataSource="gv_Vendedor_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="CO_VEN_CED">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="CO_VEN_CED" FilterControlAltText="Filter CO_VEN_CED column"
                            HeaderText="Cedula" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_VEN_NOM" FilterControlAltText="Filter CO_VEN_NOM column"
                            HeaderText="Nombre" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_VEN_TEL" FilterControlAltText="Filter CO_VEN_TEL column"
                            HeaderText="Teléfono" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_VEN_CEL" FilterControlAltText="Filter CO_VEN_CEL column"
                            HeaderText="Celular" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <%--                        <telerik:GridTemplateColumn HeaderText="CO_ESTADO" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("CO_ESTADO".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("CO_VEN_CED") %>' ImageUrl="~/Images/edit.png"
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
                        <telerik:RadTextBox ID="txt_EmpresaID" runat="server" Width="100px" ReadOnly="true" Enabled="false"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Cédula / RUC:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_VendedorCedula" runat="server" Width="200px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Nombre Venedor:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_VendedorNombre" runat="server" Width="400px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Teléfono:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_VendedorTelefono" runat="server" Width="200px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Celular:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_VendedorCelular" runat="server" Width="200px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>E-mail:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_VendedorEmail" runat="server" Width="300px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Dirección:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_VendedorDireccion" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_VendedorEstado" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
