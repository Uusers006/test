using System;
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
using Airplanes.DataSetAirplanesTableAdapters;

namespace Airplanes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSetAirplanes dataSetAirplanes;
        UsersTableAdapter usersTableAdapter;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        int time = 10;
        int countries = 0;
        public MainWindow()
        {
            InitializeComponent();
            dataSetAirplanes = new DataSetAirplanes();
            usersTableAdapter = new UsersTableAdapter();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0,0,1);
            
        }

        private void ___btn_Exit__Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ___btn_Login__Click(object sender, RoutedEventArgs e)
        {
            if (___UserName_.Text != "" && ___Password_.Password != "")
            {
                int? id_user = usersTableAdapter.Login(___UserName_.Text, ___Password_.Password);
                if (id_user != null)
                {
                    if (true == Convert.ToBoolean(usersTableAdapter.Role(___UserName_.Text, ___Password_.Password).Value))
                    {
                        Administrator stateTable = new Administrator();
                        Close();
                        stateTable.Show();
                    }
                    else
                    {
                        Users stateTable = new Users();
                        Close();
                        stateTable.Show();
                    }
                }
                else
                {
 
                    if (countries == 2)
                    {
                        time = 10;
                        ___btn_Login_.IsEnabled = false;
                        timer.Start();
                        countries = 0;
                    }
                    else
                    {
                        ___Erors_.Text = "Логин или пароль не верный повторите еще раз";
                        countries++;
                    }
                }

            }
            

            else
            {
                ___Erors_.Text = "Введите логин и/или пароль";
            }
        }
        private void timerTick(object sender, EventArgs e)
        {
            ___Erors_.Text = "Повторите попытку через " + time + " секунд ";
            time--;
            if (time == 0)
            {
                ___btn_Login_.IsEnabled = true;
                ___Erors_.Text = "";
                timer.Stop();
            }

        }
    }
}
