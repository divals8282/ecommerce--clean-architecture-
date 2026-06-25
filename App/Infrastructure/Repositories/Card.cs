using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;


public class CardRepository : ICardRepository
{

    private readonly AppDbContext _db;

    public CardRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CardEntity?> GetByIdAsync(int id)
    {
        return await _db.Cards.FindAsync(id);
    }

    public async Task<CardEntity?> GetByIdentityId(int identityId)
    {

        return await _db.Cards.Include((c) => c.Identity).FirstOrDefaultAsync((c) => c.Identity.Id == identityId);
    }

    public async Task<bool> RemoveCard(CardEntity card)
    {
        _db.Cards.Remove(card);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}