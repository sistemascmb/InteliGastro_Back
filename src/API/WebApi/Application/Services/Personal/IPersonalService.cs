using WebApi.Application.DTO.Personal;

namespace WebApi.Application.Services.Personal
{
    public interface IPersonalService
    {
        Task<PersonalDto> CreatePersonalAsync(CreatePersonalDto createPersonalDto);
        Task<PersonalDto> GetPersonalByIdAsync(long personalId);
        Task<PersonalDto> UpdatePersonalAsync(UpdatePersonalDto updatePersonalDto);
        Task<bool> DeletePersonalAsync(long personalId, string eliminadoPor);
    }
}
