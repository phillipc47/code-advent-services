﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Distributor
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistributorController : ControllerBase
	{
		[HttpGet]
		public void CountCycles([FromQuery] string input)
		{

		}
	}
}
