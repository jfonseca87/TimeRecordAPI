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
        public IActionResult SaveTimeRecord(TimeRecord entity)
        {
            try
            {
                _timeRecordBusiness.SaveTimeRecord(entity);
                return Created("", entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpPut]
        public IActionResult UpdateTimeRecord(TimeRecord entity)
        {
            try
            {
                _timeRecordBusiness.UpdateTimeRecord(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
