using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeRegisterAPI.Domain;

namespace TimeRegisterAPI.Business.Interfaces
{
    public interface ITimeRecordBusiness
    {
        Task<IEnumerable<TimeRecord>> GetTimeRecords(DateTime initialDate, DateTime finalDate);
        Task<TimeRecord> GetTimeRecordById(int id);
        Task<object> SaveTimeRecord(TimeRecord entity);
        Task<object> UpdateTimeRecord(TimeRecord entity);
    }
}
