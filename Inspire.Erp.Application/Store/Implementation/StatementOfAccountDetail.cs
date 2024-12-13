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
    public class StatementOfAccountDetail : Inspire.Erp.Application.Store.Interface.IStatementOfAccountDetail
    {
        private readonly IRepository<StatementOfAccountDetailResponse> repo;
        public StatementOfAccountDetail(IRepository<StatementOfAccountDetailResponse> _StatementOfAccountDetailRepo)
        {
            repo = _StatementOfAccountDetailRepo;
        }
        public async Task<dynamic> StatementOfAccountDetailResponse(StatementOfAccountDetailRequest obj)
        {
            try
            {
                SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
                SqlCommand CmdMaster = new SqlCommand();
                CmdMaster = cnn1.CreateCommand();
                string sSql = "[GetAccountStatementDetails] '" + obj.StartDate + "','" + obj.EndDate + "','" + obj.AcNo + "','" + obj.Description + "','" + obj.Particulars + "','" + obj.JobNo + "'";
                CmdMaster.CommandText = sSql;
                cnn1.Open();
                SqlDataAdapter ReaderMast = new SqlDataAdapter();
                ReaderMast.SelectCommand = CmdMaster;
                DataSet ds = new DataSet();

                ReaderMast.Fill(ds);
                return JsonConvert.SerializeObject(ds, Formatting.Indented);
                //return (IEnumerable<StatementOfAccountDetailResponse>)ReaderMast;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
