<%@ Page Title="" Language="C#" MasterPageFile="~/SGF_Site.Master" AutoEventWireup="true" CodeBehind="Frm_Bloques.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="SGF.Site.Cultivo.Frm_Bloques" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/sweetalert.min.js"></script>
    <style>
        .cuadro {
            border: 1px solid #ccc;
            padding: 2px;
            margin: 1px;
            border-color: YellowGreen;
        }

        .rotated-button {
            display: inline-table;
            transform: rotate(270deg);
            transform-origin: center center;
            white-space: nowrap;
            width: 100%;
        }

        .rotated-text {
            transform: rotate(270deg);
            transform-origin: left top 0;
            white-space: nowrap;
            width: 20px; /* Ajusta según sea necesario */
        }

        .datalist-item {
            display: inline-block;
            margin: 10px; /* Ajusta según sea necesario */
        }
    </style>
    <script type="text/javascript">
        function AbrirModalMapaCultivo(_id, _item) {
            debugger;
            var oManager = window.radopen("Frm_ActividadesMapaCultivo.aspx?type=" + _id + "&itemID=" + _item, "wnd_MapaCultivo");
            oManager.setSize(800, 650); //Width, Height
            oManager.center();
            oManager.SetActive();
            //return false;
        }
        function OnClientClose(oWnd, args) {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                //document.getElementById('ContentPlaceHolder1_hdn_PersonaID').value = arg.PersonaID;
                //document.getElementById('ContentPlaceHolder1_hdn_PersonaNombre').value = arg.PersonaNombre;
                //document.getElementById('ContentPlaceHolder1_hdn_PersonaIdentificacion').value = arg.PersonaIdentificacion;
                ////                document.getElementById('ctl00_ContentPlaceHolder1_txt_Observaciones').value = arg.ActividadSecundariaID;
                ////                alert(arg.NombreActividad);
                __doPostBack('ctl00$ContentPlaceHolder1$btn_RetornoVentana', '');
            }
        }
        function clientbuttonclick(sender, args) {
            var button = args.get_item();
            if (button.get_commandName() == "Eliminar")
                args.set_cancel(!confirm('Está seguro que desea eliminar el registro?'));
        }

        function openModal() {
            var oWnd = radopen("Frm_ActividadesMapaCultivo.aspx", "wnd_MapaCultivo");
            oWnd.setSize(600, 400);
            oWnd.center();
            oWnd.set_modal(true);
            return false;
        }

        //Funciones para bloquear la pantalla al momento de procesar
        function bloquearPantallaBloque() {
            // Mostrar el overlay para bloquear la pantalla
            document.getElementById("overlayPlant").style.display = "block";
        }
        function bloquearPantalla() {
            // Mostrar el overlay para bloquear la pantalla
            document.getElementById("overlay").style.display = "block";
        }

        function desbloquearPantalla() {
            // Ocultar el overlay (si es necesario desde el cliente)
            document.getElementById("overlay").style.display = "none";
        }
    </script>
    <style>
        .rotated-content {
            display: inline-block;
            transform: rotate(270deg);
            transform-origin: center center;
            white-space: nowrap;
        }

        .custom-link-rotated-content {
            display: inline-block;
            white-space: nowrap;
            transform: rotate(270deg);
            transform-origin: center center;
            color: Black;
            font-size: 18px;
            font-weight: bold;
            text-decoration: none;
            border: 5px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 25px;
            text-align: center;
            padding: 10px 2px 5px 2px;
        }

        .custom-link {
            color: Black;
            font-size: 18px;
            font-weight: bold;
            text-decoration: none;
            padding: 5px 45% 5px 45%;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 20px;
            text-align: center;
        }

        .custom-link-area {
            display: inline-block;
            white-space: nowrap;
            color: Black;
            font-size: 22px;
            font-weight: bold;
            text-decoration: none;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 20px;
            width: 100%;
        }

        .custom-link-bloque {
            display: inline-block;
            white-space: nowrap;
            color: Black;
            font-size: 20px;
            font-weight: bold;
            text-decoration: none;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 20px;
            width: 100%;
        }

        .custom-link-lado {
            display: inline-block;
            white-space: nowrap;
            color: Black;
            font-size: 18px;
            font-weight: bold;
            text-decoration: none;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 20px;
            width: 100%;
        }

        .custom-link-cama {
            display: inline-block;
            white-space: nowrap;
            color: Black;
            font-size: small;
            font-weight: bold;
            text-decoration: none;
            border: 3px solid #ccc;
            border-radius: 8px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 20px;
            width: 70px;
            text-align: center;
        }

        .custom-link-cuadro {
            display: inline-block;
            white-space: nowrap;
            color: Black;
            text-decoration: none;
            width: 60px;
            border: 1px solid #ccc;
            border-radius: 0px;
            background-color: #f9f9f9;
            transition: background-color 0.3s ease;
            height: 20px;
            text-align: center;
        }

        .custom-link-area:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-link-bloque:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-link-lado:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-link-cama:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-link-cuadro:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-link-rotated-content:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-link:hover {
            background-color: #e0e0e0;
            color: blue;
        }

        .custom-border img {
            border: 2px solid #000000; /* Borde de 2px de color negro */
            border-radius: 5px; /* Bordes redondeados, opcional */
        }
    </style>
    <style>
        .multi-line-button .riText {
            white-space: normal;
            text-align: center;
            width: 100%;
            display: inline-block;
            line-height: 1.2; /* Ajusta el espacio entre las líneas */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdn_CultivoArea" runat="server" />
    <asp:HiddenField ID="hdn_CultivoAreaID" runat="server" />
    <asp:HiddenField ID="hdn_CultivoBloque" runat="server" />
    <asp:HiddenField ID="hdn_CultivoLado" runat="server" />
    <asp:HiddenField ID="hdn_CultivoNave" runat="server" />
    <asp:HiddenField ID="hdn_CultivoCama" runat="server" />
    <asp:HiddenField ID="hdn_CultivoCuadro" runat="server" />
    <asp:HiddenField ID="hdn_CampoCultivoID" runat="server" />
    <asp:HiddenField ID="hdn_VariedadID" runat="server" />
    <telerik:RadWindowManager ID="wnd_manager" runat="server" ShowContentDuringLoad="false"
        VisibleStatusbar="false" ReloadOnShow="true" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="wnd_MapaCultivo" runat="server" Width="650" Height="500" NavigateUrl="~/Frm_ActividadesMapaCultivo.aspx" Modal="true"
                OnClientClose="OnClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="lbl_Titulo" Text="MAPA DE CULTIVO" Font-Size="Large" Font-Bold="true"></asp:Label>
            </td>
            <td></td>
        </tr>
    </table>
    <asp:Panel ID="pnl_Buscador" runat="server">
        <asp:ImageButton ID="btn_Nuevo" runat="server" ToolTip="Nuevo Registro" ImageUrl="~/Images/Agregar.png" OnClick="btn_Nuevo_Click" />
        <fieldset>
            <legend>Criterios de Búsqueda</legend>
            <table width="100%">
                <tr>
                    <td width="150px" align="left">Proveedor/Finca:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmb_BuscarSucursal" runat="server" Width="200px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="150px" align="left">Nombre:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_BuscarNombre" runat="server" Width="250px"></telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_Buscar" runat="server" ImageUrl="~/Images/buscar.png" OnClick="btn_Buscar_Click" />
                        <asp:ImageButton ID="btn_RetornoVentana" runat="server" OnClick="btn_RetornoVentana_Click"
                            ImageUrl="~/Images/dot_clear.gif" />
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="gv_MapaCultivo" runat="server" AutoGenerateColumns="false" ShowGroupPanel="true" AllowFilteringByColumn="True" OnItemCommand="gv_MapaCultivo_ItemCommand" OnNeedDataSource="gv_MapaCultivo_NeedDataSource">
                <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" EnableRowHoverStyle="True"
                    ReorderColumnsOnClient="True">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" />
                </ClientSettings>
                <MasterTableView AllowSorting="True" CommandItemDisplay="Top" DataKeyNames="CampoCultivoID" GroupLoadMode="Client">
                    <CommandItemSettings ExportToPdfText="Export to PDF" ShowAddNewRecordButton="False"
                        ShowExportToCsvButton="True" ShowExportToExcelButton="True" ShowExportToPdfButton="True"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter Nombre column"
                            HeaderText="Nombre" UniqueName="column1" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Aream2" FilterControlAltText="Filter Aream2 column"
                            HeaderText="Area Cultivo" UniqueName="column2" ItemStyle-Width="75px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CantidadPlantas" FilterControlAltText="Filter CantidadPlantas column" AllowFiltering="False"
                            HeaderText="Nro. Plantas" UniqueName="column3" ItemStyle-Width="75px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Descripcion" FilterControlAltText="Filter Descripcion column" AllowFiltering="False"
                            HeaderText="Descripción" UniqueName="column4" ItemStyle-Width="150px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="70px" AllowFiltering="False">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text='<%# ObtenerNombreEstado(Int32.Parse(Eval("Estado".ToString()).ToString()))  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Editar" Groupable="false" ItemStyle-Width="1px" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="editar"
                                    TabIndex="1" CommandArgument='<%# Eval("CampoCultivoID") %>' ImageUrl="~/Images/edit.png"
                                    ToolTip="Editar este registro" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <PagerStyle Position="Bottom" PageSizes="5,10"></PagerStyle>
            </telerik:RadGrid>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_DatosCultivo" runat="server" Visible="false">
        <telerik:RadToolBar ID="tool_principal" runat="server" Width="100%"
            OnClientButtonClicking="clientbuttonclick" OnButtonClick="tool_principal_ButtonClick">
            <Items>
                <telerik:RadToolBarButton ImageUrl="~/Images/Grabar.png" ImagePosition="AboveText"
                    CommandName="Grabar" Text="Grabar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/restart.png" ImagePosition="AboveText"
                    CommandName="Refrescar" Text="Refrescar" ValidationGroup="Grupo1">
                </telerik:RadToolBarButton>
                <telerik:RadToolBarButton ImageUrl="~/Images/Regresar.png" ImagePosition="AboveText"
                    CommandName="Cancelar" Text="Cancelar" CausesValidation="false">
                </telerik:RadToolBarButton>
            </Items>
        </telerik:RadToolBar>
        <fieldset>
            <legend><strong>MAPA DE CULTIVO</strong></legend>
            <table>
                <tr>
                    <td>Proveedor/Finca:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_Sucursal" runat="server" Width="200px"></telerik:RadComboBox>
                    </td>
                    <td>Nombre:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_NombreCultivo" runat="server" Width="150px"></telerik:RadTextBox>
                    </td>
                    <td>Dirección:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Direccion" runat="server" Width="250px" Height="30px" TextMode="MultiLine"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Área del Cultivo:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_AreaCultivo" runat="server" MinValue="0" Value="0" Width="50px" ReadOnly="true">
                            <NumberFormat DecimalDigits="2" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>Nro. de Plantas:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroPlantas" runat="server" MinValue="0" Value="0" Width="50px" ReadOnly="true">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>Descripción:</td>
                    <td>
                        <telerik:RadTextBox ID="txt_Descripcion" runat="server" Width="250px" Height="30px" TextMode="MultiLine"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Estado:
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_Estado" runat="server" ReadOnly="true"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadCheckBox ID="chk_GenerarMapa" runat="server" Text="Generar Mapa de Cultivo" RenderMode="Lightweight" OnClick="chk_GenerarMapa_Click" Visible="true"></telerik:RadCheckBox>
                    </td>
                    <td>
                        <telerik:RadRadioButtonList ID="rbt_TipoTerreno" runat="server" Direction="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbt_TipoTerreno_SelectedIndexChanged" Visible="false">
                            <Items>
                                <telerik:ButtonListItem Text="Terreno Regular" Value="1" />
                                <telerik:ButtonListItem Text="Terreno Irregular" Value="2" />
                            </Items>
                        </telerik:RadRadioButtonList>
                    </td>
                </tr>
            </table>

            <table id="tb_ListAreas" runat="server" visible="false">
                <tr>
                    <td>
                        <fieldset>
                            <legend>AREAS</legend>
                            <asp:DataList ID="dl_AreasTotales" runat="server" OnItemCommand="dl_AreasTotales_ItemCommand" OnItemDataBound="dl_AreasTotales_ItemDataBound" RepeatDirection="horizontal">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdn_CultivoArea_ID" runat="server" />
                                                <telerik:RadLabel ID="lbl_NombreArea" runat="server" Text="AREA" Font-Bold="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadImageButton ID="btn_MostrarBloques" runat="server" Text="BLOQUE" Height="70px" Width="150px" CommandName="mostrar" CommandArgument='<%# Eval("CultivoAreaID") %>'
                                                    SingleClick="true" SingleClickText="Visualizar Bloques..." Font-Bold="true">
                                                    <Image Url="~/Images/tractor.png" />
                                                </telerik:RadImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <%--                                <SeparatorTemplate>
                                    <hr />
                                </SeparatorTemplate>--%>
                            </asp:DataList>
                            <asp:LinkButton ID="lnk_AddArea" runat="server" ToolTip="Agregar Nueva Area" Visible="true" OnClick="lnk_AddArea_Click">
                                <asp:Image ID="btn_AddArea" runat="server" ImageUrl="~/Images/addItem.png" BorderStyle="None"
                                    BorderColor="Transparent" BorderWidth="0px" />
                                <div style="display: inline; vertical-align: middle;">
                                    Agregar Nueva Area
                                </div>
                            </asp:LinkButton>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset id="field_Bloques" runat="server" visible="false">
                            <legend>BLOQUES</legend>
                            <asp:DataList ID="dl_BloquesTotales" runat="server" RepeatDirection="horizontal" OnItemCommand="dl_BloquesTotales_ItemCommand" OnItemDataBound="dl_BloquesTotales_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdn_CultivoBloqueID" runat="server" />
                                                <telerik:RadLabel ID="lbl_NombreBloque" runat="server" Text="BLOQUE" Font-Bold="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadImageButton ID="btn_MostrarBloque" runat="server" Text="" Height="70px" Width="100px" CommandName="mostrar" CommandArgument='<%# Eval("CultivoBloqueID") %>'
                                                    SingleClick="true" SingleClickText="Visualizar Bloques..." Font-Bold="true" CssClass="custom-border" OnClientClicked="bloquearPantallaBloque">
                                                    <Image Url="~/Images/invernadero.png" />
                                                </telerik:RadImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <%--                                <SeparatorTemplate>
                                    <hr />
                                </SeparatorTemplate>--%>
                            </asp:DataList>
                            <asp:LinkButton ID="lnk_AddBloque" runat="server" ToolTip="Agregar Nuevo Bloque" Visible="true" OnClick="lnk_AddBloque_Click">
                                <asp:Image ID="btn_AddBloque" runat="server" ImageUrl="~/Images/addItem.png" BorderStyle="None"
                                    BorderColor="Transparent" BorderWidth="0px" />
                                <div style="display: inline; vertical-align: middle;">
                                    Agregar Nuevo Bloque
                                </div>
                            </asp:LinkButton>
                            <!-- Overlay para bloquear la pantalla -->
                            <div id="overlay" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.5); z-index: 1000; text-align: center;">
                                <div style="position: relative; top: 50%; transform: translateY(-50%); color: white; font-size: 20px;">
                                    Procesando...   
                                    <br />
                                    <img src="../Images/Spinner.gif" alt="Procesando..." />
                                </div>
                            </div>
                            <div id="overlayPlant" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.5); z-index: 1000; text-align: center;">
                                <div style="position: relative; top: 50%; transform: translateY(-50%); color: white; font-size: 20px;">
                                    Cargando...   
                                    <br />
                                    <img src="../Images/Plant.gif" alt="Cargando..." />
                                </div>
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_GenerarMapaCultivo" runat="server" Visible="false">
        <fieldset>
            <legend><strong>
                <asp:Label ID="lbl_GenerarMapaCultivo" runat="server" Text="GENERAR MAPA DE CULTIVO"></asp:Label></strong></legend>
            <table>
                <tr runat="server" id="tr_AddArea" visible="true">
                    <td>Número de Áreas:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroAreas" runat="server" MinValue="0" Value="0" Width="50px">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextAreas" runat="server" Text="Cantidad Máxima de Áreas por Sucursal:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextAreasValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Bloques por Área:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroBloques" runat="server" Value="0" NumberFormat-DecimalDigits="0" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextBloques" runat="server" Text="Cantidad Máxima de Bloques por Sucursal:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextBloquesValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Lados por Bloque:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroLados" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextLados" runat="server" Text="Cantidad Máxima de Lados por Bloque:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextLadosValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Naves por Bloque:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroNaves" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextNaves" runat="server" Text="Cantidad Máxima de Naves por Bloque:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextNavesValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Camas por Nave:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroCamas" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextCamas" runat="server" Text="Cantidad Máxima de Camas por Bloque (Nave por lado):"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextCamasValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Cuadros por Cama:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroCuadros" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextCuadros" runat="server" Text="Cantidad Máxima de Cuadros por Cama:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextCuadrosValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Cantidad de Plantas por Bloque:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_CantidadPlantas" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="75px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="RadLabel3" runat="server" Text="Cantidad de Plantas por cada Bloque "></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Área por Bloque (m2):</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_AreaBloqueM2" runat="server" Value="0" NumberFormat-DecimalDigits="2" ShowSpinButtons="false" MinValue="0" Width="75px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Área en m2 por cada Bloque "></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadButton ID="btn_GenerarCampo" runat="server" Text="Generar Mapa de Cultivo" Height="40px" Visible="true" OnClick="btn_GenerarCampo_Click">
                            <Icon PrimaryIconUrl="~/Images/Cotizar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                        </telerik:RadButton>
                        <telerik:RadButton ID="btn_GenerarBloques" runat="server" Text="Agregar Bloques al Mapa de Cultivo" Height="40px" Visible="true" OnClick="btn_GenerarBloques_Click">
                            <Icon PrimaryIconUrl="~/Images/Cotizar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                        </telerik:RadButton>
                        <telerik:RadButton ID="btn_GenerarCancelar" runat="server" Text="Cancelar" Height="40px" Visible="true" OnClick="btn_GenerarCancelar_Click">
                            <Icon PrimaryIconUrl="~/Images/regresar2.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                        </telerik:RadButton>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_GenerarMapaCultivoIrregular" runat="server" Visible="false">
        <fieldset>
            <legend><strong>GENERAR MAPA DE CULTIVO POR BLOQUE</strong></legend>
            <table>
                <tr>
                    <td>Número de Áreas:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_CultivoArea" runat="server" Width="150px">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Número de Bloque:</td>
                    <td>
                        <telerik:RadComboBox ID="cmb_CultivoBloque" runat="server" Width="150px">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Número de Lados por Bloque:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroLadosIrregular" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextNroLadosIrregular" runat="server" Text="Cantidad Máxima de Lados por Bloque:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextNroLadosIrregularValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Naves por Lado:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroNavesIrregular" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextNavesIrregular" runat="server" Text="Cantidad Máxima de Naves por Bloque:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextNavesIrregularValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Camas por Nave:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroCamasIrregular" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextCamasIrregular" runat="server" Text="Cantidad Máxima de Camas por Nave:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextCamasIrregularValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Número de Cuadros por Cama:</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_NroCuadrosIrregular" runat="server" Value="0" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" MinValue="0" Width="50px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="lbl_TextCuadrosIrregular" runat="server" Text="Cantidad Máxima de Cuadros por Cama:"></telerik:RadLabel>
                        <telerik:RadLabel ID="lbl_TextCuadrosIrregularValor" runat="server" Text=""></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>Área por Bloque (m2):</td>
                    <td>
                        <telerik:RadNumericTextBox ID="txt_AreaBloqueM2Irregular" runat="server" Value="0" NumberFormat-DecimalDigits="2" ShowSpinButtons="false" MinValue="0" Width="75px"></telerik:RadNumericTextBox>
                    </td>
                    <td>
                        <telerik:RadLabel ID="RadLabel5" runat="server" Text="Área en m2 por Bloque "></telerik:RadLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadButton ID="btn_GenerarCampoBloque" runat="server" Text="Generar Mapa de Cultivo (Bloque)" Height="40px" Visible="true" OnClick="btn_GenerarCampoBloque_Click">
                            <Icon PrimaryIconUrl="~/Images/Cotizar.png" PrimaryIconTop="4px" PrimaryIconLeft="5px" PrimaryIconHeight="50px" PrimaryIconWidth="40px" />
                        </telerik:RadButton>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnl_CargarMapaCultivo" runat="server" Visible="false">
        <fieldset>
            <legend><strong>DATOS DEL MAPA DE CULTIVO</strong></legend>
            <table>
                <tr>
                    <td>
                        <asp:DataList ID="dlAreas" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" BorderStyle="Double" Style="text-align: center">
                            <ItemTemplate>
                                <div class="area">
                                    <asp:LinkButton ID="lbt_MostrarArea" runat="server" Text='<%# Eval("Nombre") %>' ToolTip="Visualizar datos del Área" OnClick="lbt_MostrarArea_Click" CommandArgument='<%# Eval("CultivoAreaID") %>' CssClass="custom-link-area"></asp:LinkButton>
                                    <asp:DataList ID="dlBloques" runat="server" RepeatColumns="2" DataSource='<%# Eval("SGF_CultivoBloque") %>' RepeatDirection="Horizontal" BorderStyle="Solid">
                                        <ItemTemplate>
                                            <div class="bloque">
                                                <asp:LinkButton ID="lbt_MostrarBloque" runat="server" Text='<%# Eval("Nombre") %>' ToolTip="Visualizar datos del Bloque" OnClick="lbt_MostrarBloque_Click" CommandArgument='<%# Eval("CultivoBloqueID") %>' CssClass="custom-link-bloque"></asp:LinkButton>
                                                <asp:DataList ID="dlLados" runat="server" RepeatColumns="2" DataSource='<%# Eval("SGF_CultivoLado") %>' RepeatDirection="Horizontal" BorderStyle="Groove" BorderColor="#ff6600">
                                                    <ItemTemplate>
                                                        <div class="lado">
                                                            <asp:LinkButton ID="lbt_MostrarLado" runat="server" Text='<%# Eval("Nombre") %>' ToolTip="Visualizar datos del Lado" OnClick="lbt_MostrarLado_Click" CommandArgument='<%# Eval("CultivoLadoID") %>' CssClass="custom-link-lado"></asp:LinkButton>
                                                            <asp:DataList ID="dlNaves" runat="server" DataSource='<%# Eval("SGF_CultivoNave") %>' RepeatDirection="Vertical" BorderStyle="Solid" BorderColor="#006600" OnItemDataBound="dlNaves_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <div class="nave">
                                                                        <table width="100%" borderstyle="Solid">
                                                                            <tr>
                                                                                <td width="15px">
                                                                                    <asp:LinkButton ID="lbt_MostrarNaveLA" runat="server" ToolTip="Visualizar datos de la Nave" OnClick="lbt_MostrarNave_Click" CommandArgument='<%# Eval("CultivoNaveID") %>' CssClass="custom-link-rotated-content"></asp:LinkButton>
                                                                                </td>
                                                                                <td width="50%">
                                                                                    <asp:DataList ID="dlCamas" runat="server" DataSource='<%# Eval("SGF_CultivoCama") %>' RepeatDirection="Vertical" BorderStyle="Groove" BorderColor="Blue" Style="text-align: left" OnItemDataBound="dlCamas_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <div class="cama">
                                                                                                <table width="100%">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="lbt_MostrarCamaLA" runat="server" ToolTip="Visualizar datos de la Cama" OnClick="lbt_MostrarCama_Click" CommandArgument='<%# Eval("CultivoCamaID") %>' CssClass="custom-link-cama"></asp:LinkButton>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DataList ID="dlCuadros" runat="server" RepeatColumns="10" DataSource='<%# Eval("SGF_CultivoCuadro") %>' RepeatDirection="Horizontal" BorderStyle="Solid" BorderColor="Cyan" OnItemDataBound="dlCuadros_ItemDataBound">
                                                                                                                <ItemTemplate>
                                                                                                                    <div class="cuadro">
                                                                                                                        <asp:LinkButton ID="lbt_MostrarCuadro" runat="server" ToolTip="Visualizar datos del Cuadro" OnClick="lbt_MostrarCuadro_Click" CommandArgument='<%# Eval("CultivoCuadroID") %>' CssClass="custom-link-cuadro" Font-Italic="true"></asp:LinkButton>
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:DataList>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="lbt_MostrarCamaLB" runat="server" ToolTip="Visualizar datos de la Cama" OnClick="lbt_MostrarCama_Click" CommandArgument='<%# Eval("CultivoCamaID") %>' CssClass="custom-link-cama"></asp:LinkButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </td>
                                                                                <td width="15px">
                                                                                    <asp:LinkButton ID="lbt_MostrarNaveLB" runat="server" ToolTip="Visualizar datos de la Nave" OnClick="lbt_MostrarNave_Click" CommandArgument='<%# Eval("CultivoNaveID") %>' CssClass="custom-link-rotated-content"></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </ItemTemplate>
                                                    <SeparatorTemplate>
                                                        <hr />
                                                        <asp:Label ID="lbl_label1" runat="server" Text="" Width="50px"></asp:Label>
                                                        <hr />
                                                        <hr />
                                                        <hr />
                                                        <hr />
                                                        <hr />
                                                    </SeparatorTemplate>
                                                </asp:DataList>
                                            </div>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                            <hr />
                                        </SeparatorTemplate>
                                    </asp:DataList>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <hr />
                            </SeparatorTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Text="info" Position="Center"
        AutoCloseDelay="0" Width="400" Height="150" Title="Notificación" Skin="Glow" EnableRoundedCorners="true">
    </telerik:RadNotification>
</asp:Content>
