using Microsoft.EntityFrameworkCore;
using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Interface;
using S_Clinical.Infrastructure.Data;

namespace S_Clinical.Infrastructure.Repositories
{
    public class TriageRepository : ITriageRepository
    {
        private readonly AppDbContext _context;

        public TriageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Triage triage)
        {

            await _context.Triages.AddAsync(triage);
        }

        public void Update(Triage triage)
        {
            _context.Triages.Update(triage);
        }

        public void Delete(Triage triage)
        {
            _context.Triages.Remove(triage);
        }

        public async Task<Triage> GetByIdAsync(int id)
        {
            return await _context.Triages
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Triage>> GetAllAsync()
        {
            return await _context.Triages.Include(c => c.ClinicalCare).AsNoTracking().ToListAsync();
        }
    }
}

