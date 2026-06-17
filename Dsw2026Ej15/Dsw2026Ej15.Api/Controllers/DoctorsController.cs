using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


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
            return BadRequest("El nombre y la matrícula son requeridos");
        }
        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null) { 
            return BadRequest("La especialidad no existe");
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
        var doctorsActive = _persistence.GetActiveDoctors();
        return Ok(doctorsActive);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor == null || !doctor.IsActive)
        {
            throw new ValidationException("No existe doctor o esta inactivo");
            return NotFound();
        }

        return Ok(doctor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorById(Guid id)
    {
        if (_persistence.GetDoctorById(id) == null || _persistence.GetDoctorById(id).IsActive == false)
        {
            throw new ValidationException("No existe doctor o esta inactivo");
            return NotFound();
        }

        _persistence.DeactivateDoctor(id);

        return NoContent();
    }
}
