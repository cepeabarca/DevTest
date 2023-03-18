using AutoMapper;
using DevTest.BackEnd.Data.Models;
using DevTest.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.BackEnd.API
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
