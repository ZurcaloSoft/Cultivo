<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Daes.aspx.cs" Inherits="SGF.Site.Comercial.Frm_Daes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_DaesID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DAES" Font-Size="Large" Font-Bold="true"></asp:Label>
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
                    <td width="75px">DAE:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarDae" runat="server" Width="300px"></telerik:RadTextBox>
                    </td>
                    <td width="250px">
                        <asp:RadioButton ID="rbt_BuscarFDesde" runat="server" Text="Fecha Desde" GroupName="Fechas" />
                        <asp:RadioButton ID="rbt_BuscarFHasta" runat="server" Text="Fecha Hasta" GroupName="Fechas" />
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dtp_BuscarFecha" runat="server"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td align="left">Aduana:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarAduana" runat="server" Width="200px"></telerik:RadComboBox>
                    </td>
                    <td align="left">País:&nbsp;&nbsp;&nbsp;
                        <telerik:RadComboBox ID="cmb_BuscarPais" runat="server" Width="200px"></telerik:RadComboBox>
                    </td>
                    <td align="right">
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
            <telerik:RadGrid ID="gv_Daes" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Daes_ItemCommand" OnItemDataBound="gv_Daes_ItemDataBound" OnNeedDataSource="gv_Daes_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="DAESID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="AduanaNombre" FilterControlAltText="Filter AduanaNombre column"
                            HeaderText="Aduana" UniqueName="column2" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PaisNombre" FilterControlAltText="Filter PaisNombre column"
                            HeaderText="País" UniqueName="column2" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DAE" FilterControlAltText="Filter DAE column"
                            HeaderText="DAE" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Desde" FilterControlAltText="Filter Desde column" ItemStyle-Width="150px"
                            HeaderText="Fecha Desde" UniqueName="column" DataFormatString="{0:dd-MM-yyyy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Hasta" FilterControlAltText="Filter Hasta column" ItemStyle-Width="150px"
                            HeaderText="Fecha Hasta" UniqueName="column" DataFormatString="{0:dd-MM-yyyy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("DAESID") %>' ImageUrl="~/Images/edit.png"
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
                    <td>Aduana:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Aduana" runat="server" Width="200px"></telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>País:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Pais" runat="server" Width="200px"></telerik:RadComboBox>
                    </td>
                </tr>
                <%--                <tr>
                    <td>Codigo :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>--%>
                <tr>
                    <td>DAE :</td>
                    <td>
                        <telerik:RadTextBox ID="txt_DAE" runat="server" Width="400px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Fecha Desde:</td>
                    <td>
                        <telerik:RadDatePicker ID="dtp_FechaDesde" runat="server"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>Fecha Hasta:</td>
                    <td>
                        <telerik:RadDatePicker ID="dtp_FechaHasta" runat="server"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>Observaciones:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Observaciones" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
