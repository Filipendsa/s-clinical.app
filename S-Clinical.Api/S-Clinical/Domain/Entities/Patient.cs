
using S_Clinical.Domain.Enum;
using System;

namespace S_Clinical.Domain.Entities
{
    public class Patient
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        public GenderTypeEnum GenderType { get; private set; }

        private readonly List<ClinicalCare> _clinicalCares = new();
        public IReadOnlyCollection<ClinicalCare> ClinicalCares => _clinicalCares;

        public Patient(string name, string phoneNumber, string email, GenderTypeEnum genderType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Patient name cannot be empty.", nameof(name));

            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            GenderType = genderType;
        }
        public void Update(string phoneNumber, string email, string name, GenderTypeEnum genderType)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                PhoneNumber = phoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }

            if (System.Enum.IsDefined(typeof(GenderTypeEnum), genderType))
            {
                GenderType = genderType;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                Email = email;
            }
        }
    }
}