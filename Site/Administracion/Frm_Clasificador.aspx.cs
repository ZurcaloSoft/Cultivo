using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Administracion
{
    public partial class Frm_Clasificador : System.Web.UI.Page
    {
        #region Variables y Propiedades
        public string TypeClassificator
        {
            get { return ViewState["TypeClassificator"].ToString(); }
            set { ViewState["TypeClassificator"] = value; }
        }
        protected List<SGF_Clasificador> Listclasificador
        {
            get
            {
                if (ViewState["Listclasificador"] == null)
                    ViewState["Listclasificador"] = new List<SGF_Clasificador>();
                return (List<SGF_Clasificador>)ViewState["Listclasificador"];
            }
            set
            {
                ViewState["Listclasificador"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                cargarCombos();
                if (!string.IsNullOrEmpty(Request["Type"]))
                {
                    TypeClassificator = Request["Type"].ToString();
                    switch (TypeClassificator)
                    {
                        #region Administración
                        case "00000000-0000-0000-0000-000000000000":        //Libre
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE CLASIFICADORES";
                            cmb_TipoClasificador.Enabled = true;
                            fiel_Buscar.Visible = true;
                            break;
                        #endregion
                        #region Cultivo
                        case "E03E4E4D-6492-45B1-BA5F-EDE1C416C73D":        //Cultivo - Áreas
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE AREAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.CultivoAreas.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.CultivoAreas.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "4498C717-2896-4934-B6BC-545E8EA20DD2":        //Cultivo - Tallos por Malla
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE TALLOS POR MALLA";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TallosPorMalla.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TallosPorMalla.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "2CF38792-6533-4FA4-B304-0DE2A7EBA47C":        //Cultivo - Actividades
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE ACTIVIDADES";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Actividades.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Actividades.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "0B29F36C-2E8B-4298-B3E0-675F3F04DEF7":        //Cultivo - Tipo Problemas de Flor
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE TIPOS DE PROBLEMA DE LA FLOR";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TipoProblemasFlor.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TipoProblemasFlor.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "7F3A7F8E-265C-40DD-BFD8-4E8F82647010":        //Cultivo - Problemas de Flor
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE PROBLEMAS DE LA FLOR";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.ProblemasFlor.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.ProblemasFlor.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "F39FA841-A8CA-4B41-A837-7C4B4955BE86":        //Cultivo - Estado Actividades
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN ESTADO DE LA PLANTA";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Actividades.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Actividades.ToString();
                            fiel_Buscar.Visible = false;
                            break;

                        case "9BA20E91-8E64-4068-BC1A-462E26363A57":        //Cultivo - Tipo de Flor
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DEL TIPO DE FLOR";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TipoFlor.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TipoFlor.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "16D2C551-D54D-462E-9A1C-47D093129706":        //Cultivo - Unidad de Cultivo
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            lbl_Titulo.Text = "ADMINISTRACIÓN UNIDAD DE CULTIVO";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.UnidadCultivoVentas.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.UnidadCultivoVentas.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        #endregion
                        #region Poscosecha
                        case "C2609DB5-0E96-4625-90C6-B3CB79EBB948":        //Poscosecha - Tallos por Bonche
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE TALLOS POR BONCHE";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TallosPorBonche.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TallosPorBonche.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "1A3FA34D-8E49-4211-A05C-C09927439D46":        //Poscosecha - Mesas
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE MESAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Mesas.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Mesas.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "1984F8A1-850F-4C1E-9CCC-A6D0C9171003":        //Poscosecha - Longitudes
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE LONGITUDES";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Longitudes.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Longitudes.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        //case "C2609DB5-0E96-4625-90C6-B3CB79EBB948":        //Poscosecha - Tallos por Bonche
                        //    master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                        //    lbl_Titulo.Text = "ADMINISTRACIÓN DE TALLOS POR BONCHE";
                        //    cmb_TipoClasificador.Enabled = false;
                        //    cmb_TipoClasificador.SelectedValue = TipoClasificador.TallosPorBonche.ToString();
                        //    cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TallosPorBonche.ToString();
                        //    fiel_Buscar.Visible = false;
                        //    break;
                        case "F8D70789-156F-4A38-904D-E33538D59B1B":        //Poscosecha - Color
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE COLORES";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Color.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Color.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "59397B48-46C0-4314-9673-F3293114BBCB":        //Poscosecha - Calidad Flor Exportación
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE CALIDAD DE FLOR";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.CalidadFlor.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.CalidadFlor.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "11B9707F-381C-4014-BCDF-A640F277713F":        //Poscosecha - Lámina Bonche
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE LÁMINA BONCHE";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.LaminaBonche.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.LaminaBonche.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "513331CB-4EEF-4BBA-8E2D-2CE74C2D9318":        //Poscosecha - Cuartros Frios Fincas
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN CUARTOS FRIOS DE FINCAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.CuartoFrioFinca.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.CuartoFrioFinca.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "BED71956-7B9E-47CC-8F47-4BB0ED991C91":        //Poscosecha - Punto de Corte
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DEL PUNTO DE CORTE";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.PuntoCorte.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.PuntoCorte.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        #endregion
                        #region Empaque 
                        case "662A8A85-211B-45C1-B2AE-1BCBE69C8237":        //Empaque - Tipos de Caja
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Empaque;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE TIPOS DE CAJAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TipoCaja.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TipoCaja.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "3140CF3B-C4BB-434F-A67A-3D46E80E93DC":        //Empaque - Cajas
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Empaque;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE CAJAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Cajas.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Cajas.ToString();
                            fiel_Buscar.Visible = false;
                            cmb_Parent.DataSource = Listclasificador.Where(x => x.TipoClasificadorID == TipoClasificador.TipoCaja).OrderBy(x => x.Nombre);
                            cmb_Parent.DataTextField = "Nombre";
                            cmb_Parent.DataValueField = "ClasificadorID";
                            cmb_Parent.DataBind();
                            cmb_Parent.Items.Insert(0, new RadComboBoxItem("Seleccione Clasificador", Guid.Empty.ToString()));
                            break;
                        case "D2726425-33FF-4178-98B9-CBD77E2816D1":        //Empaque - Bonches por Caja
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Empaque;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE BONCHES POR CAJA";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.BonchesPorCaja.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.BonchesPorCaja.ToString();
                            fiel_Buscar.Visible = false;
                            cmb_Parent.DataSource = Listclasificador.Where(x => x.TipoClasificadorID == TipoClasificador.Cajas).OrderBy(x => x.Nombre);
                            cmb_Parent.DataTextField = "Nombre";
                            cmb_Parent.DataValueField = "ClasificadorID";
                            cmb_Parent.DataBind();
                            cmb_Parent.Items.Insert(0, new RadComboBoxItem("Seleccione Clasificador", Guid.Empty.ToString()));
                            break;
                        #endregion
                        #region Comercial
                        case "8437712C-5BB6-4D64-9C16-9317A0422B49":       //Comercial - Tipo Cliente
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN TIPO DE CLIENTE";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TipoCliente.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TipoCliente.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "6CA88FE4-81C3-4E64-AAC3-79F8E62A2BD9":       //Comercial - Países
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE PAISES";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Pais.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Pais.ToString();
                            fiel_Buscar.Visible = false;
                            cmb_Parent.DataSource = Listclasificador.Where(x => x.TipoClasificadorID == TipoClasificador.Mercado).OrderBy(x => x.Nombre);
                            cmb_Parent.DataTextField = "Nombre";
                            cmb_Parent.DataValueField = "ClasificadorID";
                            cmb_Parent.DataBind();
                            cmb_Parent.Items.Insert(0, new RadComboBoxItem("Seleccione Clasificador", Guid.Empty.ToString()));

                            break;
                        case "56195587-9FBA-452E-9A41-92CC6DBC998B":       //Comercial - Ciudades
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE CIUDADES";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Ciudad.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Ciudad.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "D54A7A61-52B6-4F05-8C3A-30C879C1E12E":       //Comercial - Cuartos Fríos Cargueras
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE CUARTOS FRIOS CARGUERAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.CuartoFrioCarguera.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.CuartoFrioCarguera.ToString();
                            fiel_Buscar.Visible = false;
                            cmb_Parent.DataSource = Listclasificador.Where(x => x.TipoClasificadorID == TipoClasificador.Cargueras).OrderBy(x => x.Nombre);
                            cmb_Parent.DataTextField = "Nombre";
                            cmb_Parent.DataValueField = "ClasificadorID";
                            cmb_Parent.DataBind();
                            cmb_Parent.Items.Insert(0, new RadComboBoxItem("Seleccione Clasificador", Guid.Empty.ToString()));
                            break;
                        case "EC0C780D-FA20-487C-84BD-0844E7C398CB":       //Comercial - Aduanas
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE ADUANAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Aduanas.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Aduanas.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "7F490CE2-4AAE-4CDB-BF8D-02F58F58F2F1":       //Comercial - Estado Venta
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN ESTADOS DE VENTA";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.EstadoVenta.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.EstadoVenta.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        case "63E098D1-74C5-41AD-BFEA-2709E1B89029":       //Comercial - Cargueras
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN CARGUERAS";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.Cargueras.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.Cargueras.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        #endregion
                        #region Recepción
                        case "C2360486-3AD7-46B0-90C3-1AD97E261C37":        //Compras
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Recepcion;
                            lbl_Titulo.Text = "ADMINISTRACIÓN TIPO DE PROVEEDORES";
                            cmb_TipoClasificador.Enabled = false;
                            cmb_TipoClasificador.SelectedValue = TipoClasificador.TipoProveedores.ToString();
                            cmb_TipoClasificadorBuscar.SelectedValue = TipoClasificador.TipoProveedores.ToString();
                            fiel_Buscar.Visible = false;
                            break;
                        #endregion
                        case "6DDB5385-8090-4D61-AD8F-62961BBA294F":        //TalentoHumano
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.TalentoHumano;
                            break;
                    }
                }

                //master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                llenarGrid();
                gv_Clasificador.DataBind();
            }
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_ClasificadorID.Value = Guid.Empty.ToString();
            pnl_Buscar.Visible = false;
            pnl_Datos.Visible = false;
            pnl_Editar.Visible = true;
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
            gv_Clasificador.DataBind();
        }

        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _clasificador = client.Clasificador_Buscar_VTA(txt_NombreCategoriaBuscar.Text, new Guid(cmb_TipoClasificadorBuscar.SelectedValue), Guid.Empty);
            gv_Clasificador.DataSource = _clasificador.Count() > 0 ? _clasificador.OrderBy(x => x.NombreClasificador).ToList() : _clasificador;
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            cmb_TipoClasificadorBuscar.SelectedValue = Guid.Empty.ToString();
            txt_NombreCategoriaBuscar.Text = "";
        }
        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Grabar":
                    Grabar();
                    break;
                case "Cancelar":
                    Cancelar();
                    break;
            }
        }

        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();

            Listclasificador = client.Clasificador_ObtenerTodo();
            cmb_TipoClasificadorBuscar.DataSource = client.TipoClasificador_ObtenerTodo().OrderBy(x => x.Nombre);
            cmb_TipoClasificadorBuscar.DataTextField = "Nombre";
            cmb_TipoClasificadorBuscar.DataValueField = "TipoClasificadorID";
            cmb_TipoClasificadorBuscar.DataBind();
            cmb_TipoClasificadorBuscar.Items.Insert(0, new RadComboBoxItem("Seleccione Tipo de Clasificador", Guid.Empty.ToString()));

            cmb_TipoClasificador.DataSource = client.TipoClasificador_ObtenerTodo().OrderBy(x => x.Nombre);
            cmb_TipoClasificador.DataTextField = "Nombre";
            cmb_TipoClasificador.DataValueField = "TipoClasificadorID";
            cmb_TipoClasificador.DataBind();
            cmb_TipoClasificador.Items.Insert(0, new RadComboBoxItem("Seleccione Tipo de Clasificador", Guid.Empty.ToString()));

            cmb_Parent.DataSource = Listclasificador.OrderBy(x => x.Nombre);
            cmb_Parent.DataTextField = "Nombre";
            cmb_Parent.DataValueField = "ClasificadorID";
            cmb_Parent.DataBind();
            cmb_Parent.Items.Insert(0, new RadComboBoxItem("Seleccione Clasificador", Guid.Empty.ToString()));

        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            switch (Estado)
            {
                case 0:
                    return "Borrado";
                case 1:
                    return "Activo";
                case 2:
                    return "Borrado";
            }
            return "";
        }

        protected void gv_Clasificador_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }

        protected void gv_Clasificador_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_ClasificadorID.Value = e.CommandArgument.ToString();
                    pnl_Buscar.Visible = false;
                    pnl_Datos.Visible = false;
                    pnl_Editar.Visible = true;
                    CargarDatos();
                    break;
            }
        }

        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscar.Visible = true;
            pnl_Datos.Visible = true;
            pnl_Editar.Visible = false;
        }
        protected void CargarDatos()
        {
            if (new Guid(hdn_ClasificadorID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_Clasificador _clasificador = client.Clasificador_ObtenerPorID(new Guid(hdn_ClasificadorID.Value));
            if (_clasificador != null)
            {
                cmb_TipoClasificador.SelectedValue = _clasificador.TipoClasificadorID.ToString();
                cmb_Parent.SelectedValue = _clasificador.ParentID.ToString();
                txt_Codigo.Text = _clasificador.Codigo;
                txt_Estado.Text = ObtenerNombreEstado((int)_clasificador.Estado);
                txt_Nombre.Text = _clasificador.Nombre;
                txt_Observaciones.Text = _clasificador.Descripcion;
                txt_Valor.Text = _clasificador.Valor;
            }
        }
        private void Grabar()
        {
            if (txt_Nombre.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el nombre del Clasificador");
                return;
            }
            if (cmb_TipoClasificador.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Módulo ");
                return;
            }
            try
            {
                SGF_Clasificador newClasificador = new SGF_Clasificador();
                newClasificador.ClasificadorID = new Guid(hdn_ClasificadorID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_ClasificadorID.Value);
                newClasificador.TipoClasificadorID = new Guid(cmb_TipoClasificador.SelectedValue);
                newClasificador.Codigo = txt_Codigo.Text;
                newClasificador.Valor = txt_Valor.Text;
                newClasificador.Nombre = txt_Nombre.Text;
                newClasificador.ParentID = new Guid(cmb_Parent.SelectedValue);
                newClasificador.Descripcion = txt_Observaciones.Text;
                newClasificador.Estado = 1;
                LogicClient client = new LogicClient();
                client.Clasificador_Grabar(newClasificador);
                VerMensaje("INFORMACIÓN", "info", "info", "Datos Registrado correctamente.");
                Cancelar();
                LimpiarControles();
                llenarGrid();
                gv_Clasificador.DataBind();
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Hubo un problema al momento de grabar el registro.");
            }
        }
        protected void LimpiarControles()
        {
            txt_NombreCategoriaBuscar.Text = "";
            cmb_TipoClasificadorBuscar.SelectedValue = TypeClassificator.ToLower();// Guid.Empty.ToString();
            cmb_TipoClasificador.SelectedValue = TypeClassificator.ToLower();// Guid.Empty.ToString();
            cmb_Parent.SelectedValue = Guid.Empty.ToString();
            hdn_ClasificadorID.Value = null;
            gv_Clasificador.DataSource = null;
            txt_Codigo.Text = "";
            txt_Estado.Text = "";
            txt_Codigo.Text = "";
            txt_Valor.Text = "";
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";
            txt_Nombre.Text = "";
        }
        private void VerMensaje(string title, string titIcon, string icon, string mensaje)
        {
            RadNotification1.Title = title;// "Friendship invitation";
            RadNotification1.TitleIcon = titIcon;// "none";//"info"
            RadNotification1.ContentIcon = icon;//"warning";//"info"
            RadNotification1.Text = mensaje;
            RadNotification1.Show();

        }

    }
}