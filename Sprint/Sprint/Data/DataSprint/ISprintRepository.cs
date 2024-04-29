using Sprint.Models.ModelSprint;



namespace Sprint.Data.DataSprint
{
    public interface ISprintRepository
    {
        List<SprintS> GetSprint();
        SprintS GetSprintById(Guid sprintid);
        SprintConfirmation CreateSprint(SprintS sprint);
        void UpdateSprint(SprintS sprint);
        void DeleteSprint(Guid sprintid);
        bool SaveChanges();
    }
}
