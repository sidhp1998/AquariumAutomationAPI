using AquariumAutomationAPI.DTO;
using AquariumAutomationAPI.Models;
using AquariumAutomationAPI.Repository;
using AquariumAutomationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquariumAutomationAPI.Controllers
{
    public class AquariumController(IDataRepository _dataRepository) : BaseApiController
    {
        [Authorize]
        [HttpPost("CreateNewAquarium")]
        public ActionResult<Aquarium> CreateNewAquarium(AquariumDTO aquariumDTO)
        {
            if (!CheckRequestValidity(aquariumDTO))
            {
                return BadRequest("This is a bad request! Please check your input from API invocation point");
            }

            Aquarium aquarium = new Aquarium
            {
                AquariumName = aquariumDTO.AquariumName,
                AquariumDescription = aquariumDTO.AquariumDescription,
                UserId = aquariumDTO.UserId,
                IsActive = true,
                AquariumComments = aquariumDTO.AquariumComments,
                AquariumCreatedDate = DateTime.Now,
                AquariumFixedPropertyComments = aquariumDTO.AquariumFixedPropertyComments,
                Length = aquariumDTO.Length,
                Width = aquariumDTO.Width,
                Height = aquariumDTO.Height,
                AquariumFixedPropertyCreatedDate = DateTime.Now
            };

            aquarium = _dataRepository.CreateNewAquariumToDb(aquarium);

            if(aquarium == null || (aquarium.AquariumId == -1 && aquarium.AquariumFixedPropertyId == -1))
            {
                return StatusCode(500);
            }
            return Ok(aquarium);
        }

        private bool CheckRequestValidity(AquariumDTO aquariumDTO)
        {
            return true;
        }


        [Authorize]
        [HttpGet]
        [Route("GetAquariumInfo/{id:int}")]
        public ActionResult<Aquarium> GetAquariumInfo(int id)
        {
            Aquarium? aquarium = _dataRepository.GetAquariumInfoById(id);
            if (aquarium == null)
            {
                return Ok("There are no aquarium with this id");
            }
            return Ok(aquarium);
        }
    }
}
