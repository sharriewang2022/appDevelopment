using FinanceWasmApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace FinanceWasmApp.Data;

public class BillDBService
{
    private List<Bill> bills = new List<Bill>();
    private string StatusMessage = "";

    [Inject]
    public HttpClient Http { get; set; }

    public async Task<List<Bill>> GetAllBillsAsync()
    {        
        try
        {
            List<Bill> billList = await Http.GetFromJsonAsync<List<Bill>>("api/Bills/getAllBillsAsync");
            if(billList == null){ 
                return new List<Bill>();
            }
            return billList;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<Bill>();
    }     

    public async Task<Bill> GetBillByIdAsync(int id)
    {
        try
        {
            // basic validation to ensure a billType exist
            if (id == 0)
                throw new Exception("Valid billType information required");
            // get a billType
            Bill? oneBill = await Http.GetFromJsonAsync<Bill>("api/Bills/getBillDetailsAsync?id=" + id);                    
            return oneBill;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve a bill detail. {0}", ex.Message);
        }
        return new Bill();
    }

    public async Task<int> SaveBillAsync(Bill bill)
    {
        int result = 0;
        try
        {
            // basic validation to ensure a billType exist
            if (null == bill)
                throw new Exception("Valid bill information required");

            // save a bill         
            if (bill.IsAdd != 0)
            {            
                await Http.PostAsJsonAsync<Bill>("api/Bills/updateOneBillAsync", bill);
                //1 means update success
                return 1;
            }
            else
            {
                await Http.PostAsJsonAsync<Bill>("api/Bills/createOneBillAsync", bill);
                //2 means insert success
                return 2;
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
            // basic validation to ensure a billType exist
            if (null == bill)
                throw new Exception("Valid billType information required");
            // delete a billType
            await Http.PostAsJsonAsync<Bill>("api/Bills/deleteOneBillAsync", bill);
            //1 means success
            return 1;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }  
}
