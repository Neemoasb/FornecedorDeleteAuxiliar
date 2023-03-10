using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarDelete.Infra.ExternalRequest.Interface
{
    public interface ICSVHelper
    {
        Task<List<string>> readCSVasStringList(string csvPath);
    }
}
