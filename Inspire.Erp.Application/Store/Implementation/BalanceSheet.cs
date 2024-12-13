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
    public class BalanceSheet : Inspire.Erp.Application.Store.Interface.IBalanceSheet
    {
        private readonly IRepository<BalanceSheetEntity> repo;
        public BalanceSheet(IRepository<BalanceSheetEntity> _BalanceSheetRepo)
        {
            repo = _BalanceSheetRepo;
        }
        //public IEnumerable<BalanceSheetResponse> BalanceSheetResponse(BalanceSheetRequest obj)
        //{
        //    try
        //    {
        //        SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
        //        SqlCommand CmdMaster = new SqlCommand();
        //        CmdMaster = cnn1.CreateCommand();
        //        string sSql = "[GetBalanceSheet] '" + obj.StartDate + "','" + obj.EndDate + "'";
        //        CmdMaster.CommandText = sSql;
        //        cnn1.Open();
        //        SqlDataAdapter ReaderMast = new SqlDataAdapter();
        //        ReaderMast.SelectCommand = CmdMaster;
        //        DataSet ds = new DataSet();

        //        for(int i=0;i<=ds.Tables.Count;i++)
        //        {
        //           var val1 = ds.Tables[i].Columns[""];
        //        }

        //        return (IEnumerable<BalanceSheetResponse>)ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


       


        public async Task<string> BalanceSheetResponse(BalanceSheetRequest obj)
        {
            try
            {

                SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
                SqlCommand CmdMaster = new SqlCommand();
                CmdMaster = cnn1.CreateCommand();
                string sSql = "[GetBalanceSheet] '" + obj.StartDate + "','" + obj.EndDate + "'";
                CmdMaster.CommandText = sSql;
                cnn1.Open();
                SqlDataAdapter ReaderMast = new SqlDataAdapter();
                ReaderMast.SelectCommand = CmdMaster;
                DataSet ds = new DataSet();

                ReaderMast.Fill(ds);
                //return (IEnumerable<StatementOfAccountSummaryResponse>)ds;
                return JsonConvert.SerializeObject(ds, Formatting.Indented);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
