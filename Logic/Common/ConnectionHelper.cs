using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.EntityClient;

namespace SGF.BussinessLogic.Common
{
    public class ConnectionHelper
    {
        public static string CreateConnectionString(Type pType)
        {
            return CreateConnectionString(pType.Name, pType.Assembly.ToString());
        }

        public static string CreateConnectionString(string pModelName)
        {
            return CreateConnectionString(pModelName, null);
        }

        public static string CreateConnectionString(string pModelName, string pAssemblyName)
        {
            EntityConnectionStringBuilder vConnectionStringBuilder = new EntityConnectionStringBuilder();
            vConnectionStringBuilder.Provider = "System.Data.SqlClient";
            vConnectionStringBuilder.ProviderConnectionString = ConfigurationManager.ConnectionStrings["POAEntities"].ConnectionString;
            vConnectionStringBuilder.Metadata = CreateMetadata(pModelName, pAssemblyName);
            return vConnectionStringBuilder.ToString();
        }

        public static string CreateMetadata(string pModelName, string pAssemblyName)
        {
            if (String.IsNullOrEmpty(pAssemblyName)) pAssemblyName = "*";
            return String.Format("res://{1}/{0}.csdl|res://{1}/{0}.ssdl|res://{1}/{0}.msl", pModelName, pAssemblyName);
        }
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["GADMUR_LUFEntities"].ConnectionString;
        }
    }
}
