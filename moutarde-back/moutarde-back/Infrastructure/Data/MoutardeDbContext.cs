using Microsoft.EntityFrameworkCore;

namespace moutarde_back.Infrastructure.Data;

public class MoutardeDbContext(DbContextOptions<MoutardeDbContext> options) : DbContext(options)
{
}