namespace Puzzles;

public interface IPuzzle
{
    string Name { get; }
    string Description { get; }
    string Explanation { get; }
    void Run();
}
