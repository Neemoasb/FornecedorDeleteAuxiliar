using AuxiliarDelete.Infra.Enum;
using AuxiliarDelete.Infra.ExternalRequest.Interface;
using AuxiliarDelete.Services.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System.Dynamic;

namespace AuxiliarDelete.Services
{
    public class FornecedoresService : IFornecedoresService
    {
        private readonly IExternalRequest _externalRequest;
        private readonly ICSVHelper _cSVHelper;
        private string urlDelete;

        public FornecedoresService(IExternalRequest externalRequest,
            ICSVHelper cSVHelper,
            IConfiguration _conf)
        {
            _externalRequest = externalRequest;
            _cSVHelper = cSVHelper;
            urlDelete = _conf["urlDelete"].ToString();

        }


        public async Task DeleteAllFornecedores()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFornecedoresByCSV(string csvPath)
        {
            List<string> IdsFornecedoresASeremExcluidos = new();
            IdsFornecedoresASeremExcluidos = await _cSVHelper.readCSVasStringList(csvPath);

            foreach (var idFornecedor in IdsFornecedoresASeremExcluidos)
            {

                await _externalRequest.Request(string.Empty, $"{urlDelete}{idFornecedor}", TipoRequest.DELETE);

                Log.Information($"{idFornecedor} Excluído com sucesso");
            }
        }
    }
}
