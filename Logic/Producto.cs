using Newtonsoft.Json;
using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {

        [OperationContract]
        public CO_ARTICULO Producto_ObtenerPorID(string id)
        {
            DataModel model = new DataModel();
            return model.CO_ARTICULO.First(x => x.CO_ART_COD == id);
        }
        [OperationContract]
        public List<CO_ARTICULO> Producto_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.CO_ARTICULO.ToList();
        }
        [OperationContract]
        public List<CO_ARTICULO> Producto_ObtenerPorVariedad(Guid variedadID)
        {
            DataModel model = new DataModel();
            return model.CO_ARTICULO.Where(x => x.CO_VARIEDAD_COD == variedadID).ToList();
        }

        [OperationContract]
        public List<SGF_Producto_VTA> Producto_ObtenerPorVariedadVTA(Guid variedadID)
        {
            DataModel model = new DataModel();
            return model.SGF_Producto_VTA.Where(x => x.VariedadID == variedadID && x.Estado == "ACTIVO").ToList();
        }
        [OperationContract]
        public void Producto_Grabar(CO_ARTICULO newProducto, string nomPC, string ip)
        {
            DataModel model = new DataModel();

            if (Producto_ValidarProducto(newProducto) > 0)
                return;

            // Crear y configurar el JsonSerializer
            var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                Formatting = Formatting.Indented // Para una salida JSON más legible
            });

            // Serializar el objeto a JSON string
            using (var stringWriter = new StringWriter())
            {
                jsonSerializer.Serialize(stringWriter, newProducto);
                string jsonString = stringWriter.ToString();
                SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_ARTICULO", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProducto.PRESENTACION, RegistroID = newProducto.CO_FAM_ART_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
            }
            /*
             * CÓDIGO PARA DESERIALIZAR OBJETO
             *  // Crear y configurar el JsonSerializer
        var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            Formatting = Formatting.Indented // Para una salida JSON más legible
        });

        // Deserializar la cadena JSON a un objeto Persona
        using (var stringReader = new StringReader(jsonString))
        using (var jsonTextReader = new JsonTextReader(stringReader))
        {
            Persona persona = jsonSerializer.Deserialize<Persona>(jsonTextReader);
            Console.WriteLine($"Nombre: {persona.Nombre}, Edad: {persona.Edad}, Correo: {persona.Correo}");
        }
             */
            model.AddToCO_ARTICULO(newProducto);

            model.SaveChanges();
        }
        [OperationContract]
        public int Producto_ValidarProducto(CO_ARTICULO newProducto)
        {
            DataModel model = new DataModel();
            int resultado = 0;
            foreach (CO_ARTICULO item in model.CO_ARTICULO.Where(X => X.CO_ART_EST == "ACTIVO").ToList())
            {
                if (newProducto.CO_PAIS_ORIGEN_COD == Guid.Empty && newProducto.CO_CATEGORIA4_COD == Guid.Empty)
                {
                    if (item.CO_VARIEDAD_COD== newProducto.CO_VARIEDAD_COD && item.CO_TIP_ART_COD == newProducto.CO_TIP_ART_COD && item.CO_CATEGORIA3_COD == newProducto.CO_CATEGORIA3_COD && item.CO_FAM_ART_COD == newProducto.CO_FAM_ART_COD)
                    {
                        resultado = 1;
                    }
                }
                else
                {
                    if (newProducto.CO_PAIS_ORIGEN_COD != Guid.Empty && newProducto.CO_CATEGORIA4_COD != Guid.Empty)
                    {
                        if (item.CO_VARIEDAD_COD == newProducto.CO_VARIEDAD_COD && item.CO_TIP_ART_COD == newProducto.CO_TIP_ART_COD && item.CO_CATEGORIA3_COD == newProducto.CO_CATEGORIA3_COD && item.CO_CATEGORIA4_COD == newProducto.CO_CATEGORIA4_COD && item.CO_FAM_ART_COD == newProducto.CO_FAM_ART_COD  && item.CO_PAIS_ORIGEN_COD == newProducto.CO_PAIS_ORIGEN_COD)
                        {
                            resultado = 1;
                        }
                    }
                    else
                    {
                        if (newProducto.CO_PAIS_ORIGEN_COD == Guid.Empty && newProducto.CO_CATEGORIA4_COD != Guid.Empty)
                        {
                            if (item.CO_VARIEDAD_COD == newProducto.CO_VARIEDAD_COD && item.CO_TIP_ART_COD == newProducto.CO_TIP_ART_COD && item.CO_CATEGORIA3_COD == newProducto.CO_CATEGORIA3_COD && item.CO_CATEGORIA4_COD == newProducto.CO_CATEGORIA4_COD && item.CO_FAM_ART_COD == newProducto.CO_FAM_ART_COD)
                            {
                                resultado = 1;
                            }
                        }
                    }
                }
            }
            return resultado;
        }

    }
}
