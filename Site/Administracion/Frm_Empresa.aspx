<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Empresa.aspx.cs" Inherits="SGF.Site.Administracion.Frm_Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_EmpresaID" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="ADMINISTRACIÓN DE EMPRESA" Font-Size="Large" Font-Bold="true"></asp:Label>
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
                    <td width="150px">Ruc Empresa :
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_RucEmpresa" runat="server" Width="200px">
                        </telerik:RadTextBox>
                    </td>
                    <td width="150px" align="center">Tipo Grupo:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoGrupoBuscar" runat="server" Width="250px">
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
    <asp:Panel ID="pnl_Buscador" runat="server">
        <fieldset>
            <legend>Empresas</legend>
            <telerik:RadGrid ID="gv_Empresa" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnItemCommand="gv_Empresa_ItemCommand" OnNeedDataSource="gv_Empresa_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="CO_EMP_RUC">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="CO_EMP_RUC" FilterControlAltText="Filter CO_EMP_RUC column"
                            HeaderText="RUC" UniqueName="column1" ItemStyle-Width="200px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_EMP_RAZ_SOC" FilterControlAltText="Filter Razon CO_EMP_RAZ_SOC column"
                            HeaderText="Razon Social" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_EMP_NOM_REP" FilterControlAltText="Filter CO_EMP_NOM_REP Legal column"
                            HeaderText="Representante Legal" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CO_EMP_DIR" FilterControlAltText="Filter CO_EMP_DIR column"
                            HeaderText="Direccion" UniqueName="column2" ItemStyle-Width="250px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("CO_ESTADO".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("CO_EMP_RUC") %>' ImageUrl="~/Images/edit.png"
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
                <%--                <tr>
                    <td>Tipo Grupo:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_TipoGrupo" runat="server"></telerik:RadComboBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>--%>
                <tr>
                    <td>RUC:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Ruc" runat="server" Width="150px">
                        </telerik:RadTextBox>
                    </td>
                    <td>Tipo de Persona:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_EmpresaTipo" runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="0" />
                                <telerik:RadComboBoxItem Text="PERSONA NATURAL" Value="N" />
                                <telerik:RadComboBoxItem Text="PERSONA NATURAL OBLIGADO A LLEVAR CONTABILIDAD" Value="C" />
                                <telerik:RadComboBoxItem Text="PERSONA JURIDCIA EMPRESA" Value="J" />
                                <telerik:RadComboBoxItem Text="CONTRIBUYENTE ESPECIAL" Value="E" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                    <td>
                        <%--                        <asp:RadioButton runat="server" ID="rbt_GrupoSi" GroupName="Grupo" Text="SI" Checked="false" />
                        <asp:RadioButton runat="server" ID="rbt_GrupoNo" GroupName="Grupo" Text="NO" Checked="true" />--%>
                    </td>
                </tr>
                <tr>
                    <td>Razon Social:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_EmpresaNombreRazon" runat="server" Width="300px"></telerik:RadTextBox></td>
                    <td>Nombre Comercial:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_EmpresaNombreComercial" runat="server" Width="300px"></telerik:RadTextBox></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Identificación Representante:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_RepresentanteCed" runat="server" Width="150px"></telerik:RadTextBox></td>
                    <td>Representante Legal:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_RepresentanteNombre" runat="server" Width="300px"></telerik:RadTextBox></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Ruc Contador:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ContadorRuc" runat="server" Width="150px"></telerik:RadTextBox></td>
                    <td>Cedula Representante:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ContadorNombre" runat="server" Width="300px"></telerik:RadTextBox></td>
                    <td>Nro. Registro:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_ContadorRegistro" runat="server" Width="150px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>Teléfono:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Telefono" runat="server" Width="100px"></telerik:RadTextBox></td>
                    <td>Celular:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Celular" runat="server" Width="100px"></telerik:RadTextBox></td>
                    <td>E-mail:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Email" runat="server" Width="200px"></telerik:RadTextBox></td>
                </tr>
                <%--                <tr>
                    <td>Fax:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Fax" runat="server" Width="100px"></telerik:RadTextBox></td>
                </tr>--%>
                <tr>
                    <td>Dirección:</td>
                    <td colspan="3">
                        <telerik:RadTextBox ID="txt_Direccion" runat="server" Width="400px" Height="40px"></telerik:RadTextBox></td>
                    <td>Ciudad:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Ciudad" runat="server" Width="150px"></telerik:RadTextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chk_Grupo" runat="server" Text="Pertenece a un Grupo ?" OnCheckedChanged="chk_Grupo_CheckedChanged" AutoPostBack="true" />
                    </td>
                    <td><<telerik:RadLabel ID="lbl_EmpresaPadre" runat="server" Text="Empresa Principal:  " Visible="false"></telerik:RadLabel>
                        <telerik:RadComboBox ID="cmb_EmpresaGrupo" runat="server" Visible="false" Width="200px"></telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="chk_ContribEspecial" runat="server" Text="Es Contribuyente Especial?" AutoPostBack="true" OnCheckedChanged="chk_ContribEspecial_CheckedChanged" />
                    </td>
                    <td><<telerik:RadLabel ID="lbl_ContribEspecial" runat="server" Text="Número:" Visible="false"></telerik:RadLabel>
                        <telerik:RadTextBox ID="txt_ContribEspecial" runat="server" Width="150px" Visible="false"></telerik:RadTextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
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
