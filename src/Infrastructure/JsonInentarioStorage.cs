using System.Text.Json;
using System.Text.Json.Serialization;
using InventarioApp.Infrastructure;
using InventarioApp.Models;
namespace InventarioApp.Infrastructure
{
    public class JsonInentarioStorage
    {
        private readonly Filemanager _filemanager;
        private readonly JsonSerializerOptions _options;

        public JsonInentarioStorage()
        {
            _filemanager = new Filemanager();
            _options = new JsonSerializerOptions
            {
                WriteIndented = true, //identacion y saltos de linea
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // ignorar mayusculas
                Converters = {new JsonStringEnumConverter()} // convierte ENUM a string
            };
        }

        public void Guardar (List<Producto> productos, string ruta)
        {
            string json = JsonSerializer.Serialize(productos, _options);
            _filemanager.Escribir(ruta,json);
        }

        public List<Producto> Cargar (string ruta)
        {
            string json = _filemanager.Leer(ruta);
            return JsonSerializer.Deserialize<List<Producto>>(json, _options) ?? new List<Producto>();
        }
        public string CrearBackup(string ruta)
        {
            if(!_filemanager.Existe(ruta)) 
                    return null!;
            string directorio = Path.GetDirectoryName(ruta)!;
            string nombreSinExtension = Path.GetExtension(ruta);
            string extension = Path.GetExtension(ruta);
            string timestamp = DateTime.Now.ToString("yyyyy-MM-dd_HH-mm-ss");

            string rutaBackup = Path.Combine(
                directorio ?? ".",
                $"{nombreSinExtension}_backup_{timestamp}{extension}"
            );

            File.Copy(ruta,rutaBackup);
            return rutaBackup;
        }
    }
}