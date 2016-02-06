using System;

namespace DemoRefactoring
{
    /*
        Code Review
        Please review the following code snippet. Assume that all referenced assemblies have
        been properly included.
        The code is used to log different messages throughout an application. We want the
        ability to be able to log to a text file, the console and/or the database. Messages can be
        marked as message, warning or error. We also want the ability to selectively be able to
        choose what gets logged, such as to be able to log only errors or only errors and
        warnings.
        1) If you were to review the following code, what feedback would you give? Please
        be specific and indicate any errors that would occur as well as other best practices
        and code refactoring that should be done.
        2) Rewrite the code based on the feedback you provided in question 1. Please
        include unit tests on your code. 
    */


    public class JobLogger
    {
        // TODO: Convert these variables to non static 
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;

        // TODO: Rename this variable, it should starst with prefix: _ , example: _logToDatabase
        private static bool LogToDatabase;

        // TODO: Remove this unused variable
        private bool _initialized;

        // TODO: Import namespaces: System.Configuration and System.Data.SqlClient then remove the references in code
        // TODO: Check the image CodeMetricsJobLogger.png
        /*
            Analysis method LogMesage
            Cyclomatic Complexity: 35, it's too high an acceptable value is 10.
            It's necesary write 35 unit tests to code coverage this methdo
            Class Couppling: 8, 
            Lines of code (loc): 41, it's recommended less than 30 loc
        */

        // TODO: Encapsulate this variables in a class, constructor signature too long
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            LogToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        // INFO: This method doesn't follow the single responsibility principle, it's too complex and difficult to mantain
        // TODO: Split this method in three more: Log Error, Log Warning and Log Message.
        // TODO: Split this method in three classes one for each responsibility: Console, text and Database
        // TODO: The two first parameters have tha same name "message"
        // TODO: Encapsulate this variables in a class, method signature too long
        public static void LogMessage(string message, bool isMessage, bool isWarning, bool isError)
        {
            // TODO: Remove this loc his value it isn't used, possible nullreferenceexception
            message.Trim();

            if (message == null || message.Length == 0)
            {
                // TODO: In this case return an argumentexception
                // INFO: possible bug in the system when send to register nothing
                return;
            }

            // TODO: Move this validation inside the constructor
            if (!_logToConsole && !_logToFile && !LogToDatabase)
            {
                throw new Exception("Invalid configuration");
            }

            // TODO: Remove this part: "(!_logError && !_logMessage && !_logWarning) ||" it's duplicate
            if ((!_logError && !_logMessage && !_logWarning) || (!isMessage && !isWarning && !isError))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            // TODO: Use the sentence "using" to release SqlConnection's resources correctly
            // TODO: Get the connection strings from ConfigurationManager.ConnectionStrings 
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);

            // INFO: Do not open the connection too early
            connection.Open();

            // TODO: Rename tha variable, what does it mean? what is its purpose?
            int t = 0;
            
            // TODO: Validation duplicate
            if (isMessage && _logMessage)
            {
                t = 1;
            }
            if (isError && _logError)
            {
                t = 2;
            }
            if (isWarning && _logWarning)
            {
                t = 3;
            }

            // TODO: Use a Stored procedure o parametrized query, possile SQL injection
            // TODO: Use the sentence "using" to release SqlCommand's resources correctly
            // TODO: Validate what kind of log to use
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " + t.ToString() + ")");
            command.ExecuteNonQuery();

            //TODO: Close the connection

            // TODO: Rename tha variable, what does it mean? what is its purpose?
            string l = string.Empty;
            
            // TODO: Set the path file in a variable, Code duplicated
            // FIX: The file name generated is incorrect, should use the function DateTime.Now.ToString("")
            if (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
            {
                l = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
            }
                        
            // TODO: Validation duplicate
            if (isError && _logError)
            {
                // TODO: Use the function string.format or StringBuilder
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (isWarning && _logWarning)
            {
                // TODO: Use the function string.format or StringBuilder
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (isMessage && _logMessage)
            {
                // TODO: Use the function string.format or StringBuilder
                l = l + DateTime.Now.ToShortDateString() + message;
            }

            // INFO: Line is Too large
            // TODO: Set the path file in a variable, Code duplicated
            // TODO: Use the function AppendAllText instead of ReadAllText, to avoid read every time all the lines of the file
            // TODO: Indicate what kind of log is used(Warning, Error, Message)
            System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);
                        
            // TODO: Validation duplicate
            if (isError && _logError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (isWarning && _logWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (isMessage && _logMessage)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            // TODO: Use the function string.format
            // TODO: Indicate what kind of log is used(Warning, Error, Message)
            Console.WriteLine(DateTime.Now.ToShortDateString() + message);
        }
    }

}
