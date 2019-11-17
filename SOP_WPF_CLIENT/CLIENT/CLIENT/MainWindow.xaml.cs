﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CLIENT.TodoService;
using System.ServiceModel;

namespace CLIENT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TodoServiceClient client;
        private UserClient userClient;

        public MainWindow(TodoServiceClient client, UserClient userClient)
        {
            InitializeComponent();
            this.client = client;
            this.userClient = userClient;
            userInfo.Text = "Bejelentkezve mint " + userClient.username;
            txtInsertAuthor.Text = userClient.username;
            txtInsertAuthor.IsEnabled = false;
            txtUpdateAuthor.Text = userClient.username;
            txtUpdateAuthor.IsEnabled = false;
        }

        private void ForceLogout()
        {
            this.userClient = null;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            listBoxAll.Items.Clear();
            try
            {
                ICollection<TodoModel> result = client.ListAll(userClient);
                foreach (TodoModel todo in result)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = todo.todo_id + ";" + todo.todo_title + ";" + todo.todo_body + ";" + todo.todo_author + ";" + todo.todo_deadline + ";" + todo.todo_priority;
                    item.MouseDoubleClick += Item_MouseDoubleClick;
                    listBoxAll.Items.Add(item);
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Hiba a kiszolgálóval!");
                ForceLogout();
            }
            catch (FaultException<LoginFailedFault>)
            {
                MessageBox.Show("Hibás kapcsolat!");
                ForceLogout();
            }
            catch(FaultException<NoAvailableTodoFault> ex)
            {
                MessageBox.Show(ex.Reason.ToString());
            }
            catch (FaultException ex)
            {
                MessageBox.Show("Ismeretlen hiba " + ex.Reason.ToString());
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Ismeretlen hiba: " + ex.Message);
            }
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)sender;
            string[] todoData = selectedItem.Content.ToString().Split(';');
            txtUpdateID.Text = todoData[0]; txtUpdateID.IsEnabled = false;
            txtUpdateTitle.Text = todoData[1];
            txtUpdateBody.Text = todoData[2];
            txtUpdateAuthor.Text = todoData[3];
            txtUpdateDeadline.Text = todoData[4];
            cmbUpdatePriority.Text = todoData[5];
        }

        private void btnListById_Click(object sender, RoutedEventArgs e)
        {
            if(txtListID.Text.All(char.IsDigit))
            {
                listBoxID.Items.Clear();
                try
                {
                    ICollection<TodoModel> oneResult = client.ListById(txtListID.Text, userClient);
                    foreach (TodoModel todo in oneResult)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = todo.todo_id + ";" + todo.todo_title + ";" + todo.todo_body + ";" + todo.todo_author + ";" + todo.todo_deadline + ";" + todo.todo_priority;
                        item.MouseDoubleClick += Item_MouseDoubleClick;
                        listBoxID.Items.Add(item);
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Hiba a kiszolgálóval!");
                    ForceLogout();
                }
                catch (FaultException<LoginFailedFault>)
                {
                    MessageBox.Show("Hibás kapcsolat!");
                    ForceLogout();
                }
                catch (FaultException<TodoNotFoundFault> ex)
                {
                    MessageBox.Show(ex.Reason.ToString());
                }
                catch (FaultException ex)
                {
                    MessageBox.Show("Ismeretlen hiba: " + ex.Reason.ToString());
                }
                
                catch(Exception ex)
                {
                    MessageBox.Show("Ismeretlen hiba: " + ex.Message);
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(txtUpdateID.Text.All(char.IsDigit))
            {
                try
                {
                    string id = txtUpdateID.Text;
                    string title = txtUpdateTitle.Text;
                    string body = txtUpdateBody.Text;
                    string author = txtUpdateAuthor.Text;
                    string deadline = txtUpdateDeadline.Text;
                    string priority = cmbUpdatePriority.Text;

                    if (Helper.IsAnyEmptyOrNull(id, title, body, author, deadline, priority))
                    {
                        MessageBox.Show("Minden mező kitöltése kötelező!");
                    }
                    else
                    {
                        bool updateResult = client.Update(id, title, body, author, deadline, priority, userClient);

                        if (updateResult)
                        {
                            MessageBox.Show("Sikeres adatmódosítás!");
                        }
                        else
                        {
                            MessageBox.Show("Sikertelen adatmódosítás!");
                        }
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Hiba a kiszolgálóval!");
                    ForceLogout();
                }
                catch (FaultException<LoginFailedFault>)
                {
                    MessageBox.Show("Hibás kapcsolat");
                    ForceLogout();
                }
                catch(FaultException ex)
                {
                    MessageBox.Show("Ismeretlen hiba: " + ex.Reason.ToString());
                }
                
                catch(Exception ex)
                {
                    MessageBox.Show("Ismeretlen hiba: " + ex.Message);
                }
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = txtInsertTitle.Text;
                string body = txtInsertBody.Text;
                string author = txtInsertAuthor.Text;
                string deadline = txtInsertDeadline.Text;
                string priority = cmbInsertPriority.Text;

                if (Helper.IsAnyEmptyOrNull(title, body, author, deadline, priority))
                {
                    MessageBox.Show("Minden mező kitöltése kötelező!");
                }
                else
                {
                    bool insertResult = client.Insert(title, body, author, deadline, priority, userClient);
                    if (insertResult)
                    {
                        MessageBox.Show("Az adatfelvitel sikeres!");
                    }
                    else
                    {
                        MessageBox.Show("Az adatfelvitel sikertelen!");
                    }
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Hiba a kiszolgálóval!");
                ForceLogout();
            }
            catch (FaultException<LoginFailedFault>)
            {
                MessageBox.Show("Hibás kapcsolat");
                ForceLogout();
            }
            catch(FaultException ex)
            {
                MessageBox.Show("Ismeretlen hiba: " + ex.Reason.ToString());
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Ismeretlen hiba: " + ex.Message);
            }
        }

        private void BtnDeleteById_Click(object sender, RoutedEventArgs e)
        {
            if (txtDeleteID.Text.All(char.IsDigit))
            {
                string deleteID = txtDeleteID.Text;
                try
                {
                    bool deleteResult = client.Delete(deleteID, userClient);
                    if (deleteResult)
                    {
                        MessageBox.Show("Törlés sikeres!");
                    }
                    else
                    {
                        MessageBox.Show("Törlés sikertelen!");
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Hiba a kiszolgálóval!");
                    ForceLogout();
                }
                catch (FaultException<LoginFailedFault>)
                {
                    MessageBox.Show("Hibás kapcsolat");
                    ForceLogout();
                }
                catch(FaultException ex)
                {
                    MessageBox.Show("Ismeretlen hiba: " + ex.Reason.ToString());
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("Ismeretlen hiba: " + ex.Message);
                }
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool logoutResult = client.Logout(userClient);
                if (logoutResult)
                {
                    userClient = null;
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Hiba a kiszolgálóval!");
                ForceLogout();
            }
            catch (FaultException<LoginFailedFault>)
            {
                MessageBox.Show("Hibás kapcsolat");
                ForceLogout();
            }
            catch(FaultException ex)
            {
                MessageBox.Show("Ismeretlen hiba: " + ex.Reason.ToString());
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Ismeretlen hiba: " + ex.Message);
            }
        }
    }
}
