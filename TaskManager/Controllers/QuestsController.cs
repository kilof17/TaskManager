using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.DTOs;
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
        private readonly IMapper _mapper;

        public QuestsController(IQuestRepository questRepository,
                                IMapper mapper)
        {
            _questRepository = questRepository;
            _mapper = mapper;
        }

        #region Get all tasks

        // GET: quests/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Return tasks", Description = " Require Admin role")]
        public async Task<ActionResult<IEnumerable<DisplayQuest>>> Get()
        {
            var result = await _questRepository.GetAllQuestsAsync();
            var allQuests = _mapper.Map<IEnumerable<DisplayQuest>>(result);
            return Ok(allQuests);
        }

        #endregion Get all tasks

        #region Get task by id

        // GET quests/{id}
        [HttpGet("{id}", Name = "GetQuestById")]
        [SwaggerOperation(Summary = "Gets specified task", Description = "Gets specified task by id ")]
        [SwaggerResponse(200, "Resource found")]
        [SwaggerResponse(400, "Wrong id")]
        public async Task<ActionResult<DisplayQuest>> GetQuestById(string id)
        {
            var result = await _questRepository.GetQuestByIdAsync(id);
            var quest = _mapper.Map<DisplayQuest>(result);
            if (quest != null)
                return Ok(quest);

            return NotFound();
        }

        #endregion Get task by id

        #region Create task

        // POST quests/
        [HttpPost]
        [SwaggerOperation(Summary = "Create task", Description = "When task expiry time and date are not set, task is marked as peristent ")]
        [SwaggerResponse(201, "Task created")]
        [SwaggerResponse(400, "Wrong data")]
        public async Task<IActionResult> Post([FromBody] CreateQuest createQuest)
        {
            if (createQuest.Expiry_ISO8601 < DateTime.Now && createQuest.Expiry_ISO8601 != default(DateTime))
                return BadRequest(new ApiResponse
                {
                    Message = "Expiration date is older than added date",
                    IsSuccess = false
                });

            var quest = _mapper.Map<Quest>(createQuest);

            _questRepository.CreateQuestAsync(quest);
            await _questRepository.SaveChangesAsync();

            var displayQuest = _mapper.Map<DisplayQuest>(quest);

            return CreatedAtRoute(nameof(GetQuestById), new { id = displayQuest.Id }, displayQuest);
        }

        #endregion Create task

        #region Update task

        // PUT quests/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("Update task with specified id")]
        [SwaggerResponse(204, "Updated succesfully")]
        [SwaggerResponse(404, "Resource not found")]
        public async Task<ActionResult> UpdateQuest(string id, [FromBody] CreateQuest updateQuest)
        {
            var questFromRepo = await _questRepository.GetQuestByIdAsync(id);
            if (questFromRepo == null)
                return NotFound();

            _mapper.Map(updateQuest, questFromRepo);
            await _questRepository.SaveChangesAsync();

            return NoContent();
        }

        #endregion Update task

        #region Mark task as in progress

        //PUT quests/inprogress/{id}
        [HttpPut("InProgress/{id}")]
        [SwaggerOperation("Mark task as in progress")]
        [SwaggerResponse(200, "Task marked as in progress")]
        [SwaggerResponse(400, "User not found")]
        public async Task<ActionResult<Quest>> QuestInProgress(string id)
        {
            var result = await _questRepository.MarkQuestInProgressAsync(id);

            if (result.IsSuccess)
            {
                await _questRepository.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest(result);
        }

        #endregion Mark task as in progress

        #region Task is not in progress

        //PUT quests/notprogress/{id}
        [HttpPut("NotProgress/{id}")]
        [SwaggerOperation("Mark specified task as is not in progress")]
        [SwaggerResponse(200, "Task marked as is not in progress")]
        [SwaggerResponse(400, "User not found")]
        public async Task<ActionResult<Quest>> QuestInNotProgress(string id)
        {
            var result = await _questRepository.UnmarkQuestInProgressAsync(id);

            if (result.IsSuccess)
            {
                await _questRepository.SaveChangesAsync();
                return Ok(result);
            }

            return BadRequest(result);
        }

        #endregion Task is not in progress

        #region Mark specified task as finished

        //PUT quests/markfinished/{id}
        [HttpPut("MarkFinished/{id}")]  //TODO: Add ScoredPoints for user which finished quest. Group lider shoult approve finsh quest?
        [SwaggerOperation("Mark specified task as finished")]
        public async Task<ActionResult<Quest>> FinishedQuest(string id)
        {
            var result = await _questRepository.MarkQuestAsFinishedAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound(result);
        }

        #endregion Mark specified task as finished

        #region Back finished task to active quests

        //PUT quests/unmarkfinished/{id}
        [HttpPut("UnmarkFinished/{id}")] //TODO: rollback form FinishedQuest set new Expiry_iso8601, substract poinst from user which (not)finisched quest
        [SwaggerOperation("Mark specified task as not finished yet")]
        public async Task<ActionResult<Quest>> UnfinishedQuest(string id)
        {
            var result = await _questRepository.UnmarkQuestFinishedAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return NotFound(result);
        }

        #endregion Back finished task to active quests

        #region Delete task

        // DELETE quests/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("Delete task")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _questRepository.RemoveQuestAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #endregion Delete task
    }
}