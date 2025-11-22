using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Agenda;
using WebApi.Application.Services.Agenda;

namespace WebApi.Application.Services.Agenda
{
    public class AgendaService : IAgendaService
    {
        private readonly ILogger<AgendaService> _logger;
        private readonly IAgendaRepository _AgendaRepository;
        private readonly ICentroRepository _centroRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPersonalRepository _personalRepository;
        private readonly IEstudiosRepository _estudiosRepository;


        private readonly IMapper _mapper;
        public AgendaService(
            ILogger<AgendaService> logger,
            IAgendaRepository AgendaRepository,
            ICentroRepository centroRepository,
            IPacienteRepository pacienteRepository,
            IPersonalRepository personalRepository,
            IEstudiosRepository estudiosRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _AgendaRepository = AgendaRepository ?? throw new ArgumentNullException(nameof(AgendaRepository));
            _centroRepository = centroRepository ?? throw new ArgumentNullException(nameof(centroRepository));
            _pacienteRepository = pacienteRepository ?? throw new ArgumentNullException(nameof(pacienteRepository));
            _personalRepository = personalRepository ?? throw new ArgumentNullException(nameof(personalRepository));
            _estudiosRepository = estudiosRepository ?? throw new ArgumentNullException(nameof(estudiosRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AgendaDto> CreateAgendaAsync(CreateAgendaDto createAgendaDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Agenda");
                // Validar que el CentroId existe
                if (createAgendaDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(createAgendaDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(createAgendaDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {createAgendaDto.CentroId}. No se puede crear el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;

                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {createAgendaDto.CentroId} está eliminado lógicamente. No se puede crear el personal.");
                }

                var AgendaEntity = _mapper.Map<AgendaEntity>(createAgendaDto);
                AgendaEntity.IsDeleted = false;
                AgendaEntity.CreatedAt = DateTime.Now;
                var newAgendaId = await _AgendaRepository.CreateAgendaAsync(AgendaEntity);
                _logger.LogInformation("Agenda creado con ID: {AgendaId}", newAgendaId);
                var newAgenda = await _AgendaRepository.GetAgendaByIdAsync(newAgendaId);
                if (newAgenda == null)
                {
                    throw new Exception("Error al recuperar el estudio recién creado.");
                }

                var newAgendaDto = _mapper.Map<AgendaEntity>(newAgenda);
                var AgendaDto = _mapper.Map<AgendaDto>(newAgenda);
                return AgendaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Agenda");
                throw;
            }
        }

        public async Task<bool> DeleteAgendaAsync(long AgendaId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Estudio con ID: {AgendaId}", AgendaId);
                if (AgendaId <= 0)
                {
                    throw new ArgumentException("El ID del estudio debe ser mayor a 0", nameof(AgendaId));
                }
                var AgendaExist = await _AgendaRepository.GetAgendaByIdAsync(AgendaId);
                var AgendaExistente = (AgendaEntity)AgendaExist;
                if (AgendaExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el estudio con ID: {AgendaId}");
                }
                if (AgendaExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Estudio con ID: {AgendaId} por que ya está eiminado lógicamente.");
                }
                AgendaExistente.IsDeleted = true;
                AgendaExistente.UpdatedAt = DateTime.Now;
                AgendaExistente.UpdatedBy = eliminadoPor;

                var result = await _AgendaRepository.UpdateAgendaAsync(AgendaExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Estudio con ID: {AgendaId}");
                }
                _logger.LogInformation("Estudio con ID: {AgendaId} eliminado lógicamente", AgendaId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Estudio con ID: {AgendaId}", AgendaId);
                throw;
            }
        }

        public async Task<IEnumerable<AgendaDto>> GetAllAgendaAsync()
        {
            Task<IEnumerable<AgendaDto>> AgendaDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Agenda");
                var Agenda = _AgendaRepository.GetAllAgendaAsync().Result;
                var AgendaList = Agenda.Where(e => !((AgendaEntity)e).IsDeleted).ToList();
                AgendaDto = Task.FromResult(_mapper.Map<IEnumerable<AgendaDto>>(AgendaList));
                _logger.LogInformation("Agenda obtenidos: {Count}", AgendaList.Count());
                return await AgendaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Agenda");
                throw;
            }
        }

        public async Task<IEnumerable<AgendaDto>> SearchAgendaByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de agenda por rango de fechas: {StartDate} - {EndDate}", startDate, endDate);

                if (startDate > endDate)
                {
                    throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final");
                }

                var agendas = await _AgendaRepository.SearchAgendaByDateRangeAsync(startDate, endDate);
                var agendaDtos = _mapper.Map<IEnumerable<AgendaDto>>(agendas);

                _logger.LogInformation("Búsqueda de agenda completada. Se encontraron {Count} registros", agendaDtos.Count());

                return agendaDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar agenda por rango de fechas");
                throw;
            }
        }

        public async Task<IEnumerable<AgendaSearchResultDto>> SearchAgendaStudiesAsync(long? medicalscheduleid, string? names, string? lastNames, string? dni)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda combinada de estudios: medicalscheduleid={MedicalScheduleId}, names={Names}, lastNames={LastNames}, dni={Dni}", medicalscheduleid, names, lastNames, dni);

                var hasScheduleId = medicalscheduleid.HasValue && medicalscheduleid.Value > 0;
                var hasPatientFilters = !string.IsNullOrWhiteSpace(names) || !string.IsNullOrWhiteSpace(lastNames) || !string.IsNullOrWhiteSpace(dni);

                if (!hasScheduleId && !hasPatientFilters)
                {
                    throw new ArgumentException("Debe proporcionar al menos uno de los filtros: medicalscheduleid, nombres, apellidos o DNI.");
                }

                // Buscar pacientes si hay filtros de paciente
                var matchedPacientes = new List<Infraestructure.Models.PacienteEntity>();
                if (hasPatientFilters)
                {
                    var pacientes = await _pacienteRepository.SearchPacientesAsync(dni, names, lastNames);
                    matchedPacientes = pacientes
                        .Select(p => (Infraestructure.Models.PacienteEntity)p)
                        .ToList();

                    _logger.LogInformation("Pacientes coincidentes: {Count}", matchedPacientes.Count);
                }

                var pacientIds = matchedPacientes.Select(p => (int)p.pacientid).Distinct().ToArray();

                // Buscar agendas por filtros
                var agendas = await _AgendaRepository.SearchAgendaByFiltersAsync(medicalscheduleid, pacientIds);
                var agendaEntities = agendas.Select(a => (Infraestructure.Models.AgendaEntity)a).ToList();

                _logger.LogInformation("Agendas coincidentes: {Count}", agendaEntities.Count);

                // Cache local de pacientes para enriquecer resultados
                var pacienteCache = matchedPacientes.ToDictionary(p => (int)p.pacientid, p => p);

                var results = new List<AgendaSearchResultDto>();
                var centroCache = new Dictionary<int, Infraestructure.Models.CentroEntity>();
                var personalCache = new Dictionary<int, Infraestructure.Models.PersonalEntity>();
                var estudiosCache = new Dictionary<int, Infraestructure.Models.EstudiosEntity>();

                foreach (var agenda in agendaEntities)
                {
                    Infraestructure.Models.PacienteEntity? pacienteEntity;
                    if (!pacienteCache.TryGetValue(agenda.PacientId, out pacienteEntity))
                    {
                        var pacienteObj = await _pacienteRepository.GetPacienteByIdAsync(agenda.PacientId);
                        pacienteEntity = pacienteObj as Infraestructure.Models.PacienteEntity;
                        if (pacienteEntity != null)
                        {
                            pacienteCache[agenda.PacientId] = pacienteEntity;
                        }
                    }

                    var agendaDto = _mapper.Map<AgendaDto>(agenda);

                    // Enriquecer con datos derivados (Centro y Personal)
                    Infraestructure.Models.CentroEntity? centroEntity;
                    if (!centroCache.TryGetValue(agenda.CentroId, out centroEntity))
                    {
                        var centroObj = await _centroRepository.GetCentroByIdAsync(agenda.CentroId);
                        centroEntity = centroObj as Infraestructure.Models.CentroEntity;
                        if (centroEntity != null)
                        {
                            centroCache[agenda.CentroId] = centroEntity;
                        }
                    }

                    Infraestructure.Models.PersonalEntity? personalEntity;
                    if (!personalCache.TryGetValue(agenda.PersonalId, out personalEntity))
                    {
                        var personalObj = await _personalRepository.GetPersonalByIdAsync(agenda.PersonalId);
                        personalEntity = personalObj as Infraestructure.Models.PersonalEntity;
                        if (personalEntity != null)
                        {
                            personalCache[agenda.PersonalId] = personalEntity;
                        }
                    }

                    // Estudio derivado
                    Infraestructure.Models.EstudiosEntity? estudioEntity;
                    if (!estudiosCache.TryGetValue(agenda.StudiesId, out estudioEntity))
                    {
                        var estudioObj = await _estudiosRepository.GetEstudiosByIdAsync((long)agenda.StudiesId);
                        estudioEntity = estudioObj as Infraestructure.Models.EstudiosEntity;
                        if (estudioEntity != null)
                        {
                            estudiosCache[agenda.StudiesId] = estudioEntity;
                        }
                    }
                    var dto = new AgendaSearchResultDto
                    {
                        // Campos principales
                        medicalscheduleid = agendaDto.medicalscheduleid,
                        PacientId = agendaDto.PacientId,
                        CentroId = agendaDto.CentroId,
                        PersonalId = agendaDto.PersonalId,
                        AppointmentDate = agendaDto.AppointmentDate,
                        HoursMedicalShedule = agendaDto.HoursMedicalShedule,
                        TypeofAppointment = agendaDto.TypeofAppointment,
                        OriginId = agendaDto.OriginId,
                        OtherOrigins = agendaDto.OtherOrigins,
                        InsuranceId = agendaDto.InsuranceId,
                        LetterOfGuarantee = agendaDto.LetterOfGuarantee,
                        StudiesId = agendaDto.StudiesId,
                        Status = agendaDto.Status,
                        TypeOfAttention = agendaDto.TypeOfAttention,
                        TypeOfPatient = agendaDto.TypeOfPatient,
                        Referral_doctorsId = agendaDto.Referral_doctorsId,
                        CenterOfOriginId = agendaDto.CenterOfOriginId,
                        AnotherCenter = agendaDto.AnotherCenter,
                        ProcedureRoomId = agendaDto.ProcedureRoomId,
                        ResourcesId = agendaDto.ResourcesId,
                        CreatedAt = agendaDto.CreatedAt,
                        CreatedBy = agendaDto.CreatedBy,
                        UpdatedAt = agendaDto.UpdatedAt,
                        UpdatedBy = agendaDto.UpdatedBy,
                        IsDeleted = agendaDto.IsDeleted,
                        AnotacionesAdicionales = agendaDto.AnotacionesAdicionales,
                        TipoProcedimientoId = agendaDto.TipoProcedimientoId,
                        UrgenteId = agendaDto.UrgenteId,
                        EstudioTeminadoId = agendaDto.EstudioTeminadoId,
                        PdfGeneradoId = agendaDto.PdfGeneradoId,
                        EstructuraHtml = agendaDto.EstructuraHtml,
                        InformePdf = agendaDto.InformePdf,
                        DictadoGuardado = agendaDto.DictadoGuardado,
                        Preparacion = agendaDto.Preparacion,

                        // Datos del paciente
                        DocumentNumber = pacienteEntity?.DocumentNumber ?? string.Empty,
                        Names = pacienteEntity?.Names ?? string.Empty,
                        LastNames = pacienteEntity?.LastNames ?? string.Empty,

                        // Derivados
                        CentroNombre = centroEntity?.Nombre ?? string.Empty,
                        PersonalNombres = personalEntity?.Nombres ?? string.Empty,
                        PersonalApellidos = personalEntity?.Apellidos ?? string.Empty,
                        StudiesName = estudioEntity?.Name ?? string.Empty
                    };

                    results.Add(dto);
                }

                _logger.LogInformation("Búsqueda combinada completada. Resultados: {Count}", results.Count);
                return results;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar la búsqueda combinada de estudios");
                throw;
            }
        }

        public async Task<AgendaDto> GetAgendaByIdAsync(long AgendaId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Estudio por ID: {AgendaId}", AgendaId);
                if (AgendaId <= 0)
                {
                    throw new ArgumentException("El ID del estudio debe ser mayor a 0", nameof(AgendaId));
                }

                var Agenda = await _AgendaRepository.GetAgendaByIdAsync(AgendaId);
                if (Agenda == null)
                {
                    throw new KeyNotFoundException($"No se encontró el estudio con ID: {AgendaId}");
                }
                var AgendaEntity = (AgendaEntity)Agenda;
                if (AgendaEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El estudio con ID: {AgendaId} está eliminado lógicamente.");
                }
                var AgendaDto = _mapper.Map<AgendaDto>(AgendaEntity);
                _logger.LogInformation("Estudio obtenido con ID: {AgendaId}", AgendaId);
                return AgendaDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<AgendaDto>> GetWhereAsync(string condicion)
        {
            AgendaDto[] AgendaDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Agenda con condición: {condicion}", condicion);
                var Agenda = _AgendaRepository.GetAllAgendaAsync().Result;
                var AgendaList = Agenda.Where(e => !((AgendaEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    AgendaList = AgendaList.Where(e =>
                    {
                        var entity = (AgendaEntity)e;
                        return (entity.OtherOrigins != null && entity.OtherOrigins.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                AgendaDto = _mapper.Map<AgendaDto[]>(AgendaList);
                _logger.LogInformation("Agenda obtenidos con condición: {Count}", AgendaDto.Length);
                return Task.FromResult<IEnumerable<AgendaDto>>(AgendaDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Agenda con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<AgendaDto> UpdateAgendaAsync(UpdateAgendaDto updateAgendaDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Estudio con ID: {AgendaId}", updateAgendaDto.medicalscheduleid);
                if (updateAgendaDto.medicalscheduleid <= 0)
                {
                    throw new ArgumentException("El ID del estudio debe ser mayor a 0", nameof(updateAgendaDto.medicalscheduleid));
                }
                // Validar que el CentroId existe
                if (updateAgendaDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(updateAgendaDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(updateAgendaDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {updateAgendaDto.CentroId}. No se puede actualizar el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {updateAgendaDto.CentroId} está eliminado lógicamente. No se puede actualizar el personal.");
                }

                var AgendaExist = await _AgendaRepository.GetAgendaByIdAsync(updateAgendaDto.medicalscheduleid);
                if (AgendaExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Estudio con ID: {updateAgendaDto.medicalscheduleid}");
                }
                var AgendaExistente = (AgendaEntity)AgendaExist;
                if (AgendaExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Estudio con ID: {updateAgendaDto.medicalscheduleid} por que está eliminado lógicamente.");
                }
                var AgendaToUpdate = _mapper.Map<AgendaEntity>(updateAgendaDto);
                AgendaToUpdate.CreatedAt = AgendaExistente.CreatedAt;
                AgendaToUpdate.CreatedBy = AgendaExistente.CreatedBy;
                AgendaToUpdate.IsDeleted = false;
                AgendaToUpdate.UpdatedAt = DateTime.Now;

                var result = await _AgendaRepository.UpdateAgendaAsync(AgendaToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Estudio con ID: {updateAgendaDto.medicalscheduleid}");
                }
                var AgendaActualizado = await _AgendaRepository.GetAgendaByIdAsync(updateAgendaDto.medicalscheduleid);
                if (AgendaActualizado == null)
                {
                    throw new Exception("Error al recuperar el estudio recién actualizado.");
                }
                var estudioEntity2 = (AgendaEntity)AgendaActualizado;
                var AgendaDto = _mapper.Map<AgendaDto>(estudioEntity2);
                return AgendaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Estudio con ID: {AgendaId}", updateAgendaDto.medicalscheduleid);
                throw;
            }
        }
    }
}
