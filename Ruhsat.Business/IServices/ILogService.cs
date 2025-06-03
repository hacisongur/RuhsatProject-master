namespace RuhsaProject.Business.IServices
{
    public interface ILogService
    {
        Task AddLogAsync(string userId, string userName, string action, string entityName, string description, string ipAddress = null);


    }
}
