using System.Diagnostics;

public class Tetris
{

    long GravityUpdateBlockTimeMilliseconds;
    long InputDelayTimeMilliseconds = 50;
    int level = 0;
    int[] levelProgression = [40, 70, 90, 105, 130, 140, 150, 160];
    // int[] levelProgression = [10, 15, 20, 25, 30, 35, 90];
    // int[] levelProgression = [1, 2, 3, 4, 5, 6, 7];

    int playerId = 4;
    string playerName = "";
    Player? player;
    int totalLineCleared;
    int score;

    TetrisBoard board;
    TetrisDisplay display;


    bool gameRunning = true;
    ConsoleKeyInfo keyinfo;
    Stopwatch gravityWatch;
    Stopwatch inputWatch;

    DataBase dataBase;


    public Tetris()
    {
        playerName = ProgramSettings.PlayerName;
        playerId = ProgramSettings.PlayerId;
        board = new TetrisBoard();
        display = new TetrisDisplay(board);
        inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
        dataBase = new DataBase(new PlayerRepoImpl(), new GameRepoImpl());
    }

    public Tetris(string path)
    {
        TetrisGameState gs = ReadGameState(path);
        playerId = gs.PlayerId;
        playerName = gs.PlayerName;
        board = new TetrisBoard(gs);
        display = new TetrisDisplay(board);
        level = gs.Level;
        score = gs.Score;
        totalLineCleared = gs.TotalLineCleared;
        inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
        dataBase = new DataBase(new PlayerRepoImpl(), new GameRepoImpl());
    }

    public void SetSeed(int seed)
    {
        board.SetSeed(seed);
    }

    public void Run()
    {
        InitGame();
        board.UpdateShape();
        display.InitDisplay();
        while (gameRunning)
        {
            HandleKeyPress();
            HandleGravityUpdate(false);
        }
        HandleGameOver();
        display.DisplayGameOver(level, score, totalLineCleared);
        Stop();
    }

    private void InitGame()
    {
        if (playerName != "")
        {
            player = new Player(playerName);
            playerId = dataBase.CreatePlayer(playerName);
        }
        UpdateDifficulty();
    }

    private void HandleGameOver()
    {
        if (playerId == -1)
        {
            dataBase.UploadGame(score, totalLineCleared, player);
        }
        else
        {
            player = dataBase.FindPlayerById(playerId);
            dataBase.UploadGame(score, totalLineCleared, player);
        }
    }

    private void Stop()
    {
        Thread.Sleep(2000);
        Console.Clear();
        display.HighScores = dataBase.GetHighScores();
        display.DisplayHighScores(0, 0);
        Thread.Sleep(2000);
        while (Console.KeyAvailable)
        {
            Console.ReadKey(false);
        }
        Console.ReadKey(true);
        Console.Clear();
        Environment.Exit(0);
    }

    private void HandleGravityUpdate(bool byPassTimer)
    {
        if (byPassTimer ||
            gravityWatch.ElapsedMilliseconds >= GravityUpdateBlockTimeMilliseconds)
        {
            board.UndoShape();
            display.UndoShapeDisplay();
            if (!board.GravityUpdate())
            {
                // DisplayShape();
                int eliminatedRows = board.EliminateRows();
                UpdateScore(eliminatedRows);
                gameRunning = board.PickNewBlock();
                if (eliminatedRows >= 0)
                {
                    UpdateDifficulty();
                    display.Display(level, score, totalLineCleared);
                }
            }

            display.DisplayShape(level, score, totalLineCleared);
            gravityWatch.Restart();
        }
    }

    private void HandlePaused()
    {
        Console.ReadKey(true);
    }

    private void HandleKeyPress()
    {
        if (Console.KeyAvailable &&
            inputWatch.ElapsedMilliseconds >= InputDelayTimeMilliseconds)
        {
            keyinfo = Console.ReadKey(true);
            board.UndoShape();
            display.UndoShapeDisplay();
            switch (keyinfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    board.MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    board.MoveRight();
                    break;
                case ConsoleKey.UpArrow:
                    board.MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    HandleGravityUpdate(true);
                    break;
                case ConsoleKey.P:
                    display.DisplayPaused();
                    display.DisplayShape(level, score, totalLineCleared);
                    HandlePaused();
                    display.Display(level, score, totalLineCleared);
                    break;
                case ConsoleKey.S:
                    SaveGameState();
                    display.DisplaySave();
                    break;
                case ConsoleKey.Q:
                    display.DisplayQuit();
                    Stop();
                    break;
                default:
                    break;
            }
            display.DisplayShape(level, score, totalLineCleared);
            inputWatch.Restart();
        }
        if (Console.KeyAvailable &&
            inputWatch.ElapsedMilliseconds < InputDelayTimeMilliseconds)
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }

    private TetrisGameState ReadGameState(string path)
    {
        GameStateRepoImpl gameStateRepo = new GameStateRepoImpl();
        return gameStateRepo.FindByPath(path);
    }

    private void SaveGameState()
    {
        GameStateRepoImpl gameStateRepo = new GameStateRepoImpl();
        TetrisGameState gs = new TetrisGameState(playerId, playerName, level, score, totalLineCleared,
                                                 board.CurrentPos, board.CurrentShape, board.NextShape,
                                                 TetrisGameState.LinkedListBoardToListBoard(board.Board),
                                                 TetrisGameState.ColorBoardToListColorBoard(board.ColorBoard));
        gameStateRepo.Write(gs);
    }

    private void UpdateScore(int linesCleared)
    {
        score += linesCleared * linesCleared * 10 * (level + 10);
        totalLineCleared += linesCleared;
    }

    private void UpdateDifficulty()
    {
        if (level >= levelProgression.Length)
        {
            level = (totalLineCleared - levelProgression[levelProgression.Length - 1]) % 10;
        }
        else
        {
            if (totalLineCleared >= levelProgression[level])
            {
                level++;
            }
        }
        GravityUpdateBlockTimeMilliseconds = TetrisConstants.BaseGravityUpdateBlockTimeMilliseconds + 900 / (level + 1);
    }

}
