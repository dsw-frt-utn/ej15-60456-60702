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

        void AddDoctor(Doctor doctor);

        List<Doctor> GetActiveDoctors();

        Doctor? GetDoctorById(Guid id);

        bool DeactivateDoctor(Guid id);
        Speciality? GetSpecialityById(Guid id);


    }
}
