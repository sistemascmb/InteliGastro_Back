using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Centro;

namespace WebApi.Application.Services.Centro
{
    public class Centroservice : ICentroService
    {
        private readonly ILogger<Centroservice> _logger;
        private readonly ICentroRepository _centroRepository;
        private readonly IMapper _mapper;
        public Centroservice(ILogger<Centroservice> logger, ICentroRepository centroRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _centroRepository = centroRepository ?? throw new ArgumentNullException(nameof(centroRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CentroDto> CreateCentroAsync(CreateCentroDto createCentroDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Centro [CRUD AUTOMÀTICO]");
                var centroEntity = _mapper.Map<Infraestructure.Models.CentroEntity>(createCentroDto);
                centroEntity.IsDeleted = false;
                centroEntity.CreatedAt = DateTime.UtcNow;

                var centroId = await _centroRepository.CreateCentroAsync(centroEntity);
                _logger.LogInformation("Centro creado exitosamente con ID: {CentroId}", centroId);

                var centroCreado = await _centroRepository.GetCentroByIdAsync(centroId);
                if (centroCreado == null)
                {
                    throw new InvalidOperationException($"No se puedo obtener el Centro creado con ID: {centroId}");
                }
                var centroEntity2 = (Infraestructure.Models.CentroEntity)centroCreado;
                var centroDto = _mapper.Map<CentroDto>(centroEntity2);
                return centroDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nueva persona");
                throw;
            }
        }

        public async Task<bool> DeleteCentroAsync(long centroId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Centro ID: {CentroId} [CRUD AUTOMÁTICO]", centroId);
                if (centroId <= 0)
                {
                    throw new ArgumentException("El ID del centro debe ser mayor a 0", nameof(centroId));
                }
                var centroExist = await _centroRepository.GetCentroByIdAsync(centroId);
                var centroExistente = (CentroEntity)centroExist;
                if (centroExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {centroId}");
                }
                if (centroExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Centro con ID: {centroId} porque está eliminado lógicamente.");
                }

                centroExistente.IsDeleted = true;
                centroExistente.UpdatedAt = DateTime.UtcNow;
                centroExistente.UpdatedBy = eliminadoPor;
                var result = await _centroRepository.UpdateCentroAsync(centroExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar lógicamente el Centro con ID: {centroId}");
                }
                _logger.LogInformation("Centro con ID: {CentroId} eliminado lógicamente exitosamente", centroId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar centro ID: {CentroId}", centroId);
                throw;
            }

        }

        public async Task<CentroDto> GetCentroByIdAsync(long centroId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtención de Centro por ID: {CentroId} [CRUD AUTOMÁTICO]", centroId);
                if (centroId <= 0)
                {
                    throw new ArgumentException("El ID del centro debe ser mayor a 0", nameof(centroId));
                }
                var centro = await _centroRepository.GetCentroByIdAsync(centroId);
                if (centro == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {centroId}");
                }
                var centroEntity = (CentroEntity)centro;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {centroId} está eliminado lógicamente.");
                }
                var centroDto = _mapper.Map<CentroDto>(centroEntity);
                _logger.LogInformation("Centro con ID: {CentroId} obtenido exitosamente", centroId);
                return centroDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener persona por ID: {CentroId}", centroId);
                throw;
            }
        }

        public async Task<CentroDto> UpdateCentroAsync(UpdateCentroDto updateCentroDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Centro ID: {CentroId} [CRUD AUTOMÁTICO]", updateCentroDto.centroid);
                if (updateCentroDto.centroid <= 0)
                {
                    throw new ArgumentException("El ID del centro debe ser mayor a 0", nameof(updateCentroDto.centroid));
                }
                var centroExist = await _centroRepository.GetCentroByIdAsync(updateCentroDto.centroid);
                if (centroExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {updateCentroDto.centroid}");
                }
                var centroExistente = (CentroEntity)centroExist;
                if (centroExistente == null)
                {
                    throw new KeyNotFoundException($"No se encontro el Centro con el Id {updateCentroDto.centroid}.");
                }

                var centroEntity = _mapper.Map<CentroEntity>(updateCentroDto);
                centroEntity.CreatedAt = centroExistente.CreatedAt;
                centroEntity.CreatedBy = centroExistente.CreatedBy;
                centroEntity.IsDeleted = false;

                var result = await _centroRepository.UpdateCentroAsync(centroEntity);

                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Centro con ID: {updateCentroDto.centroid}");
                }
                _logger.LogInformation("Centro con ID: {CentroId} actualizado exitosamente", updateCentroDto.centroid);
                var centroActualizado = await _centroRepository.GetCentroByIdAsync(updateCentroDto.centroid);
                if (centroActualizado == null)
                {
                    throw new InvalidOperationException($"No se puedo obtener el Centro actualizado con ID: {updateCentroDto.centroid}");
                }
                var centroEntity2 = (CentroEntity)centroActualizado;
                var centroDto = _mapper.Map<CentroDto>(centroEntity2);
                return centroDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Centro con ID: {CentroId}", updateCentroDto.centroid);
                throw;
            }
        }
    }
}
