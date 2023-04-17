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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddressBookWithUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] entities = new string[5];


        TextBox[] textBoxes = new TextBox[6];
        int key = 0;

        public MainWindow()
        {
            InitializeComponent();

            textBoxes[0] = txtbContactID;
            textBoxes[1] = txtbContactLN;
            textBoxes[2] = txtbContactFN;
            textBoxes[3] = txtbContactAdd;            
            textBoxes[4] = txtbContactEAdd;
            textBoxes[5] = txtbContactCont;

            // setup
            if (File.Exists("addressbookDB.csv"))
            {
                Constants.addressBook.Clear();
                string line = "";
                string[] content = new string[] { };
                using (StreamReader sr = new StreamReader("addressbookDB.csv"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        content = line.Split(',');
                        entities = new string[5];
                        key = int.Parse(content[0]);
                        for(int x = 0; x < entities.Length; x++) 
                        {
                            entities[x] = content[x + 1];
                        }

                        Constants.addressBook[key] = entities;
                    }
                }
            }


            if (Constants.addressBook.Count == 0) 
                this.Close();
            else
                readAndDisplay(key);

            foreach (TextBox t in textBoxes)
                t.IsEnabled = false;

            // read things
        }

        private void readAndDisplay(int key)
        {
            entities = new string[5];
            entities = Constants.addressBook[key];

            textBoxes[0].Text = key.ToString();
            for(int x = 0; x < entities.Length; x++) 
            {
                textBoxes[x + 1].Text = entities[x];
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Show();
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            key--;
            if(key < 0)
                key = Constants.addressBook.Count - 1;

            readAndDisplay(key);
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            key++;
            if (key == Constants.addressBook.Count)
                key = 0;

            readAndDisplay(key);
        }
    }
}
