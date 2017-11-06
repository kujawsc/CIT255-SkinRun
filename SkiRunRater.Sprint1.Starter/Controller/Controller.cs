using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            SkiRunRepository skiRunRepository = new SkiRunRepository();

            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();

                int skiRunID;
                SkiRun skiRun;
                string message;

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;
                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            ConsoleView.DisplayAllSkiRuns(skiRuns);

                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DisplaySkiRunInformation:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRun = skiRunRepository.GetSkiRunByID(skiRunID);

                            ConsoleView.DisplayAllSkiRuns(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.DeleteSkiRun:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRunRepository.DeleteSkiRun(skiRunID);

                            ConsoleView.DisplayReset();
                            message = String.Format("Ski Run ID: {0} had been deleted.", skiRunID);
                            ConsoleView.DisplayMessage(message);
                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.AddSkiRun:
                            skiRun = ConsoleView.AddSkiRun();
                            skiRunRepository.InsertSkiRun(skiRun);

                            ConsoleView.DisplayContinuePrompt();
                            break;

                        case AppEnum.ManagerAction.UpdateSkiRun:
                            skiRunID = ConsoleView.GetSkiRunID(skiRuns);
                            skiRun = skiRunRepository.GetSkiRunByID(skiRunID);

                            skiRun = ConsoleView.UpdateSkiRun(skiRun);

                            skiRunRepository.UpdateSkiRun(skiRun);
                            break;

                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            int minimumVertical = skiRunRepository.GetMinimumVertical();
                            int maximumVertical = skiRunRepository.GetMaximumVertical();

                            List<SkiRun> matchingSkiRuns = skiRunRepository.QueryByVertical(minimumVertical, maximumVertical);
                            ConsoleView.DisplayReset();

                            //Displays the new list.
                            Console.WriteLine("Ski Runs with a vertical between " + minimumVertical + " and " + maximumVertical + ".");
                            skiRunRepository.DisplayQueriedVertical(matchingSkiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
