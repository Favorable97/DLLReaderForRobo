using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace DLLReaderForRobo {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string SelectFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "DLL Files|*.dll";
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            else
                return "-1";
        }

        private void SelectFileBtn_Click(object sender, RoutedEventArgs e)
        {
            ResultLbl.Content = "";
            string path = SelectFile();
            if (path == "-1") {
                MessageBox.Show("Файл не выбран");
            } else {
                Assembly ass = Assembly.LoadFrom(path);
                foreach (Type type in ass.GetTypes()) {
                    //ResultTxt.AppendText(type.Name + "\n"); //Console.WriteLine(type.Name);
                    ResultLbl.Content += type.Name + "\n";
                    foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic)) {
                        if (method.IsFamily || method.IsPublic)
                            //ResultTxt.AppendText("\t" + method.Name + "\n"); //Console.WriteLine("    " + method.Name);
                            ResultLbl.Content += "\t" + method.Name + "\n";
                    }
                    ResultLbl.Content += "\n";
                }
            }
        }
    }
}
