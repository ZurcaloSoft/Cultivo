using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGF.Site.Compras
{
    public partial class Frm_ProveedorBodega : System.Web.UI.Page
    {
        public Int32 Numerador
        {
            get { return (Int32)ViewState["Numerador"]; }
            set { ViewState["Numerador"] = value; }
        }
        public bool EsNuevo
        {
            get { return (bool)ViewState["EsNuevo"]; }
            set { ViewState["EsNuevo"] = value; }
        }

        protected List<CO_PROVEEDOR> ListProveedorTemporal
        {
            get
            {
                if (ViewState["ListProveedorTemporal"] == null)
                    ViewState["ListProveedorTemporal"] = new List<CO_PROVEEDOR>();
                return (List<CO_PROVEEDOR>)ViewState["ListProveedorTemporal"];
            }
            set
            {
                ViewState["ListProveedorTemporal"] = value;
            }
        }
        protected List<CO_BODEGA> ListBodegaTemporal
        {
            get
            {
                if (ViewState["ListBodegaTemporal"] == null)
                    ViewState["ListBodegaTemporal"] = new List<CO_BODEGA>();
                return (List<CO_BODEGA>)ViewState["ListBodegaTemporal"];
            }
            set
            {
                ViewState["ListBodegaTemporal"] = value;
            }
        }

        //protected List<SGF_ProveedorBodega> ListProveedorBodegaGeneral
        //{
        //    get
        //    {
        //        if (ViewState["ListProveedorBodegaGeneral"] == null)
        //            ViewState["ListProveedorBodegaGeneral"] = new List<SGF_ProveedorBodega>();
        //        return (List<SGF_ProveedorBodega>)ViewState["ListProveedorBodegaGeneral"];
        //    }
        //    set
        //    {
        //        ViewState["ListProveedListProveedorBodegaGeneralorVariedadGeneral"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                Numerador = 0;
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Recepcion;
                //cargarCombos();
                //ConsultarListado();
                //gv_Temporada.DataBind();
                //EsNuevo = false;
                //txt_EmpresaRUC.Text = Me.Usuario.EmpresaID;
                //txt_EliminadoEmpresa.Text = Me.Usuario.EmpresaID;
            }
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

        }

        protected void dl_InfProVar_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void dl_InfProVar_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            switch (Estado)
            {
                case 0:
                    return "Pasivo";
                case 1:
                    return "Activo";
                case 2:
                    return "Pasivo";
            }
            return "";
        }
        protected Int32 NumeradorColumna()
        {
            Int32 _num = 0;
            _num = Numerador + 1;
            Numerador = _num;
            return _num;
        }
    }
}