using AntDesign;
using WeFin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AntDesign.IconType;
//using static Java.Util.Jar.Attributes;

namespace WeFin.Data;

public class BillDBService
{
    private SQLiteAsyncConnection conn;
    private string StatusMessage = "";

    async Task Init()
    {
        if (conn is not null)
            return;

        conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await conn.CreateTableAsync<Bill>();
    }

    public async Task<List<Bill>> GetAllBillsAsync(String accountNo)
    {
        try
        {
            await Init();
            return await conn.Table<Bill>().Where(t => t.AccountNo == accountNo).ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            throw new Exception(nameof(ex) + StatusMessage);
        }
        return new List<Bill>();
    }

    public async Task<List<Bill>> GetBillJournalReportAsync(ReportCondition reportCondition)
    {
        try
        {
            // connect database
            await Init();
            // get some bills in the reportCondition 
            String sql = "SELECT * FROM gl_bill t WHERE t.BillYear = " + reportCondition.Year
                     + " and t.AccountNo = '" + Setting.UserBasicDetail.AccountNo +"'"
                     + " and t.BillMonth >= " + reportCondition.StartMonth
                     + " and t.BillMonth <= " + reportCondition.EndMonth;


            if (!String.IsNullOrEmpty(reportCondition.BillDirection))
            {
                sql += " and t.BillDirection = " + reportCondition.BillDirection;
            }
            if (!String.IsNullOrEmpty(reportCondition.BillNo))
            {
                sql += " and t.BillNo = '" + reportCondition.BillNo + "'";
            }
            if (!String.IsNullOrEmpty(reportCondition.BillType))
            {
                sql += " and t.BillType = '" + reportCondition.BillType +"'";
            }
            if (!String.IsNullOrEmpty(reportCondition.BillName))
            {
                sql += " and t.BillName like '" + reportCondition.BillName + "'%";
            }
            if (!String.IsNullOrEmpty(reportCondition.BillAbstract))
            {
                sql += " and t.BillAbstract like '" + reportCondition.BillAbstract + "'%";
            }
            if (!String.IsNullOrEmpty(reportCondition.Remark))
            {
                sql += " and t.Remark like '" + reportCondition.Remark + "'%";
            }
            sql += " order by BillDate,_id";
            List<Bill> BillList = await conn.QueryAsync<Bill>(sql);
            return BillList;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to get some bill data. {0}", ex.Message);
            Console.WriteLine(nameof(ex) + StatusMessage);
        }
        return new List<Bill>();        
    }

    public async Task<List<LedgerReportEntity>> GetBillsLedgerSumAsync(ReportCondition reportCondition)
    {
        try
        {
            // connect database
            await Init();
            // get bills total amount in the reportCondition 
            String sql = "SELECT t.BillYear,t.AccountNo,t.BillMonth,sum(t.income) TotalIncome, sum(t.Payment) TotalPayment"
                + " FROM gl_bill t WHERE t.BillYear = " + reportCondition.Year
                + " AND t.BillMonth >= " + reportCondition.StartMonth
                + " AND t.BillMonth <= " + reportCondition.EndMonth
                + " AND t.AccountNo = '" + Setting.UserBasicDetail.AccountNo +"'";

            if (!String.IsNullOrEmpty(reportCondition.BillType))
            {
                sql += " AND t.BillType = '" + reportCondition.BillType + "'";
            }
           
            sql += " GROUP BY t.BillYear,t.AccountNo,t.BillMonth";           
            List<LedgerReportEntity> ledgerList = await conn.QueryAsync<LedgerReportEntity>(sql);
            return ledgerList;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to get some ledger data. {0}", ex.Message);
            Console.WriteLine((nameof(ex) + StatusMessage));
        }
        return new List<LedgerReportEntity>();
    }

    public async Task<Bill> GetBillByIdAsync(int id)
    {
        try
        {
            // connect database
            await Init();
            // basic validation to ensure a billType exist
            if (id == 0)
                throw new Exception("Valid billType information required");
            // save a billType         
            return await conn.Table<Bill>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            Console.WriteLine((nameof(ex) + StatusMessage));
        }
        return new Bill();
    }

    public async Task<int> SaveBillAsync(Bill bill)
    {
        int result = 0;
        try
        {
            // connect database
            await Init();

            // basic validation to ensure a billType exist
            if (null == bill)
                throw new Exception("Valid bill information required");
            // save a bill         
            if (bill.IsAdd == 0)
            {
                return await conn.UpdateAsync(bill);
            }
            else
            {
                return await conn.InsertAsync(bill);
            }
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            Console.WriteLine((nameof(ex) + StatusMessage));
        }
        return result;
    }

    public async Task<int> DeleteBillAsync(Bill bill)
    {
        int result = 0;
        try
        {
            // connect database
            await Init();

            // basic validation to ensure a billType exist
            if (null == bill)
                throw new Exception("Valid billType information required");
            // delete a billType
            return await conn.DeleteAsync(bill);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            Console.WriteLine((nameof(ex) + StatusMessage));
        }
        return result;
    }
}
