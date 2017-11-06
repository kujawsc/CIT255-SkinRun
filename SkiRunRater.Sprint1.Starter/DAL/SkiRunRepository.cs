using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    /// <summary>
    /// method to write all ski run information to the date file
    /// </summary>
    public class SkiRunRepository : IDisposable
    {
        private List<SkiRun> _skiRuns;

        public SkiRunRepository()
        {
            _skiRuns = ReadSkiRunsData(DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to read all ski run information from the data file and return it as a list of SkiRun objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of SkiRun objects</returns>
        public static List<SkiRun> ReadSkiRunsData(string dataFilePath)
        {
            const char delineator = ',';

            // create lists to hold the ski run strings and objects
            List<string> skiRunStringList = new List<string>();
            List<SkiRun> skiRunClassList = new List<SkiRun>();

            // initialize a StreamReader object for reading
            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    skiRunStringList.Add(sReader.ReadLine());
                }
            }
            
            foreach (string skiRun in skiRunStringList)
            {
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = skiRun.Split(delineator);

                // populate the ski run list with SkiRun objects
                skiRunClassList.Add(new SkiRun() { ID = Convert.ToInt32(properties[0]), Name = properties[1], Vertical = Convert.ToInt32(properties[2]) });
            }

            return skiRunClassList;
        }

        /// <summary>
        /// method to write all of the list of ski runs to the text file
        /// </summary>
        public void WriteSkiRunsData()
        {
            string skiRunString;

            // wrap the FileStream object in a StreamWriter object to simplify writing strings
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath, false);

            using (sWriter)
            {
                foreach (SkiRun skiRun in _skiRuns)
                {
                    skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

                    sWriter.WriteLine(skiRunString);
                }
            }
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void InsertSkiRun(SkiRun skiRun)
        {
            _skiRuns.Add(skiRun);
                        
            WriteSkiRunsData();
        }

        /// <summary>
        /// Ski Run Index
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int GetSkinRunIndex(int ID)
        {
            int skiRunIndex = 0;

            for (int index = 0; index < _skiRuns.Count(); index++)
            {
                if (_skiRuns[index].ID == ID)
                {
                    skiRunIndex = index;
                }
            }
            return skiRunIndex;
        }

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteSkiRun(int ID)
        {
            _skiRuns.RemoveAt(GetSkinRunIndex(ID));

            WriteSkiRunsData();
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void UpdateSkiRun(SkiRun skiRun)
        {
            if (skiRun!=null)
            {
                foreach (SkiRun run in _skiRuns)
                {
                    if (run.ID==skiRun.ID)
                    {
                        run.Name = skiRun.Name;
                        run.Vertical = skiRun.Vertical;
                        break;
                    }
                }
                WriteSkiRunsData();
            }
        }
        /// <summary>
        /// method to return a ski run object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun GetSkiRunByID(int ID)
        {
            SkiRun skiRun = null;

            skiRun = _skiRuns[GetSkinRunIndex(ID)];

            return skiRun;
        }

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> GetSkiAllRuns()
        {
            return _skiRuns;
        }

        /// <summary>
        /// method to query the data by the vertical of each ski run in feet
        /// </summary>
        /// <param name="minimumVertical">int minimum vertical</param>
        /// <param name="maximumVertical">int maximum vertical</param>
        /// <returns></returns>
        public List<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            List<SkiRun> matchingSkiRuns = new List<SkiRun>();

            matchingSkiRuns = _skiRuns.Where(sr => sr.Vertical >= minimumVertical && sr.Vertical <= maximumVertical).ToList();

            return matchingSkiRuns;
        }

        /// <summary>
        /// Minimum Vetical 
        /// </summary>
        /// <returns></returns>
        public int GetMinimumVertical()
        {
            int minimumVertical = 0;

            Console.WriteLine(Environment.NewLine + "Please enter a minimum vertical you wish to query by:");
            while (!int.TryParse(Console.ReadLine(), out minimumVertical))
            {
                ConsoleView.DisplayPromptMessage("Sorry, but the Vertical needs to be a valid number. Please try again.");
                Console.WriteLine();
            };

            return minimumVertical;
        }

        /// <summary>
        /// Max Verical option 
        /// </summary>
        /// <returns></returns>
        public int GetMaximumVertical()
        {
            int maximumVertical = 0;

            Console.WriteLine("Please enter a maximum vertical you wish to query by:");
            while (!int.TryParse(Console.ReadLine(), out maximumVertical))
            {
                ConsoleView.DisplayPromptMessage("Sorry, but the Vertical needs to be a valid number. Please try again.");
                Console.WriteLine();
            };

            return maximumVertical;
        }

        /// <summary>
        /// This show the Queried Vertical
        /// </summary>
        /// <param name="matchingSkiRuns"></param>
        public void DisplayQueriedVertical(List<SkiRun> matchingSkiRuns)
        {
            if (matchingSkiRuns.Count > 0)
            {
                foreach (SkiRun queriedSkiRun in matchingSkiRuns)
                {
                    StringBuilder skiRunInfo = new StringBuilder();

                    skiRunInfo.Append(queriedSkiRun.ID.ToString().PadRight(8));
                    skiRunInfo.Append(queriedSkiRun.Name.PadRight(25));
                    skiRunInfo.Append(queriedSkiRun.Vertical.ToString().PadRight(5));
                    ConsoleView.DisplayMessage(skiRunInfo.ToString());

                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("There were no ski runs that matched your query.");
            }
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _skiRuns = null;
        }
    }
}
