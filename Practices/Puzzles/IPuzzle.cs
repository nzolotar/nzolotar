namespace Puzzles;

public interface IPuzzle
{
    string Name { get; }
    string Description { get; }
    void Run();
}
