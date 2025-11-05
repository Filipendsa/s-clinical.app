using Microsoft.EntityFrameworkCore;
using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Enum;
using S_Clinical.Domain.Interface;
using S_Clinical.Infrastructure.Data;

namespace S_Clinical.Infrastructure.Repositories
{
    public class ClinicalCareRepository : IClinicalCareRepository
    {
        private readonly AppDbContext _context;

        public ClinicalCareRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClinicalCare clinicalCare)
        {
            await _context.ClinicalCares.AddAsync(clinicalCare);
        }
        public async Task<List<ClinicalCare>> GetAllAsync()
        {
            return await _context.ClinicalCares.Include(c => c.Patient)
            .Include(c => c.Triage).AsNoTracking().ToListAsync();
        }

        public async Task<ClinicalCare> GetByIdAsync(int id)
        {
            return await _context.ClinicalCares
                .Include(c => c.Triage)
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(ClinicalCare clinicalCare)
        {
            _context.ClinicalCares.Update(clinicalCare);
        }

        public void Delete(ClinicalCare clinicalCare)
        {
            _context.ClinicalCares.Remove(clinicalCare);
        }

        public async Task<int> GetNextSequentialNumberAsync()
        {
            var today = DateTime.Today;

            var maxSequential = await _context.ClinicalCares
                .Where(c => c.DateTimeArrival >= today)
                .MaxAsync(c => (int?)c.SequentialNumber);

            return (maxSequential ?? 0) + 1;
        }

        public async Task<List<ClinicalCare>> GetByStatusAsync(CareStatusTypeEnum status)
        {
            return await _context.ClinicalCares
                .Include(c => c.Patient)
                .Where(c => c.StatusType == status)
                .OrderBy(c => c.SequentialNumber)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ClinicalCare>> GetCompletedAsync()
        {
            return await _context.ClinicalCares
                .Include(c => c.Patient)
                .Include(c => c.Triage)
                .Where(c => c.StatusType != CareStatusTypeEnum.WAITING_TRIAGE && c.StatusType != CareStatusTypeEnum.IN_TRIAGE)
                .OrderByDescending(c => c.DateTimeArrival)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}