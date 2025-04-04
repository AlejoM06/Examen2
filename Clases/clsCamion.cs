using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Examen2.Clases
{
	public class clsCamion
	{
		private DBExamenEntities1 dbExamen = new DBExamenEntities1();
		public Camion camion { get; set; }

        //Insertar
        public string Insertar()
        {
            try

            {
                dbExamen.Camions.Add(camion);
                dbExamen.SaveChanges(); 
                return "Camion insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el Camion: " + ex.Message;
            }
        }

        //Actualizar Camion
        public string Actualizar()
        {
            try
            {
                Camion cam = Consultar(camion.Placa);
                if (cam == null)
                {
                    return "No existe camion registrado con ese código, por ende no se puede actualizar :( ";
                }
                dbExamen.Camions.AddOrUpdate(camion);
                dbExamen.SaveChanges();
                return "Camion actualizado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el camion :( " + ex.Message;

            }
        }
        //Eliminar Computador
        public string Eliminar()
        {
            try
            {
                Camion cam = Consultar(camion.Placa);

                if (cam == null)
                {
                    return "El Camion con el código ingresado no existe, por ende no se puede eliminar :( ";
                }

                dbExamen.Camions.Remove(cam);
                dbExamen.SaveChanges();

                return "Camion eliminado correctamente :) ";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el Camion :( " + ex.Message;

            }
        }
        //Consultar
        public Camion Consultar(string IDPlaca)
        {
            return dbExamen.Camions.FirstOrDefault(e => e.Placa == IDPlaca);
        }

       



    }
}