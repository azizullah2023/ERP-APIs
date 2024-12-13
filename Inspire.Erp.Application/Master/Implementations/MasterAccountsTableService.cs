using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Infrastructure;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public class MasterAccountsTableService : IMasterAccountsTableService
    {
        private IRepository<MasterAccountsTable> masteraccountstablerepository;
        private IRepository<ItemStockType> itemStockRepository;
        public MasterAccountsTableService(IRepository<MasterAccountsTable> _masteraccountstablerepository, IRepository<ItemStockType> _itemStockRepository)
        {
            masteraccountstablerepository = _masteraccountstablerepository;
            itemStockRepository = _itemStockRepository;
        }
        public IEnumerable<MasterAccountsTable> InsertAccount(MasterAccountsTable MasterAccountsTable)
        {
            bool valid = false;
            try
            {
                valid = true;
                MasterAccountsTable.MaSno = Convert.ToInt32(masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt32(x.MaSno) > 0)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : o.MaSno)) + 1;

                long AccNo = masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt64(x.MaAccNo) > 0 && x.MaRelativeNo == MasterAccountsTable.MaRelativeNo)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : Convert.ToInt64(o.MaAccNo.Substring(o.MaAccNo.Length - 3, 3))) + 1;


                MasterAccountsTable.MaAccNo = MasterAccountsTable.MaRelativeNo + String.Format("{0:000}", AccNo);

                var deletePdcDtls = masteraccountstablerepository.GetAsQueryable()
                   .Where(m => m.MaRelativeNo == MasterAccountsTable.MaRelativeNo).Select(b => b).ToList();

                int maLen = MasterAccountsTable.MaAccNo.Length;


                if (maLen > 8)
                {
                    var Shead = masteraccountstablerepository.GetAsQueryable().Where(n => n.MaAccNo == deletePdcDtls.First().MaRelativeNo)
                   .Select(a => a).ToList();

                    var Suhead = masteraccountstablerepository.GetAsQueryable().Where(v => v.MaAccNo == Shead.First().MaRelativeNo).Select(S => S).ToList();

                    var MAhead = masteraccountstablerepository.GetAsQueryable().Where(j => j.MaAccNo == Suhead.First().MaRelativeNo).Select(q => q).ToList();

                    var mainhead = MAhead.FirstOrDefault().MaAccName;
                    var Subhead = Suhead.FirstOrDefault().MaAccName;

                    MasterAccountsTable.MaMainHead = mainhead;
                    MasterAccountsTable.MaSubHead = Subhead;
                    MasterAccountsTable.MaStatus = AccountStatus.AccStatus;
                    if (MasterAccountsTable.MaAccType == ItemMasterStatus.Group || MasterAccountsTable.MaAccType == AccountMasterStatus.SubItem)
                    {

                        MasterAccountsTable.MaImageKey = ItemMasterStatus.GroupItem;
                    }
                    else
                    {
                        MasterAccountsTable.MaImageKey = ItemMasterStatus.SubItem;
                    }

                }
                else if (maLen <= 8)
                {
                    var Shead = masteraccountstablerepository.GetAsQueryable().Where(n => n.MaAccNo == deletePdcDtls.First().MaRelativeNo)
                   .Select(a => a).ToList();

                    var MAhead = masteraccountstablerepository.GetAsQueryable().Where(j => j.MaAccNo == Shead.First().MaRelativeNo).Select(q => q).ToList();

                    var mainhead = MAhead.FirstOrDefault().MaAccName;
                    var Subhead = Shead.FirstOrDefault().MaAccName;

                    MasterAccountsTable.MaMainHead = mainhead;
                    MasterAccountsTable.MaSubHead = Subhead;
                    MasterAccountsTable.MaStatus = AccountStatus.AccStatus;
                    if (MasterAccountsTable.MaAccType == ItemMasterStatus.Group || MasterAccountsTable.MaAccType == AccountMasterStatus.SubItem)
                    {

                        MasterAccountsTable.MaImageKey = ItemMasterStatus.GroupItem;
                    }
                    else
                    {
                        MasterAccountsTable.MaImageKey = ItemMasterStatus.SubItem;
                    }

                }


                //", 15);   ;
                //MasterAccountsTable.MasterAccountsTableSno = Convert.ToIntt32(masteraccountstablerepository.GetAsQueryable()
                //                             .Where(x => x.MasterAccountsTableSno>0)
                //                             .DefaultIfEmpty()
                //                             .Max(o => o == null ? 0 : o.MasterAccountsTableSno)) + 1;

                masteraccountstablerepository.Insert(MasterAccountsTable);
                return this.GetAllAccount();
            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }

        }

        public MasterAccountsTable NewAccount(MasterAccountsTable MasterAccountsTable)
        {
            bool valid = false;
            try
            {
                valid = true;
                MasterAccountsTable.MaSno = Convert.ToInt32(masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt32(x.MaSno) > 0)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : o.MaSno)) + 1;
                var accCheckDtls = masteraccountstablerepository.GetAsQueryable()
                    .Where(m => m.MaRelativeNo == MasterAccountsTable.MaRelativeNo).Select(b => b).ToList();



                long AccNo = masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt64(x.MaAccNo) > 0 && x.MaRelativeNo == MasterAccountsTable.MaRelativeNo)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : Convert.ToInt64(o.MaAccNo.Substring(o.MaAccNo.Length - 3, 3))) + 1;

                MasterAccountsTable.MaAccType = AccountStatus.Approved;

                MasterAccountsTable.MaAccNo = MasterAccountsTable.MaRelativeNo + String.Format("{0:000}", AccNo);

                int maLen = MasterAccountsTable.MaAccNo.Length;


                if (maLen > 5)
                {


                    if (accCheckDtls.Count > 0)
                    {
                        var Shead = masteraccountstablerepository.GetAsQueryable().Where(n => n.MaAccNo == accCheckDtls.First().MaRelativeNo)
                       .Select(a => a).ToList();
                        var Suhead = masteraccountstablerepository.GetAsQueryable().Where(v => v.MaAccNo == Shead.First().MaRelativeNo).Select(S => S).ToList();

                        var MAhead = masteraccountstablerepository.GetAsQueryable().Where(j => j.MaAccNo == Suhead.First().MaRelativeNo).Select(q => q).ToList();

                        var mainhead = MAhead.FirstOrDefault().MaAccName;
                        var Subhead = Suhead.FirstOrDefault().MaAccName;

                        MasterAccountsTable.MaMainHead = mainhead;
                        MasterAccountsTable.MaSubHead = Subhead;
                    }
                    else
                    {
                        var Shead = masteraccountstablerepository.GetAsQueryable().Where(n => n.MaAccNo == MasterAccountsTable.MaRelativeNo)
                        .Select(a => a).ToList();
                        var Suhead = masteraccountstablerepository.GetAsQueryable().Where(v => v.MaAccNo == Shead.First().MaRelativeNo).Select(S => S).ToList();

                        var MAhead = masteraccountstablerepository.GetAsQueryable().Where(j => j.MaAccNo == Suhead.First().MaRelativeNo).Select(q => q).ToList();

                        var mainhead = MAhead.FirstOrDefault().MaAccName;
                        var Subhead = Suhead.FirstOrDefault().MaAccName;

                        MasterAccountsTable.MaMainHead = mainhead;
                        MasterAccountsTable.MaSubHead = Subhead;
                    }

                    MasterAccountsTable.MaStatus = AccountStatus.AccStatus;
                    if (MasterAccountsTable.MaAccType == ItemMasterStatus.Group)
                    {

                        MasterAccountsTable.MaImageKey = ItemMasterStatus.GroupItem;
                    }
                    else
                    {
                        MasterAccountsTable.MaImageKey = ItemMasterStatus.SubItem;
                    }

                }
                else if (maLen <= 5)
                {
                    if (accCheckDtls.Count > 0)
                    {
                        var Shead = masteraccountstablerepository.GetAsQueryable().Where(n => n.MaAccNo == accCheckDtls.First().MaRelativeNo)
                                    .Select(a => a).ToList();

                        var MAhead = masteraccountstablerepository.GetAsQueryable().Where(j => j.MaAccNo == Shead.First().MaRelativeNo).Select(q => q).ToList();

                        var mainhead = MAhead.FirstOrDefault().MaAccName;
                        var Subhead = Shead.FirstOrDefault().MaAccName;

                        MasterAccountsTable.MaMainHead = mainhead;
                        MasterAccountsTable.MaSubHead = Subhead;
                        MasterAccountsTable.MaStatus = AccountStatus.AccStatus;
                    }
                    else
                    {
                        var Shead = masteraccountstablerepository.GetAsQueryable().Where(n => n.MaAccNo == MasterAccountsTable.MaRelativeNo)
                                    .Select(a => a).ToList();

                        var MAhead = masteraccountstablerepository.GetAsQueryable().Where(j => j.MaAccNo == Shead.First().MaRelativeNo).Select(q => q).ToList();

                        var mainhead = MAhead.FirstOrDefault().MaAccName;
                        var Subhead = Shead.FirstOrDefault().MaAccName;

                        MasterAccountsTable.MaMainHead = mainhead;
                        MasterAccountsTable.MaSubHead = Subhead;
                        MasterAccountsTable.MaStatus = AccountStatus.AccStatus;
                    }
                    if (MasterAccountsTable.MaAccType == ItemMasterStatus.Group)
                    {

                        MasterAccountsTable.MaImageKey = ItemMasterStatus.GroupItem;
                    }
                    else
                    {
                        MasterAccountsTable.MaImageKey = ItemMasterStatus.SubItem;
                    }

                }


                masteraccountstablerepository.Insert(MasterAccountsTable);
                return MasterAccountsTable;
            }
            catch (Exception ex)

            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }

        }

        public MasterAccountsTable UpdateNewAccount(MasterAccountsTable MasterAccountsTable)
        {
            bool valid = false;
            try
            {
                masteraccountstablerepository.Update(MasterAccountsTable);
                valid = true;
                return MasterAccountsTable;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }

        }
        public IEnumerable<MasterAccountsTable> UpdateAccount(MasterAccountsTable masterAccountsTable)
        {
            bool valid = false;
            try
            {
                var child = masteraccountstablerepository.GetAsQueryable().Where(a => a.MaRelativeNo == masterAccountsTable.MaAccNo).Select(k => k).ToList();
                masteraccountstablerepository.Delete(masterAccountsTable);
                if (masterAccountsTable.MaRelativeNo == null)
                {
                    masterAccountsTable.MaRelativeNo = "0";
                    masterAccountsTable.MaMainHead = masterAccountsTable.MaAccName ?? "";
                    masterAccountsTable.MaSubHead = masterAccountsTable.MaAccName ?? "";
                    masterAccountsTable.MaAccType = "G";
                }
                var AccNo = masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt64(x.MaAccNo) > 0 && x.MaRelativeNo == masterAccountsTable.MaRelativeNo)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : Convert.ToInt64(o.MaAccNo.Substring(o.MaAccNo.Length - 3, 3))) + 1;
                //masterAccountsTable.MasterAccountsTableSno = null;
                masterAccountsTable.MaAccNo = masterAccountsTable.MaRelativeNo + String.Format("{0:000}", AccNo);
                masteraccountstablerepository.Insert(masterAccountsTable);
                if (child.Count > 0)
                {
                    foreach (var item in child)
                    {
                        item.MaRelativeNo = masterAccountsTable.MaAccNo;
                        masteraccountstablerepository.Update(item);
                    }
                }
                //masteraccountstablerepository.Update(new MasterAccountsTable { 
                //    MasterAccountsTableAccNo=masterAccountsTable.MasterAccountsTableAccNo,
                //    MasterAccountsTableAccType = masterAccountsTable.MasterAccountsTableAccType,
                //    MasterAccountsTableAccName = masterAccountsTable.MasterAccountsTableAccName,
                //    MasterAccountsTableRelativeNo= masterAccountsTable.MasterAccountsTableRelativeNo
                //});
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllAccount();
        }

        //public IEnumerable<ItemStockType> GetAllStockType()
        //{
        //    return itemStockRepository.GetAll();
        //}
        public IEnumerable<MasterAccountsTable> DeleteAccount(MasterAccountsTable MasterAccountsTable)
        {
            bool valid = false;
            try
            {
                masteraccountstablerepository.Delete(MasterAccountsTable);
                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return this.GetAllAccount();
        }

        public IEnumerable<MasterAccountsTable> GetAllAccount()
        {
            IEnumerable<MasterAccountsTable> MasterAccountsTable;
            try
            {
                MasterAccountsTable = masteraccountstablerepository.GetAsQueryable().Where(k => k.MaAccNo != "" && (k.MaDelStatus == false || k.MaDelStatus == null)
                && k.MaAccType == AccountMasterStatus.Group
                ).Select(k => k);
                //|| k.MasterAccountsTableAccType == AccountMasterStatus.   ,
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //cityrepository.Dispose();
            }
            return MasterAccountsTable;
        }

        public IEnumerable<MasterAccountsTable> GetAllGroup()
        {
            IEnumerable<MasterAccountsTable> MasterAccountsTable;
            try
            {
                MasterAccountsTable = masteraccountstablerepository.GetAsQueryable().Where(k => k.MaAccNo != "" && (k.MaDelStatus == false || k.MaDelStatus == null)
                && k.MaAccType == AccountMasterStatus.Group || k.MaAccType == AccountMasterStatus.SubItem).Select(k => k);
                //|| k.MasterAccountsTableAccType == AccountMasterStatus.SubItem
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return MasterAccountsTable;
        }
        public IEnumerable<MasterAccountsTable> GetAllAccountNotGroup()
        {
            IEnumerable<MasterAccountsTable> MasterAccountsTable;
            try
            {
                MasterAccountsTable = masteraccountstablerepository.GetAsQueryable().Where(k => k.MaAccNo != "" && (k.MaDelStatus == false || k.MaDelStatus == null)
                && k.MaAccType != AccountMasterStatus.Group).Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return MasterAccountsTable;
        }

        public IEnumerable<MasterAccountsTable> GetAllAccountById(string id)
        {
            IEnumerable<MasterAccountsTable> MasterAccountsTable;
            try
            {
                MasterAccountsTable = masteraccountstablerepository.GetAsQueryable().Where(k => k.MaRelativeNo == id && (k.MaDelStatus == false || k.MaDelStatus == null))
                                                            //.Include(k => k.ItemImages)
                                                            //.Include(k => k.ItemPriceLevelDetails)
                                                            //.Include(k => k.UnitDetails)
                                                            .Select(k => k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return MasterAccountsTable;

        }


        //GetAllItemMasterById
        public MasterAccountsTable GetAllAccountMasterById(string id)
        {
            MasterAccountsTable MasterAccountsTable;
            try
            {
                MasterAccountsTable = masteraccountstablerepository.GetAsQueryable().Where(k => k.MaAccNo == id && (k.MaDelStatus == false || k.MaDelStatus == null))
                                                            //.Include(k => k.ItemImages)
                                                            //.Include(k => k.ItemPriceLevelDetails)
                                                            //.Include(k => k.UnitDetails)
                                                            .Select(k => k)
                                                            .SingleOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //cityrepository.Dispose();
            }
            return MasterAccountsTable;

        }


        public IEnumerable<MasterAccountsTable> GetAccountMastersByName(string name)
        {
            IEnumerable<MasterAccountsTable> accountMasters = masteraccountstablerepository.GetAsQueryable().Where(k => k.MaAccName.Contains(name)
            && (k.MaDelStatus == false || k.MaDelStatus == null)
            && k.MaAccNo != "" && k.MaAccType != AccountMasterStatus.Group).Select(k => k);

            return accountMasters;
        }

        public IEnumerable<MasterAccountsTable> InsertAccountGroup(MasterAccountsTable masterAccountsTable)
        {
            try
            {
                masterAccountsTable.MaSno = Convert.ToInt32(masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt32(x.MaSno) > 0)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : o.MaSno)) + 1;
                var AccNo = masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt64(x.MaAccNo) > 0 && x.MaRelativeNo == masterAccountsTable.MaRelativeNo)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : Convert.ToInt64(o.MaAccNo.Substring(o.MaAccNo.Length - 3, 3))) + 1;
                if (masterAccountsTable.MaRelativeNo != null && masterAccountsTable.MaRelativeNo != "0")
                {
                    var mainHead = masteraccountstablerepository.GetAsQueryable().FirstOrDefault(x => x.MaAccNo == masterAccountsTable.MaRelativeNo).MaMainHead;
                    var subHead = masteraccountstablerepository.GetAsQueryable().FirstOrDefault(x => x.MaAccNo == masterAccountsTable.MaRelativeNo).MaSubHead;

                    masterAccountsTable.MaMainHead = mainHead ?? "";
                    masterAccountsTable.MaSubHead = subHead ?? "";
                    masterAccountsTable.MaAccType = "S";
                }
                else
                {
                    masterAccountsTable.MaRelativeNo = "0";
                    masterAccountsTable.MaMainHead = masterAccountsTable.MaAccName ?? "";
                    masterAccountsTable.MaSubHead = masterAccountsTable.MaAccName ?? "";
                    masterAccountsTable.MaAccType = "G";
                }
                masterAccountsTable.MaStatus = AccountStatus.AccStatus;

                if (masterAccountsTable.MaAccType == ItemMasterStatus.Group || masterAccountsTable.MaAccType == AccountMasterStatus.SubItem)
                {

                    masterAccountsTable.MaImageKey = ItemMasterStatus.GroupItem;
                }
                else
                {
                    masterAccountsTable.MaImageKey = ItemMasterStatus.SubItem;
                }

                masterAccountsTable.MaAccNo = masterAccountsTable.MaRelativeNo + String.Format("{0:00}", AccNo);

                masteraccountstablerepository.Insert(masterAccountsTable);
                return this.GetAllAccount();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public MasterAccountsTable AddNewAccount(MasterAccountsTable masterAccountsTable)
        {
            try
            {
                masterAccountsTable.MaSno = Convert.ToInt32(masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt32(x.MaSno) > 0)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : o.MaSno)) + 1;
                var AccNo = masteraccountstablerepository.GetAsQueryable()
                                             .Where(x => Convert.ToInt64(x.MaAccNo) > 0 && x.MaRelativeNo == masterAccountsTable.MaRelativeNo)
                                             .DefaultIfEmpty()
                                             .Max(o => o == null ? 0 : Convert.ToInt64(o.MaAccNo.Substring(o.MaAccNo.Length - 3, 3))) + 1;

                masterAccountsTable.MaAccType = AccountStatus.Approved;

                var mainHead = masteraccountstablerepository.GetAsQueryable().FirstOrDefault(x => x.MaAccNo == masterAccountsTable.MaRelativeNo).MaMainHead;
                var subHead = masteraccountstablerepository.GetAsQueryable().FirstOrDefault(x => x.MaAccNo == masterAccountsTable.MaRelativeNo).MaSubHead;
                masterAccountsTable.MaMainHead = mainHead ?? "";
                masterAccountsTable.MaSubHead = subHead ?? "";



                masterAccountsTable.MaAccNo = masterAccountsTable.MaRelativeNo + String.Format("{0:000}", AccNo);
                var exist = masteraccountstablerepository.GetAsQueryable().Where(a => a.MaAccNo == masterAccountsTable.MaAccNo).ToList();
                if (exist.Count > 0)
                {
                    masterAccountsTable.MaAccNo = masterAccountsTable.MaRelativeNo + String.Format("{0:000}", AccNo + 1);
                }
                if (masterAccountsTable.MaAccType == ItemMasterStatus.Group)
                {

                    masterAccountsTable.MaImageKey = ItemMasterStatus.GroupItem;
                }
                else
                {
                    masterAccountsTable.MaImageKey = ItemMasterStatus.SubItem;
                }

                masteraccountstablerepository.Insert(masterAccountsTable);
                return masterAccountsTable;

            }
            catch (Exception ex)
            {
                return new MasterAccountsTable();
            }
        }
    }
}
