using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialApi.Models
{
	public class User
	{

		[Key]
		public int idUser { get; set; }
		[Required]
		public string tipoDocumento { get; set; }
		[Required]
		public string documento { get; set; }
		[Required]
		public string nombres { get; set; }
		[Required]
		public string apellidos { get; set; }
		[Required]
		public string telefono { get; set; }
		public string genero { get; set; }
		public string direccion { get; set; }
		public string barrio { get; set; }
	}
}
