using Data.Entities;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(BookingRepository bookingRepository, BookingAddressRepository bookingAddressRepository, BookingUserRepository bookingUserRepository) : ControllerBase
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly BookingAddressRepository _bookingAddressRepository = bookingAddressRepository;
    private readonly BookingUserRepository _bookingUserRepository = bookingUserRepository;

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
            new BookingUserEntity { FirstName = model.FirstName, LastName = model.FirstName, Email = model.Email });
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
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}
