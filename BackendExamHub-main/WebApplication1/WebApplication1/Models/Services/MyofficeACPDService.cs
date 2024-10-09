using Microsoft.Data.SqlClient;

namespace WebApplication1.Models.Services
{
    public class MyofficeACPDService
    {
        private readonly string _connectionString;

        public MyofficeACPDService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<MyofficeAcpd> GetAllUsers()
        {
            var users = new List<MyofficeAcpd>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM MyofficeAcpd", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new MyofficeAcpd
                            {
                                AcpdSid = reader["acpd_sid"].ToString(),
                                AcpdCname = reader["acpd_cname"].ToString(),
                                AcpdEname = reader["acpd_ename"].ToString(),
                                AcpdSname = reader["acpd_sname"].ToString(),
                                AcpdEmail = reader["acpd_email"].ToString(),
                                AcpdStatus = (byte)reader["acpd_status"],
                                AcpdStop = (bool)reader["acpd_stop"],
                                AcpdStopMemo = reader["acpd_stopMemo"]?.ToString(),
                                AcpdLoginId = reader["acpd_LoginID"].ToString(),
                                AcpdLoginPw = reader["acpd_LoginPW"].ToString(),
                                AcpdMemo = reader["acpd_memo"]?.ToString(),
                                AcpdNowdatetime = (DateTime)reader["acpd_nowdatetime"],
                                AppdNowid = reader["appd_nowid"].ToString(),
                                AcpdUpddatetitme = (DateTime)reader["acpd_upddatetitme"],
                                AcpdUpdid = reader["acpd_updid"].ToString()
                            });
                        }
                    }
                }
            }

            return users;
        }

        public void AddUser(MyofficeAcpd user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO MyofficeAcpd (acpd_cname, acpd_ename, acpd_sname, acpd_email, acpd_status, acpd_stop, acpd_stopMemo, acpd_LoginID, acpd_LoginPW, acpd_memo, acpd_nowdatetime, appd_nowid) VALUES (@Cname, @Ename, @Sname, @Email, @Status, @Stop, @StopMemo, @LoginID, @LoginPW, @Memo, @NowDatetime, @NowID)", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Cname", user.AcpdCname));
                    command.Parameters.Add(new SqlParameter("@Ename", user.AcpdEname));
                    command.Parameters.Add(new SqlParameter("@Sname", user.AcpdSname));
                    command.Parameters.Add(new SqlParameter("@Email", user.AcpdEmail));
                    command.Parameters.Add(new SqlParameter("@Status", user.AcpdStatus));
                    command.Parameters.Add(new SqlParameter("@Stop", user.AcpdStop));
                    command.Parameters.Add(new SqlParameter("@StopMemo", user.AcpdStopMemo));
                    command.Parameters.Add(new SqlParameter("@LoginID", user.AcpdLoginId));
                    command.Parameters.Add(new SqlParameter("@LoginPW", user.AcpdLoginPw));
                    command.Parameters.Add(new SqlParameter("@Memo", user.AcpdMemo));
                    command.Parameters.Add(new SqlParameter("@NowDatetime", DateTime.Now));
                    command.Parameters.Add(new SqlParameter("@NowID", user.AppdNowid));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(MyofficeAcpd user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE MyofficeAcpd SET acpd_cname = @Cname, acpd_ename = @Ename, acpd_sname = @Sname, acpd_email = @Email, acpd_status = @Status, acpd_stop = @Stop, acpd_stopMemo = @StopMemo, acpd_LoginID = @LoginID, acpd_LoginPW = @LoginPW, acpd_memo = @Memo, acpd_upddatetitme = @Upddate, acpd_updid = @UpdID WHERE acpd_sid = @Sid", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Sid", user.AcpdSid));
                    command.Parameters.Add(new SqlParameter("@Cname", user.AcpdCname));
                    command.Parameters.Add(new SqlParameter("@Ename", user.AcpdEname));
                    command.Parameters.Add(new SqlParameter("@Sname", user.AcpdSname));
                    command.Parameters.Add(new SqlParameter("@Email", user.AcpdEmail));
                    command.Parameters.Add(new SqlParameter("@Status", user.AcpdStatus));
                    command.Parameters.Add(new SqlParameter("@Stop", user.AcpdStop));
                    command.Parameters.Add(new SqlParameter("@StopMemo", user.AcpdStopMemo));
                    command.Parameters.Add(new SqlParameter("@LoginID", user.AcpdLoginId));
                    command.Parameters.Add(new SqlParameter("@LoginPW", user.AcpdLoginPw));
                    command.Parameters.Add(new SqlParameter("@Memo", user.AcpdMemo));
                    command.Parameters.Add(new SqlParameter("@Upddate", DateTime.Now));
                    command.Parameters.Add(new SqlParameter("@UpdID", user.AcpdUpdid));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Myoffice_ACPD WHERE acpd_sid = @Sid", connection))
                {
                    command.Parameters.Add(new SqlParameter("@Sid", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
