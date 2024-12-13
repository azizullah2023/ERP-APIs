using Inspire.Erp.Application.Account.Implementations;
using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inspire.Erp.Application.Store.Implementation
{
    
    public class VoucherPrinting2 
    {
        private IRepository<VoucherPrintingEntity> _VoucherPrintingRepository;

        public VoucherPrinting2(IRepository<VoucherPrintingEntity> VoucherPrintingRepository)
        {
            this._VoucherPrintingRepository = VoucherPrintingRepository;
        }
        
        public IEnumerable<VoucherPrintingResponse> VoucherPrintingResponse(VoucherPrintingRequest obj)
        {            
            try
            {
                List<VoucherPrintingEntity> Results = _VoucherPrintingRepository.GetAsQueryable()
                                                                   .Where(x => (x.AccountsTransactions_VoucherNo >= obj.voucherNoFROM
                                                                   && x.AccountsTransactions_VoucherNo <= obj.VoucherNOTo)
                                                                   && (x.AccountsTransactions_TransDate >= obj.StartDate && x.AccountsTransactions_TransDate <= obj.EndDate)
                                                                   && (x.AccountsTransactions_VoucherType == obj.VoucherType
                                                                   || x.Vouchers_Numbers_V_NO_NU == obj.VoucherNo_NU
                                                                   || x.AccountsTransactions_CheqNo == obj.chequeNo))
                                                                   .ToList();

                SqlConnection cnn1 = new SqlConnection("Server=DESKTOP-0B84I8A;Database=InspireERPDB(awais);Integrated Security=True");
                SqlCommand CmdMaster = new SqlCommand();
                CmdMaster = cnn1.CreateCommand();
                string sSql = "[GetVoucherPrinting] '" + obj.voucherNoFROM + "','" + obj.VoucherNOTo + "','" + obj.VoucherType + "','" + obj.StartDate + "','" + obj.EndDate + "','" + obj.chequeNo + "','" + obj.VoucherNo_NU + "'";
                CmdMaster.CommandText = sSql;
                cnn1.Open();
                SqlDataReader ReaderMast = CmdMaster.ExecuteReader();
                ReaderMast.Read();
                return (IEnumerable<VoucherPrintingResponse>)obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}










