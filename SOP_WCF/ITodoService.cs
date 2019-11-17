using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOP_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITodoService" in both code and config file together.
    [ServiceContract]
    public interface ITodoService
    {
        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        [FaultContract(typeof(NoAvailableTodoFault))]
        List<TodoModel> ListAll(UserClient client);

        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        [FaultContract(typeof(TodoNotFoundFault))]
        List<TodoModel> ListById(string id, UserClient client);

        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        bool Insert(string title, string body, string author, string deadline, string priority, UserClient client);

        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        bool Update(string id, string title, string body, string author, string deadline, string priority, UserClient client);

        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        bool Delete(string id, UserClient client);

        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        UserClient Login(string username, string password);

        [OperationContract]
        [FaultContract(typeof(LoginFailedFault))]
        bool Logout(UserClient client);
    }
}
