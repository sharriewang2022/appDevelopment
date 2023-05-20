using AntDesign;
using WeiFin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AntDesign.IconType;
using Newtonsoft.Json;

namespace WeiFin.Data;

public class UserRegisterDBService
{
    private SQLiteAsyncConnection conn;
    private string StatusMessage = "";

    async Task Init()
    {
        if (conn is not null)
            return;

        conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await conn.CreateTableAsync<UserRegister>();
    }

    public async Task<List<UserRegister>> GetAllUserRegistersAsync()
    {
        try
        {
            await Init();
            return await conn.Table<UserRegister>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new List<UserRegister>();
    }

    public async Task<List<UserLogin>> GetAccountNosByUserAsync(UserLogin userLogin)
    {
        try
        {
            // connect database
            await Init();
            // get one user's all accountNo
            String sql = "SELECT * FROM gl_User WHERE UserName = '"+ userLogin.UserName
                + "' and UserPassword = '" + userLogin.UserPassword +"'";
            return await conn.QueryAsync<UserLogin>(sql);
            //return await conn.Table<UserRegister>().Where(t => t.Id>0).ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data accountNos. {0}", ex.Message);
        }
        return new List<UserLogin>();

        
    }

    public async Task<UserRegister> GetUserRegisterByIdAsync(String userName, String userPassword)
    {
        try
        {
            // connect database
            await Init();
            // basic validation to ensure an User exist
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(userPassword))
                throw new Exception("Valid user login information required");
            // save a UserRegisterType         
            return await conn.Table<UserRegister>().
                Where(i => i.UserName == userName && i.UserPassword == userPassword).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return new UserRegister();
    }

    public async Task<int> SaveUserRegisterAsync(UserRegister userRegister)
    {
        int result = 0;
        try
        {
            // connect database
            await Init();

            // basic validation to ensure a UserRegisterType exist
            if (null == userRegister)
                throw new Exception("Valid user login information required");
            // save a UserRegister         
            if (userRegister.IsAdd != 0)
            {
                return await conn.UpdateAsync(userRegister);
            }
            else
            {
                return await conn.InsertAsync(userRegister);
            }
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }

    public async Task<int> DeleteUserRegisterAsync(UserRegister userRegister)
    {
        int result = 0;
        try
        {
            // connect database
            await Init();

            // basic validation to ensure a userRegister exist
            if (null == userRegister)
                throw new Exception("Valid user information required");
            // delete a UserRegisterType
            return await conn.DeleteAsync(userRegister);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
        return result;
    }

    public async Task<bool> RefreshToken()
    {
        bool isTokenRefreshed = false;
        using (var client = new HttpClient())
        {


            var serializedStr = JsonConvert.SerializeObject(new UserLogin
            {
                RefreshToken = Setting.UserBasicDetail.RefreshToken,
                AccessToken = Setting.UserBasicDetail.AccessToken
            });

            try
            {
                // var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));

                // string contentStr = await response.Content.ReadAsStringAsync();
                Random random = new();
                Setting.UserBasicDetail.AccessToken = Setting.UserBasicDetail.Name + random.Next();
                Setting.UserBasicDetail.RefreshToken = Setting.UserBasicDetail.Name + random.Next();

                string userDetailsStr = JsonConvert.SerializeObject(Setting.UserBasicDetail);
                await SecureStorage.SetAsync(nameof(Setting.UserBasicDetail), userDetailsStr);
                isTokenRefreshed = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        return isTokenRefreshed;
    }
}
