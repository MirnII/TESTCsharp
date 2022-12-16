using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

namespace TESTCsharp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities con;
        public MainWindow()
        {
            
            InitializeComponent();
            LoadDataTable();
        }

        private void LoadDataTable()
        {
            try
            {
                con = new Entities();
                con.SP.Load();
                AllSP.ItemsSource = con.SP.Local.ToBindingList();
                Closing += MainWindow_Closing;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Возникла кретическая ошибка" + Environment.NewLine + ex);
            }
        }

        private void AllSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            con.Dispose();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла кретическая ошибка" + Environment.NewLine + ex);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllSP.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < AllSP.SelectedItems.Count; i++)
                    {
                        SP SP = AllSP.SelectedItems[i] as SP;
                        if (SP != null)
                        {
                            con.SP.Remove(SP);
                        }
                    }
                }
                con.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла кретическая ошибка" + Environment.NewLine + ex);
            }
        }

        private void artsp_(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SelectArt(artsp.Text);
        }

        private void SelectArt(string str)
        {
            try
            {
                SP SP = con.SP.First(p => p.Art_SP == str);
                if (SP != null)
                {
                    cam.Text = SP.Cam.ToString();
                    thicksp.Text = SP.Thick_SP.ToString();
                    thickg.Text = SP.Thick_Glass.ToString();
                }
                else MessageBox.Show("Стеклопакет с артикулом: " + str + "ненайден");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Стеклопакет с артикулом: " + str + " ненайден ");
            }
        }

        private void artsp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
