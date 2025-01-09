<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Clasificador.aspx.cs" Inherits="SGF.Site.Genérico.Frm_Clasificador" %>

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
    <asp:Panel ID="PnlBuscar" runat="server">
        <asp:ImageButton ID="btn_Agregar" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend>BUSQUEDA</legend>
            <table width="100%">
                <tr>
                    <td width="150px">Nombre Clasificador:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_NombreCategoriaBuscar" runat="server" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="150px" align="center">Tipo Clasificador:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoClasificadorBuscar" runat="server">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">&nbsp;
                    </td>
                    <td colspan="2" align="right">
                        <telerik:RadButton ID="btn_Buscar" runat="server" Text="Buscar" OnClick="btn_Buscar_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btn_limpiar" runat="server" Text="Limpiar" OnClick="btn_limpiar_Click1">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="PnlClasificador" runat="server">
        <fieldset>
            <legend>CLASIFICADOR</legend>
            <telerik:RadGrid ID="Grid_Clasificador" runat="server" OnNeedDataSource="Grid_Clasificador_NeedDataSource"
                AutoGenerateColumns="false" OnItemCommand="Grid_Clasificador_ItemCommand">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnablePostBackOnRowClick="True"
                    EnableRowHoverStyle="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="CLASIFICADOR_ID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="TIPOCLASIFICADOR.NOMBRE" FilterControlAltText="Filter column1 column"
                            HeaderText="TIPO" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NOMBRE" FilterControlAltText="Filter column1 column"
                            HeaderText="NOMBRE" UniqueName="column1" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="ESTADO" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado((Int32)Eval("ESTADO"))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("CLASIFICADOR_ID") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </fieldset>
    </asp:Panel>
    <asp:HiddenField ID="hfClasificadorID" runat="server" />
    <asp:Panel ID="PnlEditarClasificador" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnButtonClick="tool_principal_ButtonClick"
            OnClientButtonClicking="clientbuttonclick">
            <Items>
                <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText"
                    CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/borrar.png" ImagePosition="AboveText"
                    CommandName="Eliminar" Text="Eliminar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/aprobar.png" ImagePosition="AboveText"
                    CommandName="Activar" Text="Activar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                    CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend>Datos Clasificador</legend>
            <table width="100%">
                <tr>
                    <td width="150px">Tipo Clasificador:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoClasificador" runat="server" Width="250px">
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="rfv_TipoClasificador" runat="server" ControlToValidate="cmb_TipoClasificador"
                            ErrorMessage="Debe escoger el tipo de clasificador" ForeColor="Red" InitialValue="Seleccione"
                            ValidationGroup="Grupo1" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Nombre" runat="server" MaxLength="200" Height="50px" Width="250px" TextMode="MultiLine">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_Nombre"
                            ErrorMessage="Debe ingresar el nombre" ForeColor="Red" ValidationGroup="Grupo1">*</asp:RequiredFieldValidator>
                    </td>

                </tr>
                <tr>
                    <td>Valor
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Valor" runat="server" MaxLength="50" Enabled="true">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Estado
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" MaxLength="50" Enabled="false">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Grupo1" />
</asp:Content>
