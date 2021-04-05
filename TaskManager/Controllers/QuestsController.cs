using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class QuestsController : ControllerBase
    {
        private readonly IQuestRepository _questRepository;

        public QuestsController(IQuestRepository questRepository)
        {
            _questRepository = questRepository;
        }

        // GET: quests/
        [HttpGet]
        public ActionResult<IEnumerable<Quest>> Get()
        {
            var allQuests = _questRepository.GetAllQuests();
            return Ok(allQuests);
        }

        // GET quests/{id}
        [HttpGet("{id}")]
        public ActionResult<Quest> Get(int id)
        {
            return Ok(_questRepository.GetQuest(id));
        }

        // POST quests/
        [HttpPost]
        public ActionResult Post([FromBody] Quest quest)
        {
            _questRepository.CreateQuest(quest);
            return Ok();
        }

        // PUT quests/{id}
        [Route("{id}")]
        public void Put(int id, [FromBody] Quest updateQuest)
        {
            _questRepository.UpdateQuest(id, updateQuest);
        }

        //PUT quests/inprogress/{id}
        [Route("inprogress/{id}")]
        public ActionResult<Quest> Put(int id)
        {
            _questRepository.RevertQuestInProgressFlag(id);
            return Ok(_questRepository.GetQuest(id));
        }

        // DELETE quests/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _questRepository.RemoveQuest(id);
            return Ok();
        }
    }
}