using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Enum;

namespace S_Clinical.Domain.Interface
{
    public interface IClinicalCareRepository
    {
        Task AddAsync(ClinicalCare clinicalCare);
        Task<ClinicalCare> GetByIdAsync(int id);
        Task<List<ClinicalCare>> GetAllAsync();
        void Update(ClinicalCare clinicalCare);
        void Delete(ClinicalCare clinicalCare);
        Task<int> GetNextSequentialNumberAsync();
        Task<List<ClinicalCare>> GetByStatusAsync(CareStatusTypeEnum status);
        Task<List<ClinicalCare>> GetCompletedAsync();
    }
}