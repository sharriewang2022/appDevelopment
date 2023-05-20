using AntDesign;
using WeiFin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AntDesign.IconType;
//using static Java.Util.Jar.Attributes;

namespace WeiFin.Data;

public class BillTypeDBService
{
    private SQLiteAsyncConnection conn;
    private string StatusMessage = "";
    private static readonly string[] BillTypeArray = new[]
    {
        "salary","business cost", "material fees", "journey costs", "entertainment fees",
        "education fees", "property fees", "water costs", "electricity costs",
        "heating costs", "Fuel cost", "others"
    };

    public BillTypeDBService()
    {
    }

    async Task Init()
    {
        if (conn is not null)
            return;

        conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await conn.CreateTableAsync<BillType>();
    }

    public async Task<List<BillType>> GetAllBillTypesAsync()
    {
        try
        {
            await Init();
            List<BillType> billTypeList = new List<BillType>();
            billTypeList = await conn.Table<BillType>().ToListAsync();
            if (billTypeList == null || billTypeList.Count == 0) {
                //initial bill type in the database
                for (int i = 1; i <= BillTypeArray.Length; i++) {                 
                    BillType billType = new BillType();
                    billType.BillTypeNo = i + "";
                    billType.BillTypeName = BillTypeArray[i];
                    billTypeList.Add(billType);
                    conn.InsertAsync(billType);
                }                
            }
            billTypeList.Sort((p1, p2) => {
                if (p1.BillTypeName != p2.BillTypeName) 
                {
                    return p1.BillTypeName.CompareTo(p2.BillTypeName);
                } 
                else if (p1.BillTypeNo != p2.BillTypeNo) 
                {
                    return p1.BillTypeNo.CompareTo(p2.BillTypeNo);
                } 
                else return 0;
            });            
            return billTypeList;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<BillType>();
    }

    public async Task<List<BillType>> GetBillTypeNotDoneAsync()
    {        
        try
        {
            // connect database
            await Init();      
            // get some billType
            return await conn.Table<BillType>().Where(t => t.Id > 0).ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<BillType>();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public async Task<BillType> GetBillTypeByIdAsync(int id)
    {
        try
        {
            // connect database
            await Init();

            // basic validation to ensure a billType exist
            if (id == 0)
                throw new Exception("Valid billType information required");
            // save a billType         
            return await conn.Table<BillType>().Where(i => i.Id == id).FirstOrDefaultAsync();
                }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new BillType();
    }

    public async Task<int> SaveBillTypeAsync(BillType billType)
    {
        int result = 0;
        try
        {
            // connect database
            await Init();

            // basic validation to ensure a billType exist
            if (null == billType)
                throw new Exception("Valid billType information required");
            // save a billType         
            if (billType.IsAdd != 0)
            {
                return await conn.UpdateAsync(billType);
            }
            else
            {
                return await conn.InsertAsync(billType);
            }
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }

    public async Task<int> DeleteBillTypeAsync(BillType billType)
    {
        int result = 0;
        try
        {
            // connect database
            await Init();
            
            // basic validation to ensure a billType exist
            if (null == billType)
                throw new Exception("Valid billType information required");
            // delete a billType
            return await conn.DeleteAsync(billType);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }
}
