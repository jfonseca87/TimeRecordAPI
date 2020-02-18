using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeRegisterAPI.Business.Interfaces;
using TimeRegisterAPI.Domain;

namespace TimeRegisterAPI.Controllers
{
    [ApiController]
    [Route("api/TimeRecord")]
    public class TimeRecordController : ControllerBase
    {
        private readonly ITimeRecordBusiness _timeRecordBusiness;

        public TimeRecordController(ITimeRecordBusiness timeRecordBusiness)
        {
            _timeRecordBusiness = timeRecordBusiness;
        }

        [HttpGet("{initialDate}/{finalDate}")]
        public async Task<IActionResult> GetTimeRecords(DateTime initialDate, DateTime finalDate)
        {
            try
            {
                return Ok(await _timeRecordBusiness.GetTimeRecords(initialDate, finalDate));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeRecordById(int id)
        {
            try
            {
                return Ok(await _timeRecordBusiness.GetTimeRecordById(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveTimeRecord(TimeRecord entity)
        {
            try
            {
                var response = await _timeRecordBusiness.SaveTimeRecord(entity);
                entity.Id = Convert.ToInt32(response);
                return Created("", entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTimeRecord(TimeRecord entity)
        {
            try
            {
                var response = await _timeRecordBusiness.UpdateTimeRecord(entity);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> UpdateTimeRecordState(TimeRecord entity)
        {
            try
            {                
                var response = await _timeRecordBusiness.UpdateTimeRecordState(entity.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeRecord(int id)
        {
            try
            {
                var response = await _timeRecordBusiness.DeleteTimeRecord(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
