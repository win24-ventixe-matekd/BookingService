using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class BookingAddressRepository(DataContext context) : BaseRepository<BookingAddressEntity>(context), IBookingAddressRepository
{
}
