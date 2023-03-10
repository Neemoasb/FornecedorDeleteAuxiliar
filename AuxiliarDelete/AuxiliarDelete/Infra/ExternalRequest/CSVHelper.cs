using AuxiliarDelete.Infra.ExternalRequest.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuxiliarDelete.Infra.ExternalRequest
{
    public class CSVHelper : ICSVHelper
    {

        public async Task<List<string>> readCSVasStringList(string csvPath)
        {
            csvPath = csvPath.Replace("\\", "/".Replace("\"",""));
            // ler o arquivo CSV
            string[] lines = File.ReadAllLines(csvPath);
            // criar lista para armazenar as strings
            List<string> listaCSV = new List<string>();

            // percorrer as linhas do arquivo
            foreach (string line in lines)
            {
                // separar os campos delimitados por vírgulas
                string[] fields = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                // adicionar os campos à lista
                if (fields[0].Contains("Id"))
                    continue;

                listaCSV.AddRange(fields);
            }

            return listaCSV;
        }
    }
}
