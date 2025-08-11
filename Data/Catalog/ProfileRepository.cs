using Data.Context;
using Data.Entities;
using Data.Repositories;
namespace Data.Catalog;

public class ProfileRepository(ApplicationDbContext context)
    : BaseRepository<Profile>(context), IProfileRepository
{
}