namespace SmartTalent.Hotel.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SmartTalent.Hotel.Api.Base;
    using SmartTalent.Hotel.Api.Base.Models;
    using SmartTalent.Hotel.Api.Utilities;
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dto;


    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IRoomBl repoBusinessRules) : BaseController<RoomDto, IRoomBl>(repoBusinessRules)
    {
        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomRequest data)
        {

            return Ok(await PostAsync(new RoomDto
            {
                Description = data.RoomDescription,
                Name = data.RoomName,
                Type = data.RoomType,
                HotelId = data.HotelId,
                Active = data.Active,
                BasePrice = data.RoomPrice,
                Tax = data.RoomPriceTax,
                Location = data.RoomLocation,
                MaxCapacity = data.RoomCapacity,
                ReservatedDates = []
            }));
        }

        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet("GetRooms")]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await GetListAsync());
        }

        /// <summary>
        /// Get all rooms filtered
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("GetFilteredRooms")]
        public async Task<IActionResult> GetFilteredRooms([FromBody] FilterRoomModel data)
        {
            return Ok(await GetGenericListAsync(r =>
            r.Location == data.DestinationCity &&
            r.MaxCapacity >= data.QuantityPeople &&
            !r.ReservatedDates.Any(d => d.CheckInDate >= Util.ConvertDateToShort(data.CheckInDate) && d.CheckOut < Util.ConvertDateToShort(data.CheckOut))));
        }

        /// <summary>
        /// Assign a room to a hotel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("AssignRoomToHotel")]
        public async Task<IActionResult> AssignRoomToHotel([FromBody] AssingRoomRequest data)
        {

            var room = await GetGenericAsync(x => x.Id == data.RoomId);

            if (room == null)
            {
                return BadRequest("The room you're trying to assign does not exist.");
            }
            room.HotelId = data.HotelId;
            room.HotelName = data.HotelName;
            return Ok(await PutAsync(room));
        }

        /// <summary>
        /// Mark room as booked
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal async Task<IActionResult> BookRoom([FromBody] BookRoomRequest data)
        {

            var room = await GetGenericAsync(x => x.Id == data.RoomId);

            if (room == null)
            {
                return BadRequest("The room you're trying to find does not exist.");
            }

            room.ReservatedDates.Add(new ReservatedDates
            {
                CheckInDate = data.CheckIn,
                ReservationId = data.ReservationId,
                CheckOut = data.CheckOut
            });
            return Ok(await PutAsync(room));
        }

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("UpdateRoom/{id}")]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomRequest data, string id)
        {

            var room = await GetGenericAsync(x => x.Id == id);

            if (room == null)
            {
                return BadRequest("The room you're trying to update does not exist.");
            }
            room.Name = string.IsNullOrWhiteSpace(data.RoomName) ? room.Name : data.RoomName;
            room.Type = string.IsNullOrWhiteSpace(data.RoomType) ? room.Type : data.RoomType;
            room.Description = string.IsNullOrWhiteSpace(data.RoomDescription) ? room.Description : data.RoomDescription;
            room.Location = string.IsNullOrWhiteSpace(data.RoomLocation) ? room.Location : data.RoomLocation;
            room.BasePrice = data.RoomPrice == 0 ? room.BasePrice : data.RoomPrice;
            room.Tax = data.RoomPriceTax == 0 ? room.Tax : data.RoomPriceTax;
            room.Active = data.Active;

            return Ok(await PutAsync(room));
        }


        /// <summary>
        /// Active or Inactive a Room
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut("ChangeStatusRoom/{id}")]
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
                return BadRequest("There is not a room with the provided id.");
            }
        }
    }
}
