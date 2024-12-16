using Microsoft.AspNetCore.Mvc;
using your_bike_user_backend.Database;

namespace your_bike_user_backend.Controllers
{
    [ApiController]
    [Route("api/yourBike")]
    public class AddNewBikeRequestController(DatabaseContext databaseContext) : ControllerBase
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        [HttpPost("AddNewBikeRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult<IEnumerable<>>
    }
}
