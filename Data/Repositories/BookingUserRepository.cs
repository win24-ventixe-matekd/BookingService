using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class BookingUserRepository(DataContext context) : BaseRepository<BookingUserEntity>(context), IBookingUserRepository
{
}
