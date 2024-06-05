namespace SmartTalent.Hotel.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SmartTalent.Hotel.Api.Base;
    using SmartTalent.Hotel.Api.Base.Models;
    using SmartTalent.Hotel.Api.Utilities;
    using SmartTalent.Hotel.BusinessLayer.Interfaces;
    using SmartTalent.Hotel.DataAccessLayer.Database.Dto;
    using SmartTalent.Hotel.MailSender;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(IReservationBl repoBusinessRules, IRoomBl roomBl, IConfiguration configuration) : BaseController<ReservationDto, IReservationBl>(repoBusinessRules)
    {
        private readonly RoomController roomController = new(roomBl);


        /// <summary>
        /// List all the reservations in a hotel
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListReservationsByHotel/{id}")]
        public async Task<IActionResult> ListReservationsByHotel(string id)
        {
            return Ok(await GetGenericListAsync(r => r.HotelId == id));
        }

        /// <summary>
        /// List all the active hotels 
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] ReservationRequest data)
        {
            var result = await PostAsync(new ReservationDto
            {
                HotelId = data.HotelId,
                RoomsId = data.RoomsId,
                Adults = data.Adults,
                Children = data.Children,
                CheckIn = Util.ConvertDateToShort(data.CheckIn),
                CheckOut = Util.ConvertDateToShort(data.CheckOut),
                Guests = data.Guests != null && data.Guests.Any() ? data.Guests.Cast<UserDataDto>().ToList() : [],
                UserBirth = data.UserBirth,
                UserDocument = data.UserDocument,
                UserDocumentType = data.UserDocumentType,
                UserEmail = data.UserEmail,
                UserEmergencyContact = new ReservationEmergencyContactDto
                {
                    Name = data.UserEmergencyContact.Name,
                    Phone = data.UserEmergencyContact.Phone
                },
                UserFullName = data.UserFullName,
                UserGender = data.UserGender,
                UserPhone = data.UserPhone
            });

            if (result != null)
            {
                foreach (var item in data.RoomsId)
                {
                    await roomController.BookRoom(new BookRoomRequest
                    {
                        CheckIn = Util.ConvertDateToShort(data.CheckIn),
                        CheckOut = Util.ConvertDateToShort(data.CheckOut),
                        RoomId = item,
                        ReservationId = result.Id
                    });
                }
            }

            SendReservationMail(data);

            return Ok(result);
        }

        private void SendReservationMail(ReservationRequest reservation)
        {
            //With the response we can decide whether save it to know if the mail was sent or do anything else
            Email.SendMal(new EmailModel
            {
                MailFrom = configuration["Email:MailFrom"],
                MailFromPassword = configuration["Email:MailFromPassword"],
                MailTo = reservation.UserEmail,
                Port = int.Parse(configuration["Email:Port"]),
                SMTP = configuration["Email:Smtp"],
                Subject = "Smart Talent Reservation"
            });
        }
    }
}
