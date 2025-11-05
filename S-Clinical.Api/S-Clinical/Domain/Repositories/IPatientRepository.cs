using S_Clinical.Domain.Entities;

namespace S_Clinical.Domain.Interface
{
    public interface IPatientRepository
    {
        Task AddAsync(Patient patient);
        Task<Patient> GetByIdAsync(int id);
        Task<List<Patient>> GetAllAsync();
        void Update(Patient patient);
        void Delete(Patient patient);
    }
}