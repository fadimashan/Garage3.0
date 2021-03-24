using AutoMapper;
using Garage_G5.Models;
using Garage_G5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_G5.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ParkedVehicle, GeneralInfoViewModel>();

        }
    }
}
