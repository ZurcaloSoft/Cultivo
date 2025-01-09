<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Clasificador.aspx.cs" Inherits="SGF.Site.Administracion.Frm_Clasificador" %>

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
    <asp:HiddenField ID="hdn_ClasificadorID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE CLASIFICADORES" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscar" runat="server">
        <asp:ImageButton ID="btn_Agregar" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
        <fieldset runat="server" id="fiel_Buscar">
            <legend>BUSQUEDA</legend>
            <table width="100%">
                <tr>
                    <td width="150px">Nombre Clasificador:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_NombreCategoriaBuscar" runat="server" Width="350px">
                        </telerik:RadTextBox>
                    </td>
                    <td width="150px" align="center">Tipo Clasificador:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoClasificadorBuscar" runat="server" Width="250px">
                        </telerik:RadComboBox>
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
    <asp:Panel ID="pnl_Datos" runat="server">
        <fieldset>
            <legend>CLASIFICADOR</legend>
            <telerik:RadGrid ID="gv_Clasificador" runat="server" AutoGenerateColumns="false" OnItemCommand="gv_Clasificador_ItemCommand" OnNeedDataSource="gv_Clasificador_NeedDataSource" AllowFilteringByColumn="true" >
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnablePostBackOnRowClick="True"  
                    EnableRowHoverStyle="True" ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="ClasificadorID" >
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="NombreTipoClasificador" FilterControlAltText="Filter NombreTipoClasificador column" AllowFiltering="False"
                            HeaderText="Tipo" UniqueName="column1" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NombreClasificador" FilterControlAltText="Filter NombreClasificador column"
                            HeaderText="Nombre" UniqueName="column2" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Codigo" FilterControlAltText="Filter Codigo column" AllowFiltering="False"
                            HeaderText="Código" UniqueName="column3" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="ESTADO" ItemStyle-Width="70px" AllowFiltering="False">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado((short)Eval("EstadoClasificador"))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px" AllowFiltering="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("ClasificadorID") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_Editar" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnButtonClick="tool_principal_ButtonClick">
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
                        <telerik:RadComboBox ID="cmb_TipoClasificador" runat="server" Width="350px">
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="rfv_TipoClasificador" runat="server" ControlToValidate="cmb_TipoClasificador"
                            ErrorMessage="Debe escoger el tipo de clasificador" ForeColor="Red" InitialValue="Seleccione"
                            ValidationGroup="Grupo1" SetFocusOnError="True">
                            *</asp:RequiredFieldValidator>
                    </td>
                    <td width="150px">Tipo Clasificador:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Parent" runat="server" Width="350px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Nombre" runat="server" Height="50px" Width="350px" TextMode="MultiLine">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_Nombre"
                            ErrorMessage="Debe ingresar el nombre" ForeColor="Red" ValidationGroup="Grupo1">
                            *</asp:RequiredFieldValidator>
                    </td>
                    <td>Descripción:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Observaciones" runat="server" Height="50px" Width="350px" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Código:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" Enabled="true">
                        </telerik:RadTextBox>
                    </td>
                    <td>Valor:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Valor" runat="server" Enabled="true">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Estado
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" Enabled="false">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Grupo1" />
</asp:Content>
