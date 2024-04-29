using URIS_TD.Entities;
using URIS_TD.Models;

namespace URIS_TD.InterfaceRepository
{
    public class TypeOfTechnicalDebtRepository : ITypeOfTechnicalDebtRepository
    {
        private readonly TechnicalDebtContext dbContext;

        public TypeOfTechnicalDebtRepository(TechnicalDebtContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<TypeOfTechnicalDebt> GetAllTypesOfTd()
        {
            return dbContext.Type.ToList();
        }
        public TypeOfTechnicalDebt GetTypeOfTechnicalDebtById(Guid id)
        {
            return dbContext.Type.Find(id);
        }

        public TypeOfTechnicalDebt AddTypeOfTd(TypeOfTechnicalDebt debt)
        {
            dbContext.Type.Add(debt);
            dbContext.SaveChanges();
            return debt;
        }

        public TypeOfTechnicalDebt UpdateTypeOfTd(TypeOfTechnicalDebt debt)
        {
            TypeOfTechnicalDebt typeOfTechnicalDebt = GetTypeOfTechnicalDebtById(debt.IdTod);
            typeOfTechnicalDebt.NameTotd = debt.NameTotd;
            dbContext.SaveChanges();
            return debt;
        }

        public void DeleteTypeOfTechnicalDebtById(Guid id)
        {
            var ttd = dbContext.Type.Find(id);
            if ( ttd != null ) 
            {
                dbContext.Type.Remove(ttd);
                dbContext.SaveChanges() ;
            }
        }
    }
}
