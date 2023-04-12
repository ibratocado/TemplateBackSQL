namespace ApiTemplate.Services.Interfaces
{
    public interface IExampleService
    {
        Task<object> SelectExample(object? parameters);
        Task<object> AcctionExample(object? parameters);
    }
}
