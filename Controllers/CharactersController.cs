using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController] // this attribute indecate that a type and also all derived type is use to serve http api responces + it enable sevrale api specifique features like attribute Routing and http 4xx responces if somthing is wrong with model
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }
        [HttpPut]

        public async Task<ActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updateCharacter);
            if(response.Data == null){
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}