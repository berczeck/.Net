using System;
using System.Data.SqlClient;

namespace Framework.Error
{
    public class HandlerSqlErrorInformation : IHandlerErrorInformation
    {
        #region Miembros de IManejoError<SqlException>

        public virtual void AddExceptionInformation(MessageFormat messageFormat, Exception excepcion)
        {
            var sqlexcepcion = excepcion as SqlException;

            int contEx = 1;

            messageFormat.AddExtraInformation("SqlException.Server", sqlexcepcion.Server);
            messageFormat.AddExtraInformation("SqlException.Procedure", sqlexcepcion.Procedure);
            messageFormat.AddExtraInformation("SqlException.LineNumber", sqlexcepcion.LineNumber.ToString());
            messageFormat.AddExtraInformation("SqlException.Message", sqlexcepcion.Message);
            messageFormat.AddExtraInformation("SqlException.Number", sqlexcepcion.Number.ToString());
            messageFormat.AddExtraInformation("SqlException.Class", sqlexcepcion.Class.ToString());
            messageFormat.AddExtraInformation("SqlException.State", sqlexcepcion.State.ToString());
            messageFormat.AddExtraInformation("SqlException.ErrorCode", sqlexcepcion.ErrorCode.ToString());

            foreach (SqlError errorSql in sqlexcepcion.Errors)
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
