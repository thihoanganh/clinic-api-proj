using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using SS10_WebApplication_MVC_DB.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Clinic_Web_Api.Helpers;

namespace Clinic_Web_Api.Controllers
{
	[Route("api/scientific")]
	
	public class ScientificController : Controller
	{
		private ScientificServicelmlp scientificServicelmlp;
		private IWebHostEnvironment webHostEnvironment;
	

		public ScientificController(ScientificServicelmlp _scientificServicelmlp, IWebHostEnvironment _webHostEnvironment)
		{
			scientificServicelmlp = _scientificServicelmlp;
			webHostEnvironment = _webHostEnvironment;
			
		}
		//---------------------------------------------------------
		[HttpGet("scientificlist")]

		public IActionResult ScientificList()
		{
			var rs = scientificServicelmlp.Scientificlist();
			return Ok(new { result = rs, count = rs.Count() });
		}
		[HttpGet("finddetail/{id}")]
		public IActionResult FindDetail(int id)
		{
			var scientific = scientificServicelmlp.FindDetail(id);
			if (scientific == null)
			{
				return NotFound(new { msg = "Object not found" });
			}
			return Ok(scientific);
		}
		
		[Produces("application/json")]
		[HttpPost("upload")]
		
		public IActionResult Upload(IFormFile file)
		{
			
				var fileName = FileHelpers.GenerateFileName(file.ContentType);
				var path = Path.Combine(webHostEnvironment.WebRootPath, "Image", fileName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
			
			return Ok(fileName);

		}
		//create
		
		[HttpPost("addscientific")]
		public IActionResult AddScientific([FromBody] ScientificEquipment scientificEquipment)
		{
			
			return Ok(scientificServicelmlp.Create(scientificEquipment));


		}
		//update

		[HttpPost("updatescientific")]

		public IActionResult UpdateScientific([FromBody] ScientificEquipment scientificEquipment)
		{

			
			return Ok(scientificServicelmlp.Update(scientificEquipment));

		}
		//------------------------------------------------------------------------------------

		//findall
		[Produces("application/json")]
		[HttpGet("findall")]
		public IActionResult FindAll()
		{
			try
			{

				return Ok(scientificServicelmlp.FindAll());
			}
			catch
			{
				return BadRequest();
			}
		}
		//brand
		[Produces("application/json")]
		[HttpGet("brand")]
		public IActionResult Brand()
		{
			try
			{

				return Ok(scientificServicelmlp.Brand());
			}
			catch
			{
				return BadRequest();
			}
		}
		//origin
		[Produces("application/json")]
		[HttpGet("origin")]
		public IActionResult Origin()
		{
			try
			{

				return Ok(scientificServicelmlp.Orgin());
			}
			catch
			{
				return BadRequest();
			}
		}
		//type thiet bi
		[Produces("application/json")]
		[HttpGet("machineCategory")]
		public IActionResult MachineCategory()
		{
			try
			{

				return Ok(scientificServicelmlp.MachineCategory());
			}
			catch
			{
				return BadRequest();
			}
		}
		//type thuoc
		[Produces("application/json")]
		[HttpGet("typeofmedicine")]
		public IActionResult TypeOfMedicine()
		{
			try
			{

				return Ok(scientificServicelmlp.TypeOfMedicine());
			}
			catch
			{
				return BadRequest();
			}
		}
		//price
		[Produces("application/json")]
		[HttpGet("price")]
		public IActionResult Price()
		{
			try
			{

				return Ok(scientificServicelmlp.Price());
			}
			catch
			{
				return BadRequest();
			}
		}
		//search

		[Produces("application/json")]
		[HttpGet("searchkey/{keyword}")]
		public IActionResult Search(string keyword)
		{
			try
			{
				return Ok(scientificServicelmlp.Search(keyword));
			}
			catch
			{
				return BadRequest();
			}
		}
		//search type
		[Produces("application/json")]
		[HttpGet("searchtype/{machinetype}")]
		public IActionResult SearchType(int machinetype)
		{
			try
			{
				return Ok(scientificServicelmlp.SearchType(machinetype));
			}
			catch
			{
				return BadRequest();
			}
		}
		//find
		[Produces("application/json")]
		[HttpGet("find/{id}")]
		public IActionResult Find(int id)
		{
			try
			{
				return Ok(scientificServicelmlp.Find(id));
			}
			catch
			{
				return BadRequest();
			}
		}
		//delete
		[HttpDelete("delete/{id}")]
		public IActionResult Delete(int id)
		{
			try
			{

				scientificServicelmlp.Delete(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}



		
		//add barand
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("addbrand")]
		public IActionResult AddBrand([FromBody] Brand brand)
		{
			try
			{

				return Ok(scientificServicelmlp.AddBrand(brand));
			}
			catch
			{
				return BadRequest();
			}
		}
		//delete brand
		[HttpDelete("deletebrand/{id}")]
		public IActionResult DeleteBrand(int id)
		{
			try
			{
				scientificServicelmlp.DeleteBrand(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}


		//add origin
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("addorigin")]
		public IActionResult AddOrigin([FromBody] Origin origin)
		{
			try
			{

				return Ok(scientificServicelmlp.AddOrigin(origin));
			}
			catch
			{
				return BadRequest();
			}
		}
		//delete origin
		[HttpDelete("deleteorigin/{id}")]
		public IActionResult DeleteOrigin(int id)
		{
			try
			{
				scientificServicelmlp.DeleteOrigin(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		//add addmachinecategory
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("addmachinecategory")]
		public IActionResult AddMachineCategory([FromBody] MachineCategory machineCategory)
		{
			try
			{

				return Ok(scientificServicelmlp.AddMachineCategory(machineCategory));
			}
			catch
			{
				return BadRequest();
			}
		}
		//delete deletemachinecategory
		[HttpDelete("deletemachinecategory/{id}")]
		public IActionResult DeleteMachineCategory(int id)
		{
			try
			{
				scientificServicelmlp.DeleteMachineCategory(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		//add addprice
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("addprice")]
		public IActionResult AddPrice([FromBody] Price price)
		{
			try
			{

				return Ok(scientificServicelmlp.AddPrice(price));
			}
			catch
			{
				return BadRequest();
			}
		}
		//delete deleteprice
		[HttpDelete("deleteprice/{id}")]
		public IActionResult DeletePrice(int id)
		{
			try
			{
				scientificServicelmlp.DeletePrice(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

	}
}
