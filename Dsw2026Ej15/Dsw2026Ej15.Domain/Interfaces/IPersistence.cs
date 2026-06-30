using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{

    //Hacer los métodos asincronos
    public interface IPersistence
    {   
        //Métodos para doctor
        Task<IEnumerable<Doctor>> GetActiveDoctors();

        Task<Doctor?> GetDoctorById(Guid id);

        Task UpdateDoctor(Doctor doctor);
        Task AddDoctor(Doctor doctor);

        //Método para especialidades
        Task<Speciality?> GetSpecialityById(Guid id);


    }
}
