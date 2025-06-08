using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(IBookingRepository bookingRepository, IBookingAddressRepository bookingAddressRepository, IBookingUserRepository bookingUserRepository) : ControllerBase
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IBookingAddressRepository _bookingAddressRepository = bookingAddressRepository;
    private readonly IBookingUserRepository _bookingUserRepository = bookingUserRepository;

    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingModel model)
    {
        if (!ModelState.IsValid || model == null)
            return BadRequest(ModelState);

        var addressResult = await _bookingAddressRepository.AddAsync(
            new BookingAddressEntity { StreetName = model.StreetName, PostalCode = model.PostalCode, City = model.City });
        if (!addressResult.Success)
            return BadRequest(ModelState);

        var userResult = await _bookingUserRepository.AddAsync(
            new BookingUserEntity { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email });
        if (!userResult.Success)
            return BadRequest(ModelState);

        var bookingResult = await _bookingRepository.AddAsync(new BookingEntity
        {
            EventName = model.EventName,
            EventDate = model.EventDate,
            EventLocation = model.EventLocation,
            PackageName = model.PackageName,
            PackagePrice = model.PackagePrice,
            Currency = model.Currency,
            Amount = model.Amount,
            UserId = userResult.Result!.Id,
            AddressId = addressResult.Result!.Id
        });
        if (!bookingResult.Success)
            return BadRequest(ModelState);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingRepository.GetAllAsync(includes: x => x.User);
        return bookings.Success
            ? Ok(bookings)
            : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var booking = await _bookingRepository.GetAsync(x => x.Id == id, includes: x => x.User);
        return booking.Success
            ? Ok(booking)
            : NotFound();
    }
}
