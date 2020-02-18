using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeRegisterAPI.Business.Interfaces;
using TimeRegisterAPI.Domain;
using TimeRegisterAPI.Repository.Interfaces;

namespace TimeRegisterAPI.Business.Implementations
{
    public class TimeRecordBusiness : ITimeRecordBusiness
    {
        private readonly ITimeRecordRepository _timeRecordRepository;

        public TimeRecordBusiness(ITimeRecordRepository timeRecordRepository)
        {
            _timeRecordRepository = timeRecordRepository;
        }

        public async Task<IEnumerable<TimeRecord>> GetTimeRecords(DateTime initialDate, DateTime finalDate)
        {
            return await _timeRecordRepository.GetTimeRecords(initialDate, finalDate);
        }

        public async Task<TimeRecord> GetTimeRecordById(int id)
        {
            return await _timeRecordRepository.GetTimeRecordById(id);
        }

        public async Task<object> SaveTimeRecord(TimeRecord entity)
        {
            return await _timeRecordRepository.SaveTImeRecord(entity);
        }

        public async Task<object> UpdateTimeRecord(TimeRecord entity)
        {
            TimeRecord recordUpdate = await _timeRecordRepository.GetTimeRecordById(entity.Id);

            if (recordUpdate?.Id == 0)
            {
                throw new Exception("Time record not found");
            }

            recordUpdate.ActivityNumber = entity.ActivityNumber;
            recordUpdate.UsedTime = entity.UsedTime;
            recordUpdate.Comments = entity.Comments;

            return await _timeRecordRepository.UpdateTimeRecord(recordUpdate);
        }

        public async Task<object> UpdateTimeRecordState(int id)
        {
            TimeRecord timeRecordUpdateState = await _timeRecordRepository.GetTimeRecordById(id);

            if (timeRecordUpdateState?.Id == 0)
            {
                throw new Exception("Time record not exists");
            }

            return await _timeRecordRepository.UpdateTimeRecordState(id);
        }

        public async Task<object> DeleteTimeRecord(int id)
        {
            TimeRecord recordDelete = await _timeRecordRepository.GetTimeRecordById(id);

            if (recordDelete?.Id == 0)
            {
                throw new Exception("Time record not found");
            }

            return await _timeRecordRepository.DeleteTimeRecord(recordDelete.Id);
        }
    }
}
