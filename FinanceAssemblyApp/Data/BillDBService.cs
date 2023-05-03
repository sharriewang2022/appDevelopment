using AntDesign;
using FinanceAssemblyApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AntDesign.IconType;
//using static Java.Util.Jar.Attributes;

namespace FinanceAssemblyApp.Data;

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

    public async Task<List<Bill>> GetAllBillsAsync()
    {
        try
        {
            await Init();
            return await conn.Table<Bill>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<Bill>();
    }

    public async Task<List<Bill>> GetBillsNotDoneAsync()
    {
        try
        {
            // connect database
            await Init();
            // get some billType
            return await conn.Table<Bill>().Where(t => t.Id>0).ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<Bill>();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
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
            if (bill.IsAdd != 0)
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
        }
        return result;
    }
}
