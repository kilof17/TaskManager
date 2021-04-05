using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FinishedQuestsController : ControllerBase
    {
        private IFinishedQuestsRepository _finishedQuestRepository;

        public FinishedQuestsController(IFinishedQuestsRepository finishedQuestRepository)
        {
            _finishedQuestRepository = finishedQuestRepository;
        }

        // GET: finishedquests/
        [HttpGet]
        public ActionResult<IEnumerable<FinishedQuest>> Get() //TODO: acces only for admin
        {
            var allQuests = _finishedQuestRepository.GetAllFinishedQuests();
            return Ok(allQuests);
        }

        // GET: finishedquests/{userId}
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<FinishedQuest>> Get(string userId)
        {
            var filteredQuests = _finishedQuestRepository.GetUserFinishedQuests(userId);
            return Ok(filteredQuests);
        }

        // DELETE: finishedquests/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _finishedQuestRepository.RemoveFinishedQuest(id);
            return Ok();
        }
    }
}