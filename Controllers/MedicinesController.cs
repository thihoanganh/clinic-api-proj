using Clinic_Web_Api.Entities;
using Clinic_Web_Api.Helpers;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SS10_WebApplication_MVC_DB.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Controllers
{
	[Route("api/medicine")]

	public class MedicinesController : Controller
	{
		private MedicineServicelmlp MedicineServicelmlp;
		private IWebHostEnvironment webHostEnvironment;
		public MedicinesController(MedicineServicelmlp _medicineServicelmlp, IWebHostEnvironment _webHostEnvironment)
		{
			MedicineServicelmlp = _medicineServicelmlp;
			webHostEnvironment = _webHostEnvironment;
		}
		//---------------------------------------------------------------------------------------------
		[HttpGet("medicinelist")]
		public IActionResult MedicineList()
		{
			var rs = MedicineServicelmlp.Medicinelist();
			return Ok(new { result = rs, count = rs.Count() });
		}
		[HttpGet("finddetail/{id}")]
		public IActionResult FindDetail(int id)
		{
			var medicine = MedicineServicelmlp.FindDetail(id);
			if (medicine == null)
			{
				return NotFound(new { msg = "Object not found" });
			}
			return Ok(medicine);
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
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("addmedicine")]
		public IActionResult AddScientific([FromBody] Medicine medicine)
		{

			return Ok(MedicineServicelmlp.AddMedicine(medicine));


		}
		//update
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("updatemedicine")]

		public IActionResult UpdateScientific([FromBody] Medicine medicine)
		{


			return Ok(MedicineServicelmlp.UpdateMedicine(medicine));

		}

		//------------------------------------------------------------------------------------
		[HttpGet("findall")]
		public IActionResult FindAll()
		{
			return Ok(MedicineServicelmlp.FindAll());
		}
		//type thuoc
		[HttpGet("typemedicine")]
		public IActionResult TypeMedicine()
		{
			try
			{

				return Ok(MedicineServicelmlp.TypeMedicine());
			}
			catch
			{
				return BadRequest();
			}
		}
		//search

		[Produces("application/json")]
		[HttpGet("search/{keyword}")]
		public IActionResult Search(string keyword)
		{
			try
			{
				return Ok(MedicineServicelmlp.Search(keyword));
			}
			catch
			{
				return BadRequest();
			}
		}
		//search type
		[Produces("application/json")]
		[HttpGet("searchtype/{madicinetype}")]
		public IActionResult SearchType(int madicinetype)
		{
			try
			{
				return Ok(MedicineServicelmlp.SearchType(madicinetype));
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
				return Ok(MedicineServicelmlp.Find(id));
			}
			catch
			{
				return BadRequest();
			}
		}




		//delete medicine
		[HttpDelete("deletemedicine/{id}")]
		public IActionResult DeleteMedicine(int id)
		{
			try
			{

				MedicineServicelmlp.DeleteMedicine(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}
		//add type medicine
		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPost("addmedicinetype")]
		public IActionResult AddMedicineType([FromBody] TypeOfMedicine typeOfMedicine)
		{

			return Ok(MedicineServicelmlp.AddTypeMedicine(typeOfMedicine));

		}
		//delete type medicine
		[HttpDelete("deletemedicinetype/{id}")]
		public IActionResult DeleteMedicineType(int id)
		{
			try
			{

				MedicineServicelmlp.DeleteTypeMedicine(id);
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}
	}
}
