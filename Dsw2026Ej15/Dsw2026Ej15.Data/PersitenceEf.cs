using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersitenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersitenceEf(Dsw2026Ej15DbContext context) 
        {
            _context = context;
        }

        public async Task AddDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            _context.Update(doctor);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Doctor>> GetActiveDoctors()
        {
            return await _context.Doctors
                .Include(nameof(Doctor.Speciality))
                .Where(d => d.IsActive).ToListAsync();
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors.Include(d => d.Speciality).SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return await _context.Specialities.SingleOrDefaultAsync(d => d.Id == id);
        }

    }
}
