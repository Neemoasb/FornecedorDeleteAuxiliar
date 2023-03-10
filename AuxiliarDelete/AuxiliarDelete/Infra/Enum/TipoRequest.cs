using System.ComponentModel;

namespace AuxiliarDelete.Infra.Enum
{
    public enum TipoRequest
    {
        [Description("PUT")]
        PUT,

        [Description("GET")]
        GET,

        [Description("POST")]
        POST,

        [Description("PATCH")]
        PATCH,

        [Description("DELETE")]
        DELETE
    }
}
