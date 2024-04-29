using URIS_TD.Models;

namespace URIS_TD.InterfaceRepository
{
    public interface ITypeOfTechnicalDebtRepository
    {
        List<TypeOfTechnicalDebt> GetAllTypesOfTd();
        TypeOfTechnicalDebt GetTypeOfTechnicalDebtById(Guid id);

        TypeOfTechnicalDebt AddTypeOfTd(TypeOfTechnicalDebt debt);

        TypeOfTechnicalDebt UpdateTypeOfTd(TypeOfTechnicalDebt debt);

        void DeleteTypeOfTechnicalDebtById(Guid id);
    }
}
