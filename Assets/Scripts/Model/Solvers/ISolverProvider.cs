namespace Tactile.TactileMatch3Challenge.Model.Solvers
{
    public interface ISolverProvider
    {
        ISolver GetSolver(int index);
    }
}