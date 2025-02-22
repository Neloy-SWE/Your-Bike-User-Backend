﻿using Microsoft.AspNetCore.Mvc;
using your_bike_user_backend.Database;
using your_bike_user_backend.Models;

namespace your_bike_user_backend.Controllers
{
    [ApiController]
    [Route("api/yourBike")]
    public class AddNewBikeRequestController(DatabaseContext databaseContext) : ControllerBase
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        [HttpPost("AddNewBikeRequest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult AddNewBikeRequest([FromBody] Notification notification)
        {
            try
            {
                if (_databaseContext.Bikes.FirstOrDefault(u => u.Name.ToLower() == notification.Model.ToLower() && u.BrandName.ToLower() == notification.Brand.ToLower()) != null)
                {
                    BaseData<String> fail = new()
                    {
                        Status = "fail",
                        Message = "Bike already Exists!",
                        Data = ""
                    };
                    return BadRequest(fail);
                }
                if (_databaseContext.Notifications.FirstOrDefault(u => u.Brand.ToLower() == notification.Brand.ToLower() && u.Model.ToLower() == notification.Model.ToLower()) != null)
                {
                    BaseData<String> fail = new()
                    {
                        Status = "fail",
                        Message = "Already someone added this request!",
                        Data = ""
                    };
                    return BadRequest(fail);
                }
                if (notification == null)
                {
                    BaseData<String> fail = new()
                    {
                        Status = "fail",
                        Message = "Did not get any data from user-end!",
                        Data = ""
                    };
                    return BadRequest(fail);
                }

                Notification newNotification = new() { Brand = notification.Brand, Model = notification.Model, Read = true };

                _databaseContext.Notifications.Add(newNotification);
                _databaseContext.SaveChanges();
                BaseData<String> success = new()
                {
                    Status = "success",
                    Message = "Request added!",
                    Data = "An admin will look into this very soon."
                };

                return CreatedAtAction(nameof(AddNewBikeRequest), success);

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
