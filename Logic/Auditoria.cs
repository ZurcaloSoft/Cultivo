using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public void Auditoria_Grabar(SGF_Auditoria registro)
        {
            DataModel model = new DataModel();
            model.AddToSGF_Auditoria(registro);
            model.SaveChanges();
        }

    }
}
