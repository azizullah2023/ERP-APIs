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
    public class VoucherPrinting : IvoucherPrinting
    {
        private readonly IRepository<VoucherPrintingResponse> repo;
        public VoucherPrinting(IRepository<VoucherPrintingResponse> _StatementOfAccountSummaryRepo)
        {
            repo = _StatementOfAccountSummaryRepo;
        }
        //public IEnumerable<StatementOfAccountSummaryResponse> StatementOfAccountSummaryResponse(StatementOfAccountSummaryRequest obj)
        public async Task<string> VoucherPrintingRequest(VoucherPrintingRequest obj)
        {
            try
            {
                //var rs = repo.GetBySP($"EXEC GetStatementOfAccountSummary @AcNo,@StartDate", new SqlParameter("@AcNo", obj.AcNo), new SqlParameter("@StartDate", obj.StartDate));
                //return rs;

                SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
                SqlCommand CmdMaster = new SqlCommand();
                CmdMaster = cnn1.CreateCommand();
                //string sSql = "[GetVoucherPrinting] '" + obj.voucherNoFROM + "','" + obj.VoucherNOTo + "','" + obj.VoucherType + "','" + obj.StartDate + "','" + obj.EndDate + "','" + obj.chequeNo + "','" + obj.VoucherNo_NU + "'";
                string sSql = "[GetVoucherPrinting]" ;
                CmdMaster.CommandText = sSql;
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
