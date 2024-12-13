using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Store.Implementation
{
    public class ProfitAndLoss : IProfitAndLoss
    {
        private readonly IRepository<ProfitAndLossResponse> repo;
        public ProfitAndLoss(IRepository<ProfitAndLossResponse> _ProfitAndLossRepo)
        {
            repo = _ProfitAndLossRepo;
        }
        public IEnumerable<ProfitAndLossResponse> ProfitAndLossResponse(ProfitAndLossRequest obj)
        {
            try
            {
                //var rs = repo.GetBySP($"EXEC [GetProfitAndLoss] @StartDate,@EndDate", new SqlParameter("@StartDate", obj.StartDate), new SqlParameter("@EndDate", obj.EndDate));
                //return rs;

                SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
                SqlCommand CmdMaster = new SqlCommand();
                CmdMaster = cnn1.CreateCommand();
                string sSql = "[GetProfitAndLoss]  '" + obj.StartDate + "','" + obj.EndDate + "'";
                CmdMaster.CommandText = sSql;
                cnn1.Open();
                SqlDataReader ReaderMast = CmdMaster.ExecuteReader();
                ReaderMast.Read();
                return (IEnumerable<ProfitAndLossResponse>)ReaderMast;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
