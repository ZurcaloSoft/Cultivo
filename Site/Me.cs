using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGF.Site
{

    public class Me
    {
        public static void LlenaUsuario
      (SEG_Usuarios_VTA wsUsuario,
      List<SEG_Formularios_VTA> wsMenues,
      List<SEG_Botones_VTA> wsBotones)
        {
            if (wsUsuario == null)
                HttpContext.Current.Session["Usuario"] = null;

            Usuario usuario = new Usuario();
            usuario.UsuarioID = wsUsuario.UsuarioID;
            usuario.NombreUsuario = wsUsuario.UserName;
            usuario.Nombre = wsUsuario.Nombre;
            usuario.Cedula = wsUsuario.Identificacion;
            usuario.EmpresaID = wsUsuario.EmpresaID;
            usuario.EmpresaNombre = wsUsuario.EmpresaNombre;
            usuario.Menus = wsMenues;
            usuario.Botones = wsBotones;
            usuario.TipoPersonaID = wsUsuario.TipoPersonaID;
            usuario.TipoUsuarioID = wsUsuario.TipoUsuarioID;
            HttpContext.Current.Session["Usuario"] = usuario;
            //HttpContext.Current.Session["Perfiles"] = Perfiles;
        }

        public static void CerrarSession()
        {
            HttpContext.Current.Session["Usuario"] = null;
            HttpContext.Current.Response.Redirect("~/Login.aspx", true);
        }

        public static bool Logueado
        {
            get
            {
                return HttpContext.Current.Session["Usuario"] != null;
            }
        }
        public static void ValidarSesion()
        {
            if (!Logueado)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx", true);
            }
        }

        public static Usuario Usuario
        {
            get
            {
                return (Usuario)HttpContext.Current.Session["Usuario"];
            }
        }
        public int MyProperty { get; set; }

        public static List<String> Perfiles
        {
            get
            {
                return (List<String>)HttpContext.Current.Session["Perfiles"];
            }
        }
    }

    public class Usuario
    {
        public Guid UsuarioID { get; set; }

        public String NombreUsuario { get; set; }

        public String Nombre { get; set; }

        public String Cedula { get; set; }
        public String EmpresaID { get; set; }
        public String EmpresaNombre { get; set; }
        public Guid TipoPersonaID { get; set; }
        public Guid TipoUsuarioID { get; set; }

        public List<SEG_Formularios_VTA> Menus { get; set; }
        public List<SEG_Botones_VTA> Botones { get; set; }
    }
    public class Perfil
    {
        public decimal PerfilID { set; get; }

        public string PerfilDescripcion { set; get; }
    }
}