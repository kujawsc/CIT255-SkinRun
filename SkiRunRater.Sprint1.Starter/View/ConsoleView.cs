using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    public static class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        /// <summary>
        /// method to display the manager menu and get the user's choice
        /// </summary>
        /// <returns></returns>
        public static AppEnum.ManagerAction GetUserActionChoice()
        {
            AppEnum.ManagerAction userActionChoice = AppEnum.ManagerAction.None;
            //
            // set a string variable with a length equal to the horizontal margin and filled with spaces
            //
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            //
            // set up display area
            //
            DisplayReset();

            //
            // display the menu
            //
            DisplayMessage("Ski Manager Menu");
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "1. Display All Ski Runs" + Environment.NewLine +
                leftTab + "2. Display Ski Runs Information" + Environment.NewLine +
                leftTab + "3. Delete a Ski Run" + Environment.NewLine +
                leftTab + "4. Add a Ski Run" + Environment.NewLine +
                leftTab + "5. Edit A Ski Run" + Environment.NewLine +
                leftTab + "6. Query Ski Runs by Vertical" + Environment.NewLine +
                leftTab + "E. Exit" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number/letter for the menu choice.");
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userActionChoice = AppEnum.ManagerAction.ListAllSkiRuns;
                    break;
                case '2':
                    userActionChoice = AppEnum.ManagerAction.DisplaySkiRunInformation;
                    break;
                case '3':
                    userActionChoice = AppEnum.ManagerAction.DeleteSkiRun;
                    break;
                case '4':
                    userActionChoice = AppEnum.ManagerAction.AddSkiRun;
                    break;
                case '5':
                    userActionChoice = AppEnum.ManagerAction.UpdateSkiRun;
                    break;
                case '6':
                    userActionChoice = AppEnum.ManagerAction.QuerySkiRunsByVertical;
                    break;
                case 'E':
                case 'e':
                    userActionChoice = AppEnum.ManagerAction.Quit;
                    break;
                default:
                    Console.WriteLine(
                        "It appears you have selected an incorrect choice." + Environment.NewLine +
                        "Press any key to try again or the ESC key to exit.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userActionChoice = AppEnum.ManagerAction.Quit;
                    }
                    break;
            }

            return userActionChoice;
        }

        /// <summary>
        /// method to display all ski run info
        /// </summary>
        public static void DisplayAllSkiRuns(List<SkiRun> skiRuns)
        {
            DisplayReset();

            DisplayMessage("All of the existing ski runs are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));
            columnHeader.Append("Vertical in Feet".PadRight(5));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in skiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));
                skiRunInfo.Append(skiRun.Vertical.ToString().PadRight(5));

                DisplayMessage(skiRunInfo.ToString());
            }
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }

        /// <summary>
        /// Skin Run information
        /// </summary>
        /// <param name="skiRun"></param>
        public static void DisplaySkiRunInformation(SkiRun skiRun)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Ski Run Detail", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Ski Run: {0}", skiRun.Name));
            DisplayMessage("");

            DisplayMessage(String.Format("ID: {0}", skiRun.ID.ToString()));
            DisplayMessage(String.Format("Vertical in Feet: {0}", skiRun.Vertical.ToString()));

            DisplayMessage("");
        }

        /// <summary>
        /// Main closing validation
        /// </summary>
        /// <param name="runs"></param>
        /// <returns></returns>
        public static bool DisplayClosingValidation(List<SkiRun> runs)
        {
            bool isActive = true;
            DisplayReset();
            Console.WriteLine("Are you sure you want to exit? (y/n)");
            string input = Console.ReadLine();
            if (getYesValidation(input, AppEnum.ManagerAction.None, runs))
            {
                isActive = false;
            }

            return isActive;

        }

        /// <summary>
        /// Delete Ski Run Records
        /// </summary>
        /// <param name="runs"></param>
        /// <returns></returns>
        public static int DisplayDeleteRecord(List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("Enter the ID of the record you wish to delete.");
            int ID = -1;
            if (int.TryParse(Console.ReadLine(), out ID))
            {
                SkiRun record = CheckRunExists(ID, runs);

                if (record != null)
                {
                    ID = DisplayDeleteValidation(record, runs);
                }
                else
                {
                    DisplaySkiRunNotFound(ID.ToString(), AppEnum.ManagerAction.DeleteSkiRun, runs);
                    ID = -1;
                }
            }
            else
            {
                DisplayInvalidInput(AppEnum.ManagerAction.None, runs);
            }
            return ID;
        }

        /// <summary>
        /// method to get the user's choice of ski run id
        /// </summary>
        /// <param name="skiRuns">list of all ski runs</param>
        /// <returns>id of user selected ski run</returns>
        public static int GetSkiRunID(List<SkiRun> skiRuns)
        {
            int skiRunID = -1;

            DisplayAllSkiRuns(skiRuns);

            DisplayMessage("");
            DisplayPromptMessage("Enter the ski run ID: ");

            skiRunID = ConsoleUtil.ValidateIntegerResponse("Please enter the ski run ID: ", Console.ReadLine());

            return skiRunID;
        }

        /// <summary>
        /// Add any Ski Run
        /// </summary>
        /// <returns></returns>
        public static SkiRun AddSkiRun()
        {
            SkiRun skinRun = new SkiRun();

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Add a Ski Run", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayPromptMessage("Enter the Ski Run ID: ");
            skinRun.ID = ConsoleUtil.ValidateIntegerResponse("Please enter the Ski Run ID: ", Console.ReadLine());
            DisplayMessage("");

            DisplayPromptMessage("Enter the Ski Run name: ");
            skinRun.Name = Console.ReadLine();
            DisplayMessage("");

            DisplayPromptMessage("Enter the Ski Run Vertical in feet; ");
            skinRun.Vertical = ConsoleUtil.ValidateIntegerResponse("Please the Ski Run Vertical in feet: ", Console.ReadLine());

            return skinRun;

        }

        /// <summary>
        /// Updated any exciting Ski Run in the list
        /// </summary>
        /// <param name="skiRun"></param>
        /// <returns></returns>
        public static SkiRun UpdateSkiRun(SkiRun skiRun)
        {
            string userResponse = "";

            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Edit a Ski Run", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage(String.Format("Current Name: {0}", skiRun.Name));
            DisplayPromptMessage("Enter a new name or just press Enter to keep the current name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                skiRun.Name = userResponse;
            }

            DisplayMessage("");

            DisplayMessage(String.Format("Current Vertical in Feet: {0}", skiRun.Vertical.ToString()));
            DisplayPromptMessage("Enter the new Vertical in feet or just press Enter to keep the current Vertical: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                skiRun.Vertical = ConsoleUtil.ValidateIntegerResponse("Please enter the Vertical in feet.", userResponse);
            }

            DisplayContinuePrompt();

            return skiRun;
        }

        /// <summary>
        /// Get the Query Result for each Ski Run
        /// </summary>
        /// <param name="matchingSkiRuns"></param>
        public static void DisplayQueryResults(List<SkiRun> matchingSkiRuns)
        {
            DisplayReset();

            DisplayMessage("");
            Console.WriteLine(ConsoleUtil.Center("Display Ski Run Query Results", WINDOW_WIDTH));
            DisplayMessage("");

            DisplayMessage("All of the matching ski runs are displayed below;");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in matchingSkiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));

                DisplayMessage(skiRunInfo.ToString());
            }
        }

        /// <summary>
        /// Promt showing that the Ski Run you choice was not found in the list of Ski Run
        /// </summary>
        /// <param name="name"></param>
        /// <param name="redirect"></param>
        /// <param name="runs"></param>
        private static void DisplaySkiRunNotFound(string name, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine(name + " was not found. Please try a different Ski Run name.");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Promt the user that they have choise a invalid input
        /// </summary>
        /// <param name="redirect"></param>
        /// <param name="runs"></param>
        private static void DisplayInvalidInput(AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("That is not a valid input. Please try again.");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Check the run exists
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="runs"></param>
        /// <returns></returns>
        private static SkiRun CheckRunExists(int ID, List<SkiRun> runs)
        {
            SkiRun run = null;

            foreach (SkiRun record in runs)
            {
                if (record.ID==ID)
                {
                    run = record;
                    break;
                }
            }

            return run;
        }

        /// <summary>
        /// Shoice the deleted validation that use pick
        /// </summary>
        /// <param name="record"></param>
        /// <param name="runs"></param>
        /// <returns></returns>
        private static int DisplayDeleteValidation(SkiRun record, List<SkiRun> runs)
        {
            int ID = -1;
            DisplayReset();
            Console.WriteLine("Delete this record? (y/n)");
            Console.WriteLine("ID: " + record.ID);
            Console.WriteLine("Ski Run: " + record.Name);
            Console.WriteLine("Vertical: " + record.Vertical);

            if (getYesValidation(Console.ReadLine(), AppEnum.ManagerAction.DeleteSkiRun, runs))
            {
                ID = record.ID;
                DisplayDeleteSuccess(runs);
            }

            return ID;
        }

        /// <summary>
        /// Validation for the answer yes or no
        /// </summary>
        /// <param name="input"></param>
        /// <param name="redirect"></param>
        /// <param name="runs"></param>
        /// <returns></returns>
        private static bool getYesValidation(string input, AppEnum.ManagerAction redirect, List<SkiRun> runs)
        {
            bool Yes = false;
            switch (input)
            {
                case "y":
                    Yes = true;
                    break;
                case "n":
                    break;
                default:
                    DisplayInvalidInput(redirect, runs);
                    break;
            }
            return Yes;
        }
        
        /// <summary>
        /// Show the use the the delete was successful
        /// </summary>
        /// <param name="runs"></param>
        private static void DisplayDeleteSuccess(List<SkiRun> runs)
        {
            DisplayReset();
            Console.WriteLine("Your deletion was successful.");
            DisplayContinuePrompt();
        }

        #endregion
    }
}
