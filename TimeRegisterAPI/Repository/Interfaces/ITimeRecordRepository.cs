using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeRegisterAPI.Domain;

namespace TimeRegisterAPI.Repository.Interfaces
{
    public interface ITimeRecordRepository
    {
        Task<IEnumerable<TimeRecord>> GetTimeRecords(DateTime initDate, DateTime finalDate);
        Task<TimeRecord> GetTimeRecordById(int id);
        Task<object> SaveTImeRecord(TimeRecord entity);
        Task<object> UpdateTimeRecord(TimeRecord entity);
        Task<object> UpdateTimeRecordState(int id);
        Task<object> DeleteTimeRecord(int id);
    }
}
