using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        List<Doctor> Doctors {  get; }

        List<Speciality> Specialities { get; }

        void AddDoctor(Doctor doctor);

        List<Doctor> GetActiveDoctors();

        Doctor? GetDoctorById(Guid id);

        bool DeactivateDoctor(Guid id);


    }
}
