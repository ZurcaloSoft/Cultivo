using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SGF.BussinessLogic.Common
{
    [Serializable]
    public class InfraestructureException
    {
        public InfraestructureException(string Message, string SupportCode)
        {
            this.ID = Guid.NewGuid();
            this.Message = Message;
            this.SupportCode = SupportCode;
        }

        public Guid ID { get; set; }
        public string Message { get; set; }
        public string SupportCode { get; set; }
    }

    [Serializable]
    public class LogicException
    {
        public LogicException(string Message, int Code)
        {
            this.ID = Guid.NewGuid();
            this.Message = Message;
            this.Code = Code;
        }

        public Guid ID { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }

    [Serializable]
    public class SecurityException
    {
        public SecurityException(string Message, SecurityActions Action)
        {
            this.ID = Guid.NewGuid();
            this.Message = Message;
            this.Action = Action;
        }
        public Guid ID { get; set; }
        public string Message { get; set; }
        public SecurityActions Action { get; set; }
    }

    public enum SecurityActions
    {
        Message = 1, CloseSession = 2
    }

    public class ExceptionHelper
    {
        public static string LogException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (ex is System.Data.SqlClient.SqlException)
                {
                    sb.AppendLine(ex.Message);
                    sb.AppendLine(((System.Data.SqlClient.SqlException)ex).Procedure);
                    sb.AppendLine(((System.Data.SqlClient.SqlException)ex).LineNumber.ToString());
                    sb.AppendLine(((System.Data.SqlClient.SqlException)ex).Server);

                    if (((System.Data.SqlClient.SqlException)ex).Class != null)
                        sb.AppendLine(((System.Data.SqlClient.SqlException)ex).Class.ToString());

                    sb.AppendLine(((System.Data.SqlClient.SqlException)ex).ErrorCode.ToString());

                    sb.AppendLine(ex.Source);

                    if (ex.TargetSite != null)
                        sb.AppendLine(ex.TargetSite.Name);

                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine(ex.HelpLink);
                }
                else
                {
                    sb.AppendLine(ex.Message);
                    sb.AppendLine(ex.Source);
                    sb.AppendLine(ex.StackTrace);
                    if (ex.TargetSite != null)
                        sb.AppendLine(ex.TargetSite.Name);
                    sb.AppendLine(ex.HelpLink);
                }

                if (ex.InnerException == null)
                    break;

                ex = ex.InnerException;
                sb.AppendLine("========================================================");
            }
            return sb.ToString();
        }
    }
}
