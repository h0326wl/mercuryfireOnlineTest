using Microsoft.Data.SqlClient;

namespace WebApplication1.Models.Services
{
    public class MyofficeExecutionLogService
    {
        private readonly string _connectionString;

        public MyofficeExecutionLogService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<MyofficeExcuteionLog> GetAllLogs()
        {
            var logs = new List<MyofficeExcuteionLog>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Myoffice_ExcuteionLog", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new MyofficeExcuteionLog
                            {
                                DeLogAuthId = (int)reader["DeLog_AuthId"],
                                DeLogStoredPrograms = reader["DeLog_StoredPrograms"].ToString(),
                                DeLogGroupId = (Guid)reader["DeLog_GroupId"],
                                DeLogIsCustomDebug = (bool)reader["DeLog_isCustomDebug"],
                                DeLogExecutionProgram = reader["DeLog_ExecutionProgram"].ToString(),
                                DeLogExecuteionInfo = reader["DeLog_ExecuteionInfo"]?.ToString(),
                                DeLogVerifyNeeded = (bool)reader["DeLog_VerifyNeeded"]
                            });
                        }
                    }
                }
            }

            return logs;
        }

        public void AddLog(MyofficeExcuteionLog log)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Myoffice_ExcuteionLog (DeLog_StoredPrograms, DeLog_GroupID, DeLog_isCustomDebug, DeLog_ExecutionProgram, DeLog_ExecuteionInfo, DeLog_VerifyNeeded) VALUES (@StoredPrograms, @GroupID, @IsCustomDebug, @ExecutionProgram, @ExecuteionInfo, @VerifyNeeded)", connection))
                {
                    command.Parameters.Add(new SqlParameter("@StoredPrograms", log.DeLogStoredPrograms));
                    command.Parameters.Add(new SqlParameter("@GroupID", log.DeLogGroupId));
                    command.Parameters.Add(new SqlParameter("@IsCustomDebug", log.DeLogIsCustomDebug));
                    command.Parameters.Add(new SqlParameter("@ExecutionProgram", log.DeLogExecutionProgram));
                    command.Parameters.Add(new SqlParameter("@ExecuteionInfo", log.DeLogExecuteionInfo));
                    command.Parameters.Add(new SqlParameter("@VerifyNeeded", log.DeLogVerifyNeeded));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
