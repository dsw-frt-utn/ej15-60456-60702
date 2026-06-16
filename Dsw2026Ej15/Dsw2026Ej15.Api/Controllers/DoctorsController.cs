using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")] //todo en minuscula por convencion
public class DoctorsController : ControllerBase
{
    //Inyecto la persistencia
    private readonly IPersistence _persistence;
    public DoctorsController(IPersistence persistence)
    {
        //Inyeccion de dependencias
        _persistence = persistence;
    }

    //Verbo
    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request) //de ahora en mas, asyncronicos en controladores
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

        //crear obj doctor, del dominio, new Doctor(...),
        var newDoctor = new Doctor
        {
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            IsActive = true,
            Speciality = speciality
        };
        _persistence.AddDoctor(newDoctor);

        //return Ok(); //Es un metodo que tiene definido controllerbase, que representa un codigo de estado200
        return Created($"api/doctors/{newDoctor.Id}", newDoctor); 
    }
    //Por ahroa, hacemos las validaciones aca
}
