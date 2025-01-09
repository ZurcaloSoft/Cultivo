using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Reflection;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace SGF.Site
{
    public static class Utils
    {
        public enum TiposIdentificacionValidador { Cedula = 1, RUC = 2, Error = 3 }
        public enum TiposOrigenRUCValidador { PersonaNatural = 1, Privada = 2, Publica = 3, Error = 4 }
        #region Messages
        //"warning", "error", "success" , "info".
        public static void MessageBox(System.Web.UI.Page p, string Titulo, string Message, string Tipo)
        {
            ScriptManager.RegisterClientScriptBlock(p, typeof(string), "SweetAlert", "swal('" + Titulo + "','" + Message + "', '" + Tipo + "');", true);
            //ScriptManager.RegisterClientScriptBlock(p, typeof(string), Guid.NewGuid().ToString(), "<script>customAlert.alert('" + Message + "', '" + Tipo + "');</script>", false);
        }

        public static void MessageBoxUrl(System.Web.UI.Page p, string Titulo, string Message, string Tipo, string Url)
        {
            ScriptManager.RegisterStartupScript(p, typeof(string), "SweetAlert", "swal({ title: '" + Titulo + "', text: '" + Message + "',  type: '" + Tipo + "'}).then(function() { window.location.pathname = '" + Url + "';        });", true);
        }

        //public static void MessageBox(System.Web.UI.Page p, string Message)
        //{
        //    p.RegisterClientScriptBlock("Message", "<script>window.alert('" + Message + "');</script>");
        //}
        //public static void MessageBox(System.Web.UI.Page p, string Message, string RedirectURL)
        //{
        //    p.RegisterClientScriptBlock("Message", "<script>window.alert('" + Message + "');window.location='" + RedirectURL + "';</script>");
        //}
        //public static void MessageBox(System.Web.UI.Page p, string Message, string RedirectURL, string window)
        //{
        //    p.RegisterClientScriptBlock("Message", "<script>window.alert('" + Message + "');" + window + ".location='" + RedirectURL + "';</script>");
        //}
        //public static void MessageBox(System.Web.UI.Page p, string Message, string RedirectURL, bool UseSession)
        //{
        //    p.RegisterClientScriptBlock("Message", "<script>window.alert('" + Message + "');window.location='" + (p.Session["URLUltimaBusqueda"] == null ? RedirectURL : p.Session["URLUltimaBusqueda"].ToString()) + "';</script>");
        //}

        public static void MessageBox2(System.Web.UI.Page p, string Message)
        {
            ScriptManager.RegisterClientScriptBlock(p, typeof(string), Guid.NewGuid().ToString(), "<script>window.alert('" + Message + "');</script>", false);
        }

        public static void MessageBox2(System.Web.UI.Page p, string Message, string RedirectURL)
        {
            ScriptManager.RegisterClientScriptBlock(p, typeof(string), Guid.NewGuid().ToString(), "<script>window.alert('" + Message + "');window.location='" + RedirectURL + "';</script>", false);
        }

        public static void MessageBox2(System.Web.UI.Page p, string Message, string RedirectURL, string window)
        {
            ScriptManager.RegisterClientScriptBlock(p, typeof(string), Guid.NewGuid().ToString(), "<script>window.alert('" + Message + "');" + window + ".location='" + RedirectURL + "';</script>", false);
        }

        public static void MessageBox2(System.Web.UI.Page p, string Message, string RedirectURL, bool UseSession)
        {
            //Message = HttpContext.Current.Server.HtmlEncode(Message);
            ScriptManager.RegisterClientScriptBlock(p, typeof(string), Guid.NewGuid().ToString(), "<script>window.alert('" + Message + "');window.location='" + (p.Session["URLUltimaBusqueda"] == null ? RedirectURL : p.Session["URLUltimaBusqueda"].ToString()) + "';</script>", false);
        }
        #endregion

        #region Encriptacion
        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
        #endregion

        #region Validaciones

        public static bool IsValidEmail(string strMailAddress)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        public static String Validate_Identification(String numero)
        {
            TiposIdentificacionValidador tipoIdentificacion = TiposIdentificacionValidador.Error;
            TiposOrigenRUCValidador tipoOrigen = TiposOrigenRUCValidador.Error;
            return Validate_Identification(numero, out tipoIdentificacion, out tipoOrigen);
        }

        public static String Validate_Identification(String numero, out TiposIdentificacionValidador tipoIdentificacion, out TiposOrigenRUCValidador tipoOrigen)
        {
            tipoIdentificacion = TiposIdentificacionValidador.Error;
            tipoOrigen = TiposOrigenRUCValidador.Error;

            /* IdType: 1 para cédula, 2 para RUC */
            int IdType = 0;
            if (numero.Length == 10)
                IdType = 1;
            if (numero.Length == 13)
                IdType = 2;
            if (IdType == 0)
            {
                tipoIdentificacion = TiposIdentificacionValidador.Error;
                tipoOrigen = TiposOrigenRUCValidador.Error;
                return "La identificación ingresada es incorrecta";
            }

            tipoIdentificacion = IdType == 1 ? TiposIdentificacionValidador.Cedula : TiposIdentificacionValidador.RUC;

            var suma = 0;
            var residuo = 0;
            var Private = false;
            var Public = false;
            var Natural = false;
            //var NumProvincias = 24;
            var Modulo = 11;

            /* Verifico que el campo no contenga letras */
            foreach (var item in numero)
            {
                if (item < '0' || item > '9')
                {
                    tipoIdentificacion = TiposIdentificacionValidador.Error;
                    tipoOrigen = TiposOrigenRUCValidador.Error;
                    return "La identificación no puede contener letras, sólo números";
                }
            }
            //Validación de los dos primeros dígitos (Código de Provincia)
            //if (Convert.ToInt32(numero.Substring(0, 2)) > NumProvincias)
            //{
            //    tipoIdentificacion = TiposIdentificacionValidador.Error;
            //    tipoOrigen = TiposOrigenRUCValidador.Error;
            //    return "El código de la provincia (dos primeros dígitos) es inválido";
            //}

            /* Aqui almacenamos los digitos de la cedula en variables. */
            int d1 = Convert.ToInt32(numero.Substring(0, 1));
            int d2 = Convert.ToInt32(numero.Substring(1, 1));
            int d3 = Convert.ToInt32(numero.Substring(2, 1));
            int d4 = Convert.ToInt32(numero.Substring(3, 1));
            int d5 = Convert.ToInt32(numero.Substring(4, 1));
            int d6 = Convert.ToInt32(numero.Substring(5, 1));
            int d7 = Convert.ToInt32(numero.Substring(6, 1));
            int d8 = Convert.ToInt32(numero.Substring(7, 1));
            int d9 = Convert.ToInt32(numero.Substring(8, 1));
            int d10 = Convert.ToInt32(numero.Substring(9, 1));

            if (d3 == 7 || d3 == 8)
            {
                tipoIdentificacion = TiposIdentificacionValidador.Error;
                tipoOrigen = TiposOrigenRUCValidador.Error;
                return "El tercer dígito ingresado es inválido";
            }

            int p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0, p6 = 0, p7 = 0, p8 = 0, p9 = 0;
            if (IdType == 1)
            {
                //Validación sólo para cédulas
                /* El tercer digito es menor que 6 (0,1,2,3,4,5) para personas naturales */
                /* Solo para personas naturales (modulo 10) */
                if (d3 < 6)
                {
                    Natural = true;
                    p1 = d1 * 2; if (p1 >= 10) p1 -= 9;
                    p2 = d2 * 1; if (p2 >= 10) p2 -= 9;
                    p3 = d3 * 2; if (p3 >= 10) p3 -= 9;
                    p4 = d4 * 1; if (p4 >= 10) p4 -= 9;
                    p5 = d5 * 2; if (p5 >= 10) p5 -= 9;
                    p6 = d6 * 1; if (p6 >= 10) p6 -= 9;
                    p7 = d7 * 2; if (p7 >= 10) p7 -= 9;
                    p8 = d8 * 1; if (p8 >= 10) p8 -= 9;
                    p9 = d9 * 2; if (p9 >= 10) p9 -= 9;
                    Modulo = 10;
                }
                else
                {
                    tipoIdentificacion = TiposIdentificacionValidador.Error;
                    tipoOrigen = TiposOrigenRUCValidador.Error;
                    return "El tercer dígito ingresado es inválido";
                }

                suma = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9;
                residuo = suma % Modulo;

                /* Si residuo=0, dig.ver.=0, caso contrario 10 - residuo*/
                int digitoVerificador;
                if (residuo == 0) digitoVerificador = 0;
                else digitoVerificador = Modulo - residuo;

                /* ahora comparamos el elemento de la posicion 10 con el dig. verificador */
                if (digitoVerificador != d10)
                {
                    tipoIdentificacion = TiposIdentificacionValidador.Error;
                    tipoOrigen = TiposOrigenRUCValidador.Error;
                    return "El número de cédula de la persona natural es incorrecto.";
                }
            }
            else
            {
                //Validación sólo para RUC's
                /* El tercer digito es: */
                /* 9 para sociedades privadas y extranjeros */
                /* 6 para sociedades publicas */
                /* Solo para sociedades publicas (modulo 11) */
                /* Aqui el digito verficador esta en la posicion 9, en las otras 2 en la pos. 10 */
                if (d3 == 6)
                {
                    Public = true;
                    p1 = d1 * 3;
                    p2 = d2 * 2;
                    p3 = d3 * 7;
                    p4 = d4 * 6;
                    p5 = d5 * 5;
                    p6 = d6 * 4;
                    p7 = d7 * 3;
                    p8 = d8 * 2;
                    p9 = 0;
                }

                /* Solo para entidades privadas (modulo 11) */
                if (d3 == 9)
                {
                    Private = true;
                    p1 = d1 * 4;
                    p2 = d2 * 3;
                    p3 = d3 * 2;
                    p4 = d4 * 7;
                    p5 = d5 * 6;
                    p6 = d6 * 5;
                    p7 = d7 * 4;
                    p8 = d8 * 3;
                    p9 = d9 * 2;
                }

                /* El tercer digito es menor que 6 (0,1,2,3,4,5) para personas naturales */
                /* Solo para personas naturales (modulo 10) */
                if (d3 < 6)
                {
                    Natural = true;
                    p1 = d1 * 2; if (p1 >= 10) p1 -= 9;
                    p2 = d2 * 1; if (p2 >= 10) p2 -= 9;
                    p3 = d3 * 2; if (p3 >= 10) p3 -= 9;
                    p4 = d4 * 1; if (p4 >= 10) p4 -= 9;
                    p5 = d5 * 2; if (p5 >= 10) p5 -= 9;
                    p6 = d6 * 1; if (p6 >= 10) p6 -= 9;
                    p7 = d7 * 2; if (p7 >= 10) p7 -= 9;
                    p8 = d8 * 1; if (p8 >= 10) p8 -= 9;
                    p9 = d9 * 2; if (p9 >= 10) p9 -= 9;
                    Modulo = 10;
                }

                suma = p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8 + p9;
                residuo = suma % Modulo;

                /* Si residuo=0, dig.ver.=0, caso contrario 10 - residuo*/
                int digitoVerificador;
                if (residuo == 0) digitoVerificador = 0;
                else digitoVerificador = Modulo - residuo;

                /* ahora comparamos el elemento de la posicion 10 con el dig. verificador */
                if (Public)
                {
                    tipoOrigen = TiposOrigenRUCValidador.Publica;
                    if (digitoVerificador != d9)
                    {
                        tipoIdentificacion = TiposIdentificacionValidador.Error;
                        tipoOrigen = TiposOrigenRUCValidador.Error;
                        return "El RUC de la empresa del sector público es incorrecto.";
                    }

                    /* El ruc de las empresas del sector publico terminan con 0001*/
                    if (numero.Substring(9, 4) == "0000")
                    {
                        tipoIdentificacion = TiposIdentificacionValidador.Error;
                        tipoOrigen = TiposOrigenRUCValidador.Error;
                        return "El RUC de la empresa del sector público debe terminar con una secuencia de 0001";
                    }
                }
                if (Private)
                {
                    tipoOrigen = TiposOrigenRUCValidador.Privada;
                    if (digitoVerificador != d10)
                    {
                        tipoIdentificacion = TiposIdentificacionValidador.Error;
                        tipoOrigen = TiposOrigenRUCValidador.Error;
                        return "El RUC de la empresa del sector privado es incorrecto.";
                    }
                    if (numero.Substring(10, 3) == "000")
                    {
                        tipoIdentificacion = TiposIdentificacionValidador.Error;
                        tipoOrigen = TiposOrigenRUCValidador.Error;
                        return "El RUC de la empresa del sector privado debe terminar con una secuencia de 001";
                    }
                }
                if (Natural)
                {
                    tipoOrigen = TiposOrigenRUCValidador.PersonaNatural;
                    if (digitoVerificador != d10)
                    {
                        tipoIdentificacion = TiposIdentificacionValidador.Error;
                        tipoOrigen = TiposOrigenRUCValidador.Error;
                        return "El RUC de la persona natural es incorrecto.";
                    }
                    if (numero.Substring(10, 3) == "000")
                    {
                        tipoIdentificacion = TiposIdentificacionValidador.Error;
                        tipoOrigen = TiposOrigenRUCValidador.Error;
                        return "El RUC de la persona natural debe terminar con una secuencia de 001";
                    }
                }
            }

            return "";
        }

        public static string GenerarClave(int longitud)
        {
            string letras = "ABCDEFGHIJKLMNOPQRSTUWXYZ1234567890";
            StringBuilder Resultado = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < longitud; i++)
            {
                int index = rnd.Next(letras.Length);
                Resultado.Append(letras[index]);
            }

            return Resultado.ToString();
        }
        #endregion

        #region XML
        public static DataTable ParseXML(String XML)
        {
            XDocument doc = XDocument.Load(new StringReader(XML));
            DataTable dt = new DataTable();

            // Leo la definición de las columnas desde el XML
            var header =
                from data in doc.Descendants("data").Descendants("header").Descendants()
                select new
                {
                    col = data.Name.LocalName,
                    name = data.Attribute("name").Value,
                    type = data.Attribute("type").Value
                };

            // creo las columnas en el datatable
            foreach (var column in header)
                dt.Columns.Add(column.name, typeof(string)); //RenderType(column.type)); // no estoy tomando en cuenta el tipo de dato

            // leo la definición de las filas
            var rows =
                from data in doc.Descendants("data").Descendants("rows")
                from row in data.Descendants("row").Descendants()
                where row.Name.LocalName == "value"
                select new
                {
                    col = row.Parent.Name.LocalName,
                    value = row.Value
                };

            // lleno los datos en el datatable
            // se asume que los datos vienen en el mismo orden de lo indicado en la cabecera
            int RowCount = 0;
            List<object> rowdata = new List<object>();
            foreach (var row in rows)
            {
                RowCount++;
                rowdata.Add(row.value);

                if (RowCount == header.Count())
                {
                    RowCount = 0;
                    dt.Rows.Add(rowdata.ToArray());
                    rowdata.Clear();
                }
            }

            return dt;
        }

        public static DataTable ParseXMLLite(String XML)
        {
            XDocument doc = XDocument.Load(new StringReader(XML));
            DataTable dt = new DataTable();

            // Leo la definición de las columnas desde el XML
            var header =
                from data in doc.Descendants("data").Descendants("header").Descendants()
                select new
                {
                    col = data.Name.LocalName,
                    name = data.Attribute("name").Value,
                    type = data.Attribute("type").Value
                };

            // creo las columnas en el datatable
            foreach (var column in header)
                dt.Columns.Add(column.name, typeof(string)); //RenderType(column.type)); // no estoy tomando en cuenta el tipo de dato

            // leo la definición de las filas
            var rows =
                from data in doc.Descendants("data").Descendants("rows")
                from row in data.Descendants("row").Descendants()
                where row.Name.LocalName == "value"
                select new
                {
                    col = row.Parent.Name.LocalName,
                    value = row.Value
                };

            // lleno los datos en el datatable
            // se asume que los datos vienen en el mismo orden de lo indicado en la cabecera
            int RowCount = 0;
            List<object> rowdata = new List<object>();
            foreach (var row in rows)
            {
                RowCount++;
                if (row.value.Length > 50)
                    rowdata.Add(row.value.Substring(0, 49) + "...");
                else rowdata.Add(row.value);

                if (RowCount == header.Count())
                {
                    RowCount = 0;
                    dt.Rows.Add(rowdata.ToArray());
                    rowdata.Clear();
                }
            }

            return dt;
        }

        // no se está usando los tipos de dato
        private static Type RenderType(string p)
        {
            switch (p)
            {
                case "varchar":
                    return typeof(string);
                case "numeric":
                    return typeof(double);
                default:
                    return typeof(string);
            }
        }
        #endregion

        #region EMAIL
        public static string GenerarContenido(string TemplatePath, Dictionary<string, string> ParamValues)
        {
            // Envío al correo electrónico
            string EmailContent = System.IO.File.ReadAllText(TemplatePath);

            List<string> DetectedParams = new List<string>();
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\[[a-zA-Z0-9\.]*\]", System.Text.RegularExpressions.RegexOptions.Compiled);
            foreach (System.Text.RegularExpressions.Match match in regex.Matches(EmailContent))
            {
                DetectedParams.Add(match.Value);
            }

            foreach (string DetectedParam in DetectedParams)
            {
                EmailContent = EmailContent.Replace(DetectedParam, ParamValues[DetectedParam.Replace("[", "").Replace("]", "")]);
            }
            return EmailContent;
        }

        public static void SendMail(string Email, string CopyTo, string Body, string Subject)
        {
            string MailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom"];
            string DisplayName = System.Configuration.ConfigurationManager.AppSettings["DisplayName"];
            SendMail(MailFrom, DisplayName, Email, CopyTo, Body, Subject);
        }

        public static void SendMail(string Email, string CopyTo, string Body, string Subject, byte[] AttachmentXls, string TitleReport)
        {
            string MailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom"];
            string DisplayName = System.Configuration.ConfigurationManager.AppSettings["DisplayName"];
            SendMailAttachment(MailFrom, DisplayName, Email, CopyTo, Body, Subject, AttachmentXls, AttachmentXls, TitleReport);
        }

        public static void SendMail(string From, string DisplayName, string Email, string CopyTo, string Body, string Subject)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            if (DisplayName == "")
                message.From = new System.Net.Mail.MailAddress(From);
            else
                message.From = new System.Net.Mail.MailAddress(From, DisplayName);

            string[] parts = Email.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
                message.To.Add(new System.Net.Mail.MailAddress(part.Trim()));

            if (!string.IsNullOrEmpty(CopyTo))
            {
                string[] partscopy = CopyTo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in partscopy)
                    message.CC.Add(new System.Net.Mail.MailAddress(part.Trim()));
            }
            message.IsBodyHtml = true;
            message.Subject = Subject;
            message.Body = Body;
            message.Priority = System.Net.Mail.MailPriority.Normal;

            string Host = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
            int Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            string Username = System.Configuration.ConfigurationManager.AppSettings["SMTPUser"];
            string Password = System.Configuration.ConfigurationManager.AppSettings["SMTPPass"];

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            if (!string.IsNullOrEmpty(Username))
                client.Credentials = new System.Net.NetworkCredential(Username, Password);

            client.Host = Host;
            client.Port = Port;
            client.Send(message);
        }

        public static void SendMailAttachment(string From, string DisplayName, string Email, string CopyTo, string Body, string Subject, byte[] AttachmentPdf, byte[] AttachmentXls, string TitleReport)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            if (DisplayName == "")
                message.From = new System.Net.Mail.MailAddress(From);
            else
                message.From = new System.Net.Mail.MailAddress(From, DisplayName);
            string[] parts = Email.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
                message.To.Add(new System.Net.Mail.MailAddress(part.Trim()));

            if (!string.IsNullOrEmpty(CopyTo))
            {
                string[] partscopy = CopyTo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in partscopy)
                    message.CC.Add(new System.Net.Mail.MailAddress(part.Trim()));
            }
            message.IsBodyHtml = true;
            message.Subject = Subject;
            message.Body = Body;
            message.Priority = System.Net.Mail.MailPriority.Normal;
            //message.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(AttachmentPdf), TitleReport + ".pdf"));
            message.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(AttachmentXls), TitleReport + ".xls"));

            string Host = System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
            int Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            string Username = System.Configuration.ConfigurationManager.AppSettings["SMTPUser"];
            string Password = System.Configuration.ConfigurationManager.AppSettings["SMTPPass"];
            bool EnableSSL = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableSSL"]);

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            if (!string.IsNullOrEmpty(Username))
                client.Credentials = new System.Net.NetworkCredential(Username, Password);

            client.Host = Host;
            client.Port = Port;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.EnableSsl = EnableSSL;
            client.Send(message);
        }

        #endregion

        #region REPORTES

        //public static byte[] GeneratePDF(string ReportPath)
        //{
        //    string format = "PDF";
        //    string deviceInfo = null;
        //    string encoding = String.Empty;
        //    string mimeType = String.Empty;
        //    string extension = String.Empty;
        //    Warning[] warnings = null;
        //    string[] streamIDs = null;


        //    LocalReport report = new LocalReport();
        //    report.EnableExternalImages = true;
        //    report.ReportPath = HttpContext.Current.Server.MapPath("~/ReporteDeclaracion.rdlc");

        //    //report.SetParameters(
        //    //report.DataSources

        //    Byte[] pdfArray = report.Render(format, deviceInfo, out mimeType, out encoding,
        //    out extension, out streamIDs, out warnings);

        //    return pdfArray;
        //}

        #endregion

        #region EXCEPCIONES

        public static void HandleException(System.Web.UI.Page page, System.Exception ex)
        {
            if (ex is System.ServiceModel.FaultException<SGF_Service.LogicException>)
            {
                string Mensaje = ((System.ServiceModel.FaultException<SGF_Service.LogicException>)ex).Detail.Messagek__BackingField;
                int Codigo = ((System.ServiceModel.FaultException<SGF_Service.LogicException>)ex).Detail.Codek__BackingField;

                page.ClientScript.RegisterStartupScript(typeof(string), "excepcion", "alert('" + Mensaje + " (Cod:" + Codigo.ToString() + ")');", true);
            }
            else if (ex is System.ServiceModel.FaultException<SGF_Service.InfraestructureException>)
            {
                //string Mensaje = ((System.ServiceModel.FaultException<WS_TransferenciaDominio.LogicException>)ex).Detail.Messagek__BackingField;
                string SupportCode = ((System.ServiceModel.FaultException<SGF_Service.InfraestructureException>)ex).Detail.SupportCodek__BackingField;

                page.ClientScript.RegisterStartupScript(typeof(string), "excepcion", "alert('Ha ocurrido un error en el sistema, por favor tome contacto con el administrador y proporciónele el siguiente código: " + SupportCode + "');", true);
            }
            else if (ex is System.ServiceModel.FaultException<SGF_Service.SecurityException>)
            {
                switch (((System.ServiceModel.FaultException<SGF_Service.SecurityException>)ex).Detail.Actionk__BackingField)
                {
                    case SGF_Service.SecurityActions.CloseSession:
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session.Abandon();
                        HttpContext.Current.Response.Redirect("~/Login.aspx");
                        break;
                    case SGF_Service.SecurityActions.Message:

                        string Mensaje = ((System.ServiceModel.FaultException<SGF_Service.SecurityException>)ex).Detail.Messagek__BackingField;
                        page.ClientScript.RegisterStartupScript(typeof(string), "excepcion", "alert('" + Mensaje + "');", true);
                        break;
                }
            }
            else
            {
                // Log del error
                string FileName = "";
                string ErrorCode = "";

                // Resolve File Name
                while (true)
                {
                    ErrorCode = Guid.NewGuid().ToString().Substring(30);
                    ErrorCode = "C" + ErrorCode;
                    FileName = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFolder"] + ErrorCode + ".txt";
                    if (!System.IO.File.Exists(FileName))
                        break;
                }


                if (FileName == "") return;

                // Extract Text From Exception
                StringBuilder sb = new StringBuilder();
                while (true)
                {

                    sb.AppendLine(ex.Message);
                    sb.AppendLine(ex.Source);
                    sb.AppendLine(ex.StackTrace);
                    if (ex.TargetSite != null)
                        sb.AppendLine(ex.TargetSite.Name);
                    sb.AppendLine(ex.HelpLink);


                    if (ex.InnerException == null)
                        break;

                    ex = ex.InnerException;
                    sb.AppendLine("========================================================");
                }

                System.IO.File.WriteAllText(FileName, sb.ToString());

                page.ClientScript.RegisterStartupScript(typeof(string), "excepcion", "alert('Ha ocurrido un error en el sistema, por favor tome contacto con el administrador y proporciónele el siguiente código: " + ErrorCode.ToString() + "');", true);
            }
        }

        #endregion

        #region Mes
        public static string Mes(string mes)
        {
            string digito = "01";
            switch (mes.Substring(0, 3))
            {
                case "ENE":
                    digito = "01";
                    break;
                case "FEB":
                    digito = "02";
                    break;
                case "MAR":
                    digito = "03";
                    break;
                case "ABR":
                    digito = "04";
                    break;
                case "MAY":
                    digito = "05";
                    break;
                case "JUN":
                    digito = "06";
                    break;
                case "JUL":
                    digito = "07";
                    break;
                case "AGO":
                    digito = "08";
                    break;
                case "SEP":
                    digito = "09";
                    break;
                case "OCT":
                    digito = "10";
                    break;
                case "NOV":
                    digito = "11";
                    break;
                case "DIC":
                    digito = "12";
                    break;
            }
            return digito;
        }

        public static string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString("dd-MMMM-yyyy", new System.Globalization.CultureInfo("es-ES"));
        }
        #endregion

        public static string getIP()
        {
            ////string strHostName = Dns.GetHostName();
            ////IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            ////return Convert.ToString(ipEntry.AddressList[ipEntry.AddressList.Length - 1]);
            //System.Web.HttpContext context = System.Web.HttpContext.Current;
            //string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (!string.IsNullOrEmpty(ipAddress))
            //{
            //    string[] addresses = ipAddress.Split(',');
            //    if (addresses.Length != 0)
            //    {
            //        return addresses[0];
            //    }
            //}
            //return context.Request.ServerVariables["REMOTE_ADDR"];
            string ipAddress = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());// objeto para guardar la ip
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipAddress = ip.ToString();// esta es nuestra ip
                    break;
                }
            }
            return ipAddress;
        }

        public static string getMAC()
        {
            //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            //String sMacAddress = string.Empty;
            //foreach (NetworkInterface adapter in nics)
            //{
            //    if (adapter.OperationalStatus == OperationalStatus.Up && adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)// only return MAC Address from first card  
            //    {
            //        IPInterfaceProperties properties = adapter.GetIPProperties();
            //        sMacAddress = adapter.GetPhysicalAddress().ToString();
            //    }
            //}
            //return sMacAddress;
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        public static string getHostName(string ipAddress)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
                return hostEntry.HostName;
            }
            catch (Exception)
            {
                return "Desconocido";
            }
        }
        public static DateTime SumarDiasLaborables(DateTime fechaInicio, int cantidad)
        {
            while (cantidad != 0)
            {
                fechaInicio = fechaInicio.AddDays(1);
                if (fechaInicio.DayOfWeek != DayOfWeek.Saturday &&
                fechaInicio.DayOfWeek != DayOfWeek.Sunday)
                    cantidad--;
            }
            return fechaInicio;
        }

        #region TimeOut
        public static void ChangeTimeout(Component component, int timeout)
        {
            if (!component.GetType().Name.Contains("TableAdapter"))
            {
                return;
            }

            PropertyInfo adapterProp = component.GetType().GetProperty("CommandCollection", BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
            if (adapterProp == null)
            {
                return;
            }

            SqlCommand[] command = adapterProp.GetValue(component, null) as SqlCommand[];

            if (command == null)
            {
                return;
            }

            command[0].CommandTimeout = timeout;
        }
        #endregion

        public static string ObtenerEstado(Int32 Estado)
        {
            switch (Estado)
            {
                case 0:
                    return "Borrado";
                case 1:
                    return "Activo";
                case 2:
                    return "Borrado";
                default:
                    return "";
            }
        }
    }
}