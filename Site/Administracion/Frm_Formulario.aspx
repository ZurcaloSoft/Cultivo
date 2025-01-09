<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Formulario.aspx.cs" Inherits="SGF.Site.Administracion.Frm_Formulario" %>

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
    <asp:HiddenField ID="hdn_FormularioID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE FORMULARIOS" Font-Size="Large" Font-Bold="true"></asp:Label>
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
                    <td width="150px" align="left">Nombre Formulario:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="350px"></telerik:RadTextBox>
                    </td>
                    <td width="150px" align="left">Módulo:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarModulo" runat="server" Width="250px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                    </td>
                </tr>

            </table>
            <telerik:RadGrid ID="gv_Formulario" runat="server" AutoGenerateColumns="false" AllowPaging="false" OnItemCommand="gv_Formulario_ItemCommand" OnNeedDataSource="gv_Formulario_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="FormularioID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="NombreModulo" FilterControlAltText="Filter Modulo column"
                            HeaderText="Módulo" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NombreFormulario" FilterControlAltText="Filter Nombre column"
                            HeaderText="Nombre Formulario" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Codigo" FilterControlAltText="Filter Codigo column"
                            HeaderText="Página" UniqueName="column3" ItemStyle-Width="200px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("EstadoFormulario".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("FormularioID") %>' ImageUrl="~/Images/edit.png"
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
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend>DATOS</legend>
            <table width="100%">
                <tr>
                    <td>Formulario:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_NombreFormulario" runat="server" Width="300px" >
                        </telerik:RadTextBox>
                    </td>
                    <td>Módulo:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Modulo" runat="server" Width="250px"></telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Página:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" Width="300px" ></telerik:RadTextBox>
                    </td>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Observaciones:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Observaciones" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
     <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
            AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
        </telerik:RadNotification>
</asp:Content>
