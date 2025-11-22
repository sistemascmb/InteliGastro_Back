using System.Text.Json.Serialization;

namespace WebApi.Application.DTO.SystemUsers
{
    public class LoginRequestDto
    {
        [JsonPropertyName("usuario")]
        public string Usuario { get; set; }

        [JsonPropertyName("contraseña")]
        public string Contraseña { get; set; }
    }
}