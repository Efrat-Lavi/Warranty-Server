using AutoMapper;
using Warranty.API.PostModels;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.API
{
    public class MappingPostProfile:Profile
    {
        public MappingPostProfile()
        {
            CreateMap<CompanyPostModel, CompanyDto>();
            CreateMap<PermissionPostModel, PermissionDto>();
            CreateMap<RecordPostModel, RecordDto>();
            CreateMap<RolePostModel, RoleDto>();
            //CreateMap<UserPostModel, UserDto>();
            CreateMap<WarrantyPostModel, WarrantyDto>();

            CreateMap<UserPostModel, RegisterDto>();
            //CreateMap<RolePostModel, RoleModel>();
            CreateMap<UserPostModel, UserDto>()
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new RoleModel { NameRole = src.Role })); // ממירים מחרוזת לאובייקט

            //CreateMap<UserDto, UserPostModel>()
            //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.NameRole)); // הפוך - מאובייקט למחרוזת

        }
    }
}
