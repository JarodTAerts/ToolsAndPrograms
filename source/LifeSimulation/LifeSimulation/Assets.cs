using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation
{
    static class Assets
    {
        public static Dictionary<string, Substance> allSubstances;

        private const string substancesFile = "substances.txt";

        public static void GetAllSubstancesFromFile()
        {
            allSubstances=TextParsers.ParseSubstances(substancesFile);
        }
    }
}
