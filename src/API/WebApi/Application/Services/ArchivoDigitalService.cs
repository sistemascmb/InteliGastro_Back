using Domain.Entities;
using WebApi.Application.DTO.ArchivoDigital;
using AutoMapper;
using Domain.DomainInterfaces;
using Domain.RepositoriesInterfaces;

namespace WebApi.Application.Services
{
    public class ArchivoDigitalService : IArchivoDigitalService
    {
        private readonly IArchivoDigitalRepository archivoDigitalRepository;
        private readonly ICurrentArchivoDigital currentArchivoDigital;
        private readonly IMapper mapper;

        public ArchivoDigitalService(
        IArchivoDigitalRepository _archivoDigitalRepository,
        ICurrentArchivoDigital _currentArchivoDigital,
        IMapper mapper)
        {
            this.archivoDigitalRepository = _archivoDigitalRepository;
            this.currentArchivoDigital = _currentArchivoDigital;
            this.mapper = mapper;
        }

        public Task<ArchivoDigitalItemListResponseDto> CreateArchivoDigitalAsync(ArchivoDigitalItemListResponseDto archivoDigital)
        {
            throw new NotImplementedException();
        }

        public async Task<ArchivoDigitalItemListResponseDto> CreateArchivoDigitalLaboratorioAsyncc(ArchivoDigitalRequestDto archivoDigitalRequestDto)
        {
            //ArchivoDigital archivoDigital = mapper.Map<ArchivoDigital>(archivoDigitalRequestDto);
            //archivoDigital.IdAtencion = archivoDigitalRequestDto.IdAtencion;
            //archivoDigital.Archivo = archivoDigitalRequestDto.Archivo;

            //ArchivoDigital createarchivoDigital = await archivoDigitalRepository.CreateArchivoDigitalLaboratorioAsync(archivoDigital);

            //return mapper.Map<ArchivoDigitalItemListResponseDto>(createarchivoDigital);

            throw new NotImplementedException();

        }

        public async Task<bool> DeleteArchivoDigitalAsync(int id, string deletedBy)
        {
            throw new NotImplementedException();
        }

        public async Task<ArchivoDigitalItemListResponseDto?> GetArchivoDigitalByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ArchivoDigitalItemListResponseDto> UpdateArchivoDigitalAsync(ArchivoDigitalItemListResponseDto archivoDigital)
        {
            throw new NotImplementedException();
        }
    }
}
