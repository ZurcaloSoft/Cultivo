<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_LineasAereas.aspx.cs" Inherits="SGF.Site.Comercial.Frm_LineasAereas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_LineAerealID" runat="server" />
        <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="LÍNEAS AÉREAS" Font-Size="Large" Font-Bold="true"></asp:Label>
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
                    <td width="150px" align="center">Nombre :
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
            <telerik:RadGrid ID="gv_LineAerea" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_LineAerea_ItemCommand" OnNeedDataSource="gv_LineAerea_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="LineaAereaID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Codigo" FilterControlAltText="Filter Codigo column"
                            HeaderText="Codigo" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter Nombre column"
                            HeaderText="Nombre" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CedulaRUC" FilterControlAltText="Filter CedulaRUC column"
                            HeaderText="CedulaRUC" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PrefijoGuia" FilterControlAltText="Filter PrefijoGuia column"
                            HeaderText="PrefijoGuia" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Observacion" FilterControlAltText="Filter Observacion column"
                            HeaderText="Observacion" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("LineaAereaID") %>' ImageUrl="~/Images/edit.png"
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
                    <td>Codigo :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Nombre :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Nombre" runat="server" Width="400px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Cedula/RUC :</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_CedRUC" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Codigo CAE :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_CodCAE" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Codigo SRI :</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_CodSRI" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Prefijo Guia :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_PreGuia" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>E-mail :</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Email" runat="server" Width="400px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Observación :</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Observacion" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                </tr>
<%--                <tr>
                    <td>Usuario :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Usuario" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Fecha :</td>
                    <td>
                        <telerik:RadDatePicker ID="rdp_Fecha" runat="server"></telerik:RadDatePicker>
                    </td>
                </tr>--%>
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
      <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
      AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
  </telerik:RadNotification>
</asp:Content>
