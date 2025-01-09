using SGF.BussinessLogic.Common;
using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using SecurityException = SGF.BussinessLogic.Common.SecurityException;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        [WebMethod]
        [FaultContract(typeof(Common.InfraestructureException))]
        [FaultContract(typeof(Common.LogicException))]
        [FaultContract(typeof(Common.SecurityException))]
        public SEG_Usuarios_VTA Seguridad_ObtenerUsuarioPorID(String NombreUsuario, String Clave)
        {
            DataModel model = new DataModel();

            //Se debe determinar si tiene permisos ya que si no los tiene no debe ingresar al sistema
            //esto se lo hace en la vista STD_PERFIL_MENU_VTA ya qjue aqui se 
            string appID = (ConfigurationManager.AppSettings["AppID"]);

            var Usuario = model.SEG_Usuarios_VTA.Where(f =>
                    f.UserName.ToLower() == NombreUsuario.ToLower()
                    && f.EstadoPersona == "ACTIVO" && f.EstadoUsuario == 1
                    && f.AppID == appID);

            if (Usuario == null)
                throw new FaultException<SecurityException>(new SecurityException("El usuario o la clave estan incorrectas", SecurityActions.Message), new FaultReason(""));
            if (Usuario.Count() == 0)
                throw new FaultException<SecurityException>(new SecurityException("El usuario o la clave estan incorrectas", SecurityActions.Message), new FaultReason(""));
            if (EncryptText.EncryptText.VerifyPassword(Clave, Usuario.First().Password) == false)
            {
                throw new FaultException<SecurityException>(new SecurityException("El usuario o la clave estan incorrectas", SecurityActions.Message), new FaultReason(""));
                // return null;
            }     
            if (Usuario != null)
            {
                return Usuario.First();
            }

            return null;
        }

        [OperationContract]
        [WebMethod]
        [FaultContract(typeof(Common.InfraestructureException))]
        [FaultContract(typeof(Common.LogicException))]
        [FaultContract(typeof(Common.SecurityException))]
        public List<SEG_Usuarios_VTA> Seguridad_ObtenerUsuarioTodos()
        {
            DataModel model = new DataModel();

            string appID = (ConfigurationManager.AppSettings["AppID"]);

            var Usuario = model.SEG_Usuarios_VTA.Where(f => f.AppID == appID);

            if (Usuario != null)
            {
                return Usuario.ToList();
            }
            return null;
        }

        [OperationContract]
        [WebMethod]
        [FaultContract(typeof(Common.InfraestructureException))]
        [FaultContract(typeof(Common.LogicException))]
        [FaultContract(typeof(Common.SecurityException))]
        public List<SEG_Formularios_VTA> Menu_ObtenerTodosPor(Guid usuarioID)
        {
            DataModel model = new DataModel();

            decimal appID = Decimal.Parse(ConfigurationManager.AppSettings["AppID"]);

            var formularios = model.SEG_Formularios_VTA.Where(f =>
                    f.UsuarioID == usuarioID &&
                    f.EstadoPermiso == 1);

            return formularios.ToList();
        }

        [OperationContract]
        [WebMethod]
        [FaultContract(typeof(Common.InfraestructureException))]
        [FaultContract(typeof(Common.LogicException))]
        [FaultContract(typeof(Common.SecurityException))]
        public List<SEG_Botones_VTA> Botones_ObtenerTodosPor(Guid usuarioID)
        {
            DataModel model = new DataModel();

            decimal appID = Decimal.Parse(ConfigurationManager.AppSettings["AppID"]);

            var formularios = model.SEG_Botones_VTA.Where(f =>
                    f.UsuarioID == usuarioID &&
                    f.EstadoBoton == 1 && f.EstadoPermiso == 1);

            return formularios.ToList();
        }

        [OperationContract]
        [WebMethod]
        [FaultContract(typeof(Common.InfraestructureException))]
        [FaultContract(typeof(Common.LogicException))]
        [FaultContract(typeof(Common.SecurityException))]
        public List<SGF_ParametrosPassword> Seguridad_ParametrosPassword()
        {
            DataModel model = new DataModel();

            return model.SGF_ParametrosPassword.Where(f => f.Estado == 1).ToList();
        }
    }
}
