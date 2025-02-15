using HangmanExceptions;

namespace CLIArgs
{
    /// <summary>
    /// arg to select ASCII art asset
    /// </summary>
    class ArtSelectionArg : CommandLineArg
    {
        string selection = "";

        public ArtSelectionArg(string flag) : base(flag)
        {
        }

        /// <summary>
        /// update GameSettings.hangmanArt according to arg. 
        /// </summary>
        /// <exception cref="UnrecognizedParamException"></exception>
        public override void Act()
        {
            if (selection == "1")
            {
                GameSettings.hangmanArt = DisplayConstants.HANGMAN_ART_1;
                return;
            }

            if (selection == "2")
            {
                GameSettings.hangmanArt = DisplayConstants.HANGMAN_ART_2;
                return;
            }
            throw new UnrecognizedParamException("Unrecognized parameter.");
        }

        /// <summary>
        /// read 1 required argument 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <exception cref="ParamsCountException"></exception>
        public override void ConsumeParameters(string[] args, ref int index)
        {
            try
            {
                selection = args[++index];
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new ParamsCountException("Incorrect number of parameters.");
            }
            catch (System.Exception)
            {
                throw new ParamsCountException("Unknown error when reading parameters.");
            }
        }

        public override string Description()
        {
            return $"   --{flag} <param> selects between different hangman art options.\n        Available params are \n       1 - (default) art option 1 \n       2 - art option 2";
        }
    }
}