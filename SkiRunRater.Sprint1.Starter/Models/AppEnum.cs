using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class AppEnum
    {
        public enum ManagerAction
        {
            None,
            ListAllSkiRuns,
            DisplaySkiRunInformation,
            DeleteSkiRun,
            AddSkiRun,
            UpdateSkiRun,
            QuerySkiRunsByVertical,
            Quit
        }
    }
}
