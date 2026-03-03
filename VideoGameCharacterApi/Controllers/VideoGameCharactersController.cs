using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.DTOs;
using VideoGameCharacterApi.Models;
using VideoGameCharacterApi.Services;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameCharactersController(IVideoGameCharacterService service) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CharacterResponse>>>> GetAllCharacters()
        {
            var character = await service.GetAllCharactersAsync();

            return SuccessResponse(character, "Character retrieved successfully.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponse>>> ShowCharacter(int Id)
        {
            var character = await service.ShowCharacterByIdAsync(Id);

            if (character is null)
            {
                return ErrorResponse<CharacterResponse>("Character with the given Id was not found.");
            }

            return SuccessResponse(character, "Character retrieved successfully.");
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CharacterResponse>>> CreateCharacter(CreateCharacterRequest character)
        {
            var createdCharacter = await service.CreateCharacterAsync(character);
            
            if (createdCharacter == null) {
                return ErrorResponse<CharacterResponse>("Failed to create character.", 400);
            }

            return SuccessResponse<CharacterResponse>(createdCharacter, "Created Character Successfully.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponse>>> UpdateCharacter(int Id, UpdateCharacterRequest character)
        {
            var updated = await service.UpdateCharacterAsync(Id, character);

            if (!updated)
            {
                return ErrorResponse<CharacterResponse>("Character with the given Id was not found.");
            }

            return SuccessResponse<CharacterResponse>(message: "Character updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponse>>> DestroyCharacter(int Id)
        {
            var deleted = await service.DestroyCharacterAsync(Id);
            
            if (!deleted)
            {
                return ErrorResponse<CharacterResponse>("Character with the given Id was not found.");
            }

            return SuccessResponse<CharacterResponse>(message: "Character deleted successfully.");
        }
    }
}
