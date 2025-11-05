using S_Clinical.Domain.Entities;

namespace S_Clinical.Domain.Interface
{
    public interface ITriageRepository
    {
        Task AddAsync(Triage triage);
        Task<Triage> GetByIdAsync(int id);
        Task<List<Triage>> GetAllAsync();
        void Update(Triage triage);
        void Delete(Triage triage);

    }
}
