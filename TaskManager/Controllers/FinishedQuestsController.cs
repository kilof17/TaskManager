using Microsoft.AspNetCore.Authorization;
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
        private readonly IFinishedQuestsRepository _finishedQuestRepository;

        public FinishedQuestsController(IFinishedQuestsRepository finishedQuestRepository)
        {
            _finishedQuestRepository = finishedQuestRepository;
        }

        // GET: finishedquests/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<FinishedQuest>>> Get()
        {
            var result = await _finishedQuestRepository.GetAllFinishedQuestsAsync();
            return Ok(result);
        }

        // GET: finishedquests/{userId}
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<FinishedQuest>> Get(string userId)
        {
            var filteredQuests = _finishedQuestRepository.GetUserFinishedQuestsAsync(userId);
            return Ok(filteredQuests);
        }

        // DELETE: finishedquests/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _finishedQuestRepository.RemoveFinishedQuestAsync(id);
            return Ok();
        }
    }
}