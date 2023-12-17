using System.Collections.Generic;
using System.Threading.Tasks;
using WannaBePrincipal.Models;

public interface IUserModel
{
    Task<string> AddUser(UserData user);
    Task<UserData> GetUser(string userID);
    Task<bool> EditUser(string userID, UserData user);
    Task<bool> DeleteUser(string userID);
    Task<List<UserData>> GetUsersFromDB();
    Task DeleteCollection(string collectionName, int batchSize = 100);
}