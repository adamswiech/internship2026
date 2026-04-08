using KSeF_app_fix.Server.Data;
using KSeF_app_fix.Server.Models;
using Microsoft.EntityFrameworkCore;


public class PartyService
{
    private readonly KsefContext _db;

    public PartyService(KsefContext db)
    {
        _db = db;
    }

    public async Task<Party?> GetByIdAsync(int Id)
    {
        return await _db.Parties.FindAsync(Id);
    }

}
