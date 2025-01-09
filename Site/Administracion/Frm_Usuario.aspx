<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Usuario.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="SGF.Site.Administracion.Frm_Usuario" %>

<%@ Register Src="~/Controles/Control_Multiple.ascx" TagName="Ctl_ClasificadorMultiple" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AbrirModalPersona() {
            debugger;
<%--            var clv1 = document.getElementById("<%=txt_TipoPersona.ClientID%>").value.trim();--%>

            var oManager = window.radopen("Frm_BuscarPersona.aspx?type=1", "wnd_Persona");
            oManager.setSize(650, 500); //Width, Height
            oManager.center();
            oManager.SetActive();
            //return false;
        }
        function OnClientClose(oWnd, args) {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                document.getElementById('ContentPlaceHolder1_hdn_PersonaID').value = arg.PersonaID;
                document.getElementById('ContentPlaceHolder1_hdn_PersonaNombre').value = arg.PersonaNombre;
                document.getElementById('ContentPlaceHolder1_hdn_PersonaIdentificacion').value = arg.PersonaIdentificacion;
                //                document.getElementById('ctl00_ContentPlaceHolder1_txt_Observaciones').value = arg.ActividadSecundariaID;
                //                alert(arg.NombreActividad);
                __doPostBack('ctl00$ContentPlaceHolder1$btn_RetornoVentana', '');
            }
        }
        function clientbuttonclick(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "Eliminar")
                args.set_cancel(!confirm('Está seguro que desea eliminar el registro?'));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_PersonaID" runat="server" />
    <asp:HiddenField ID="hdn_PersonaNombre" runat="server" />
    <asp:HiddenField ID="hdn_PersonaIdentificacion" runat="server" />
    <asp:HiddenField ID="hdn_UsuarioID" runat="server" />
    <telerik:RadTextBox ID="txt_TipoPersona" runat="server" Width="80px" Visible="false" Text="1"></telerik:RadTextBox>
    <telerik:RadWindowManager ID="wnd_manager" runat="server" ShowContentDuringLoad="false"
        VisibleStatusbar="false" ReloadOnShow="true" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="wnd_Persona" runat="server" Width="650" Height="500" NavigateUrl="~/Frm_BuscarPersona.aspx" Modal="true"
                OnClientClose="OnClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE USUARIOS"
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
                    <td width="150px" align="left">Cédula:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarCedula" runat="server" Width="80px"></telerik:RadTextBox>
                    </td>
                    <td width="150px" align="left">Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td width="150px" align="left">Tipo Usuario:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarTipoUsuario" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                    </td>
                </tr>

            </table>
            <telerik:RadGrid ID="gv_Usuario" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Usuario_ItemCommand" OnNeedDataSource="gv_Usuario_NeedDataSource">
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
    <asp:Panel ID="pnl_Datos" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%" OnButtonClick="tool_principal_ButtonClick"
            OnClientButtonClicking="clientbuttonclick">
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
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_CedulaPersona" runat="server" MaxLength="13" Width="100px" ReadOnly="true">
                        </telerik:RadTextBox>
                        <telerik:RadTextBox ID="txt_NombrePersona" runat="server" Width="200px" ReadOnly="true">
                        </telerik:RadTextBox>
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="btn_RetornoVentana" runat="server" OnClick="btn_RetornoVentana_Click"
                            ImageUrl="~/Images/dot_clear.gif" />
                        <asp:HyperLink ID="btn_MostrarPersona" runat="server" ToolTip="Visualizar datos de Recepción" NavigateUrl="javascript:AbrirModalPersona();"
                            ImageUrl="~/Images/BuscarModal.png" Height="20px" />

                    </td>
                    <td>Tipo de Usuario:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoUsuario" runat="server"></telerik:RadComboBox>
                    </td>
                    <td>IP:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_IP" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Usuario:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Usuario" runat="server"></telerik:RadTextBox>
                    </td>
                    <td>Contraseña:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Password" runat="server" TextMode="Password"></telerik:RadTextBox>
                    </td>
                    <td>Nombre PC:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_NombrePC" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Fecha Expiración:</td>
                    <td>
                        <telerik:RadDatePicker ID="dtp_FechaExpiracion" runat="server"></telerik:RadDatePicker>
                    </td>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                    <td>MAC:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_MAC" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Observaciones:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Observaciones" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <asp:Panel ID="pnl_AgruparActividades" runat="server">
                <fieldset>
                    <legend><strong>ASIGNAR PERMISOS</strong></legend>
                    <table width="100%">
                        <tr>
                            <td align="center" width="33%">
                                <fieldset>
                                    <legend>MÓDULOS</legend>
                                    <telerik:RadListBox ID="rlb_Modulo" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="300" RenderMode="Lightweight" AutoPostBack="true"
                                        Height="300px" OnSelectedIndexChanged="rlb_Modulo_SelectedIndexChanged">
                                    </telerik:RadListBox>
                                    <br />
                                </fieldset>
                            </td>
                            <td></td>
                            <td align="center" valign="top" width="33%">
                                <fieldset>
                                    <legend>FORMULARIOS</legend>
                                    <telerik:RadListBox ID="rlb_Formulario" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="300" RenderMode="Lightweight" AutoPostBack="true"
                                        Height="300px" OnSelectedIndexChanged="rlb_Formulario_SelectedIndexChanged" OnCheckAllCheck="rlb_Formulario_CheckAllCheck" OnItemCheck="rlb_Formulario_ItemCheck">
                                    </telerik:RadListBox>
                                    <br />
                                </fieldset>
                            </td>
                            <td></td>
                            <%--<td width="33%" valign="top">
                                <fieldset>
                                    <legend>BOTONES</legend>
                                    <telerik:RadListBox ID="rlb_Boton" runat="server" CheckBoxes="true" ShowCheckAll="true" Width="300" RenderMode="Lightweight"
                                        Height="300px">
                                    </telerik:RadListBox>
                                    <br />
                                </fieldset>

                            </td>--%>
                        </tr>
                    </table>
                    <%-- <table>
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>TIPO DE REQUERIMIENTO</legend>
                                    <uc2:Ctl_ClasificadorMultiple ID="Ctl_Formularios" runat="server" Visible="false" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>--%>
                </fieldset>
            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
