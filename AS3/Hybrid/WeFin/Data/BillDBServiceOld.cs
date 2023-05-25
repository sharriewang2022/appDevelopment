using AntDesign;
using WeFin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AntDesign.IconType;

namespace WeFin.Data;

public class BillDBServiceOld
{
    private SQLiteConnection conn;
    private string _dbPath = "";
    private string StatusMessage = "";

    private void Init()
    {
        if (conn != null)
            return;
        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Bill>();
    }

    public void AddNewBill(Bill bill)
    {
        int result = 0;
        try
        {
            // connect database
            Init();
            // basic validation to ensure a bill was entered
            if (null == bill)
                throw new Exception("Valid bill information required");
            // add a new bill
            result = conn.Insert(new Bill { BillDate = DateTime.Today });
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
    }

    public List<Bill> GetAllBills()
    {
        try
        {
            Init();
            return conn.Table<Bill>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<Bill>();
    }

    public Bill GetByBillType(string billType)
    {
        try
        {
            // connect database
            Init();
            // basic validation to ensure a bill exist
            if (null == billType)
                throw new Exception("Valid bill information required");
           var bill = from u in conn.Table<Bill>()
                       where u.BillType == billType
                       select u;
            // get some bill
            return bill.FirstOrDefault();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new Bill();
    }

    public int UpdateBill(Bill bill)
    {
        int result = 0;
        try
        {
            // connect database
            Init();
            // basic validation to ensure a bill exist
            if (null == bill)
                throw new Exception("Valid bill information required");
            // update a bill
            result = conn.Update(bill);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }

    public int DeleteBill(Bill bill)
    {
        int result = 0;
        try
        {
            // connect database
            Init();
            // basic validation to ensure a bill exist
            if (null == bill)
                throw new Exception("Valid bill information required");
            // delete a bill
            result = conn.Delete<Bill>(bill);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }


}
