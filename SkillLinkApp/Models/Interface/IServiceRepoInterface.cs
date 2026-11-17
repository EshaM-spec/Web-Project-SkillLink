namespace SkillLinkApp.Models.Interface
{
    public interface IServiceRepoInterface
    {
        List<Service> GetAllServices();

        Service GetServiceById(string id);

        List<Service> GetServicesByType(string serviceType);

        void AddService(Service service);

        void UpdateService(Service service, string id);

        void DeleteService(string id);
    }
}
