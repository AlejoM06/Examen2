using Examen2.Models;
using proyecto.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Examen2.Controllers
{
    [RoutePrefix("api/SubirFoto")]
    public class FotosController : ApiController
    {
        [HttpPost]
        [Route("CargarArchivo")]
        public async Task<HttpResponseMessage> CargarArchivo(HttpRequestMessage request, string Datos, string Proceso)
        {
            clsFotoPesaje upload = new clsFotoPesaje();
            upload.Datos = Datos;
            upload.Proceso = Proceso;
            upload.request = request;
            return await upload.GrabarArchivo(false);
        }
        [HttpGet]
        [Route("ConsultarArchivo")]
        public HttpResponseMessage ConsultarArchivo(string Imagen)
        {
            clsFotoPesaje upload = new clsFotoPesaje();
            return upload.DescargarArchivo(Imagen);
        }
        [HttpPut]
        [Route("ActualizarArchivo")]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage request, string Datos, string Proceso)
        {
            clsFotoPesaje upload = new clsFotoPesaje();
            upload.request = request;
            return await upload.GrabarArchivo(true);
        }
        [HttpDelete]
        [Route("EliminarImagen")]
        public HttpResponseMessage EliminarImagen(string Imagen)
        {
            try
            {
                // Ruta del directorio de archivos
                string root = HttpContext.Current.Server.MapPath("~/Archivos");
                string filePath = Path.Combine(root, Imagen);

                // Verificar si el archivo existe y eliminarlo
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "La imagen no existe en el servidor.");
                }

                // Eliminar la referencia en la base de datos
                using (var db = new DBExamenEntities1())
                {
                    var imagen = db.FotoPesajes.FirstOrDefault(i => i.ImagenVehiculo == Imagen);
                    if (imagen != null)
                    {
                        db.FotoPesajes.Remove(imagen);
                        db.SaveChanges();
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "La imagen no existe en la base de datos.");
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Imagen eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error al eliminar la imagen: " + ex.Message);
            }
        }

    }
}