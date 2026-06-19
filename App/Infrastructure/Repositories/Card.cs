using App.Domain.Entities;
using App.Infrastructure.Presistence;

namespace App.Infrastructure.Repositories;


public class CardRepository {

     private readonly AppDbContext _db;

    public CardRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CardEntity?> GetByIdAsync(int id)
    {
        return await _db.Cards.FindAsync(id);
    }
}