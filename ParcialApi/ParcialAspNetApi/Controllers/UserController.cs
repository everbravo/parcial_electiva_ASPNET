using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		// GET: api/<UserController>
		[HttpGet]
		public IActionResult Get()
		{
			RpUsers rpUser = new RpUsers();
			return Ok(rpUser.getUsers());
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			RpUsers rpUser = new RpUsers();
			var userRet = rpUser.getUser(id);
			if (userRet == null)
			{
				var nf = NotFound("El id " + id.ToString() + " no fue encontrado.");
				return nf;
			}
			return Ok(userRet);
		}

		// POST api/<UserController>
		[HttpPost]
		public IActionResult NewUser(User user)
		{
			RpUsers rpUser = new RpUsers();
			rpUser.Agregar(user);
			return CreatedAtAction(nameof(NewUser), user);
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] User user)
		{
			RpUsers rpUser = new RpUsers();
			var userRet = rpUser.getUser(id);
			if (userRet == null)
			{
				var nf = NotFound("El Id " + id.ToString() + " no fue encontrado encontrado.");
				return nf;
			}
			else
			{
				var dictionaryUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.Text.Json.JsonSerializer.Serialize<User>(userRet));
				
				foreach (string key in dictionaryUser.Keys)
				{
					var value = user.GetType().GetProperty(key).GetValue(user, null);
					if (value != null)
					{
						userRet.GetType().GetProperty(key).SetValue(userRet, value, null);
					}
				}
			}
			return Ok(userRet);
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			RpUsers rpUser = new RpUsers();
			var userRet = rpUser.getUser(id);
			if (userRet == null)
			{
				var nf = NotFound("El Id " + id.ToString() + " no fue encontrado.");
				return nf;
			}
			else
			{
				rpUser.DeleteUser(userRet);
				return Ok("El Id " + id.ToString() + " ha sido eliminado");
			}
		}
	}
}
