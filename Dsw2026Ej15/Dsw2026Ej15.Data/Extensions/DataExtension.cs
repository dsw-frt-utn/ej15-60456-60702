using Dsw2026Ej15.Data.Dto;
using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Extensions
{
    public static class DataExtension
    {
        public static void SeedworkSpeciality(this Dsw2026Ej15DbContext context, string dataSource)
        {
            if (context.Set<Speciality>().Any()) return;

            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources"
                   , dataSource);// Base directory, incluye Dsw.., api, bin, etc. Sources 
            var json = File.ReadAllText(jsonPath);
            var entities = JsonSerializer.Deserialize<List<SpecialityDto>>(json
                , new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
            var specialities = entities.Select(e => new Speciality(e.Name,e.Description, e.Id));
            context.Set<Speciality>().AddRange(specialities);
            context.SaveChanges();
        }
    }
}
