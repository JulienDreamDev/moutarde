using Microsoft.EntityFrameworkCore;

namespace moutarde_back.Data;

public class MoutardeDbContext(DbContextOptions<MoutardeDbContext> options) : DbContext(options)
{
}