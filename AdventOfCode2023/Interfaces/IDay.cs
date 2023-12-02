namespace AdventOfCode2023.Interfaces;

public interface IDay
{
    public object Solve();

    public void GetResult()
    {
        var answer = Solve();
        Logger.Log(answer);
    }
}