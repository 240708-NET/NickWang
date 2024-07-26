using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using TetrisRepo;
using TetrisResources;

namespace TetrisGame;
public class Tetris
{

    long GravityUpdateBlockTimeMilliseconds;
    long InputDelayTimeMilliseconds = 30;
    int level = 0;
    int[] levelProgression = [40, 70, 90, 105, 130, 140, 150, 160];
    // int[] levelProgression = [10, 15, 20, 25, 30, 35, 90];
    // int[] levelProgression = [1, 2, 3, 4, 5, 6, 7];

    int playerId;
    string playerName = "";
    // Player? player;
    int totalLineCleared;
    int score;

    TetrisBoard board;
    TetrisDisplay display;


    bool gameRunning = true;
    ConsoleKeyInfo keyinfo;
    Stopwatch gravityWatch;
    Stopwatch inputWatch;

    HttpClient client;

    public Tetris()
    {
        playerName = ProgramSettings.PlayerName;
        playerId = ProgramSettings.PlayerId;
        board = new TetrisBoard();
        display = new TetrisDisplay(board);
        client = new HttpClient();
        inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
    }

    public Tetris(string path)
    {
        GameState gs = ReadGameState(path);
        ProgramSettings.PlayerId = gs.PlayerId;
        ProgramSettings.PlayerName = gs.PlayerName;
        board = new TetrisBoard(gs);
        display = new TetrisDisplay(board);
        level = gs.Level;
        score = gs.Score;
        totalLineCleared = gs.TotalLineCleared;
        client = new HttpClient();
        inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
    }

    public void SetSeed(int seed)
    {
        board.SetSeed(seed);
    }

    public async Task Run()
    {
        await InitGame();
        board.UpdateShape();
        display.InitDisplay();
        while (gameRunning)
        {
            await HandleKeyPress();
            HandleGravityUpdate(false);
        }
        await HandleGameOver();
        display.DisplayGameOver(level, score, totalLineCleared);
        await Stop();
    }

    private async Task InitGame()
    {
        playerName = ProgramSettings.PlayerName;
        playerId = ProgramSettings.PlayerId;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (playerName != "")
        {
            Player player = new Player(playerName);
            var res = await client.PostAsync($"{ProgramSettings.DatabaseURL}/Player", JsonContent.Create<Player>(player));
            player = JsonSerializer.Deserialize<Player>(res.Content.ReadAsStream(), options);
            playerId = player.Id;
            playerName = player.Name;
        }
        else if (playerId != 0)
        {
            var res = await client.GetAsync($"{ProgramSettings.DatabaseURL}/Player/{playerId}");
            Player player = JsonSerializer.Deserialize<Player>(res.Content.ReadAsStream(), options);
            playerName = player.Name;
        }
        display.PlayerName = playerName;
        UpdateDifficulty();
    }

    private async Task HandleGameOver()
    {
        if (playerId != 0)
        {
            await client.PatchAsync($"{ProgramSettings.DatabaseURL}/Player/{playerId}", null);
        }

        Game game = new Game
        {
            Score = score,
            LinesCleared = totalLineCleared,
            PlayerId = playerId,
            Time = DateTime.Now,
        };

        await client.PostAsync($"{ProgramSettings.DatabaseURL}/Game", JsonContent.Create<Game>(game));
    }

    private async Task Stop()
    {
        Thread.Sleep(500);
        Console.Clear();
        var res = await client.GetAsync($"{ProgramSettings.DatabaseURL}/Game/top/{20}");
        display.HighScores = JsonSerializer.Deserialize<List<string>>(res.Content.ReadAsStream());
        display.DisplayHighScores(0, 0);
        Thread.Sleep(500);
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

    private async Task HandleKeyPress()
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
                    await Stop();
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

    private GameState ReadGameState(string path)
    {
        GameStateRepository gameStateRepo = new GameStateRepository();
        return gameStateRepo.FindByPath(path);
    }

    private void SaveGameState()
    {
        GameStateRepository gameStateRepo = new GameStateRepository();
        GameState gs = new GameState(playerId, ProgramSettings.PlayerName, level, score, totalLineCleared,
                                                 board.CurrentPos, board.CurrentShape, board.NextShape,
                                                 GameState.LinkedListBoardToListBoard(board.Board),
                                                 GameState.ColorBoardToListColorBoard(board.ColorBoard));
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
