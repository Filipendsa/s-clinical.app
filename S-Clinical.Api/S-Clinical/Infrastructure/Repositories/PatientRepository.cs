using Microsoft.EntityFrameworkCore;
using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Interface;
using S_Clinical.Infrastructure.Data;

namespace S_Clinical.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.AsNoTracking().ToListAsync();
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
        }

        public void Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
        }
    }
}