using System.Data.SqlClient;

namespace WcfServiceNlog.Info
{
    public class HandlerSqlErrorInformation : IHandlerErrorInformation<SqlException>
    {
        #region Miembros de IManejoError<SqlException>

        public void AddExceptionInformation(MessageFormat messageFormat, SqlException excepcion)
        {
            int contEx = 1;

            messageFormat.AddExtraInformation("SqlException.Server", excepcion.Server);
            messageFormat.AddExtraInformation("SqlException.Procedure", excepcion.Procedure);
            messageFormat.AddExtraInformation("SqlException.LineNumber", excepcion.LineNumber.ToString());
            messageFormat.AddExtraInformation("SqlException.Message", excepcion.Message);
            messageFormat.AddExtraInformation("SqlException.Number", excepcion.Number.ToString());
            messageFormat.AddExtraInformation("SqlException.Class", excepcion.Class.ToString());
            messageFormat.AddExtraInformation("SqlException.State", excepcion.State.ToString());
            messageFormat.AddExtraInformation("SqlException.ErrorCode", excepcion.ErrorCode.ToString());

            foreach (SqlError errorSql in excepcion.Errors)
            {
                string tituloSqlError = string.Format("SqlException.SqlError{0}", contEx);
                messageFormat.AddExtraInformation(tituloSqlError + ".Server", errorSql.Server);
                messageFormat.AddExtraInformation(tituloSqlError + ".Procedure", errorSql.Procedure);
                messageFormat.AddExtraInformation(tituloSqlError + ".LineNumber", errorSql.LineNumber.ToString());
                messageFormat.AddExtraInformation(tituloSqlError + ".Message", errorSql.Message);
                messageFormat.AddExtraInformation(tituloSqlError + ".Number", errorSql.Number.ToString());
                messageFormat.AddExtraInformation(tituloSqlError + ".Class", errorSql.Class.ToString());
                messageFormat.AddExtraInformation(tituloSqlError + ".State", errorSql.State.ToString());
                contEx++;
            }
        }

        #endregion

    }
}
