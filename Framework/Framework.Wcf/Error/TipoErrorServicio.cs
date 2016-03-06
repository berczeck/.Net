using System;
using System.Runtime.Serialization;

namespace Framework.Wcf.Error
{
    [Serializable]
    [DataContract]
    public enum TipoErrorServicio
    {
        [EnumMember] ErrorNoManejado,
        [EnumMember] ErrorBaseDatos,
        [EnumMember] ErrorActualizacionBaseDatos,
        [EnumMember] ErrorEliminacionBaseDatos,
        [EnumMember] ErrorTiempoConexion,
        [EnumMember] ErrorNegocio,
        [EnumMember] ErrorComunicacion,
        [EnumMember] ErrorValidacion,
    }
}