using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TimeRegisterAPI.Common;
using TimeRegisterAPI.Domain;
using TimeRegisterAPI.Repository.Interfaces;

namespace TimeRegisterAPI.Repository.SQLImplementations
{
    public class TimeRecordSQL : ITimeRecordRepository
    {
        private readonly DatabaseOptions _options;

        public TimeRecordSQL(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        public async Task<IEnumerable<TimeRecord>> GetTimeRecords(DateTime initialDate, DateTime finalDate)
        {
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("GetTimeRecords", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@InitialDate", initialDate));
                    cmd.Parameters.Add(new SqlParameter("@FinalDate", finalDate));

                    using (SqlDataReader dt = await cmd.ExecuteReaderAsync())
                    {
                        List<TimeRecord> timeRecords = new List<TimeRecord>();

                        while (dt.Read())
                        {
                            timeRecords.Add(new TimeRecord
                            {
                                Id = Convert.ToInt32(dt["Id"]),
                                ActivityNumber = Convert.ToInt32(dt["ActivityNumber"]),
                                UsedTime = Convert.ToDecimal(dt["UsedTime"]),
                                Comments = dt["Comments"].ToString(),
                                DateRecord = Convert.ToDateTime(dt["DateRecord"]),
                                State = Convert.ToBoolean(dt["State"])
                            });
                        }

                        return timeRecords;
                    }
                }
            }
        }

        public async Task<TimeRecord> GetTimeRecordById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("GetTimeRecordById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdTimeRecord", id));

                    using (SqlDataReader dt = await cmd.ExecuteReaderAsync())
                    {
                        TimeRecord timeRecord = new TimeRecord();

                        while (dt.Read())
                        {
                            timeRecord = new TimeRecord
                            {
                                Id = Convert.ToInt32(dt["Id"]),
                                ActivityNumber = Convert.ToInt32(dt["ActivityNumber"]),
                                UsedTime = Convert.ToDecimal(dt["UsedTime"]),
                                Comments = dt["Comments"].ToString(),
                                DateRecord = Convert.ToDateTime(dt["DateRecord"]),
                            };
                        }

                        return timeRecord;
                    }
                }
            }
        }

        public async Task<object> SaveTImeRecord(TimeRecord entity)
        {
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SaveTimeRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ActivityNumber", entity.ActivityNumber));
                    cmd.Parameters.Add(new SqlParameter("@UsedTime", entity.UsedTime));
                    cmd.Parameters.Add(new SqlParameter("@Comments", entity.Comments));
                    cmd.Parameters.Add(new SqlParameter("@DateRecord", entity.DateRecord));

                    var response = await cmd.ExecuteScalarAsync();

                    return response;
                }
            }
        }

        public async Task<object> UpdateTimeRecord(TimeRecord entity)
        {
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("UpdateTimeRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdTimeRecord", entity.Id));
                    cmd.Parameters.Add(new SqlParameter("@ActivityNumber", entity.ActivityNumber));
                    cmd.Parameters.Add(new SqlParameter("@UsedTime", entity.UsedTime));
                    cmd.Parameters.Add(new SqlParameter("@Comments", entity.Comments));

                    var response = await cmd.ExecuteScalarAsync();

                    return response;
                }
            }
        }

        public async Task<object> UpdateTimeRecordState(int id)
        {
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateStateTimeRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdTimeRecord", id));

                    await conn.OpenAsync();
                    var response = await cmd.ExecuteScalarAsync();

                    return response;
                }
            }
        }

        public async Task<object> DeleteTimeRecord(int id)
        {
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteTimeRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdTimeRecord", id));

                    await conn.OpenAsync();
                    var response = await cmd.ExecuteScalarAsync();

                    return response;
                }
            }
        }
    }
}
