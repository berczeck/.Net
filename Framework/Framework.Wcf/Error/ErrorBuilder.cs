using System;
using System.Data.SqlClient;
using System.ServiceModel;
using Framework.Error;

namespace Framework.Wcf.Error
{
    internal static class ErrorBuilder
    {
        public static ErrorServicioRespuesta ConstruirError(Exception error)
        {
            var faultDetail = new ErrorServicioRespuesta {Codigo = "0000"};
            Type tipoError = error.GetType();

            if (tipoError == typeof (SqlException))
            {
                faultDetail.Mensaje = "Ocurrió un error al conectarse a la base de datos.";
                faultDetail.Tipo = TipoErrorServicio.ErrorBaseDatos;
            }
            else if (tipoError == typeof (ExcepcionActualizarRegistro))
            {
                faultDetail.Mensaje = error.Message;
                faultDetail.Tipo = TipoErrorServicio.ErrorActualizacionBaseDatos;
            }
            else if (tipoError == typeof (ExcepcionEliminarRegistro))
            {
                faultDetail.Mensaje = error.Message;
                faultDetail.Tipo = TipoErrorServicio.ErrorEliminacionBaseDatos;
            }
            else if (tipoError == typeof (TimeoutException))
            {
                faultDetail.Mensaje = "Ocurrió un error de tiempo de espera.";
                faultDetail.Tipo = TipoErrorServicio.ErrorTiempoConexion;
            }
            else if (tipoError == typeof (CommunicationException))
            {
                faultDetail.Mensaje = "Ocurrió un error al comunicarse con el servicio.";
                faultDetail.Tipo = TipoErrorServicio.ErrorComunicacion;
            }
            else if (tipoError == typeof (EndpointNotFoundException))
            {
                faultDetail.Mensaje = "No se encuentra el servicio especificado.";
                faultDetail.Tipo = TipoErrorServicio.ErrorComunicacion;
            }
            else if (tipoError == typeof (ExcepcionReglaNegocio))
            {
                faultDetail.Codigo = ((ExcepcionReglaNegocio) error).Codigo;
                faultDetail.Mensaje = error.Message;
                faultDetail.Tipo = TipoErrorServicio.ErrorNegocio;
            }
            else if (tipoError == typeof(ExcepcionValidacion))
            {
                faultDetail.Mensaje = error.Message;
                faultDetail.Tipo = TipoErrorServicio.ErrorValidacion;
            }
            else
            {
                faultDetail.Mensaje = "Ocurrió un error inesperado en el servicio.";
                faultDetail.Tipo = TipoErrorServicio.ErrorNoManejado;
            }

            return faultDetail;
        }

    }
}