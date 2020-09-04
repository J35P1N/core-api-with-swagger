using CoreApiWithSwagger.Controllers.Base;
using CoreApiWithSwagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiWithSwagger.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class CustomersController : MySecureApiController
    {
        /// <summary>
        /// Get Customer Details By Id
        /// </summary>
        /// <response code="200">Returns details about a customer</response>   
        /// <response code="401">User is not authorized to make this request</response>
        /// <response code="404">Customer was not found</response>   
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{customerId}")]
        [MapToApiVersion("1.0")]
        public ActionResult<CustomerDetails> GetCustomerDetails([FromRoute] int customerId)
        {
            if(customerId != 3)
            {
                return Unauthorized();
            }

            return new CustomerDetails()
            {
                Forename = "Kevin",
                Surname = "Bacon"
            };
        }

        /// <summary>
        /// Get Customer Details By Id
        /// </summary>
        /// <response code="200">Returns details about a customer</response>   
        /// <response code="401">User is not authorized to make this request</response>
        /// <response code="404">Customer was not found</response>   
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{customerId}")]
        [MapToApiVersion("1.1")]
        public ActionResult<CustomerDetails> GetCustomerDetailsv1_1([FromRoute] int customerId)
        {
            if (customerId != 5)
            {
                return Unauthorized();
            }

            return new CustomerDetails()
            {
                Forename = "Keanu",
                Surname = "Reeves"
            };
        }
    }
}
