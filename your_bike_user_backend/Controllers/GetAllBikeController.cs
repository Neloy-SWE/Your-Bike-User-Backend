using Microsoft.AspNetCore.Mvc;
using your_bike_user_backend.Database;
using your_bike_user_backend.Models;

namespace your_bike_user_backend.Controllers
{
    [ApiController]
    [Route("api/yourBike")]
    public class GetAllBikeController(DatabaseContext databaseContext) : ControllerBase
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        [HttpGet("GetAllBikes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult<IEnumerable<Bike>> GetAllBikes()
        {
            try
            {
                BaseData<List<Bike>> data = new()
                {
                    Status = "success",
                    Message = "Get All Bikes!",
                    Data = _databaseContext.Bikes.ToList()
                };
                return Ok(data);
            }
            catch (Exception)
            {
                BaseData<string> data = new()
                {
                    Status = "fail",
                    Message = "Something went wrong!",
                    Data = "Try again leter."
                };
                return Accepted(data);
            }

        }
    }
}
