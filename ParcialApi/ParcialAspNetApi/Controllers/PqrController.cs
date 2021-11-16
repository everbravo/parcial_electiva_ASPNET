using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParcialApi.Models;
using ParcialApi.Repositorio;
using System.Text.Json;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParcialApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PqrController : ControllerBase
	{
		// GET: api/<PqrController>
		[HttpGet]
		public IActionResult Get()
		{
			RpPqr rpPqr = new RpPqr();
			return Ok(rpPqr.getPqrs());
		}

		// GET api/<PqrController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			RpPqr rpPqr = new RpPqr();
			var pqrRet = rpPqr.getPqr(id);
			if (pqrRet == null)
			{
				var nf = NotFound("La PQR " + id.ToString() + " no se encontró.");
				return nf;
			}
			return Ok(pqrRet);
		}

		// POST api/<PqrController>
		[HttpPost]
		public IActionResult NewPqr(PQR pqr)
		{
			RpUsers rpUser = new RpUsers();
			User user = rpUser.getUser(pqr.usuario.idUser);
			if (user != null)
			{
				var dictionaryUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Text.Json.JsonSerializer.Serialize<User>(pqr.usuario));
				var dictionaryUser2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Text.Json.JsonSerializer.Serialize<User>(user));
				var dictionarDifference = dictionaryUser.Where(entry => dictionaryUser2[entry.Key] != entry.Value).ToDictionary(entry => entry.Key, entry => entry.Value);
				if (dictionarDifference.Count != 0)
				{
					return BadRequest("El id " + user.idUser + " ya existe y contiene información diferente a la proporcionada.");
				}
				pqr.usuario = user;
			}
			RpPqr rpPqr = new RpPqr();
			rpPqr.Agregar(pqr);
			return CreatedAtAction(nameof(NewPqr), pqr);
		}

		// PUT api/<PqrController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] PQR pqr)
		{
			var pqrRet = new RpPqr().getPqr(id);
			if (pqrRet == null)
			{
				var nf = NotFound("La PQR " + id.ToString() + " no se encontró.");
				return nf;
			}
			else
			{
				User user = new RpUsers().getUser(pqr.usuario.idUser);
				if (user != null)
				{
					var dictionaryUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Text.Json.JsonSerializer.Serialize<User>(pqr.usuario));
					var dictionaryUser2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Text.Json.JsonSerializer.Serialize<User>(user));
					var dictionarDifference = dictionaryUser.Where(entry => dictionaryUser2[entry.Key] != entry.Value).ToDictionary(entry => entry.Key, entry => entry.Value);
					if (dictionarDifference.Count != 0)
					{
						return BadRequest("El usuario con id " + user.idUser + " ya existe y la cuenta contiene información diferente a la enviada.");
					}
				}
				else
				{
					return BadRequest("El Usuario id " + pqr.usuario.idUser + " no fue encontrado.");
				}

				pqrRet.usuario = user;
				var dictionaryPqr = JsonConvert.DeserializeObject<Dictionary<string, object>>(System.Text.Json.JsonSerializer.Serialize<PQR>(pqr));

				foreach (string key in dictionaryPqr.Keys)
				{
					if (key != "usuario" && key != "fecha") {
						var value = pqr.GetType().GetProperty(key).GetValue(pqr, null);
						if (value != null)
						{
							pqrRet.GetType().GetProperty(key).SetValue(pqrRet, value, null);
						}
					}
				}
			}
			return Ok(pqrRet);
		}

		// DELETE api/<PqrController>/
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			RpPqr rpPqr = new RpPqr();
			var pqrRet = rpPqr.getPqr(id);
			if (pqrRet == null)
			{
				var nf = NotFound("La PQR " + id.ToString() + " no fue encontrada.");
				return nf;
			}
			else
			{
				rpPqr.DeletePqr(pqrRet);
				return Ok("La PQR " + id.ToString() + " fue eliminada");
			}
		}
	}
}
