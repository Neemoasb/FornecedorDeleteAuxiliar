using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarDelete.Services.Interfaces
{
    public interface IFornecedoresService
    {
        Task DeleteFornecedoresByCSV(string csvPath);
        Task DeleteAllFornecedores();
    }
}
