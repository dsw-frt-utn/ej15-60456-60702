using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;


namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")] 
public class DoctorsController : ControllerBase
{
    
    private readonly IPersistence _persistence;
    public DoctorsController(IPersistence persistence)
    {
        
        _persistence = persistence;
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request) 
    {
        if(string.IsNullOrWhiteSpace(request.Name) || 
            string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            
            throw new ValidationException("El nombre y la matrícula son requeridos");
        }
        var speciality = await _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null) {
            throw new ValidationException("La especialidad no existe");
        }

        
        var newDoctor = new Doctor
        { 
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };
        _persistence.AddDoctor(newDoctor);

        
        return Created($"api/doctors/{newDoctor.Id}", newDoctor); 
    }


    [HttpGet]
    public async Task<IActionResult> GetDoctorsActive()
    {
        var doctorsActive = await _persistence.GetActiveDoctors();
        return Ok(doctorsActive.Select(d => new DoctorModel.Response(d.Name, d.LicenseNumber, d.Speciality?.Name)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var doctor = await _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            throw new NotFoudException("No existe doctor o esta inactivo");
            
        }

        return Ok(new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Name));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorById([FromRoute]Guid id)
    {
        var doctor = (await GetDoctor(id))!;
        doctor.IsActive = false;
        //if (_persistence.GetDoctorById(id) == null || _persistence.GetDoctorById(id).IsActive == false)
        //{
        //    throw new NotFoudException("No existe doctor o esta inactivo"); 
        //}

        _persistence.UpdateDoctor(doctor);

        return NoContent();
    }

    private async Task<Doctor?> GetDoctor(Guid id)
    {
        return await _persistence.GetDoctorById(id) ?? throw new NotFoudException($"Médico de ID: {id} no encontrado");
    }
}
