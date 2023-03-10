using AuxiliarDelete.Infra.Enum;

namespace AuxiliarDelete.Infra.ExternalRequest.Interface
{
    public interface IExternalRequest
    {
        Task<string> GetToken();
        Task<string> Request(string json, string url, TipoRequest Method);
        Task<dynamic> RequestFile(string json, string url, TipoRequest Method);
    }
}
