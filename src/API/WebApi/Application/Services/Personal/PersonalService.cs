using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Personal;

namespace WebApi.Application.Services.Personal
{
    public class PersonalService : IPersonalService
    {
        private readonly ILogger<PersonalService> _logger;
        private readonly IPersonalRepository _personalRepository;
        private readonly ICentroRepository _centroRepository;
        private readonly IMapper _mapper;

        public PersonalService(ILogger<PersonalService> logger, IPersonalRepository personalRepository, ICentroRepository centroRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personalRepository = personalRepository ?? throw new ArgumentNullException(nameof(personalRepository));
            _centroRepository = centroRepository ?? throw new ArgumentNullException(nameof(centroRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PersonalDto> CreatePersonalAsync(CreatePersonalDto createPersonalDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Personal");
                
                // Validar que el CentroId existe
                if (createPersonalDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(createPersonalDto.CentroId));
                }
                
                var centroExiste = await _centroRepository.GetCentroByIdAsync(createPersonalDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {createPersonalDto.CentroId}. No se puede crear el personal.");
                }
                
                var centroEntity = (CentroEntity)centroExiste;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {createPersonalDto.CentroId} está eliminado lógicamente. No se puede crear el personal.");
                }
                
                var personalEntity = _mapper.Map<Infraestructure.Models.PersonalEntity>(createPersonalDto);
                personalEntity.IsDeleted = false;
                personalEntity.CreatedAt = DateTime.UtcNow;
                var personalId = _personalRepository.CreatePersonalAsync(personalEntity).Result;
                _logger.LogInformation("Personal creado exitosamente con ID: {PersonalId}", personalId);
                var personalCreado = _personalRepository.GetPersonalByIdAsync(personalId).Result;
                if (personalCreado == null)
                {
                    throw new InvalidOperationException($"No se puedo obtener el Personal creado con ID: {personalId}");
                }
                var personalEntity2 = (Infraestructure.Models.PersonalEntity)personalCreado;
                var personalDto = _mapper.Map<PersonalDto>(personalEntity2);
                return personalDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo personal");
                throw;
            }
        }

        public async Task<bool> DeletePersonalAsync(long personalId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Personal ID: {PersonalId}", personalId);
                if (personalId <= 0)
                {
                    throw new ArgumentException("El ID del dentro debe ser mayor a 0", nameof(personalId));
                }
                var personalExist = await _personalRepository.GetPersonalByIdAsync(personalId);
                var personalExistente = (PersonalEntity)personalExist;
                if (personalExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Personal con ID: {personalId}");
                }
                if (personalExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Personal con ID: {personalId} porque ya está eliminado lógicamente.");
                }
                personalExistente.IsDeleted = true;
                personalExistente.UpdatedAt = DateTime.UtcNow;
                personalExistente.UpdatedBy = eliminadoPor;

                var result = await _personalRepository.UpdatePersonalAsync(personalExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Personal con ID: {personalId}");
                }
                _logger.LogInformation("Personal con ID: {PersonalId} eliminado lógicamente exitosamente", personalId);
                return result;
            }
            catch (Exception)
            {
                _logger.LogError("Error al eliminar personal con ID: {PersonalId}", personalId);
                throw;
            }
        }

        public async Task<PersonalDto> GetPersonalByIdAsync(long personalId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtención de Personal por ID: {personalid}", personalId);
                if (personalId <= 0)
                {
                    throw new ArgumentException("El ID del dentro debe ser mayor a 0", nameof(personalId));
                }

                var personal = _personalRepository.GetPersonalByIdAsync(personalId).Result;
                if (personal == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Personal con ID: {personalId}");
                }

                var personalEntity = (PersonalEntity)personal;
                if (personalEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Personal con ID: {personalId} está eliminado lógicamente.");
                }
                var personalDto = _mapper.Map<PersonalDto>(personalEntity);
                _logger.LogInformation("Personal con ID: {personalid} obtenido exitosamente", personalId);
                return personalDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PersonalDto> UpdatePersonalAsync(UpdatePersonalDto updatePersonalDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Personal ID: {personalId}", updatePersonalDto.personalid);
                if (updatePersonalDto.personalid <= 0)
                {
                    throw new ArgumentException("El ID del Personal debe ser mayor a 0", nameof(updatePersonalDto.personalid));
                }

                // Validar que el CentroId existe
                if (updatePersonalDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(updatePersonalDto.CentroId));
                }
                
                var centroExiste = await _centroRepository.GetCentroByIdAsync(updatePersonalDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {updatePersonalDto.CentroId}. No se puede actualizar el personal.");
                }
                
                var centroEntity = (CentroEntity)centroExiste;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {updatePersonalDto.CentroId} está eliminado lógicamente. No se puede actualizar el personal.");
                }

                var personalExist = await _personalRepository.GetPersonalByIdAsync(updatePersonalDto.personalid);
                if (personalExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Personal con ID: {updatePersonalDto.personalid}");
                }   
                var personalExistente = (PersonalEntity)personalExist;
                if (personalExistente == null)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Personal con ID: {updatePersonalDto.personalid} porque está eliminado lógicamente.");
                }

                var personalEntity = _mapper.Map<PersonalEntity>(updatePersonalDto);
                personalEntity.CreatedAt = personalExistente.CreatedAt;
                personalEntity.CreatedBy = personalExistente.CreatedBy;
                personalEntity.IsDeleted = false;
                var result = await _personalRepository.UpdatePersonalAsync(personalEntity);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Personal con ID: {updatePersonalDto.personalid}");
                }
                var personalActualizado = await _personalRepository.GetPersonalByIdAsync(updatePersonalDto.personalid);
                if (personalActualizado == null)
                {
                    throw new InvalidOperationException($"No se puedo obtener el Personal actualizado con ID: {updatePersonalDto.personalid}");
                }
                var personalEntity2 = (PersonalEntity)personalActualizado;
                var personalDto = _mapper.Map<PersonalDto>(personalEntity2);
                return personalDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar personal con ID: {personalId}", updatePersonalDto.personalid);
                throw;
            }
        }
    }
}
