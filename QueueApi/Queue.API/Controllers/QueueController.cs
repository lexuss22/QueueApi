using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queue.BLL.Services.Interfaces;
using Queue.DTO.Models;

namespace Queue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueueServices _queue;

        public QueueController(IQueueServices queue)
        {
            _queue = queue;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetGroupsByParams(string city, string startTime, string finishTime)
        {
            var result = await _queue.GetGroupsByParams(new SearchGroupsRequest
            {
                City = city,
                StartTime = startTime,
                FinishTime = finishTime
            });
            
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> IsUnelectrorizedByGroup(string groupName)
        {
            var result = await _queue.IsUnelectrorizedByGroup(groupName);

            return Ok(result);
        }

        [HttpPut]
        [Route("[action]/Schedule/{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleUpdateRequest request)
        {
            var updatedSchedule = await _queue.UpdateSchedule(id, request);

            return Ok(new UpdateResponse<ScheduleDTO>
            {
                Message = "Schedule updated successfully",
                Data = updatedSchedule
            });
        }

        [HttpPut]
        [Route("[action]/Group/{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupUpdateRequest request)
        {
            var updatedGroup = await _queue.UpdateGroup(id, request);
            return Ok(new UpdateResponse<GroupDTO>
            {
                Message = "Group updated successfully",
                Data = updatedGroup
            });
        }
    }
} 
