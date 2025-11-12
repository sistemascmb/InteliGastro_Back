using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.Services.Estudios;
using WebApi.Application.Services.ArchivoDigital;

namespace WebApi.Application.Services.ArchivoDigital
{
    public class ArchivoDigitalServiceN : IArchivoDigitalServiceN
    {
        private readonly ILogger<ArchivoDigitalServiceN> _logger;
        private readonly IArchivoDigitalRepositoryN _archivoDigitalRepository;
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;
        public ArchivoDigitalServiceN(
            ILogger<ArchivoDigitalServiceN> logger,
            IArchivoDigitalRepositoryN ArchivoDigitalRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _archivoDigitalRepository = ArchivoDigitalRepository ?? throw new ArgumentNullException(nameof(ArchivoDigitalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ArchivoDigitalDto> CreateArchivoDigitalAsync(CreateArchivoDigitalDto createArchivoDigitalDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de ArchivoDigital");

                var ArchivoDigitalEntity = _mapper.Map<ArchivoDigitalEntity>(createArchivoDigitalDto);
                ArchivoDigitalEntity.IsDeleted = false;
                ArchivoDigitalEntity.CreatedAt = DateTime.Now;
                var newdigitalfileid = await _archivoDigitalRepository.CreateArchivoDigitalAsync(ArchivoDigitalEntity);
                _logger.LogInformation("ArchivoDigital creado con ID: {digitalfileid}", newdigitalfileid);
                var newArchivoDigital = await _archivoDigitalRepository.GetArchivoDigitalByIdAsync(newdigitalfileid);
                if (newArchivoDigital == null)
                {
                    throw new Exception("Error al recuperar el ArchivoDigital recién creado.");
                }

                var newArchivoDigitalDto = _mapper.Map<ArchivoDigitalEntity>(newArchivoDigital);
                var ArchivoDigitalDto = _mapper.Map<ArchivoDigitalDto>(newArchivoDigital);
                return ArchivoDigitalDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear ArchivoDigital");
                throw;
            }
        }

        public async Task<bool> DeleteArchivoDigitalAsync(long digitalfileid, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de ArchivoDigital con ID: {digitalfileid}", digitalfileid);
                if (digitalfileid <= 0)
                {
                    throw new ArgumentException("El ID del ArchivoDigital debe ser mayor a 0", nameof(digitalfileid));
                }
                var ArchivoDigitalExist = await _archivoDigitalRepository.GetArchivoDigitalByIdAsync(digitalfileid);
                var ArchivoDigitalExistente = (ArchivoDigitalEntity)ArchivoDigitalExist;
                if (ArchivoDigitalExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el ArchivoDigital con ID: {digitalfileid}");
                }
                if (ArchivoDigitalExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el ArchivoDigital con ID: {digitalfileid} por que ya está eiminado lógicamente.");
                }
                ArchivoDigitalExistente.IsDeleted = true;
                ArchivoDigitalExistente.UpdatedAt = DateTime.Now;
                ArchivoDigitalExistente.UpdatedBy = eliminadoPor;

                var result = await _archivoDigitalRepository.UpdateArchivoDigitalAsync(ArchivoDigitalExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el ArchivoDigital con ID: {digitalfileid}");
                }
                _logger.LogInformation("ArchivoDigital con ID: {digitalfileid} eliminado lógicamente", digitalfileid);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el ArchivoDigital con ID: {digitalfileid}", digitalfileid);
                throw;
            }
        }

        public async Task<IEnumerable<ArchivoDigitalDto>> GetAllArchivoDigitalAsync()
        {
            Task<IEnumerable<ArchivoDigitalDto>> ArchivoDigitalDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los ArchivoDigital");
                var ArchivoDigital = _archivoDigitalRepository.GetAllArchivoDigitalAsync().Result;
                var ArchivoDigitalList = ArchivoDigital.Where(e => !((ArchivoDigitalEntity)e).IsDeleted).ToList();
                ArchivoDigitalDto = Task.FromResult(_mapper.Map<IEnumerable<ArchivoDigitalDto>>(ArchivoDigitalList));
                _logger.LogInformation("ArchivoDigital obtenidos: {Count}", ArchivoDigitalList.Count());
                return await ArchivoDigitalDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los ArchivoDigital");
                throw;
            }
        }

        public async Task<ArchivoDigitalDto> GetArchivoDigitalByIdAsync(long digitalfileid)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de ArchivoDigital por ID: {digitalfileid}", digitalfileid);
                if (digitalfileid <= 0)
                {
                    throw new ArgumentException("El ID del ArchivoDigital debe ser mayor a 0", nameof(digitalfileid));
                }

                var ArchivoDigital = await _archivoDigitalRepository.GetArchivoDigitalByIdAsync(digitalfileid);
                if (ArchivoDigital == null)
                {
                    throw new KeyNotFoundException($"No se encontró el ArchivoDigital con ID: {digitalfileid}");
                }
                var ArchivoDigitalEntity = (ArchivoDigitalEntity)ArchivoDigital;
                if (ArchivoDigitalEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El ArchivoDigital con ID: {digitalfileid} está eliminado lógicamente.");
                }
                var ArchivoDigitalDto = _mapper.Map<ArchivoDigitalDto>(ArchivoDigitalEntity);
                _logger.LogInformation("ArchivoDigital obtenido con ID: {digitalfileid}", digitalfileid);
                return ArchivoDigitalDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<ArchivoDigitalDto>> GetWhereAsync(string condicion)
        {
            ArchivoDigitalDto[] ArchivoDigitalDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de ArchivoDigital con condición: {condicion}", condicion);
                var ArchivoDigital = _archivoDigitalRepository.GetAllArchivoDigitalAsync().Result;
                var ArchivoDigitalList = ArchivoDigital.Where(e => !((ArchivoDigitalEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    ArchivoDigitalList = ArchivoDigitalList.Where(e =>
                    {
                        var entity = (ArchivoDigitalEntity)e;
                        return (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.TypeArchive != null && entity.TypeArchive.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                ArchivoDigitalDto = _mapper.Map<ArchivoDigitalDto[]>(ArchivoDigitalList);
                _logger.LogInformation("ArchivoDigital obtenidos con condición: {Count}", ArchivoDigitalDto.Length);
                return Task.FromResult<IEnumerable<ArchivoDigitalDto>>(ArchivoDigitalDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los ArchivoDigital con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<ArchivoDigitalDto> UpdateArchivoDigitalAsync(UpdateArchivoDigitalDto updateArchivoDigitalDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de ArchivoDigital con ID: {digitalfileid}", updateArchivoDigitalDto.digitalfileid);
                if (updateArchivoDigitalDto.digitalfileid <= 0)
                {
                    throw new ArgumentException("El ID del ArchivoDigital debe ser mayor a 0", nameof(updateArchivoDigitalDto.digitalfileid));
                }

                var ArchivoDigitalExist = await _archivoDigitalRepository.GetArchivoDigitalByIdAsync(updateArchivoDigitalDto.digitalfileid);
                if (ArchivoDigitalExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el ArchivoDigital con ID: {updateArchivoDigitalDto.digitalfileid}");
                }
                var ArchivoDigitalExistente = (ArchivoDigitalEntity)ArchivoDigitalExist;
                if (ArchivoDigitalExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el ArchivoDigital con ID: {updateArchivoDigitalDto.digitalfileid} por que está eliminado lógicamente.");
                }
                var ArchivoDigitalToUpdate = _mapper.Map<ArchivoDigitalEntity>(updateArchivoDigitalDto);
                ArchivoDigitalToUpdate.CreatedAt = ArchivoDigitalExistente.CreatedAt;
                ArchivoDigitalToUpdate.CreatedBy = ArchivoDigitalExistente.CreatedBy;
                ArchivoDigitalToUpdate.IsDeleted = false;
                ArchivoDigitalToUpdate.UpdatedAt = DateTime.Now;


                var result = await _archivoDigitalRepository.UpdateArchivoDigitalAsync(ArchivoDigitalToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el ArchivoDigital con ID: {updateArchivoDigitalDto.digitalfileid}");
                }
                var ArchivoDigitalActualizado = await _archivoDigitalRepository.GetArchivoDigitalByIdAsync(updateArchivoDigitalDto.digitalfileid);
                if (ArchivoDigitalActualizado == null)
                {
                    throw new Exception("Error al recuperar el ArchivoDigital recién actualizado.");
                }
                var estudioEntity2 = (ArchivoDigitalEntity)ArchivoDigitalActualizado;
                var ArchivoDigitalDto = _mapper.Map<ArchivoDigitalDto>(estudioEntity2);
                return ArchivoDigitalDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el ArchivoDigital con ID: {digitalfileid}", updateArchivoDigitalDto.digitalfileid);
                throw;
            }
        }

        
        public async Task<IEnumerable<ArchivoDigitalDto>> SearchArchivoDigitalsAsync(string? documentNumber, string? names, string? lastNames)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de ArchivoDigitals con filtros");

                if (string.IsNullOrWhiteSpace(documentNumber) &&
                    string.IsNullOrWhiteSpace(names) &&
                    string.IsNullOrWhiteSpace(lastNames))
                {
                    throw new ArgumentException("Debe proporcionar al menos un criterio de búsqueda");
                }

                var ArchivoDigitals = await _archivoDigitalRepository.SearchArchivoDigitalsAsync(documentNumber, names, lastNames);
                var ArchivoDigitalsDto = ArchivoDigitals.Select(p => _mapper.Map<ArchivoDigitalDto>((ArchivoDigitalEntity)p));

                _logger.LogInformation("Se encontraron {Count} ArchivoDigitals con los filtros especificados",
                    ArchivoDigitalsDto.Count());

                return ArchivoDigitalsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar ArchivoDigitals con los filtros especificados");
                throw;
            }
        }
    }
}
