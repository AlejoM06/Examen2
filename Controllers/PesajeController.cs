using Antlr.Runtime.Tree;
using Examen2.Clases;
using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Examen2.Controllers
{
    [RoutePrefix("api/Pesaje")]
    public class PesajeController : ApiController
    {
        [HttpGet]
        [Route("ConsultarImagenes")]
        public IQueryable ConsultarImagenes(int idPesaje)
        {
            clsPesaje clsPesaje = new clsPesaje();  
            return clsPesaje.ListarFotosPesaje(idPesaje);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] RegistroPesaje datos)
        {
            clsPesaje pesajeManager = new clsPesaje();
            pesajeManager.pesaje = datos.Pesaje;
            pesajeManager.camion = datos.Camion;
            return pesajeManager.Insertar();
        }


        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Pesaje pesaje)
        {
            clsPesaje Pesaje = new clsPesaje();
            Pesaje.pesaje = pesaje;
            return Pesaje.Actualizar();

        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Pesaje pesaje)
        {
            clsPesaje Pesaje = new clsPesaje();
            Pesaje.pesaje = pesaje;
            return Pesaje.Eliminar();
        }



        [HttpGet]
        [Route("ConsultarXPlaca")] //Es el nombre del metodo que se va a invocar
        public Pesaje ConsultarXPlaca(string PlacaCamion)
        {
            clsPesaje Pesaje = new clsPesaje();
            return Pesaje.Consultar(PlacaCamion);
        }
    }
}