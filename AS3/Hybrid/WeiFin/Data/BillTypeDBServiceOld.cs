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

public class BillTypeDBServiceOld
{
    private SQLiteConnection conn;
    private string _dbPath = "";
    private string StatusMessage = "";

    private void Init()
    {
        if (conn != null)
            return;
        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<BillType>();
    }
    public void AddNewBillType(BillType billType)
    {
        int result = 0;
        try
        {
            // connect database
            Init();
            // basic validation to ensure a billType was entered
            if (null == billType)
                throw new Exception("Valid name required");
            // add a new billType
            result = conn.Insert(new BillType {});
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
    }

    public List<BillType> GetAllBillTypes()
    {
        try
        {
            Init();
            return conn.Table<BillType>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<BillType>();
    }

    public BillType GetByBillTypeId(string billTypeNo)
    {
        try
        {
            // connect database
            Init();
            // basic validation to ensure a billType exist
            if (null == billTypeNo)
                throw new Exception("Valid billType information required");
           var billType = from u in conn.Table<BillType>()
                       where u.BillTypeNo == billTypeNo
                       select u;
            // get some billType
            return billType.FirstOrDefault();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new BillType();
    }

    public int UpdateBillType(BillType billType)
    {
        int result = 0;
        try
        {
            // connect database
            Init();
            // basic validation to ensure a billType exist
            if (null == billType)
                throw new Exception("Valid billType information required");
            // update a billType
            result = conn.Update(billType);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }

    public int DeleteBillType(BillType billType)
    {
        int result = 0;
        try
        {
            // connect database
            Init();
            // basic validation to ensure a billType exist
            if (null == billType)
                throw new Exception("Valid billType information required");
            // delete a billType
            result = conn.Delete<BillType>(billType);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }


}
