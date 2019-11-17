using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RestSharp;

namespace SOP_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TodoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TodoService.svc or TodoService.svc.cs at the Solution Explorer and start debugging.
    public class TodoService : ITodoService
    {
        private string REST_URL = "http://127.0.0.1/!SOP/";

        public bool Delete(string id, UserClient client)
        {
            try
            {
                if (HasGuid(client))
                {
                    var restClient = new RestClient(REST_URL);
                    string ROUTE = "index.php?id={id}";
                    var request = new RestRequest(ROUTE, Method.DELETE);
                    request.AddParameter("id", id);
                    IRestResponse<JSONResponse> response = restClient.Execute<JSONResponse>(request);

                    return (response.Data.Status == "1") ? true : false;
                }

                return false;
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public bool Insert(string title, string body, string author, string deadline, string priority, UserClient client)
        {
            try
            {
                if (HasGuid(client))
                {
                    var postBody = new
                    {
                        todo_title = title,
                        todo_body = body,
                        todo_author = author,
                        todo_deadline = deadline,
                        todo_priority = priority
                    };

                    var restClient = new RestClient(REST_URL);
                    string ROUTE = "index.php";
                    var request = new RestRequest(ROUTE, Method.POST);
                    request.AddJsonBody(postBody);
                    IRestResponse<JSONResponse> response = restClient.Execute<JSONResponse>(request);

                    return (response.Data.Status == "1") ? true : false;
                }

                return false;
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public List<TodoModel> ListAll(UserClient client)
        {
            try
            {
                if (HasGuid(client))
                {
                    var restClient = new RestClient(REST_URL);
                    string ROUTE = "index.php";
                    var request = new RestRequest(ROUTE, Method.GET);
                    IRestResponse<List<TodoModel>> response = restClient.Execute<List<TodoModel>>(request);

                    if(response.Data.Count > 0)
                    {
                        return response.Data;
                    }
                    else
                    {
                        throw new NoAvailableTodoException();
                    }
                }
                throw new LoginFailedException();
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (LoginFailedException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (NoAvailableTodoException)
            {
                throw new FaultException<NoAvailableTodoFault>(new NoAvailableTodoFault("Nincs megjeleníthető adat!"), new FaultReason("Nincs megjeleníthető adat!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public UserClient Login(string username, string password)
        {
            try
            {
                if((username == "root" && password == "root") || (username == "Zorro" && password == "Zorro"))
                {
                    UserClient user = new UserClient(username, Guid.NewGuid().ToString());
                    lock (Host.loggedIn)
                    {
                        Host.loggedIn.Add(user);
                    }
                    return user;
                }
                else
                {
                    throw new LoginFailedException();
                }
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (LoginFailedException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (IncorrectPasswordException)
            {
                throw new FaultException<IncorrectPasswordFault>(new IncorrectPasswordFault("Helytelen jelszo!"), new FaultReason("Helytelen jelszó!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public bool Logout(UserClient client)
        {
            try
            {
                if (HasGuid(client))
                {
                    lock (Host.loggedIn)
                    {
                        var item = Host.loggedIn.Where(u => u.Username == client.Username && u.GUID == client.GUID).First();
                        Host.loggedIn.Remove(item);
                    }
                    return true;
                }

                return false;
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public bool Update(string id, string title, string body, string author, string deadline, string priority, UserClient client)
        {
            try
            {
                if (HasGuid(client))
                {
                    var postBody = SimpleJson.SerializeObject(new
                    {
                        todo_title = title.ToString(),
                        todo_body = body.ToString(),
                        todo_author = author.ToString(),
                        todo_deadline = deadline.ToString(),
                        todo_priority = priority.ToString()
                    });

                    var restClient = new RestClient(REST_URL);
                    string ROUTE = "index.php?id={id}";
                    var request = new RestRequest(ROUTE, Method.PUT);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("id", id, ParameterType.QueryString);
                    request.AddJsonBody(postBody);

                    IRestResponse<JSONResponse> response = restClient.Execute<JSONResponse>(request);

                    return (response.Data.Status == "1") ? true : false;
                }
                return false;
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public bool HasGuid(UserClient client)
        {
            try
            {
                foreach (var item in Host.loggedIn)
                {
                    if (item.GUID == client.GUID && item.Username == client.Username)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }

        public List<TodoModel> ListById(string id, UserClient client)
        {
            try
            {
                if (HasGuid(client))
                {
                    var restClient = new RestClient(REST_URL);
                    string ROUTE = "index.php?id={id}";
                    var request = new RestRequest(ROUTE, Method.GET);
                    request.AddParameter("id", id);
                    IRestResponse<List<TodoModel>> response = restClient.Execute<List<TodoModel>>(request);
                    if (response.Data.Count > 0)
                    {
                        return response.Data;
                    }
                    else
                    {
                        throw new TodoNotFoundException();
                    }
                }
                else
                {
                    throw new LoginFailedException();
                }
            }
            catch (NullReferenceException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (LoginFailedException)
            {
                throw new FaultException<LoginFailedFault>(new LoginFailedFault("Sikertelen bejelentkezés!"), new FaultReason("Sikertelen bejelentkezés!"));
            }
            catch (TodoNotFoundException)
            {
                throw new FaultException<TodoNotFoundFault>(new TodoNotFoundFault("Nem létező TODO!"), new FaultReason("Nem létező TODO!"));
            }
            catch (Exception ex)
            {
                throw new FaultException(new FaultReason(ex.Message));
            }
        }
    }
}
