using Microsoft.AspNetCore.Mvc;
using your_bike_user_backend.Database;
using your_bike_user_backend.Models;

namespace your_bike_user_backend.Controllers
{
    [ApiController]
    [Route("api/yourBike")]
    public class GetBikeDetailsController(DatabaseContext databaseContext) : ControllerBase
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        [HttpGet("GetSingleBike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSingleBike(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    BaseData<String> fail = new()
                    {
                        Status = "fail",
                        Message = "Something went wrong!",
                        Data = "No bike found!"
                    };
                    return BadRequest(fail);
                }
                Bike bike = _databaseContext.Bikes.FirstOrDefault(u => u.Id == Id)!;

                if (Ok(bike).Value == null)
                {
                    BaseData<String> fail = new()
                    {
                        Status = "fail",
                        Message = "Something went wrong!",
                        Data = "No bike found!"
                    };
                    return NotFound(fail);
                }
                BaseData<Bike> success = new()
                {
                    Status = "success",
                    Message = "Get bike data success!",
                    Data = bike
                };
                return Ok(success);
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
