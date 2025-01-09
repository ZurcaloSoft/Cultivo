<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Proveedor.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="SGF.Site.Compras.Frm_Proveedor" %>

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
    <asp:HiddenField ID="hdn_ProveedorBodega" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE PROVEEDORES" Font-Size="Large" Font-Bold="true"></asp:Label>
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
                    <td>Cédula:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarCedula" runat="server" Width="150px"></telerik:RadTextBox>
                    </td>
                    <td>Nombre:
                    </td>

                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="350px"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
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
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="CO_PRO_COD">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="CO_PRO_CED" FilterControlAltText="Filter CO_PRO_CED column"
                            HeaderText="Identificación" UniqueName="column1" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_PRO_NOM" FilterControlAltText="Filter CO_PRO_NOM column"
                            HeaderText="Nombre" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_PRO_MAI" FilterControlAltText="Filter CO_PRO_MAI column"
                            HeaderText="Email" UniqueName="column3" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_PRO_CEL" FilterControlAltText="Filter CO_PRO_CEL column"
                            HeaderText="Celular" UniqueName="column4" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_PRO_TEL1" FilterControlAltText="Filter CO_PRO_TEL1 column"
                            HeaderText="Telefono" UniqueName="column5" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_PRO_EST" FilterControlAltText="Filter CO_PRO_EST column"
                            HeaderText="Estado" UniqueName="column6" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("CO_PRO_COD") %>' ImageUrl="~/Images/edit.png"
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
            <legend>DATOS PERSONA</legend>
            <table width="100%">
                <tr>
                    <td>Empresa RUC:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_EmpresaRUC" runat="server" MaxLength="13" ReadOnly="true"></telerik:RadTextBox></td>
                    <td>Código:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Codigo" runat="server" MaxLength="10" ReadOnly="false"></telerik:RadTextBox></td>
                    <td>Tipo de Proveedor:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_ProveedorTipo" runat="server" AutoPostBack="true" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Tipo Identificación:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_ProveedorTipoIdentificacion" runat="server" OnSelectedIndexChanged="cmb_TipoIdentificacion_SelectedIndexChanged" AutoPostBack="true" Width="200px"></telerik:RadComboBox>
                    </td>
                    <td>Identificación:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorIdentificacion" runat="server"></telerik:RadTextBox></td>
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorNombre" runat="server" Width="350px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Representante Legal:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorRepresentanteLegal" runat="server" Width="300px"></telerik:RadTextBox>
                    </td>
                    <td>País:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_ProveedorPais" runat="server"></telerik:RadComboBox>
                    </td>
                    <td>Ciudad:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorCiudad" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Email:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorEmail" runat="server" Width="250px"></telerik:RadTextBox></td>
                    <td>Teléfono</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorTelefono" runat="server" Width="250px" EmptyMessage="2456234"></telerik:RadTextBox></td>
                    <td>Celular:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorCelular" runat="server" Width="250px" EmptyMessage="0987654321"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Email2:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorEmail2" runat="server" Width="250px"></telerik:RadTextBox></td>
                    <td>Teléfono2</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorTelefono2" runat="server" Width="250px" EmptyMessage="2456234"></telerik:RadTextBox></td>
                    <td>Email3:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorEmail3" runat="server" Width="250px" EmptyMessage=""></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <fieldset>
                            <legend><strong>Datos de Cultivo</strong></legend>
                            <telerik:RadCheckBoxList ID="chk_ProveedorCultivo" runat="server" Direction="Horizontal">
                                <Items>
                                    <telerik:ButtonListItem Text="Tiene Cultivo" Value="1" />
                                    <telerik:ButtonListItem Text="Solo Envío" Value="2" />
                                    <telerik:ButtonListItem Text="Entrega Directa" Value="3" />
                                </Items>
                            </telerik:RadCheckBoxList>
                        </fieldset>
                    </td>
                    <td>Cuenta:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorCuenta" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        <telerik:RadCheckBox ID="chk_ProveedorCredito" runat="server" Text="Crédito" OnClick="chk_ProveedorCredito_Click"></telerik:RadCheckBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_ProveedorCredito" runat="server" Text="Monto Crédito:" Visible="false"></telerik:RadLabel>
                        <telerik:RadNumericTextBox ID="txt_ProveedorLimiteCredito" runat="server" MinValue="0" Value="0">
                            <NumberFormat DecimalDigits="2" />
                        </telerik:RadNumericTextBox>
                    </td>

                </tr>
                <tr>
                    <td>Dirección:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorDireccion" runat="server" Width="300px" Height="50px"></telerik:RadTextBox></td>
                    <td>Observaciones:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorObservaciones" runat="server" Width="300px" Height="50px"></telerik:RadTextBox></td>
                    <td colspan="2">
                        <telerik:RadCheckBox ID="chk_ProveedorConEsp" runat="server" Text="Cont. Especial"></telerik:RadCheckBox>
                    </td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ProveedorEstado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <fieldset>
                            <legend>Lugar de entrega de Flor</legend>
                            <table>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <legend>Finca</legend>
                                            <telerik:RadComboBox ID="cmb_Finca" runat="server" Width="200px" OnSelectedIndexChanged="cmb_Finca_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>

                                            <telerik:RadListBox ID="rlb_Finca" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="true" Width="300px"
                                                Height="200px" ToolTip="Finca" OnSelectedIndexChanged="rlb_Finca_SelectedIndexChanged" Visible="false">
                                            </telerik:RadListBox>
                                        </fieldset>
                                    </td>
                                    <td>
                                        <fieldset>
                                            <legend>Bodega</legend>
                                            <telerik:RadComboBox ID="cmb_Bodega" runat="server" Width="200px"></telerik:RadComboBox>

                                            <telerik:RadListBox ID="rlb_Bodega" runat="server" CheckBoxes="true" ShowCheckAll="true" RenderMode="Lightweight" AutoPostBack="false" Width="300px"
                                                Height="200px" ToolTip="Bodega" Visible="false">
                                            </telerik:RadListBox>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
