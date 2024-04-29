using URIS_TD.Models;

namespace URIS_TD.InterfaceRepository
{
    public interface ITechnicalDebtRepository
    {
        List<TechnicalDebt> GetAllTd();
        TechnicalDebt  GetTdById(Guid id);
        TechnicalDebt AddTd(TechnicalDebt debt);
        TechnicalDebt UpdateTd(TechnicalDebt debt);
        void DeleteTd(Guid id);

        List<TechnicalDebt> GetTdBySprintId(Guid sprintId);


    }
}
