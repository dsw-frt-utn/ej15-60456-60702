using Dsw2026Ej15.Data.Dto;
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
        

        public List<Doctor> _doctors { get; private set; } = new ();
        public List<Speciality> _specialities { get; private set; } = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }
        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(s => s.Id == id);
        }
        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources"
                    , "specialities.json");// Base directory, incluye Dsw.., api, bin, etc. Sources 
                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json
                    , new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? [];
                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];


            }
            catch (Exception ex) { }
        }

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public List<Doctor> GetActiveDoctors()
        {
            return _doctors.Where(d => d.IsActive).ToList();
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id && d.IsActive);
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
