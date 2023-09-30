namespace Tactile.TactileMatch3Challenge.Application
{
    public interface IAppContext
    {
        T Resolve<T>();
    }
}