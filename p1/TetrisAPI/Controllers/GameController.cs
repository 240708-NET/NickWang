using Microsoft.AspNetCore.Mvc;
using TetrisRepo;

namespace TetrisAPI;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    private readonly IGameRepository _gameRepository;

    private readonly ILogger<GameController> _logger;

    public GameController(ILogger<GameController> logger, IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<Game> GetAll()
    {
        return _gameRepository.All();
    }

    [HttpGet("{id}")]
    public Game GetById(int id)
    {
        return _gameRepository.FindById(id);
    }

    [HttpGet("top/{amount}")]
    public List<string> GetTopScores(int amount)
    {
        return _gameRepository.GetHighScores(amount);
    }

    [HttpPost("")]
    public Game Post(Game game)
    {
        return _gameRepository.Write(game);
    }
}
