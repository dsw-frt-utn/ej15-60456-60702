using Dsw2026Ej15.Data.Migrations;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly Dsw2026Ej15DbContext _context;
    public PersistenceEf(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctos()
    {
        return  _context.Doctors.Where(d => d.IsActive);
    }


    public async Task<Doctor?> GetDoctorById(Guid id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
    }

    public async Task SaveDoctor(Doctor doctor)
    {
        _context.Add(doctor);
        await _context.SaveChangesAsync();
    }
    public Task<Speciality?> GetSpecialityById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDoctor(Doctor doctor)
    {
        throw new NotImplementedException();
    }

    
}
