using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGF.Site
{
    public partial class _Blank : System.Web.UI.Page
    {
        #region Variables y Propiedades
        public string TipoModulo
        {
            get { return ViewState["TipoModulo"].ToString(); }
            set { ViewState["TipoModulo"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.ViewMenu(true);
                master.HideMenuModulo(false);
                if (!string.IsNullOrEmpty(Request["Type"]))
                {
                    TipoModulo = Request["Type"].ToString();
                    switch (TipoModulo)
                    {
                        case "B44C1F71-21EC-4FD7-AD4C-D6C0F3434CE0":        //Administración
                            master.ViewMenuModulo((int)Enums.ModuloIndex.Administracion, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                            break;
                        case "D0DF07F1-01B8-44C3-B472-B6AB7110FD76":        //Cultivo
                            master.ViewMenuModulo((int)Enums.ModuloIndex.Cultivo, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                            break;
                        case "259A1102-7C1B-4917-BDEA-60059A3D468F":        //Poscosecha
                            master.ViewMenuModulo((int)Enums.ModuloIndex.Poscosecha, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Poscosecha;
                            break;
                        case "34A4F66C-5F98-47FA-A9AB-97855093E20D":        //Empaque
                            master.ViewMenuModulo((int)Enums.ModuloIndex.Empaque, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Empaque;
                            break;
                        case "A7FD68C3-D7E5-4A80-997F-C576A618A3F6":       //Comercial
                            master.ViewMenuModulo((int)Enums.ModuloIndex.Comercial, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            break;
                        case "0A007EA0-36C2-43A4-AA67-20457436BFA2":        //Recepcion
                            master.ViewMenuModulo((int)Enums.ModuloIndex.Recepcion, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Recepcion;
                            break;
                        case "6DDB5385-8090-4D61-AD8F-62961BBA294F":        //TalentoHumano
                            master.ViewMenuModulo((int)Enums.ModuloIndex.TalentoHumano, true);
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.TalentoHumano;
                            break;
                    }
                }
            }
        }
    }
}
