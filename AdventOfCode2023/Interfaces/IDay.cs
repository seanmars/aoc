namespace AdventOfCode2023.Interfaces;

public interface IDay
{
    public object Solve(int part);

    public void GetResult(int part)
    {
        var answer = Solve(part);
        Logger.Log(answer);
    }
}