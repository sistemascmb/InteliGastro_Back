using AutoMapper;
using Domain.Entities;
using Infraestructure.Models;
using Microsoft.AspNetCore.Hosting.Server;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.Centro;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.DTO.Examenes;
using WebApi.Application.DTO.Personal;
using WebApi.Application.DTO.Preparacion;
using WebApi.Application.DTO.Recursos;
using WebApi.Application.DTO.Roles;
using WebApi.Application.DTO.Salas;
using WebApi.Application.DTO.Seguros;
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

            //Centro
            CreateMap<CentroEntity, CentroDto>()
                .ForMember(dest => dest.centroid, opt => opt.MapFrom(src => src.centroid))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Abreviatura, opt => opt.MapFrom(src => src.Abreviatura))
                .ForMember(dest => dest.InicioAtencion, opt => opt.MapFrom(src => src.InicioAtencion))
                .ForMember(dest => dest.FinAtencion, opt => opt.MapFrom(src => src.FinAtencion))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.RUC, opt => opt.MapFrom(src => src.RUC))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateCentroDto, CentroEntity>()
                .ForMember(dest => dest.centroid, opt => opt.Ignore()) // Se genera automáticamente
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Abreviatura, opt => opt.MapFrom(src => src.Abreviatura))
                .ForMember(dest => dest.InicioAtencion, opt => opt.MapFrom(src => src.InicioAtencion))
                .ForMember(dest => dest.FinAtencion, opt => opt.MapFrom(src => src.FinAtencion))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.RUC, opt => opt.MapFrom(src => src.RUC))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCentroDto, CentroEntity>()
                .ForMember(dest => dest.centroid, opt => opt.MapFrom(src => src.centroid))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Abreviatura, opt => opt.MapFrom(src => src.Abreviatura))
                .ForMember(dest => dest.InicioAtencion, opt => opt.MapFrom(src => src.InicioAtencion))
                .ForMember(dest => dest.FinAtencion, opt => opt.MapFrom(src => src.FinAtencion))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.RUC, opt => opt.MapFrom(src => src.RUC))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Personal
            CreateMap<PersonalEntity, PersonalDto>()
                .ForMember(dest => dest.personalid, opt => opt.MapFrom(src => src.personalid))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.FecNac, opt => opt.MapFrom(src => src.FecNac))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Grado, opt => opt.MapFrom(src => src.Grado))
                .ForMember(dest => dest.NLicencia, opt => opt.MapFrom(src => src.NLicencia))
                .ForMember(dest => dest.TipoTrabajo, opt => opt.MapFrom(src => src.TipoTrabajo))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.TipoDoc, opt => opt.MapFrom(src => src.TipoDoc))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreatePersonalDto, PersonalEntity>()
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.FecNac, opt => opt.MapFrom(src => src.FecNac))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Grado, opt => opt.MapFrom(src => src.Grado))
                .ForMember(dest => dest.NLicencia, opt => opt.MapFrom(src => src.NLicencia))
                .ForMember(dest => dest.TipoTrabajo, opt => opt.MapFrom(src => src.TipoTrabajo))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.TipoDoc, opt => opt.MapFrom(src => src.TipoDoc))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdatePersonalDto, PersonalEntity>()
                .ForMember(dest => dest.personalid, opt => opt.MapFrom(src => src.personalid))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.FecNac, opt => opt.MapFrom(src => src.FecNac))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Grado, opt => opt.MapFrom(src => src.Grado))
                .ForMember(dest => dest.NLicencia, opt => opt.MapFrom(src => src.NLicencia))
                .ForMember(dest => dest.TipoTrabajo, opt => opt.MapFrom(src => src.TipoTrabajo))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
                .ForMember(dest => dest.Distrito, opt => opt.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.TipoDoc, opt => opt.MapFrom(src => src.TipoDoc))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());


            //Estudios
            CreateMap<EstudiosEntity, EstudiosDto>()
                .ForMember(dest => dest.studiesid, opt => opt.MapFrom(src => src.studiesid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom(src => src.OperatingHours))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.InformedConsent, opt => opt.MapFrom(src => src.InformedConsent))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateEstudiosDto, EstudiosEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom(src => src.OperatingHours))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.InformedConsent, opt => opt.MapFrom(src => src.InformedConsent))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateEstudiosDto, EstudiosEntity>()
                .ForMember(dest => dest.studiesid, opt => opt.MapFrom(src => src.studiesid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom(src => src.OperatingHours))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.InformedConsent, opt => opt.MapFrom(src => src.InformedConsent))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Salas
            CreateMap<SalasEntity, SalasDto>()
                .ForMember(dest => dest.procedureroomid, opt => opt.MapFrom(src => src.procedureroomid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateSalasDto, SalasEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateSalasDto, SalasEntity>()
                .ForMember(dest => dest.procedureroomid, opt => opt.MapFrom(src => src.procedureroomid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Recursos
            CreateMap<RecursosEntity, RecursosDto>()
                .ForMember(dest => dest.resourcesid, opt => opt.MapFrom(src => src.resourcesid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.procedureroomid, opt => opt.MapFrom(src => src.procedureroomid))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateRecursosDto, RecursosEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.procedureroomid, opt => opt.MapFrom(src => src.procedureroomid))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateRecursosDto, RecursosEntity>()
                .ForMember(dest => dest.resourcesid, opt => opt.MapFrom(src => src.resourcesid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.procedureroomid, opt => opt.MapFrom(src => src.procedureroomid))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());


            //Examenes
            CreateMap<ExamenesEntity, ExamenesDto>()
                .ForMember(dest => dest.examsid, opt => opt.MapFrom(src => src.examsid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateExamenesDto, ExamenesEntity>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateExamenesDto, ExamenesEntity>()
                .ForMember(dest => dest.examsid, opt => opt.MapFrom(src => src.examsid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Abbreviation))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Preparacion
            CreateMap<PreparacionEntity, PreparacionDto>()
                .ForMember(dest => dest.preparationid, opt => opt.MapFrom(src => src.preparationid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreatePreparacionDto, PreparacionEntity>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdatePreparacionDto, PreparacionEntity>()
                .ForMember(dest => dest.preparationid, opt => opt.MapFrom(src => src.preparationid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Seguros
            CreateMap<SegurosEntity, SegurosDto>()
                .ForMember(dest => dest.insuranceid, opt => opt.MapFrom(src => src.insuranceid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateSegurosDto, SegurosEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateSegurosDto, SegurosEntity>()
                .ForMember(dest => dest.insuranceid, opt => opt.MapFrom(src => src.insuranceid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identification))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Roles
            CreateMap<RolesEntity, RolesDto>()
                .ForMember(dest => dest.profiletypeid, opt => opt.MapFrom(src => src.profiletypeid))
                .ForMember(dest => dest.profile_name, opt => opt.MapFrom(src => src.profile_name))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateRolesDto, RolesEntity>()
                .ForMember(dest => dest.profile_name, opt => opt.MapFrom(src => src.profile_name))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateRolesDto, RolesEntity>()
                .ForMember(dest => dest.profiletypeid, opt => opt.MapFrom(src => src.profiletypeid))
                .ForMember(dest => dest.profile_name, opt => opt.MapFrom(src => src.profile_name))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //CreateMap<ServerFilterRequestDto, ServerFilter>();
        }
    }
}
