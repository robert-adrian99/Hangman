using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Hangman.Models;
using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        SerializationActions actions = new SerializationActions();
        public SignInWindow()
        {
            InitializeComponent();
            try
            {
                FileStream file = new FileStream("Users.xml", FileMode.Open);
                file.Dispose();
            }
            catch (FileNotFoundException)
            {
                return;
            }
            DataContext = actions.DeserializeObject("Users.xml");
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            //actions.SerializeObject("Users.xml", DataContext as SignInViewModel);
            HomeWindow window = new HomeWindow();
            this.Close();
            window.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow window = new StartWindow();
            this.Close();
            window.Show();
        }
    }
}
