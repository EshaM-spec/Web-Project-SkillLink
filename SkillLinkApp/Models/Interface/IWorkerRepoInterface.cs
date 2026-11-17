namespace SkillLinkApp.Models.Interface
{
    public interface IWorkerRepoInterface
    {
        public List<Worker> GetAllWorkers();
        public Worker GetWorkerById(string id);
        public void UpdateWorker(Worker worker, string id);
        public void DeleteWorker(string id);
        public void AddWorker(Worker worker);
    }
}
