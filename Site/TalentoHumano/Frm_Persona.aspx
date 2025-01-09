<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Persona.aspx.cs" Inherits="SGF.Site.TalentoHumano.Frm_Persona" %>

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
    <asp:HiddenField ID="hdn_PersonaID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE PERSONAS" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscar" runat="server" Visible="true">
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png"
            OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend>BUSQUEDA DE PERSONA</legend>
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
                    <td width="150px" align="left">Tipo Persona:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarTipoPersona" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="left">&nbsp;
                    </td>
                    <td colspan="3" align="right">
                        <telerik:RadButton ID="btn_Buscar" runat="server" Text="Buscar" OnClick="btn_Buscar_Click">
                        </telerik:RadButton>
                        <telerik:RadButton ID="btn_limpiar" runat="server" Text="Limpiar" OnClick="btn_limpiar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="gv_Persona" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Persona_ItemCommand" OnNeedDataSource="gv_Persona_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="PersonaID">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="TipoPersonaNombre" FilterControlAltText="Filter TipoPersonaNombre column"
                            HeaderText="Tipo Persona" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Identificacion" FilterControlAltText="Filter Identificacion column"
                            HeaderText="Identificación" UniqueName="column2" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NombrePersona" FilterControlAltText="Filter NombrePersona column"
                            HeaderText="Nombre" UniqueName="column3" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Pais" FilterControlAltText="Filter Pais column"
                            HeaderText="País" UniqueName="column4" ItemStyle-Width="125px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"
                            HeaderText="Email" UniqueName="column5" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Celular" FilterControlAltText="Filter Celular column"
                            HeaderText="Celular" UniqueName="column6" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>

                        <%--                        <telerik:GridNumericColumn DataField="VALOR" FilterControlAltText="Filter VALOR column"
                            HeaderText="VALOR" UniqueName="column2" ItemStyle-Width="150px" DecimalDigits="2"
                            NumericType="Currency">
                        </telerik:GridNumericColumn>
                        <telerik:GridCheckBoxColumn DataField="INSPECCIONPREVIA" FilterControlAltText="Filter INSPECCIONPREVIA column"
                            HeaderText="INSPECCION PREVIA" UniqueName="column3" ItemStyle-Width="150px">
                        </telerik:GridCheckBoxColumn>--%>
                        <telerik:GridBoundColumn DataField="EstadoPersona" FilterControlAltText="Filter EstadoPersona column"
                            HeaderText="Estado" UniqueName="column6" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <%--                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("EstadoPersona".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("PersonaID") %>' ImageUrl="~/Images/edit.png"
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
        <asp:Panel ID="pnl_DatosEmpleado" runat="server" Visible="false">
            <fieldset>
                <legend>DATOS Empleado</legend>
                <table width="100%">
                    <tr>
                        <td>Tipo de Persona:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_TipoPersona" runat="server">
                            </telerik:RadComboBox>
                        </td>
                        <td>Código:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Codigo" runat="server"></telerik:RadTextBox></td>
                        <td>País:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_Pais" runat="server"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Tipo Identificación:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_TipoIdentificacion" runat="server" OnSelectedIndexChanged="cmb_TipoIdentificacion_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                        </td>
                        <td>Identificación:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Identificacion" runat="server"></telerik:RadTextBox></td>
                        <td>Nombre:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_NombrePersona" runat="server" Width="250px"></telerik:RadTextBox></td>
                    </tr>
                    <tr>
                        <td>Identificación Representante:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_RepIdentificacion" runat="server"></telerik:RadTextBox>
                        </td>
                        <td>Representante Legal:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_RepNombre" runat="server"></telerik:RadTextBox></td>
                        <td>Nombre Comercial:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_NombreComercial" runat="server" Width="250px"></telerik:RadTextBox></td>
                    </tr>
                    <tr>
                        <td>Fecha Nacimiento:</td>
                        <td>
                            <telerik:RadDatePicker ID="dtp_FechaNacimiento" runat="server"></telerik:RadDatePicker>
                        </td>
                        <td>Género:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_Genero" runat="server"></telerik:RadComboBox>
                        </td>
                        <td>Estado Civil:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_EstadoCivil" runat="server"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Email" runat="server"></telerik:RadTextBox></td>
                        <td>Teléfono</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Telefono" runat="server" EmptyMessage="2456234"></telerik:RadTextBox></td>
                        <td>Celular:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Celular" runat="server" EmptyMessage="0987654321"></telerik:RadTextBox></td>
                    </tr>
                    <tr>
                        <td>Cargo:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Cargo" runat="server"></telerik:RadTextBox></td>
                        <td>Fecha de Ingreso:</td>
                        <td>
                            <telerik:RadDatePicker ID="dtp_FechaIngreso" runat="server"></telerik:RadDatePicker>
                        </td>
                        <td>Fecha Fin Contrato:</td>
                        <td>
                            <telerik:RadDatePicker ID="dtp_FechaExpiracion" runat="server"></telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>Estado:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_Estado" runat="server"></telerik:RadTextBox></td>
                        <td>Observaciones:</td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_Observaciones" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                    </tr>

                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnl_DatosCarguera" runat="server" Visible="false">
            <fieldset>
                <legend>DATOS Carguera</legend>
                <table width="100%">
                    <tr>
                        <td>Tipo Identificación:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_CargueraTipoIdentificacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmb_CargueraTipoIdentificacion_SelectedIndexChanged"></telerik:RadComboBox>
                        </td>
                        <td>Identificación:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraIdentificacion" runat="server"></telerik:RadTextBox></td>
                        <td>Cuarto Frío:</td>
                        <td>
                            <telerik:RadComboBox ID="cmb_CargueraCuartoFrio" runat="server"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Código:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraCodigo" runat="server"></telerik:RadTextBox></td>
                        <td>Nombre:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraNombre" runat="server" Width="250px"></telerik:RadTextBox></td>
                        <td>Contacto:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraContacto" runat="server"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraEmail" runat="server" Width="250px"></telerik:RadTextBox></td>
                        <td>Teléfono</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraTelefono" runat="server" EmptyMessage="2456234"></telerik:RadTextBox></td>
                        <td>Celular:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraCelular" runat="server" EmptyMessage="0987654321"></telerik:RadTextBox></td>
                    </tr>
                    <tr>
                        <td>Sitio Web:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraSitioWeb" runat="server" Width="250px"></telerik:RadTextBox></td>
                        <td>Dirección:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraDireccion" runat="server" Width="350px" Height="40px"></telerik:RadTextBox></td>
                        <td>Observaciones:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraObservaciones" runat="server" Width="350px" Height="40px"></telerik:RadTextBox></td>
                    </tr>
                    <tr>
                        <td>Estado:</td>
                        <td>
                            <telerik:RadTextBox ID="txt_CargueraEstado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
