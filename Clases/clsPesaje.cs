using Antlr.Runtime.Misc;
using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Examen2.Clases
{
	public class clsPesaje
	{
        private DBExamenEntities1 dbExamen = new DBExamenEntities1();
        public Pesaje pesaje { get; set; }
        public Camion camion { get; set; }
        //Insertar
        public string Insertar()
        {
            try
            {
                // Validar si el camión existe
                var camionExiste = dbExamen.Camions.Any(c => c.Placa == pesaje.PlacaCamion);

                // Si no existe, se debe insertar
                if (!camionExiste)
                {
                    if (camion == null)
                        return "El camión no existe y no se proporcionaron los datos para crearlo.";

                    camion.Placa = pesaje.PlacaCamion; // asegurarse de que coincidan
                    dbExamen.Camions.Add(camion);
                    dbExamen.SaveChanges(); // se guarda primero el camión
                }

                // Insertar el pesaje
                dbExamen.Pesajes.Add(pesaje);
                dbExamen.SaveChanges();

                return "Pesaje insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el pesaje: " + ex.Message;
            }
        }

        //Actualizar pesaje
        public string Actualizar()
        {
            try
            {
                Pesaje pes = Consultar(pesaje.PlacaCamion);
                if (pes == null)
                {
                    return "No existe pesaje registrado con ese código, por ende no se puede actualizar :( ";
                }
                dbExamen.Pesajes.AddOrUpdate(pesaje);
                dbExamen.SaveChanges();
                return "pesaje actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el pesaje " + ex.Message;

            }
        }
        //Eliminar Computador
        public string Eliminar()
        {
            try
            {
                Pesaje cam = Consultar(pesaje.PlacaCamion);

                if (cam == null)
                {
                    return "El pesaje con el código ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbExamen.Pesajes.Remove(cam);
                dbExamen.SaveChanges();

                return "pesaje eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el pesaje " + ex.Message;

            }
        }
        //Consultar
        public Pesaje Consultar(string PlacaCamion)
        {
            return dbExamen.Pesajes.FirstOrDefault(e => e.PlacaCamion == PlacaCamion);
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

        public IQueryable ListarFotosPesaje(int idPesaje)
        {
            return from P in dbExamen.Set<Pesaje>()
                   join C in dbExamen.Set<Camion>()
                   on P.PlacaCamion equals C.Placa
                   join I in dbExamen.Set<FotoPesaje>()
                   on P.id equals I.idPesaje
                   where P.id == idPesaje
                   orderby I.ImagenVehiculo
                   select new
                   {
                       idPesaje = P.id,
                       Fecha = P.FechaPesaje,
                       Peso = P.Peso,
                       PlacaCamion = C.Placa,
                       Marca = C.Marca,
                       NumeroEjes = C.NumeroEjes,
                       Foto = I.ImagenVehiculo
                   };
        }

       
    }
}