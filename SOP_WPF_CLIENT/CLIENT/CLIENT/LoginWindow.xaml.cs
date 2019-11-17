using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CLIENT.TodoService;

namespace CLIENT
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private TodoServiceClient client;
        private UserClient userClient;
        public LoginWindow()
        {
            InitializeComponent();
            client = new TodoServiceClient();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(pswPassword.Password))
            {
                MessageBox.Show("Tölts ki minden mezőt!");
            }
            else
            {
                string username = txtUsername.Text;
                string password = pswPassword.Password;
                try
                {
                    userClient = client.Login(username, password);
                    MainWindow mainWindow = new MainWindow(this.client, this.userClient);
                    mainWindow.Show();
                    this.Close();
                }
                catch(FaultException<LoginFailedFault> ex)
                {
                    MessageBox.Show(ex.Reason.ToString());
                }
                catch(EndpointNotFoundException)
                {
                    MessageBox.Show("Hiba a kiszolgálóval!");
                }
            }
        }
    }
}
