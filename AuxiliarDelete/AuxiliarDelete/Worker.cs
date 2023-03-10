using AuxiliarDelete.Services;
using AuxiliarDelete.Services.Interfaces;

namespace AuxiliarDelete
{
    public class Worker : BackgroundService
    {
        private readonly IFornecedoresService _fornecedoresService;
        private ILogger<Worker>               _logger;
        private string                        _csvPath;

        public Worker(
            IFornecedoresService fornecedoresService,
            ILogger<Worker> logger,
            IConfiguration _conf
            )
        {
            _logger              = logger;
            _fornecedoresService = fornecedoresService;
            _csvPath             = _conf["csvPath"].ToString();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("====================================================");
                    _logger.LogInformation("=                                                  =");
                    _logger.LogInformation("=             Auxiliar de Exclusão Coupa           =");
                    _logger.LogInformation($"=             Iniciado as {DateTime.Now.ToShortTimeString()}    =");
                    _logger.LogInformation("====================================================");

                    await _fornecedoresService.DeleteFornecedoresByCSV(_csvPath);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.Message);
                }
                await Task.Delay(50000000, stoppingToken);
            }
        }
    }
}