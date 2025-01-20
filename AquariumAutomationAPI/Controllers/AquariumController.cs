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
            Aquarium? aquarium = _dataRepository.GetAquariumInfoByAquariumId(id);
            if (aquarium == null)
            {
                return Ok("There are no aquarium with this id");
            }
            return Ok(aquarium);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAquarium/{id:int}")]
        public ActionResult<List<Aquarium>> GetAllAquarium(int id)
        {
            List<Aquarium>? aquariums = _dataRepository.GetAquariumInfoByUserId(id);
            if(aquariums == null || aquariums.Count == 0)
            {
                return Ok($"There are no aquarium associated with UserId {id}");
            }
            return Ok(aquariums);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateAquarium")]
        public ActionResult<int> UpdateAquarium(AquariumUpdateDTO aquariumDTO)
        {
            Aquarium aquarium = new Aquarium
            {
                AquariumId=aquariumDTO.AquariumId,
                AquariumName=aquariumDTO.AquariumName,
                AquariumDescription=aquariumDTO.AquariumDescription,
                AquariumComments=aquariumDTO.AquariumComments,
                AquariumFixedPropertyId=aquariumDTO.AquariumFixedPropertyId,
                AquariumFixedPropertyComments=aquariumDTO.AquariumFixedPropertyComments,
                Length=aquariumDTO.Length,
                Width=aquariumDTO.Width,
                Height=aquariumDTO.Height,
            };
            int status = _dataRepository.UpdateAquarium(aquarium);
            if (status == -1)
            {
                return StatusCode(500);
            }
            else if (status == 0)
            {
                return Ok($"No aquariums associated with the AquariumId {aquariumDTO.AquariumId}");
            }
            return Ok(status);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAquarium/{id:int}")]
        public ActionResult<int> DeleteAquarium(int id)
        {
            int status = _dataRepository.DeleteAquarium(id);
            if(status==-1)
            {
                return StatusCode(500);
            }
            return Ok(status);
        }
    }
}
