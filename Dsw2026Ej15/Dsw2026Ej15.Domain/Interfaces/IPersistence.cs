using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        List<Doctor> _doctors {  get; }

        List<Speciality> _specialities { get; }

        Task AddDoctor(Doctor doctor);

        List<Doctor> GetActiveDoctors();

        Task<Doctor?>  GetDoctorById(Guid id);

        bool DeactivateDoctor(Guid id);
        Task<Speciality?> GetSpecialityById(Guid id);

        Task UpdateDoctor(Doctor doctor);
    }
}
