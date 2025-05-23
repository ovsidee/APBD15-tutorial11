using APBD15_tutorial11.Data;

namespace APBD15_tutorial11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    
}