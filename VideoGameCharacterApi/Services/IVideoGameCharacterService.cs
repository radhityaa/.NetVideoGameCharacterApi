using VideoGameCharacterApi.DTOs;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    public interface IVideoGameCharacterService
    {
        Task<List<CharacterResponse>> GetAllCharactersAsync();
        Task<CharacterResponse?> ShowCharacterByIdAsync(int Id);
        Task<CharacterResponse> CreateCharacterAsync(CreateCharacterRequest character);
        Task<bool> UpdateCharacterAsync(int Id, UpdateCharacterRequest character);
        Task<bool> DestroyCharacterAsync(int Id);
    }
}
