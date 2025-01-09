<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Grupo.aspx.cs" Inherits="SGF.Site.Administracion.Frm_Grupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_GrupoID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DEL GRUPO" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend>Grupos</legend>
            <telerik:RadGrid ID="gv_Grupo" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Grupo_ItemCommand" OnNeedDataSource="gv_Grupo_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="GrupoID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Codigo" FilterControlAltText="Filter Nombre column"
                            HeaderText="Nombre" UniqueName="column1" ItemStyle-Width="200px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter Descripcion column"
                            HeaderText="Descripcion" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Observaciones" FilterControlAltText="Filter Descripcion column"
                            HeaderText="Descripcion" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("GrupoID") %>' ImageUrl="~/Images/edit.png"
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
                    <td>Codigo:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" Width="200px">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Nombre" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Observaciones:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Observacion" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
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
