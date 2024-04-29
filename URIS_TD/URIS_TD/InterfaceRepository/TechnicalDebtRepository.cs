using Microsoft.EntityFrameworkCore;
using URIS_TD.Entities;
using URIS_TD.Models;

namespace URIS_TD.InterfaceRepository
{
    public class TechnicalDebtRepository : ITechnicalDebtRepository
    {
        private readonly TechnicalDebtContext dbContext;
        public TechnicalDebtRepository(TechnicalDebtContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public List<TechnicalDebt> GetAllTd()
        {
            return dbContext.Debts.ToList();
        }

        public TechnicalDebt GetTdById(Guid id) 
        {
            return dbContext.Debts.Find(id);
        }

        public TechnicalDebt AddTd(TechnicalDebt debt) 
        {
            dbContext.Debts.Add(debt);
            dbContext.SaveChanges();
            return debt;
        }

        public TechnicalDebt UpdateTd(TechnicalDebt debt) 
        {
            TechnicalDebt td = GetTdById(debt.IdTd);
            td.NameTd = debt.NameTd;
            td.DescriptionTd = debt.DescriptionTd;
            dbContext.SaveChanges();
            return debt;
        }

        public void DeleteTd(Guid id) 
        {
            var td = dbContext.Debts.Find(id);
            if ( td != null) 
            {
                dbContext.Debts.Remove(td);
                dbContext.SaveChanges();
            }
        }

        public List<TechnicalDebt> GetTdBySprintId(Guid sprintId)
        {
            return dbContext.Debts.Where(e => e.SprintId == sprintId).ToList();
        }

    }
}
