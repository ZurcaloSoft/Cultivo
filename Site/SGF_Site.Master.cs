using SGF.Site.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site
{
    public partial class SGF_Site : System.Web.UI.MasterPage
    {
        public Int32 VisibilityMenuItem
        {
            get
            {
                if (ViewState["VisibilityMenuItem"] == null)
                    ViewState["VisibilityMenuItem"] = -1;
                return (Int32)ViewState["VisibilityMenuItem"];
            }
            set
            {
                ViewState["VisibilityMenuItem"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Me.Logueado)
                Me.CerrarSession();

            //menu.Visible = true;

            if(!IsPostBack)
            {

                #region Menú Administración
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Administración".ToUpper() && f.EstadoPermiso== 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.Administracion)
                    menu.Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipo Clasificador".ToUpper() && f.EstadoPermiso== 1) != 0)
                    menu.Items[0].Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Clasificador".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[1].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Parámetros".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[2].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Grupo".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[3].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Empresa".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[4].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Sucursal".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[5].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Bodega".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[6].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipo de Usuario".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[7].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Usuario".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[8].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Módulo".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[9].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Formularios".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[10].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Botones".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[11].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Parámetros Contraseña".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[12].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Almacén".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[0].Items[13].Visible = true;
                #endregion

                #region Menú Cultivo
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Cultivo".ToUpper() && f.EstadoPermiso == 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.Cultivo)
                    menu.Items[1].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Envío de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Mapa de Cultivo".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[1].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Variedades".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[2].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Áreas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[3].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tallos por Malla".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[4].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Actividades".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[5].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipos Problema de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[6].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Problemas de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[7].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Estado de la Planta".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[8].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Estado de la Variedad".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[9].Visible = false;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipo de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[10].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Unidad de Cultivo".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[11].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Temporada".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[12].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Proyección de Producción".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[1].Items[13].Visible = true;
                #endregion

                #region Menú PosCosecha
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Poscosecha".ToUpper() && f.EstadoPermiso == 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.Poscosecha)
                    menu.Items[2].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Recepción de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Distribución de Mallas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[1].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Clasificación de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[2].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Etiquetado Flor de Exportación".ToUpper() && f.EstadoPermiso == 1) != 0)
                {
                    menu.Items[2].Items[3].Visible = true;
                    menu.Items[2].Items[3].NavigateUrl= "http://localhost:8091/UI/index.html?id=" + Me.Usuario.UsuarioID.ToString();
                }
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Mesas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[4].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Longitudes".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[5].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tallos por Bonche".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[6].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Color".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[7].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Calidad Flor Exportación".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[8].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Lámina Bonche".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[9].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Productos de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[10].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Cuartos Fríos Fincas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[11].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Punto de Corte".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[2].Items[12].Visible = true;
                #endregion

                #region Menú Empaque
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Empaque".ToUpper() && f.EstadoPermiso == 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.Empaque)
                    menu.Items[3].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Empaque de Piezas".ToUpper() && f.EstadoPermiso == 1) != 0)
                {
                    menu.Items[3].Items[0].Visible = true;
                    menu.Items[3].Items[0].NavigateUrl= "http://localhost:8091/UI/index.html?id=" + Me.Usuario.UsuarioID.ToString();
                }
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Carga de Disponibilidad".ToUpper() && f.EstadoPermiso == 1) != 0)
                {
                    menu.Items[3].Items[1].Visible = true;
                    menu.Items[3].Items[1].NavigateUrl= "http://localhost:8091/UI/index.html?id=" + Me.Usuario.UsuarioID.ToString();

                }
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Bajas de Flor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[3].Items[2].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipos de Caja".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[3].Items[3].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Cajas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[3].Items[4].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Bonches por Caja".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[3].Items[5].Visible = true;
                #endregion

                #region Menú Comercial
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Comercial".ToUpper() && f.EstadoPermiso == 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.Comercial)
                    menu.Items[4].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Daes".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Ordenes Fijas".ToUpper() && f.EstadoPermiso == 1) != 0)
                {
                    menu.Items[4].Items[1].Visible = true;
                    menu.Items[4].Items[1].NavigateUrl = "http://localhost:8091/UI/index.html?id=" + Me.Usuario.UsuarioID.ToString();
                }
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Venta Exportación".ToUpper() && f.EstadoPermiso == 1) != 0)
                {
                    menu.Items[4].Items[2].NavigateUrl = "http://localhost:8091/UI/index.html?id=" + Me.Usuario.UsuarioID.ToString();
                    menu.Items[4].Items[2].Visible = true;
                }
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Venta Nacional".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[3].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Facturas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[4].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Clientes".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[5].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipo Cliente".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[6].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Marcaciones".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[7].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Líneas Aéreas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[8].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Países".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[9].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Ciudades".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[10].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Vendedores".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[11].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Cuartos Fríos (Carguera)".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[12].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Aduanas".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[13].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Cargueras".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[14].Visible = true;
                //if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Estado Cliente".ToUpper() && f.EstadoPermiso == 1) != 0)
                //    menu.Items[4].Items[15].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Estado Venta".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[4].Items[15].Visible = true;
                #endregion

                #region Menú Recepción
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Recepción".ToUpper() && f.EstadoPermiso == 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.Recepcion)
                    menu.Items[5].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipo de Proveedor".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[5].Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Proveedores".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[5].Items[1].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Proveedor Variedad".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[5].Items[2].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Proveedor Precios".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[5].Items[3].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Proveedor Bodega".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[5].Items[4].Visible = true;
                #endregion

                #region Menú Talento Humano
                if (Me.Usuario.Menus.Count(f => f.Modulo.ToUpper() == "Talento Humano".ToUpper() && f.EstadoPermiso == 1) != 0 && VisibilityMenuItem == (int)Enums.ModuloIndex.TalentoHumano)
                    menu.Items[6].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Tipo de Persona".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[6].Items[0].Visible = true;
                if (Me.Usuario.Menus.Count(f => f.Formulario.ToUpper() == "Persona".ToUpper() && f.EstadoPermiso == 1) != 0)
                    menu.Items[6].Items[1].Visible = true;
                #endregion
            }

        }

        protected void menu_principal_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {

        }

        protected void btnexit_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected String GetUsuario()
        {
            if (Me.Logueado)
                return Me.Usuario.NombreUsuario;
            return "";
        }
        protected String GetDependencia()
        {
            if (Me.Logueado)
                return Me.Usuario.EmpresaNombre;
            return "";
        }
        protected String GetNombre()
        {
            if (Me.Logueado)
                return Me.Usuario.Nombre;
            return "";
        }

        public void ViewMenu(bool valor)
        {
            menu.Visible = valor;
        }
        public void HideMenuModulo(bool valor)
        {
            menu.Items[0].Visible = valor;
            menu.Items[1].Visible = valor;
            menu.Items[2].Visible = valor;
            menu.Items[3].Visible = valor;
            menu.Items[4].Visible = valor;
            menu.Items[5].Visible = valor;
            menu.Items[6].Visible = valor;
        }
        public void ViewMenuModulo(int index, bool valor)
        {
            menu.Items[index].Visible = valor;
        }
    }
}