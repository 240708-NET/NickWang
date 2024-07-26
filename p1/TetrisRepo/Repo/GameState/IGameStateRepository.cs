namespace TetrisRepo;
using TetrisResources;

public interface IGameStateRepository
{

    public GameState FindByPath(string path);
    public int Write(GameState data);

}