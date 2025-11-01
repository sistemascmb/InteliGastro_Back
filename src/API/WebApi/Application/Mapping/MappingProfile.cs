using AutoMapper;
using Domain.Entities;
using Infraestructure.Models;
using Microsoft.AspNetCore.Hosting.Server;
using WebApi.Application.DTO.Agenda;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.Centro;
using WebApi.Application.DTO.Cie10;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.DTO.Examenes;
using WebApi.Application.DTO.Macros;
using WebApi.Application.DTO.MedicoReferencia;
using WebApi.Application.DTO.Paciente;
using WebApi.Application.DTO.Personal;
using WebApi.Application.DTO.Plantilla;
using WebApi.Application.DTO.Preparacion;
using WebApi.Application.DTO.Recursos;
using WebApi.Application.DTO.Roles;
using WebApi.Application.DTO.Salas;
using WebApi.Application.DTO.Seguros;
using WebApi.Application.DTO.Suministros;
using WebApi.Application.DTO.SystemParameter;
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
                .ForMember(dest => dest.profiletypeid, opt => opt.MapFrom(src => src.profiletypeid))
                .ForMember(dest => dest.Personalid, opt => opt.MapFrom(src => src.Personalid))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña))
                .ForMember(dest => dest.ContraseñaC, opt => opt.MapFrom(src => src.ContraseñaC))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            // Mapeo de CreateSystemUsersDto a SystemUsersEntity
            CreateMap<CreateSystemUsersDto, SystemUsersEntity>()
                .ForMember(dest => dest.userid, opt => opt.Ignore()) // Se genera automáticamente
                .ForMember(dest => dest.profiletypeid, opt => opt.MapFrom(src => src.profiletypeid))
                .ForMember(dest => dest.Personalid, opt => opt.MapFrom(src => src.Personalid))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña))
                .ForMember(dest => dest.ContraseñaC, opt => opt.MapFrom(src => src.ContraseñaC))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // Mapeo de UpdateSystemUsersDto a SystemUsersEntity
            CreateMap<UpdateSystemUsersDto, SystemUsersEntity>()
                .ForMember(dest => dest.userid, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.profiletypeid, opt => opt.MapFrom(src => src.profiletypeid))
                .ForMember(dest => dest.Personalid, opt => opt.MapFrom(src => src.Personalid))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña))
                .ForMember(dest => dest.ContraseñaC, opt => opt.MapFrom(src => src.ContraseñaC))
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

            //MedicoReferencia
            CreateMap<MedicoReferenciaEntity, MedicoReferenciaDto>()
                .ForMember(dest => dest.referraldoctorsd, opt => opt.MapFrom(src => src.referraldoctorsd))
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Names))
                .ForMember(dest => dest.Surnames, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Date_of_birth, opt => opt.MapFrom(src => src.Date_of_birth))
                .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.Profession))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateMedicoReferenciaDto, MedicoReferenciaEntity>()
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Names))
                .ForMember(dest => dest.Surnames, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Date_of_birth, opt => opt.MapFrom(src => src.Date_of_birth))
                .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.Profession))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateMedicoReferenciaDto, MedicoReferenciaEntity>()
                .ForMember(dest => dest.referraldoctorsd, opt => opt.MapFrom(src => src.referraldoctorsd))
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Names))
                .ForMember(dest => dest.Surnames, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Date_of_birth, opt => opt.MapFrom(src => src.Date_of_birth))
                .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.Profession))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Macros
            CreateMap<MacrosEntity, MacrosDto>()
                .ForMember(dest => dest.macrosid, opt => opt.MapFrom(src => src.macrosid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.Macro, opt => opt.MapFrom(src => src.Macro))
                .ForMember(dest => dest.SelectAll, opt => opt.MapFrom(src => src.SelectAll))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateMacrosDto, MacrosEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.Macro, opt => opt.MapFrom(src => src.Macro))
                .ForMember(dest => dest.SelectAll, opt => opt.MapFrom(src => src.SelectAll))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateMacrosDto, MacrosEntity>()
                .ForMember(dest => dest.macrosid, opt => opt.MapFrom(src => src.macrosid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.Macro, opt => opt.MapFrom(src => src.Macro))
                .ForMember(dest => dest.SelectAll, opt => opt.MapFrom(src => src.SelectAll))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Plantilla
            CreateMap<PlantillaEntity, PlantillaDto>()
                .ForMember(dest => dest.templatesid, opt => opt.MapFrom(src => src.templatesid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.ExamsId, opt => opt.MapFrom(src => src.ExamsId))
                .ForMember(dest => dest.Plantilla, opt => opt.MapFrom(src => src.Plantilla))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.AllPersonalMed, opt => opt.MapFrom(src => src.AllPersonalMed))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreatePlantillaDto, PlantillaEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.ExamsId, opt => opt.MapFrom(src => src.ExamsId))
                .ForMember(dest => dest.Plantilla, opt => opt.MapFrom(src => src.Plantilla))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.AllPersonalMed, opt => opt.MapFrom(src => src.AllPersonalMed))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdatePlantillaDto, PlantillaEntity>()
                .ForMember(dest => dest.templatesid, opt => opt.MapFrom(src => src.templatesid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.ExamsId, opt => opt.MapFrom(src => src.ExamsId))
                .ForMember(dest => dest.Plantilla, opt => opt.MapFrom(src => src.Plantilla))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.AllPersonalMed, opt => opt.MapFrom(src => src.AllPersonalMed))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Paciente
            CreateMap<PacienteEntity, PacienteDto>()
                .ForMember(dest => dest.pacientid, opt => opt.MapFrom(src => src.pacientid))
                .ForMember(dest => dest.TypeDoc, opt => opt.MapFrom(src => src.TypeDoc))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Names))
                .ForMember(dest => dest.LastNames, opt => opt.MapFrom(src => src.LastNames))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.StatusMarital, opt => opt.MapFrom(src => src.StatusMarital))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.MedicalHistory))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.isdeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreatePacienteDto, PacienteEntity>()
                .ForMember(dest => dest.TypeDoc, opt => opt.MapFrom(src => src.TypeDoc))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Names))
                .ForMember(dest => dest.LastNames, opt => opt.MapFrom(src => src.LastNames))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.StatusMarital, opt => opt.MapFrom(src => src.StatusMarital))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.MedicalHistory))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdatePacienteDto, PacienteEntity>()
                .ForMember(dest => dest.pacientid, opt => opt.MapFrom(src => src.pacientid))
                .ForMember(dest => dest.TypeDoc, opt => opt.MapFrom(src => src.TypeDoc))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Names))
                .ForMember(dest => dest.LastNames, opt => opt.MapFrom(src => src.LastNames))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.StatusMarital, opt => opt.MapFrom(src => src.StatusMarital))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.MedicalHistory))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Agenda
            CreateMap<AgendaEntity, AgendaDto>()
                .ForMember(dest => dest.medicalscheduleid, opt => opt.MapFrom(src => src.medicalscheduleid))
                .ForMember(dest => dest.PacientId, opt => opt.MapFrom(src => src.PacientId))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.HoursMedicalShedule, opt => opt.MapFrom(src => src.HoursMedicalShedule))
                .ForMember(dest => dest.TypeofAppointment, opt => opt.MapFrom(src => src.TypeofAppointment))
                .ForMember(dest => dest.OriginId, opt => opt.MapFrom(src => src.OriginId))
                .ForMember(dest => dest.OtherOrigins, opt => opt.MapFrom(src => src.OtherOrigins))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.InsuranceId))
                .ForMember(dest => dest.LetterOfGuarantee, opt => opt.MapFrom(src => src.LetterOfGuarantee))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TypeOfAttention, opt => opt.MapFrom(src => src.TypeOfAttention))
                .ForMember(dest => dest.TypeOfPatient, opt => opt.MapFrom(src => src.TypeOfPatient))
                .ForMember(dest => dest.Referral_doctorsId, opt => opt.MapFrom(src => src.Referral_doctorsId))
                .ForMember(dest => dest.CenterOfOriginId, opt => opt.MapFrom(src => src.CenterOfOriginId))
                .ForMember(dest => dest.AnotherCenter, opt => opt.MapFrom(src => src.AnotherCenter))
                .ForMember(dest => dest.ProcedureRoomId, opt => opt.MapFrom(src => src.ProcedureRoomId))
                .ForMember(dest => dest.ResourcesId, opt => opt.MapFrom(src => src.ResourcesId))
                .ForMember(dest => dest.StudiesId, opt => opt.MapFrom(src => src.StudiesId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateAgendaDto, AgendaEntity>()
                .ForMember(dest => dest.PacientId, opt => opt.MapFrom(src => src.PacientId))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.HoursMedicalShedule, opt => opt.MapFrom(src => src.HoursMedicalShedule))
                .ForMember(dest => dest.TypeofAppointment, opt => opt.MapFrom(src => src.TypeofAppointment))
                .ForMember(dest => dest.OriginId, opt => opt.MapFrom(src => src.OriginId))
                .ForMember(dest => dest.OtherOrigins, opt => opt.MapFrom(src => src.OtherOrigins))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.InsuranceId))
                .ForMember(dest => dest.LetterOfGuarantee, opt => opt.MapFrom(src => src.LetterOfGuarantee))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TypeOfAttention, opt => opt.MapFrom(src => src.TypeOfAttention))
                .ForMember(dest => dest.TypeOfPatient, opt => opt.MapFrom(src => src.TypeOfPatient))
                .ForMember(dest => dest.Referral_doctorsId, opt => opt.MapFrom(src => src.Referral_doctorsId))
                .ForMember(dest => dest.CenterOfOriginId, opt => opt.MapFrom(src => src.CenterOfOriginId))
                .ForMember(dest => dest.AnotherCenter, opt => opt.MapFrom(src => src.AnotherCenter))
                .ForMember(dest => dest.ProcedureRoomId, opt => opt.MapFrom(src => src.ProcedureRoomId))
                .ForMember(dest => dest.ResourcesId, opt => opt.MapFrom(src => src.ResourcesId))
                .ForMember(dest => dest.StudiesId, opt => opt.MapFrom(src => src.StudiesId))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateAgendaDto, AgendaEntity>()
                .ForMember(dest => dest.medicalscheduleid, opt => opt.MapFrom(src => src.medicalscheduleid))
                .ForMember(dest => dest.PacientId, opt => opt.MapFrom(src => src.PacientId))
                .ForMember(dest => dest.CentroId, opt => opt.MapFrom(src => src.CentroId))
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
                .ForMember(dest => dest.HoursMedicalShedule, opt => opt.MapFrom(src => src.HoursMedicalShedule))
                .ForMember(dest => dest.TypeofAppointment, opt => opt.MapFrom(src => src.TypeofAppointment))
                .ForMember(dest => dest.OriginId, opt => opt.MapFrom(src => src.OriginId))
                .ForMember(dest => dest.OtherOrigins, opt => opt.MapFrom(src => src.OtherOrigins))
                .ForMember(dest => dest.InsuranceId, opt => opt.MapFrom(src => src.InsuranceId))
                .ForMember(dest => dest.LetterOfGuarantee, opt => opt.MapFrom(src => src.LetterOfGuarantee))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TypeOfAttention, opt => opt.MapFrom(src => src.TypeOfAttention))
                .ForMember(dest => dest.TypeOfPatient, opt => opt.MapFrom(src => src.TypeOfPatient))
                .ForMember(dest => dest.Referral_doctorsId, opt => opt.MapFrom(src => src.Referral_doctorsId))
                .ForMember(dest => dest.CenterOfOriginId, opt => opt.MapFrom(src => src.CenterOfOriginId))
                .ForMember(dest => dest.AnotherCenter, opt => opt.MapFrom(src => src.AnotherCenter))
                .ForMember(dest => dest.ProcedureRoomId, opt => opt.MapFrom(src => src.ProcedureRoomId))
                .ForMember(dest => dest.ResourcesId, opt => opt.MapFrom(src => src.ResourcesId))
                .ForMember(dest => dest.StudiesId, opt => opt.MapFrom(src => src.StudiesId))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Suministros
            CreateMap<SuministrosEntity, SuministrosDto>()
                .ForMember(dest => dest.provisionid, opt => opt.MapFrom(src => src.provisionid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateSuministrosDto, SuministrosEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateSuministrosDto, SuministrosEntity>()
                .ForMember(dest => dest.provisionid, opt => opt.MapFrom(src => src.provisionid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //Cie10
            CreateMap<Cie10Entity, Cie10Dto>()
                .ForMember(dest => dest.cieid, opt => opt.MapFrom(src => src.cieid))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateCie10Dto, Cie10Entity>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateCie10Dto, Cie10Entity>()
                .ForMember(dest => dest.cieid, opt => opt.MapFrom(src => src.cieid))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.GenderId, opt => opt.MapFrom(src => src.GenderId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            //SystemParameter
            CreateMap<SystemParameterEntity, SystemParameterDto>()
                .ForMember(dest => dest.parameterid, opt => opt.MapFrom(src => src.parameterid))
                .ForMember(dest => dest.groupid, opt => opt.MapFrom(src => src.groupid))
                .ForMember(dest => dest.Value1, opt => opt.MapFrom(src => src.Value1))
                .ForMember(dest => dest.Value2, opt => opt.MapFrom(src => src.Value2))
                .ForMember(dest => dest.ParentParameterId, opt => opt.MapFrom(src => src.ParentParameterId))
                .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Sort))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<CreateSystemParameterDto, SystemParameterEntity>()
                .ForMember(dest => dest.parameterid, opt => opt.MapFrom(src => src.parameterid))
                .ForMember(dest => dest.groupid, opt => opt.MapFrom(src => src.groupid))
                .ForMember(dest => dest.Value1, opt => opt.MapFrom(src => src.Value1))
                .ForMember(dest => dest.Value2, opt => opt.MapFrom(src => src.Value2))
                .ForMember(dest => dest.ParentParameterId, opt => opt.MapFrom(src => src.ParentParameterId))
                .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Sort))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se establece en el servicio
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateSystemParameterDto, SystemParameterEntity>()
                .ForMember(dest => dest.parameterid, opt => opt.MapFrom(src => src.parameterid))
                .ForMember(dest => dest.groupid, opt => opt.MapFrom(src => src.groupid))
                .ForMember(dest => dest.Value1, opt => opt.MapFrom(src => src.Value1))
                .ForMember(dest => dest.Value2, opt => opt.MapFrom(src => src.Value2))
                .ForMember(dest => dest.ParentParameterId, opt => opt.MapFrom(src => src.ParentParameterId))
                .ForMember(dest => dest.Sort, opt => opt.MapFrom(src => src.Sort))
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Se mantiene el valor original
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());


            //CreateMap<ServerFilterRequestDto, ServerFilter>();
        }
    }
}
