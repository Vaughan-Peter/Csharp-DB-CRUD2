using System;
using System.Windows;
using System.Windows.Controls;

// Added
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DB_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CommandHandler cmdNewFace1;
        private CommandHandler cmdNewFace2;
        private CommandHandler cmdNewFace3;
        private CommandHandler cmdNewFace4;
        private CommandHandler cmdNewFace5;
        private CommandHandler cmdNewFace6;
        private CommandHandler cmdNewFace7;
        private CommandHandler cmdNewFace8;
        private CommandHandler cmdNewFace9;
        private CommandHandler cmdNewFace10;
        private CommandHandler cmdNewFace11;
        private CommandHandler cmdNewFace12;
        private CommandHandler cmdNewFace13;
        private CommandHandler cmdNewFace14;
        private CommandHandler cmdNewFace15;
        private CommandHandler cmdNewFace16;
        private CommandHandler cmdNewFace17;
        private CommandHandler cmdNewFace18;


        //NC-3 Specify a connection string.  
        string connstr = DB_03.Utility.GetConnectionString();
        //string connstr = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\datadb.mdf;Integrated Security=True;Connect Timeout=30"

        // Declare DataTable class-wide, so delete button can access
        DataTable dt;

        // update needs current primary key
        int current_primary_key = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Fill the data grid with Person table data
            FillDataGrid();

            loadDataToDBcomboBox();

            cmdNewFace1 = new CommandHandler(() => hairOne(), true);
            cmdNewFace2 = new CommandHandler(() => hairTwo(), true);
            cmdNewFace3 = new CommandHandler(() => hairThree(), true);
            cmdNewFace4 = new CommandHandler(() => hairFour(), true);

            cmdNewFace5 = new CommandHandler(() => eyesOne(), true);
            cmdNewFace6 = new CommandHandler(() => eyesTwo(), true);
            cmdNewFace7 = new CommandHandler(() => eyesThree(), true);
            cmdNewFace8 = new CommandHandler(() => eyesFour(), true);

            cmdNewFace9 = new CommandHandler(() => noseOne(), true);
            cmdNewFace10 = new CommandHandler(() => noseTwo(), true);
            cmdNewFace11 = new CommandHandler(() => noseThree(), true);
            cmdNewFace12 = new CommandHandler(() => noseFour(), true);

            cmdNewFace13 = new CommandHandler(() => mouthOne(), true);
            cmdNewFace14 = new CommandHandler(() => mouthTwo(), true);
            cmdNewFace15 = new CommandHandler(() => mouthThree(), true);
            cmdNewFace16 = new CommandHandler(() => mouthFour(), true);

            cmdNewFace17 = new CommandHandler(() => randomFace(), true);
            cmdNewFace18 = new CommandHandler(() => MyImageMethod(), true);

            DataContext = new
            {
                newFaceCMD1 = cmdNewFace1,
                newFaceCMD2 = cmdNewFace2,
                newFaceCMD3 = cmdNewFace3,
                newFaceCMD4 = cmdNewFace4,
                newFaceCMD5 = cmdNewFace5,
                newFaceCMD6 = cmdNewFace6,
                newFaceCMD7 = cmdNewFace7,
                newFaceCMD8 = cmdNewFace8,
                newFaceCMD9 = cmdNewFace9,
                newFaceCMD10 = cmdNewFace10,
                newFaceCMD11 = cmdNewFace11,
                newFaceCMD12 = cmdNewFace12,
                newFaceCMD13 = cmdNewFace13,
                newFaceCMD14 = cmdNewFace14,
                newFaceCMD15 = cmdNewFace15,
                newFaceCMD16 = cmdNewFace16,
                newFaceCMD17 = cmdNewFace17,
                newFaceCMD18 = cmdNewFace18,
            };

            InputBindings.Add(new KeyBinding(cmdNewFace1, new KeyGesture(Key.F1, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace2, new KeyGesture(Key.F2, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace3, new KeyGesture(Key.F3, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace4, new KeyGesture(Key.F4, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace5, new KeyGesture(Key.F5, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace6, new KeyGesture(Key.F6, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace7, new KeyGesture(Key.F7, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace8, new KeyGesture(Key.F8, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace9, new KeyGesture(Key.F9, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace10, new KeyGesture(Key.F10, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace11, new KeyGesture(Key.F11, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace12, new KeyGesture(Key.F12, ModifierKeys.None)));
            InputBindings.Add(new KeyBinding(cmdNewFace13, new KeyGesture(Key.A, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(cmdNewFace14, new KeyGesture(Key.B, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(cmdNewFace15, new KeyGesture(Key.C, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(cmdNewFace16, new KeyGesture(Key.D, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(cmdNewFace17, new KeyGesture(Key.R, ModifierKeys.Control)));
            InputBindings.Add(new KeyBinding(cmdNewFace18, new KeyGesture(Key.N, ModifierKeys.Control)));
        }

        BitmapImage imgHair1 = new BitmapImage(new Uri("../../../images/hair1.png", UriKind.Relative));
        BitmapImage imgHair2 = new BitmapImage(new Uri("../../../images/hair2.png", UriKind.Relative));
        BitmapImage imgHair3 = new BitmapImage(new Uri("../../../images/hair3.png", UriKind.Relative));
        BitmapImage imgHair4 = new BitmapImage(new Uri("../../../images/hair4.png", UriKind.Relative));

        BitmapImage imgEyes1 = new BitmapImage(new Uri("../../../images/eyes1.png", UriKind.Relative));
        BitmapImage imgEyes2 = new BitmapImage(new Uri("../../../images/eyes2.png", UriKind.Relative));
        BitmapImage imgEyes3 = new BitmapImage(new Uri("../../../images/eyes3.png", UriKind.Relative));
        BitmapImage imgEyes4 = new BitmapImage(new Uri("../../../images/eyes4.png", UriKind.Relative));

        BitmapImage imgNose1 = new BitmapImage(new Uri("../../../images/nose1.png", UriKind.Relative));
        BitmapImage imgNose2 = new BitmapImage(new Uri("../../../images/nose2.png", UriKind.Relative));
        BitmapImage imgNose3 = new BitmapImage(new Uri("../../../images/nose3.png", UriKind.Relative));
        BitmapImage imgNose4 = new BitmapImage(new Uri("../../../images/nose4.png", UriKind.Relative));

        BitmapImage imgMouth1 = new BitmapImage(new Uri("../../../images/mouth1.png", UriKind.Relative));
        BitmapImage imgMouth2 = new BitmapImage(new Uri("../../../images/mouth2.png", UriKind.Relative));
        BitmapImage imgMouth3 = new BitmapImage(new Uri("../../../images/mouth3.png", UriKind.Relative));
        BitmapImage imgMouth4 = new BitmapImage(new Uri("../../../images/mouth4.png", UriKind.Relative));



        // Bind the Database Person table to the screen datagrid
        private void FillDataGrid()
        {

            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                // pull whatever columns we want on the grid...get all
                CmdString = "SELECT p.Id, p.firstName, p.lastName, p.city, p.hair, p.eyes, p.nose, p.mouth, h.Name AS Hobby, " +
                    "o.name AS Occupation FROM Person p LEFT JOIN Hobby h ON p.HobbyID = h.id LEFT JOIN Occupation o ON p.JobID = o.id";
                //CmdString = "SELECT Id,  lastName, city, firstName FROM Person";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);             // adapter to connect both sides
                dt = new DataTable("gridPerson");
                sda.Fill(dt);
                gridPerson.ItemsSource = dt.DefaultView;  // Fill grid with DataTable                
            }
        }


        Random random = new Random();
        string hair = "";
        string eyes = "";
        string nose = "";
        string mouth = "";

        public void overallClear() {
            myCanvas.Children.Clear();
            hair = "";
            eyes = "";
            nose = "";
            mouth = "";

        }
        public void loadDataToDBcomboBox()
        {
            // https://stackoverflow.com/questions/12494634/fill-combobox-from-database
            DBcomboBox.Items.Clear();
            using (SqlConnection con = new SqlConnection(connstr))
            {
                string query = "SELECT firstName FROM Person";      //query the database
                //command.Connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                queryStatus.Connection.Open();  //StackOverflow didn't include this, but got exception
                SqlDataReader reader = queryStatus.ExecuteReader();

                while (reader.Read())   //loop reader and fill the combobox
                {
                    DBcomboBox.Items.Add(reader["firstName"].ToString());
                }
            }
            loadDataToDBcomboBoxTwo();
            loadDataToDBcomboBoxThree();
        }

        public void b_findInfo_Click(object sender, RoutedEventArgs e) {
            loadDataToDBcomboBoxTwo();
            loadDataToDBcomboBoxThree();
        
        }
        public void loadDataToDBcomboBoxTwo()
        {
            // https://stackoverflow.com/questions/12494634/fill-combobox-from-database
            DBcomboBoxTwo.Items.Clear();
            using (SqlConnection con = new SqlConnection(connstr))
            {
                string query = "SELECT name FROM Hobby";      //query the database
                //command.Connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                queryStatus.Connection.Open();  //StackOverflow didn't include this, but got exception
                SqlDataReader reader = queryStatus.ExecuteReader();

                while (reader.Read())   //loop reader and fill the combobox
                {
                    DBcomboBoxTwo.Items.Add(reader["name"].ToString());
                }
            }
        }


        public void loadDataToDBcomboBoxThree()
        {
            // https://stackoverflow.com/questions/12494634/fill-combobox-from-database
            DBcomboBoxThree.Items.Clear();
            using (SqlConnection con = new SqlConnection(connstr))
            {
                string query = "SELECT name FROM Occupation";      //query the database
                //command.Connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                queryStatus.Connection.Open();  //StackOverflow didn't include this, but got exception
                SqlDataReader reader = queryStatus.ExecuteReader();

                while (reader.Read())   //loop reader and fill the combobox
                {
                    DBcomboBoxThree.Items.Add(reader["name"].ToString());
                }
            }
        }

        // Event for buttons start with "b_"
        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            String fn = tb_first.Text;
            String ln = tb_last.Text;
            String city = tb_city.Text;

               // Add this record if values not empty
            if (fn != "" && ln != "" && city != "")
            {
                this.addPerson(fn, ln, city);  // old school SQL-over-the-connection
                //this.addPerson1(fn, ln, city);  // Stored procedure
            }
            else
            {
                MessageBox.Show("A field is empty...enter all fields");
            }

            // Update changes to the grid
            FillDataGrid();
            loadDataToDBcomboBox();
        }

        private void b_cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_first.Clear();
            tb_last.Clear();
            tb_city.Clear();
            tb_occupation.Clear();
            tb_hobbies.Clear();
            overallClear();
        }


        // Add a person record with these attributes...SQL INSERT command
        private void addPerson(String fn, String ln, String city)
        {

            /*
            // old school insert statement...note Trace output should show format of SQL Insert command
            String cmd_Text = "INSERT INTO Person(firstName, lastName, city, hair, eyes, nose, mouth, JobID, HobbyID) VALUES('" + fn + "', '" + ln + "', '" + city + "', '" + hair + "', '" + eyes + "', '" + nose + "', '" + mouth + "', '" + DBcomboBoxThree.SelectedIndex + 1 + "', '" + DBcomboBoxTwo.SelectedIndex + 1 + "'); ";
        
            Trace.Write(cmd_Text);

            using (SqlConnection con = new SqlConnection(connstr)) {
                // Example of C# named parameters...a good idea for important library calls
                SqlCommand command = new SqlCommand(cmdText: cmd_Text, connection: con);
                command.Connection.Open();
                command.ExecuteNonQuery();  //does the actual insert statement
            }
            */

            int hobby = DBcomboBoxTwo.SelectedIndex;
            int job = DBcomboBoxThree.SelectedIndex;

   
            string query = "INSERT INTO Person(firstName, lastName, city, hair, eyes, nose, mouth, HobbyID, JobID) " +
                "VALUES(@fn, @ln, @city, @hair, @eyes, @nose, @mouth, @hobby, @job)";
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fn", fn);
                    cmd.Parameters.AddWithValue("@ln", ln);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@hair", hair);
                    cmd.Parameters.AddWithValue("@eyes", eyes);
                    cmd.Parameters.AddWithValue("@nose", nose);
                    cmd.Parameters.AddWithValue("@mouth", mouth);
                    cmd.Parameters.AddWithValue("@hobby", hobby);
                    cmd.Parameters.AddWithValue("@job", job);

                    MessageBox.Show(hobby + "   " + job);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex) {
                        MessageBox.Show("Error adding person: " + ex.Message);
                    }
                }

            }
                overallClear();
        }


        // Add a person record with these attributes...SQL INSERT command
        private void delPerson(int pkey)
        {
            // Old school connection
            SqlConnection conn = new SqlConnection(connstr);

            // old school insert statement...note Trace output should show format of SQL Insert command
            String cmd_Text = "DELETE FROM Person WHERE Id = " + pkey + ";";
            Trace.Write(cmd_Text);

            // DB insert in try-catch
            try
            {
                // Example of C# named parameters...a good idea for important library calls
                SqlCommand command = new SqlCommand(cmdText: cmd_Text, connection: conn);
                command.Connection.Open();
                command.ExecuteNonQuery();  //does the actual insert statement
            }
            catch { MessageBox.Show("DB Delete Exception"); }
            finally { conn.Close(); }

            loadDataToDBcomboBox();

        }



        // Add a person record with these attributes...SQL INSERT command
        private void upPerson(int pkey, String fn, String ln, String city)
        {

            int hobby = DBcomboBoxTwo.SelectedIndex;
            int job = DBcomboBoxThree.SelectedIndex;


            string query = "UPDATE Person SET firstName = @fn, lastName = @ln, city = @city, hair = @hair, eyes = @eyes, nose = @nose, mouth = @mouth, HobbyID = @HobbyID, JobID = JobID WHERE Id = @id";
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fn", fn);
                    cmd.Parameters.AddWithValue("@ln", ln);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@hair", hair);
                    cmd.Parameters.AddWithValue("@eyes", eyes);
                    cmd.Parameters.AddWithValue("@nose", nose);
                    cmd.Parameters.AddWithValue("@mouth", mouth);
                    cmd.Parameters.AddWithValue("@HobbyID", hobby);
                    cmd.Parameters.AddWithValue("@JobID", job);
                    cmd.Parameters.AddWithValue("@id", current_primary_key);

                    MessageBox.Show(hobby + "   " + job);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding person: " + ex.Message);
                    }
                }

            }
            overallClear();
        }

        // Gets called when grid is selected
        private void gridPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When we get here after deleting a row, we can't get the current row
            try
            {
                if ((gridPerson.SelectedItem as DataRowView) != null)
                {
                    // fetch the columns from the selected row
                    current_primary_key = (int)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[0];
                    tb_first.Text = (string)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[1];
                    tb_last.Text = (string)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[2];
                    tb_city.Text = (string)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[3];

                    Trace.WriteLine("Selected = " + current_primary_key + tb_first.Text + tb_last.Text);
                }
                else {
                    current_primary_key = -1;
                    tb_first.Text = ":(";
                    tb_last.Text = ":(";
                    tb_city.Text = ":(";
                    tb_occupation.Text = ":(";
                    tb_hobbies.Text = ":(";
                    myCanvas.Children.Clear();
                }
            }
            catch
            {
                // If deleting row, get exception trying to get it's data
                Trace.WriteLine("No Row (deleted?)...default record used");
                current_primary_key = -1;
                tb_first.Text = ":(";
                tb_last.Text = ":(";
                tb_city.Text = ":(";
                tb_occupation.Text = ":(";
                tb_hobbies.Text = ":(";
            }
        }

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            if (gridPerson.SelectedItem != null)
            {
                int key = (int)(gridPerson.SelectedItem as DataRowView).Row.ItemArray[0];
                delPerson(key);

                // These lines update the grid, but not the database
                dt.Rows.Remove((gridPerson.SelectedItem as DataRowView).Row);
                dt.AcceptChanges();

                overallClear();
            }
        }
        private void b_find_Click(object sender, RoutedEventArgs e)
        {
            dt.Rows.Clear();
            Trace.WriteLine("WILSON! WILSON! WILSON!");
            string name = tb_find.Text.Trim();
            string query = "SELECT Id, firstName, lastName, city, hair, eyes, nose, mouth FROM Person WHERE lastName LIKE @name";

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");

                    try
                    {
                        Trace.WriteLine("NNNNNNOOOOOOOO");
                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex) {
                        MessageBox.Show("We cannot find " + name);
                    }
                }
            }
            
            gridPerson.ItemsSource = dt.DefaultView;
   
        }

        private void b_update_Click(object sender, RoutedEventArgs e)
        {
            if (current_primary_key >-1)
            {
                upPerson(current_primary_key, tb_first.Text, tb_last.Text, tb_city.Text);

                // Update changes to the grid
                FillDataGrid();

                // These lines update the grid, but not the database
                //dt.Rows.Remove((gridPerson.SelectedItem as DataRowView).Row);
                //dt.AcceptChanges();
            }
        }

        private void hairOne()
        {
            Image Hair1 = new Image();
            Hair1.Source = imgHair1;
            Hair1.Width = imgHair1.Width;
            Hair1.Height = imgHair1.Height;

            Canvas.SetLeft(Hair1, 50);
            Canvas.SetTop(Hair1, 50);
            myCanvas.Children.Add(Hair1);
            hair = "Hair1";
        }

        private void hairTwo()
        {
            Image Hair2 = new Image();
            Hair2.Source = imgHair2;
            Hair2.Width = imgHair2.Width;
            Hair2.Height = imgHair2.Height;

            Canvas.SetLeft(Hair2, 50);
            Canvas.SetTop(Hair2, 50);
            myCanvas.Children.Add(Hair2);
            hair = "Hair2";
        }

        private void hairThree()
        {
            Image Hair3 = new Image();
            Hair3.Source = imgHair3;
            Hair3.Width = imgHair3.Width;
            Hair3.Height = imgHair3.Height;

            Canvas.SetLeft(Hair3, 50);
            Canvas.SetTop(Hair3, 50);
            myCanvas.Children.Add(Hair3);
            hair = "Hair3";
        }
        private void hairFour()
        {
            Image Hair4 = new Image();
            Hair4.Source = imgHair4;
            Hair4.Width = imgHair4.Width;
            Hair4.Height = imgHair4.Height;

            Canvas.SetLeft(Hair4, 50);
            Canvas.SetTop(Hair4, 50);
            myCanvas.Children.Add(Hair4);
            hair = "Hair4";
        }

        private void eyesOne()
        {
            Image Eyes1 = new Image();
            Eyes1.Source = imgEyes1;
            Eyes1.Width = imgEyes1.Width;
            Eyes1.Height = imgEyes1.Height;

            Canvas.SetLeft(Eyes1, 50);
            Canvas.SetTop(Eyes1, 250);
            myCanvas.Children.Add(Eyes1);
            eyes = "eyes1";
        }
        private void eyesTwo()
        {
            Image Eyes2 = new Image();
            Eyes2.Source = imgEyes2;
            Eyes2.Width = imgEyes2.Width;
            Eyes2.Height = imgEyes2.Height;

            Canvas.SetLeft(Eyes2, 50);
            Canvas.SetTop(Eyes2, 250);
            myCanvas.Children.Add(Eyes2);
            eyes = "eyes2";
        }
        private void eyesThree()
        {
            Image Eyes3 = new Image();
            Eyes3.Source = imgEyes3;
            Eyes3.Width = imgEyes3.Width;
            Eyes3.Height = imgEyes3.Height;

            Canvas.SetLeft(Eyes3, 50);
            Canvas.SetTop(Eyes3, 250);
            myCanvas.Children.Add(Eyes3);
            eyes = "eyes3";
        }
        private void eyesFour()
        {
            Image Eyes4 = new Image();
            Eyes4.Source = imgEyes4;
            Eyes4.Width = imgEyes4.Width;
            Eyes4.Height = imgEyes4.Height;

            Canvas.SetLeft(Eyes4, 50);
            Canvas.SetTop(Eyes4, 250);
            myCanvas.Children.Add(Eyes4);
            eyes = "eyes4";
        }

        private void noseOne()
        {
            Image Nose1 = new Image();
            Nose1.Source = imgNose1;
            Nose1.Width = imgNose1.Width;
            Nose1.Height = imgNose1.Height;

            Canvas.SetLeft(Nose1, 50);
            Canvas.SetTop(Nose1, 450);
            myCanvas.Children.Add(Nose1);
            nose = "nose1";
        }
        private void noseTwo()
        {
            Image Nose2 = new Image();
            Nose2.Source = imgNose2;
            Nose2.Width = imgNose2.Width;
            Nose2.Height = imgNose2.Height;

            Canvas.SetLeft(Nose2, 50);
            Canvas.SetTop(Nose2, 450);
            myCanvas.Children.Add(Nose2);
            nose = "nose2";
        }
        private void noseThree()
        {
            Image Nose3 = new Image();
            Nose3.Source = imgNose3;
            Nose3.Width = imgNose3.Width;
            Nose3.Height = imgNose3.Height;

            Canvas.SetLeft(Nose3, 50);
            Canvas.SetTop(Nose3, 450);
            myCanvas.Children.Add(Nose3);
            nose = "nose3";
        }
        private void noseFour()
        {
            Image Nose4 = new Image();
            Nose4.Source = imgNose4;
            Nose4.Width = imgNose4.Width;
            Nose4.Height = imgNose4.Height;

            Canvas.SetLeft(Nose4, 50);
            Canvas.SetTop(Nose4, 450);
            myCanvas.Children.Add(Nose4);
            nose = "nose4";
        }

        private void mouthOne()
        {
            Image Mouth1 = new Image();
            Mouth1.Source = imgMouth1;
            Mouth1.Width = imgMouth1.Width;
            Mouth1.Height = imgMouth1.Height;

            Canvas.SetLeft(Mouth1, 50);
            Canvas.SetTop(Mouth1, 650);
            myCanvas.Children.Add(Mouth1);
            mouth = "mouth1";
        }
        private void mouthTwo()
        {
            Image Mouth2 = new Image();
            Mouth2.Source = imgMouth2;
            Mouth2.Width = imgMouth2.Width;
            Mouth2.Height = imgMouth2.Height;

            Canvas.SetLeft(Mouth2, 50);
            Canvas.SetTop(Mouth2, 650);
            myCanvas.Children.Add(Mouth2);
            mouth = "mouth2";
        }
        private void mouthThree()
        {
            Image Mouth3 = new Image();
            Mouth3.Source = imgMouth3;
            Mouth3.Width = imgMouth3.Width;
            Mouth3.Height = imgMouth3.Height;

            Canvas.SetLeft(Mouth3, 50);
            Canvas.SetTop(Mouth3, 650);
            myCanvas.Children.Add(Mouth3);
            mouth = "mouth3";
        }
        private void mouthFour()
        {
            Image Mouth4 = new Image();
            Mouth4.Source = imgMouth4;
            Mouth4.Width = imgMouth4.Width;
            Mouth4.Height = imgMouth4.Height;

            Canvas.SetLeft(Mouth4, 50);
            Canvas.SetTop(Mouth4, 650);
            myCanvas.Children.Add(Mouth4);
            mouth = "mouth4";
        }
        private void randomFace()
        {

            int rx1 = random.Next(1, 200);
            int ry1 = random.Next(1, 200);
            int rx2 = random.Next(1, 200);
            int ry2 = random.Next(1, 200);

            //Hair
            if (ry1 % 4 == 0)
                hairOne();

            else if (ry1 % 4 == 1)
                hairTwo();

            else if (ry1 % 4 == 2)
                hairThree();

            else
                hairFour();

            //eyes
            if (ry1 % 4 == 0)
                eyesOne();

            else if (ry1 % 4 == 1)
                eyesTwo();

            else if (ry1 % 4 == 2)
                eyesThree();
            else
                eyesFour();

            //Nose
            if (rx2 % 4 == 0)
                noseOne();

            else if (rx2 % 4 == 1)
                noseTwo();

            else if (rx2 % 4 == 2)
                noseThree();

            else
                noseFour();

            //Mouth
            if (ry2 % 4 == 0)
                mouthOne();

            else if (ry2 % 4 == 1)
                mouthTwo();

            else if (ry2 % 4 == 2)
                mouthThree();

            else
                mouthFour();

        }

        public void MyImageMethod()
        {
            hairOne();
            eyesOne();
            noseOne();
            mouthOne();
        }

    }
}