namespace Tactile.TactileMatch3Challenge.Model.Solvers
{
    public class SolverProvider : ISolverProvider
    {
        private readonly ISolver[] solvers;

        public SolverProvider(params ISolver[] solvers)
        {
            this.solvers = solvers;
        }

        public ISolver GetSolver(int index)
        {
            index %= solvers.Length;
            return solvers[index];
        }
    }
}