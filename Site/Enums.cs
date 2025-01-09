using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGF.Site
{
    public class Enums
    {
        public enum Estado { Activo = 1, Pasivo = 0 }
        public enum TiposIdentificacionValidador { Cedula = 1, RUC = 2, Error = 3 }
        public enum TiposOrigenRUCValidador { PersonaNatural = 1, Privada = 2, Publica = 3, Error = 4 }
        public enum ModuloIndex { Administracion = 0, Cultivo = 1, Poscosecha = 2, Empaque = 3, Comercial = 4, Recepcion = 5, TalentoHumano = 6 }
        public enum MapaCultivo { MapaCultivo = 0, Area = 1, Bloque = 2, Lado = 3, Nave = 4, Cama = 5, Cuadro = 6 }
    }
    public static class ActividadCultivo
    {
        public static readonly Guid Siembra = new Guid("08893F02-0221-4E22-A32E-53B271D6B345");
        public static readonly Guid Injerto = new Guid("58D983A7-E747-47A7-B0ED-803BD4E9A00F");
        public static readonly Guid CierreSiembra = new Guid("6D268584-6A6F-44DD-AB0E-6AE2F469843F");
        public static readonly Guid PatronLibre = new Guid("00000000-1111-1111-1111-000000000000");
        public static readonly Guid DistribucionCuadrantes = new Guid("D4AB5225-F3FB-4E57-A2C8-5A88E8A194CD");
        public static readonly Guid ReDistribucionCuadrantes = new Guid("381516D1-D127-4D39-B41E-610006A18507");

    }

    public static class Lados
    {
        public static readonly Guid LadoA = new Guid("5140B0C1-53AC-44B9-9A88-A9A95B93E42C");
        public static readonly Guid LadoB = new Guid("016FB943-24E8-4DC7-A4FE-4AB6436B0A6F");
    }
    public static class MenuModulo
    {
        public static readonly Guid Administracion = new Guid("B44C1F71-21EC-4FD7-AD4C-D6C0F3434CE0");
        public static readonly Guid Comercial = new Guid("A7FD68C3-D7E5-4A80-997F-C576A618A3F6");
        public static readonly Guid Poscosecha = new Guid("259A1102-7C1B-4917-BDEA-60059A3D468F");
        public static readonly Guid Empaque = new Guid("34A4F66C-5F98-47FA-A9AB-97855093E20D");
        public static readonly Guid Recepcion = new Guid("0A007EA0-36C2-43A4-AA67-20457436BFA2");
        public static readonly Guid TalentoHumano = new Guid("6DDB5385-8090-4D61-AD8F-62961BBA294F");
        public static readonly Guid Cultivo = new Guid("D0DF07F1-01B8-44C3-B472-B6AB7110FD76");
    }
    public static class TipoClasificador
    {
        public static readonly Guid Libre = new Guid("00000000-0000-0000-0000-000000000000");
        public static readonly Guid TipoFlor = new Guid("9BA20E91-8E64-4068-BC1A-462E26363A57");
        public static readonly Guid TipoIdentificacion = new Guid("1463EBEE-2C54-42B8-A29F-506B5CCD8A8A");
        public static readonly Guid TipoIdentificacionNew = new Guid("B3F4388E-02EE-4042-8359-E2C99B15058B");
        public static readonly Guid EstadoCivil = new Guid("7FEDA544-4114-4DE3-9BD0-8CF2A56A8C4E");
        public static readonly Guid Longitudes = new Guid("1984F8A1-850F-4C1E-9CCC-A6D0C9171003");
        public static readonly Guid TallosPorBonche = new Guid("C2609DB5-0E96-4625-90C6-B3CB79EBB948");
        public static readonly Guid TallosPorMalla = new Guid("4498C717-2896-4934-B6BC-545E8EA20DD2");
        public static readonly Guid Genero = new Guid("2CA9D300-2D0F-44EB-9FDD-CCDF08FCDD86");
        public static readonly Guid Color = new Guid("F8D70789-156F-4A38-904D-E33538D59B1B");
        public static readonly Guid CalidadFlor = new Guid("59397B48-46C0-4314-9673-F3293114BBCB");
        public static readonly Guid Pais = new Guid("6CA88FE4-81C3-4E64-AAC3-79F8E62A2BD9");
        public static readonly Guid Ciudad = new Guid("56195587-9FBA-452E-9A41-92CC6DBC998B");
        public static readonly Guid Actividades = new Guid("2CF38792-6533-4FA4-B304-0DE2A7EBA47C");
        public static readonly Guid TipoProblemasFlor = new Guid("0B29F36C-2E8B-4298-B3E0-675F3F04DEF7");
        public static readonly Guid ProblemasFlor = new Guid("7F3A7F8E-265C-40DD-BFD8-4E8F82647010");
        public static readonly Guid EstadoVenta = new Guid("7F490CE2-4AAE-4CDB-BF8D-02F58F58F2F1");
        public static readonly Guid Aduanas = new Guid("EC0C780D-FA20-487C-84BD-0844E7C398CB");
        public static readonly Guid TipoProveedores = new Guid("C2360486-3AD7-46B0-90C3-1AD97E261C37");
        public static readonly Guid TipoCaja = new Guid("662A8A85-211B-45C1-B2AE-1BCBE69C8237");
        public static readonly Guid CuartoFrioFinca = new Guid("513331CB-4EEF-4BBA-8E2D-2CE74C2D9318");
        public static readonly Guid CuartoFrioCarguera = new Guid("D54A7A61-52B6-4F05-8C3A-30C879C1E12E");
        public static readonly Guid Cajas = new Guid("3140CF3B-C4BB-434F-A67A-3D46E80E93DC");
        public static readonly Guid UnidadCultivoVentas = new Guid("16D2C551-D54D-462E-9A1C-47D093129706");
        public static readonly Guid PuntoCorte = new Guid("BED71956-7B9E-47CC-8F47-4BB0ED991C91");
        public static readonly Guid EstadoPlanta = new Guid("F39FA841-A8CA-4B41-A837-7C4B4955BE86");
        public static readonly Guid TipoCliente = new Guid("8437712C-5BB6-4D64-9C16-9317A0422B49");
        public static readonly Guid LaminaBonche = new Guid("11B9707F-381C-4014-BCDF-A640F277713F");
        public static readonly Guid Mercado = new Guid("976EDA2C-695C-4AEB-804F-D00B0C8F6C98");
        public static readonly Guid PuestoTrabajo = new Guid("E5130B80-8CD8-48FB-A5C2-FF57F7E24900");
        public static readonly Guid TipoCalidad = new Guid("8901F6F8-AE76-4CB4-BE38-4058B4AB9972");
        public static readonly Guid Mesas = new Guid("1A3FA34D-8E49-4211-A05C-C09927439D46");
        public static readonly Guid Cargueras = new Guid("63E098D1-74C5-41AD-BFEA-2709E1B89029");
        public static readonly Guid BonchesPorCaja = new Guid("D2726425-33FF-4178-98B9-CBD77E2816D1");
        public static readonly Guid CultivoAreas = new Guid("E03E4E4D-6492-45B1-BA5F-EDE1C416C73D");
        public static readonly Guid CultivoBloques = new Guid("2C0BB68F-8D62-4DFA-9B40-C13C6BB003EA");
        public static readonly Guid CultivoLados = new Guid("5A69AD9F-65E6-4011-9F82-A490A8A950A2");
        public static readonly Guid CultivoNaves = new Guid("BB421ADD-C972-4FDE-AB61-13489BDFB229");
        public static readonly Guid CultivoCamas = new Guid("D5630115-EF0C-4BFA-9B17-F6048EE5EF89");
        public static readonly Guid CultivoCuadros = new Guid("E3BBAA72-6A5C-4F7A-AF4F-E47253DF7A4B");
        public static readonly Guid TipoBodega = new Guid("5F06615A-178A-40E7-B968-F64E524D57A5");
    }

    public static class TipoPersona
    {
        public static readonly Guid Empleado = new Guid("38B30AFB-3437-4648-9CCA-B25BFFFD7176");
        public static readonly Guid Cliente = new Guid("5CCF1F3A-8795-4F08-BC83-F8F0989D1879");
        public static readonly Guid Proveedor = new Guid("C22CDF1E-28C3-4D23-A19B-409748464417");
        public static readonly Guid Carguera = new Guid("8E4F8CD1-964E-47CB-ACF1-0A28BE059B3F");
    }
    public static class TipoUsuario
    {
        public static readonly Guid Administrador = new Guid("ACEFC77F-E174-42E5-B649-421C082AF907");
        public static readonly Guid Proveedor = new Guid("AB53DBDC-E68E-41B0-81D6-DBB95594FCB4");
        public static readonly Guid Contabilidad = new Guid("00000000-0000-0000-0000-000000000000");
        public static readonly Guid Ventas = new Guid("00000000-0000-0000-0000-000000000000");
    }
}