using Microsoft.AspNetCore.Mvc;
using TetrisRepo;

namespace TetrisAPI;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{

    private readonly IPlayerRepository _playerRepository;

    private readonly ILogger<PlayerController> _logger;

    public PlayerController(ILogger<PlayerController> logger, IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<Player> GetAll()
    {
        return _playerRepository.All();
    }

    [HttpGet("{id}")]
    public Player GetById(int id)
    {
        return _playerRepository.FindById(id);
    }

    [HttpPost("")]
    public Player Post(Player player)
    {
        return _playerRepository.Write(player);
    }

    [HttpPatch("{id}")]
    public Player IncreaseGameCount(int id)
    {
        return _playerRepository.IncreaseGameCount(id);
    }
}
