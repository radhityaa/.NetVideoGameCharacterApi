using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.DTOs;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Services
{
    public class VideoGameCharacterService(AppDbContext context) : IVideoGameCharacterService
    {
        public async Task<CharacterResponse> CreateCharacterAsync(CreateCharacterRequest character)
        {
            var newCharacter = new Character
            {
                Name = character.Name,
                Game = character.Game,
                Role = character.Role
            };

            context.Characters.Add(newCharacter);
            await context.SaveChangesAsync();

            return new CharacterResponse
            {
                Id = newCharacter.Id,
                Name = newCharacter.Name,
                Game = newCharacter.Game,
                Role = newCharacter.Role
            };
        }

        public async Task<bool> DestroyCharacterAsync(int Id)
        {
            var characterToDeleted = await context.Characters.FindAsync(Id);

            if (characterToDeleted is null) return false;

            context.Characters.Remove(characterToDeleted);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<CharacterResponse>> GetAllCharactersAsync()
        {
            var characters = await context.Characters.Select(c => new CharacterResponse
            {
                Id = c.Id,
                Name = c.Name,
                Game = c.Game,
                Role = c.Role
            }).ToListAsync();

            return characters;
        }

        public async Task<CharacterResponse?> ShowCharacterByIdAsync(int Id)
        {
            var character = await context.Characters
                .Where(c => c.Id == Id)
                .Select(c => new CharacterResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Game = c.Game,
                    Role = c.Role
                })
                .FirstOrDefaultAsync();

            return character;
        }

        public Task<bool> UpdateCharacterAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCharacterAsync(int Id, UpdateCharacterRequest character)
        {
            var existingCharacter = await context.Characters.FindAsync(Id);

            if (existingCharacter is null) return false;

            existingCharacter.Name = character.Name;
            existingCharacter.Game = character.Game;
            existingCharacter.Role = character.Role;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
