namespace ApiTemplate.Services.Interfaces
{
    public interface IExampleService
    {
        string Add(object model);
        string Delete(object model);
        object Select();
        object SelectById(object id);
        string Update(object model);
        bool ExitsById(object id);
    }
}
