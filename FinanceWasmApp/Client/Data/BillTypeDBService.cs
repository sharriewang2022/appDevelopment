using FinanceWasmApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Json;


namespace FinanceWasmApp.Data;

public class BillTypeDBService
{
    [Inject]
    public HttpClient Http { get; set; }
    private string StatusMessage = "";

    public async Task<List<BillType>> GetAllBillTypesAsync()
    {   
        try
        {             
            List<BillType> billTypeList = new List<BillType>();     
            try {
                billTypeList = await Http.GetFromJsonAsync<List<BillType>>("api/BillTypes/getAllBillTypsAsync");
            }
            catch(Exception ex){
                throw ex;
            }
            /*
            if (billTypeArray == null || billTypeArray.Length == 0) {
                //initial bill type in the database
                for (int i = 1; i <= BillTypeArray.Length; i++) {                 
                    BillType billType = new BillType();
                    billType.BillTypeNo = i + "";
                    billType.BillTypeName = BillTypeArray[i];
                    billTypeList.Add(billType);
                    await Http.PostAsJsonAsync<BillType>("api/BillTypes/createOneBillTypeAsync", billType);
                }                
            }*/
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

    public async Task<BillType> GetBillTypeByIdAsync(int id)
    {
        try
        {
            // basic validation to ensure a billType exist
            if (id == 0)
                throw new Exception("Valid billType information required");
            // get a billType by id
            return await Http.GetFromJsonAsync<BillType>("api/BillTypes/getBillTypeDetailsAsync?id=" + id);
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
            // basic validation to ensure a billType exist
            if (null == billType)
                throw new Exception("Valid billType information required");
            // save a billType         
            if (billType.IsAdd != 0)
            {
                //update a billtype
                await Http.PostAsJsonAsync<BillType>("api/BillTypes/updateOneBillTypeAsync", billType);
                return 1;
            }
            else
            {
                //insert a billtype
                await Http.PostAsJsonAsync<BillType>("api/BillTypes/createOneBillTypeAsync", billType);
                return 2;
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
            // basic validation to ensure a billType exist
            if (null == billType)
                throw new Exception("Valid billType information required");
            // delete a billType
            await Http.PostAsJsonAsync<BillType>("api/BillTypes/deleteOneBillTypeAsync", billType);
            return 1;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }    
}
