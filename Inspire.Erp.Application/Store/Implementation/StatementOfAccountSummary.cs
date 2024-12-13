using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Store.Implementation
{
    public class StatementOfAccountSummary : IStatementOfAccountSummary
    {
        private readonly IRepository<StatementOfAccountSummaryResponse> repo;
        public StatementOfAccountSummary(IRepository<StatementOfAccountSummaryResponse> _StatementOfAccountSummaryRepo)
        {
            repo = _StatementOfAccountSummaryRepo;
        }
        //public IEnumerable<StatementOfAccountSummaryResponse> StatementOfAccountSummaryResponse(StatementOfAccountSummaryRequest obj)
        public async Task<string> StatementOfAccountSummaryResponse(StatementOfAccountSummaryRequest obj)
        {
            try
            {
                //var rs = repo.GetBySP($"EXEC GetStatementOfAccountSummary @AcNo,@StartDate", new SqlParameter("@AcNo", obj.AcNo), new SqlParameter("@StartDate", obj.StartDate));
                //return rs;

                SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
                ///CmdMaster = cnn1.CreateCommand();
                //string sSql = "[GetStatementOfAccountSummary] '" + obj.AcNo + "','" + obj.StartDate + "'";
                string sSql = "GetStatementOfAccountSummary";
                //CmdMaster.CommandText = sSql;
                SqlCommand CmdMaster = new SqlCommand(sSql,cnn1);
                CmdMaster.CommandType = CommandType.StoredProcedure;
                CmdMaster.Parameters.AddWithValue("@AcNo", obj.AcNo);
                CmdMaster.Parameters.AddWithValue("@StartDate", obj.StartDate);
                cnn1.Open();
                SqlDataAdapter ReaderMast = new SqlDataAdapter();
                ReaderMast.SelectCommand = CmdMaster;
                DataSet ds = new DataSet();

                ReaderMast.Fill(ds);
                //return (IEnumerable<StatementOfAccountSummaryResponse>)ds;
                return JsonConvert.SerializeObject(ds,Formatting.Indented);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public IEnumerable<StatementOfAccountSummaryResponse> StatementOfAccountSummaryResponse()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
