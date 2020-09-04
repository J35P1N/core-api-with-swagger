using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithSwagger.Controllers.Base
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	[Consumes("application/json")]
	public abstract class MyApiController : ControllerBase
	{

	}
}
