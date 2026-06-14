using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor
    {
        private string Name { get; set; }
        private string LicenseNumber { get; set; }
        private bool IsActive { get; set; }
        private Speciality? speciality;
    }
}
