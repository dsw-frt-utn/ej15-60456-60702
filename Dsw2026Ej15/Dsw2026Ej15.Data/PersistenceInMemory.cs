using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        public List<Doctor> Doctors { get; private set; } = new ();
        public List<Speciality> Specialities { get; private set; } = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }
        private void LoadSpecialities()
        {
            string filePath = "specialities.json";

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                if(data != null)
                {
                    Specialities = data;
                }

            }
            else 
            {
                Console.WriteLine("Not found");
            }
        }

        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }

        public List<Doctor> GetActiveDoctors()
        {
            throw new NotImplementedException();
        }

        public Doctor? GetDoctorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeactivateDoctor(Guid id)
        {
            var doctor = GetDoctorById(id);
            if (doctor != null)
            {
                doctor.IsActive = false; // Borrado lógico
                return true;
            }
            return false;
        }
    }
}
