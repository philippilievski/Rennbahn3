using Rennbahn3.Logic;
using Rennbahn3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Rennbahn3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataLogic dataLogic = new DataLogic();
        public MainWindow()
        {
            InitializeComponent();
            dgDrivers.ItemsSource = dataLogic.GetDrivers();
            comboBoxRace.ItemsSource = dataLogic.GetRaces();
            cmbBoxSeason.ItemsSource = dataLogic.GetSaisons();
        }

        private void btnAddToResult_Click(object sender, RoutedEventArgs e)
        {
            Result result = new Result();
            result.Driver = (Driver)dgDrivers.SelectedItem;
            result.Position = dataLogic.GetHighestPositionFromResults((Race)comboBoxRace.SelectedItem) + 1;
            result.Race = (Race)comboBoxRace.SelectedItem;
            dataLogic.AddResult(result);

            dgResult.ItemsSource = dataLogic.GetResults((Race)comboBoxRace.SelectedItem);
            SortDataGrid();
        }

        private void btnRemoveFromResult_Click(object sender, RoutedEventArgs e)
        {
            Result result = (Result)dgResult.SelectedItem;
            dataLogic.RemoveResult(result);
            dgResult.ItemsSource = dataLogic.GetResults((Race)comboBoxRace.SelectedItem);
            SortDataGrid();
        }

        private void comboBoxRace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgResult.ItemsSource = dataLogic.GetResults((Race)comboBoxRace.SelectedItem);
            SortDataGrid();
        }

        /// <summary>
        /// Moves up the position of a driver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveUpPosition_Click(object sender, RoutedEventArgs e)
        {
            dataLogic.MoveUpPosition((Result)dgResult.SelectedItem);
            dgResult.ItemsSource = dataLogic.GetResults((Race)comboBoxRace.SelectedItem);
            SortDataGrid();
        }

        /// <summary>
        /// Sorts the result datagrid by position
        /// </summary>
        public void SortDataGrid()
        {
            ListSortDirection listSortDirection = new ListSortDirection();

            var column = dgResult.Columns[0];
            dgResult.Items.SortDescriptions.Clear();
            dgResult.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, listSortDirection));
            foreach (var col in dgResult.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = listSortDirection;
            dgResult.Items.Refresh();
        }


        /// <summary>
        /// Moves down the position of a driver 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveDownPosition_Click(object sender, RoutedEventArgs e)
        {
            dataLogic.MoveDownPosition((Result)dgResult.SelectedItem);
            dgResult.ItemsSource = dataLogic.GetResults((Race)comboBoxRace.SelectedItem);
            SortDataGrid();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            dataLogic.UpdatePoints();
        }

        private void cmbBoxSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxRace.ItemsSource = dataLogic.GetRaces((Saison)cmbBoxSeason.SelectedItem);
        }

        private void btnClearSelection_Click(object sender, RoutedEventArgs e)
        {
            cmbBoxSeason.SelectedIndex = -1;
            comboBoxRace.ItemsSource = dataLogic.GetRaces();
        }

        private void btnAddDriver_Click(object sender, RoutedEventArgs e)
        {
            AddDriver addDriver = new AddDriver();
            addDriver.ShowDialog();
        }
    }
}
