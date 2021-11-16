using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ParcialApi.Models
{
	public class PQR
	{
        [Key]
        public int idPqr { get; set; }
        [Required]
        public string grupoDeInteres { get; set; }
        [Required]
        public string pais { get; set; }
        [Required]
        public string departamento { get; set; }
        [Required]
        public string ciudad { get; set; }
        public DateTime fecha { get; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string correo { get; set; }
        [Required]
        public string medioDeNotificacionPara { get; set; }
        public User usuario { get; set; }
        [Required]
        public string asunto { get; set; }
        [Required]
        public string descripcion { get; set; }
        public string status { get; set; }
    }
}
