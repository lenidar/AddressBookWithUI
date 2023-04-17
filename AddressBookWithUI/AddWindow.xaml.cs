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

namespace AddressBookWithUI
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        TextBox[] textBoxes = new TextBox[6];
        string[] entities = new string[5];
        int key = 0;
        public AddWindow()
        {
            InitializeComponent();

            // setup
            textBoxes[0] = txtbContactID;
            textBoxes[1] = txtbContactLN;
            textBoxes[2] = txtbContactFN;
            textBoxes[3] = txtbContactAdd;
            textBoxes[4] = txtbContactEAdd;
            textBoxes[5] = txtbContactCont;

            // get last ID from dictionary
            key = forceRecheckLength();


            textBoxes[0].IsEnabled = false;
            textBoxes[0].Text = key.ToString();
        }

        private int forceRecheckLength()
        {
            if (Constants.addressBook.Count >= 1)
                return Constants.addressBook.Keys.ToArray()[Constants.addressBook.Count - 1] + 1;
            else
                return 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool proceed = false;

            for(int x = 1; x < textBoxes.Length; x++) 
            {
                if (textBoxes[x].Text.Length > 0) 
                {
                    proceed = true;
                }
                else
                {
                    proceed = false;
                    textBoxes[x].Focus();
                    break;
                }
            }

            if(proceed)
            {
                for (int x = 0; x < entities.Length; x++)
                {
                     entities[x] = textBoxes[x + 1].Text;
                }
                Constants.addressBook[key] = entities;
                writeToFile();
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }

        }

        private void writeToFile()
        {
            using (StreamWriter sw = new StreamWriter("addressbookDB.csv"))
            {
                string line = "";
                foreach(KeyValuePair<int, string[]> kvp in Constants.addressBook)
                {
                    line = kvp.Key + ",";
                    foreach(string value in kvp.Value)
                    {
                        line += value + ",";
                    }

                    sw.WriteLine(line);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            key = forceRecheckLength();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
