using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Examen2.Clases
{
	public class clsPesaje
	{
        private DBExamenEntities1 dbExamen = new DBExamenEntities1();
        public Pesaje pesaje { get; set; }

        //Insertar
        public string Insertar()
        {
            try

            {
                dbExamen.Pesajes.Add(pesaje);
                dbExamen.SaveChanges();
                return "Pesaje insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el Pesaje: " + ex.Message;
            }
        }

        //Actualizar pesaje
        public string Actualizar()
        {
            try
            {
                Pesaje pes = Consultar(pesaje.id);
                if (pes == null)
                {
                    return "No existe pesaje registrado con ese código, por ende no se puede actualizar :( ";
                }
                dbExamen.Pesajes.AddOrUpdate(pesaje);
                dbExamen.SaveChanges();
                return "pesaje actualizado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el pesaje :( " + ex.Message;

            }
        }
        //Eliminar Computador
        public string Eliminar()
        {
            try
            {
                Pesaje cam = Consultar(pesaje.id);

                if (cam == null)
                {
                    return "El pesaje con el código ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbExamen.Pesajes.Remove(cam);
                dbExamen.SaveChanges();

                return "pesaje eliminado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el pesaje :( " + ex.Message;

            }
        }
        //Consultar
        public Pesaje Consultar(int ID)
        {
            return dbExamen.Pesajes.FirstOrDefault(e => e.id == ID);
        }

        public string GrabarImagenPesaje(int idPesaje, List<string> Imagenes)
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    FotoPesaje fotoPesaje = new FotoPesaje();
                    fotoPesaje.idPesaje = idPesaje;
                    fotoPesaje.ImagenVehiculo = imagen;
                    dbExamen.FotoPesajes.Add(fotoPesaje);
                }
                dbExamen.SaveChanges();
                return "Se grabó la información en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public IQueryable ListarImagenes(int idProducto)
        {
            return from P in dbSuper.Set<PRODucto>()
                   join TP in dbSuper.Set<TIpoPRoducto>()
                   on P.CodigoTipoProducto equals TP.Codigo
                   join I in dbSuper.Set<ImagenesProducto>()
                   on P.Codigo equals I.idProducto
                   where P.Codigo == idProducto
                   orderby I.NombreImagen
                   select new
                   {
                       idTipoProducto = TP.Codigo,
                       TipoProducto = TP.Nombre,
                       idProducto = P.Codigo,
                       Producto = P.Nombre,
                       Imagen = I.NombreImagen
                   };
        }
    }
}