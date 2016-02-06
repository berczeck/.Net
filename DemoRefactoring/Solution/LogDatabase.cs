using static System.Configuration.ConfigurationManager;
using System.Data.SqlClient;
using System.Data;
using static DemoRefactoring.Validator;

namespace DemoRefactoring
{
    public class LogDatabase : ILog
    {
        private readonly string connectionString;

        public LogDatabase() :
            this(ConnectionStrings["ConnectionString"].ConnectionString)
        {
        }

        public LogDatabase(string connectionString)
        {
            InputText(connectionString);
            this.connectionString = connectionString;
        }

        public Message LastMessage { get; private set; }
        public MessageType LastMessageType { get; private set; }

        public void LogError(Message message) => Register(message, MessageType.Error);

        public void LogMessage(Message message) => Register(message, MessageType.Info);

        public void LogWarning(Message message) => Register(message, MessageType.Warning);

        private void Register(Message message, MessageType type)
        {
            ObjectParam(message);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("usp_add_log", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@textLog", message.ToString()));
                    command.Parameters.Add(new SqlParameter("@typeLog", (int)type));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            LastMessage = message;
            LastMessageType = type;
        }
    }
}
