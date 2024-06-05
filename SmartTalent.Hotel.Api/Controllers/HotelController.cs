namespace SmartTalent.Hotel.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SmartTalent.Hotel.Api.Base;
    using SmartTalent.Hotel.Api.Base.Models;
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database.Dto;

    [ApiController]
    [Route("api/[controller]")]
    public class HotelController(IHotelBl repoBusinessRules) : BaseController<HotelDto, IHotelBl>(repoBusinessRules)
    {
        /// <summary>
        /// Create a new hotel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("CreateHotel")]
        public async Task<IActionResult> CreateHotel([FromBody] HotelRequest data)
        {
            var result = await GetGenericAsync(x => x.Nit == data.HotelNit);
            if (result == null)
            {
                return Ok(await PostAsync(new HotelDto
                {
                    Name = data.HotelName,
                    Nit = data.HotelNit,
                }));
            }
            else
            {
                return BadRequest("The Hotel You're trying to create, already exists.");
            }
        }

        /// <summary>
        /// Update a hotel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("UpdateHotel/{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelRequest data, string id)
        {
            var result = await GetGenericAsync(x => x.Id == id);
            if (result != null)
            {
                result.Name = string.IsNullOrWhiteSpace(data.HotelName) ? result.Name : data.HotelName;
                result.Nit = string.IsNullOrWhiteSpace(data.HotelNit) ? result.Nit : data.HotelNit;
                return Ok(await PutAsync(result));
            }
            else
            {
                return BadRequest("There is not a hotel with the provided id.");
            }
        }


        /// <summary>
        /// Active or Inactive a Hotel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("ChangeStatusHotel/{id}")]
        public async Task<IActionResult> ChangeStatusHotel([FromBody] StatusRequest data, string id)
        {
            var result = await GetGenericAsync(x => x.Id == id);
            if (result != null)
            {
                result.Active = data.Active;
                return Ok(await PutAsync(result));
            }
            else
            {
                return BadRequest("There is not a hotel with the provided id.");
            }
        }

        /// <summary>
        /// List all the hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListHotels")]
        public async Task<IActionResult> GetHotels()
        {
            return Ok(await GetListAsync());
        }

        /// <summary>
        /// List all the active hotels 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListActiveHotels")]
        public async Task<IActionResult> GetActiveHotels()
        {
            return Ok(await GetGenericListAsync(h => h.Active));
        }
    }
}
