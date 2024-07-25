using System.Threading.Tasks.Dataflow;

public interface IGameStateRepo
{

    public TetrisGameState FindByPath(string path);
    public int Write(TetrisGameState data);

}