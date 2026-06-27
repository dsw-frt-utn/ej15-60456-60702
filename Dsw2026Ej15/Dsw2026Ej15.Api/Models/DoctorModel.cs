namespace Dsw2026Ej15.Api.Models;

public record DoctorModel
{
    public record Request(string Name, string LicenseNumber, Guid SpecialityId);
    public record Response(string Name, string LicenseNumber, string? SpecialityName);
    //recordsd anidados
    //lo hacemos asi, por parte de una buena practica, cuando la estructura que defino
    //va a servir como parte de un obj de trasnsferencia quw vienen en una request o 
    //vuelven en una response

    //Habilito la posibilidad de lo siguiente
    //public record Response(Guid Id, string Name, string LicenseNumber, Guid SpecialityId);
}
