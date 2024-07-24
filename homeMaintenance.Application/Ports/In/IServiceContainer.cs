namespace homeMaintenance.Application.Ports.In
{
    public interface IServiceContainer
    {
        IUserService UserService { get; }
    }
}
