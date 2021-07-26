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
using DataModel;
using DAL;
using BLL;

namespace LoginUBD.GUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = this.userNameTextBox.Text;
            userInfo.Password = this.passwordTextBox.Password;
            LoginOptions loginOptions = new LoginOptions();

            switch (loginOptions.UserLogin(userInfo))
            {
                case LoginStatus.UserNameNotExist:
                    MessageBox.Show("用户名不存在，请重新输入");
                    break;
                case LoginStatus.DataNotMatch:
                    MessageBox.Show("密码不正确，请重新输入");
                    break;
                case LoginStatus.NomalState:
                    MessageBox.Show("登录成功");
                    this.userNameTextBox.Clear();
                    this.passwordTextBox.Clear();
                    break;
                default:
                    MessageBox.Show("YOU WILL NEVER SEE ME");
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = this.userNameTextBox.Text;
            userInfo.Password = this.passwordTextBox.Password;
            LoginOptions loginOptions = new LoginOptions();

            switch (loginOptions.RegisterNewUser(userInfo))
            {
                case LoginStatus.UserNameSizeError:
                    MessageBox.Show("用户名长度4-10，请重新输入");
                    break;
                case LoginStatus.PasswordSizeError:
                    MessageBox.Show("密码长度6-10，请重新输入");
                    break;
                case LoginStatus.UserNameExist:
                    MessageBox.Show("用户名已存在，请重新输入");
                    break;
                case LoginStatus.NomalState:
                    MessageBox.Show("注册成功");
                    this.userNameTextBox.Clear();
                    this.passwordTextBox.Clear();
                    break;
                default:
                    MessageBox.Show("YOU WILL NEVER SEE ME");
                    break;
            }
        }
    }
}
