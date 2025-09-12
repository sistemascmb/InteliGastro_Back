using AutoMapper;
using Domain.Entities;
using Infraestructure.Models;
using Microsoft.AspNetCore.Hosting.Server;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.SystemUsers;

namespace WebApi.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureServerMappings();
        }

        private void ConfigureServerMappings()
        {
            CreateMap<ArchivoDigital, ArchivoDigitalItemListResponseDto>();

            CreateMap<ArchivoDigitalRequestDto, ArchivoDigital>()
                .ForMember(destino => destino.IdArchivo, option => option.Ignore())
                .ForMember(destino => destino.Fecha, option => option.Ignore())
                .ForMember(destino => destino.Hora, option => option.Ignore())
                .ForMember(destino => destino.IdUsuario, option => option.Ignore())
                .ForMember(destino => destino.Equipo, option => option.Ignore())
                .ForMember(destino => destino.IdProveedor, option => option.Ignore())
                .ForMember(destino => destino.Archivo, option => option.Ignore())
                .ForMember(destino => destino.Descripcion, option => option.Ignore())
                .ForMember(destino => destino.FechaArchivo, option => option.Ignore())
                .ForMember(destino => destino.TipoArchivo, option => option.Ignore())
                .ForMember(destino => destino.IdAtencion, option => option.Ignore())
                .ForMember(destino => destino.Historia, option => option.Ignore());
            //SystemUsers
            CreateMap<SystemUsersEntity, SystemUsersDto>()
                .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.userid))
                .ForMember(dest => dest.ProfileTypeId, opt => opt.MapFrom(src => src.ProfileTypeId))
                .ForMember(dest => dest.IsInternalUser, opt => opt.MapFrom(src => src.IsInternalUser))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.SecondLastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.RegistroEliminado, opt => opt.MapFrom(src => src.RegistroEliminado))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            // Mapeo de CreateSystemUsersDto a SystemUsersEntity
            CreateMap<CreateSystemUsersDto, SystemUsersEntity>()
                .ForMember(dest => dest.userid, opt => opt.Ignore()) // Se genera automáticamente
                .ForMember(dest => dest.ProfileTypeId, opt => opt.MapFrom(src => src.ProfileTypeId))
                .ForMember(dest => dest.IsInternalUser, opt => opt.MapFrom(src => src.IsInternalUser))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.SecondLastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.RegistroEliminado, opt => opt.MapFrom(src => src.RegistroEliminado))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // Mapeo de UpdateSystemUsersDto a SystemUsersEntity
            CreateMap<UpdateSystemUsersDto, SystemUsersEntity>()
                .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ProfileTypeId, opt => opt.MapFrom(src => src.ProfileTypeId))
                .ForMember(dest => dest.IsInternalUser, opt => opt.MapFrom(src => src.IsInternalUser))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SecondLastName, opt => opt.MapFrom(src => src.SecondLastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                //.ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                //.ForMember(dest => dest.RegistroEliminado, opt => opt.MapFrom(src => src.RegistroEliminado))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());





            //CreateMap<ServerFilterRequestDto, ServerFilter>();
        }
    }
}
