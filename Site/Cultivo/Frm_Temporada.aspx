<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Temporada.aspx.cs" Inherits="SGF.Site.Cultivo.Frm_Temporada" %>

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
    <asp:Panel ID="pnl_Buscador" runat="server">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE TEMPORADA POR PROVEEDOR"
                        Font-Size="Large" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
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
                    <td width="150px" align="left">Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="gv_Temporada" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Temporada_ItemCommand" OnItemDataBound="gv_Temporada_ItemDataBound">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="UsuarioID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="TipoPersona" FilterControlAltText="Filter TipoPersona column"
                            HeaderText="Tipo de Persona" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Identificacion" FilterControlAltText="Filter Identificacion column"
                            HeaderText="Identificación" UniqueName="column2" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter Nombre column"
                            HeaderText="Nombre" UniqueName="column3" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column"
                            HeaderText="Usuario" UniqueName="column4" ItemStyle-Width="125px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TipoUsuario" FilterControlAltText="Filter TipoUsuario column"
                            HeaderText="Tipo de Usuario" UniqueName="column5" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("EstadoUsuario".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("UsuarioID") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
            </telerik:RadGrid>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_Datos" runat="server" Visible="true">
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
            <legend><strong>INFORMACIÓN DE LA TEMPORADA</strong>            </legend>
            <table width="100%">
                <tr>
                    <td>Proveedor:</td>
                    <td colspan="2">
                        <telerik:RadComboBox ID="cmb_Proveedor" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadCheckBox ID="chk_Todos" runat="server" Text="Aplicar para todos los proveedores" Checked="false"></telerik:RadCheckBox>
                    </td>
                </tr>
                <tr>
                    <td>Fecha Inicio:</td>
                    <td>
                        <telerik:RadDatePicker ID="dtp_FechaInicio" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td>Fecha Fin:</td>
                    <td>
                        <telerik:RadDatePicker ID="dtp_FechaFin" runat="server"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>Observaciones:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Observaciones" runat="server" TextMode="MultiLine" Height="30px" Width="400px"></telerik:RadTextBox></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server"></telerik:RadTextBox></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
