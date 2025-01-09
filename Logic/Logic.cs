using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using SGF.DataAccess;
using SGF.BussinessLogic;

namespace SGF.BussinessLogic
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceContract]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class Logic
    {
        
        #region ServiceActivation

        [OperationContract]
        //[FaultContract(typeof(InfraestructureFault))]
        //[FaultContract(typeof(ValidationFault))]
        //[FaultContract(typeof(SecurityFault))]
        public void Activate()
        {
        }

        #endregion

        #region Funciones basicas

        #endregion

        #region Funciones complejas
        
        #endregion
    }
    public class Enums
    {
        public enum TiposIdentificacionValidador { Cedula = 1, RUC = 2, Error = 3 }
        public enum TiposOrigenRUCValidador { PersonaNatural = 1, Privada = 2, Publica = 3, Error = 4 }
        public enum ModuloIndex { Administracion = 0, Cultivo = 1, Poscosecha = 2, Empaque = 3, Comercial = 4, Compras = 5, TalentoHumano = 6 }
        public enum MapaCultivo { MapaCultivo = 0, Area = 1, Bloque = 2, Lado = 3, Nave = 4, Cama = 5, Cuadro = 6 }
    }
}
