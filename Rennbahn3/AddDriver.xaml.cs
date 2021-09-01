using Rennbahn3.Logic;
using Rennbahn3.Model;
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
using System.Windows.Shapes;

namespace Rennbahn3
{
    /// <summary>
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        DataLogic dataLogic = new DataLogic();
        public AddDriver()
        {
            InitializeComponent();
            cmbBoxSaison.ItemsSource = dataLogic.GetSaisons();
            cmbBoxTeam.ItemsSource = dataLogic.GetTeams();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Driver driver = new Driver();
            driver.Name = txtBoxName.Text;
            driver.Saison = (Saison)cmbBoxSaison.SelectedItem;
            driver.Team = (Team)cmbBoxTeam.SelectedItem;

            dataLogic.AddDriver(driver);
            this.Close();
        }
    }
}
