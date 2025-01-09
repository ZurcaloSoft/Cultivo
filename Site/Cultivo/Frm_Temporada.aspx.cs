using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGF.Site.Cultivo
{
    public partial class Frm_Temporada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SGF_Site master = (SGF_Site)Page.Master;
            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;


        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void gv_Temporada_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void gv_Temporada_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
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

    }
}