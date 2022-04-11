using System;
using System.Drawing;
using System.Windows.Forms;
using VisualHint.SmartFieldPackEditor.LatitudeLongitude;
using VisualHint.SmartFieldPackEditor.DateTimePack;
using VisualHint.SmartFieldPackEditor;
using System.Resources;
using System.Reflection;

namespace FieldPackEditorShowRoom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            try
            {
                // When you have a license file, add it to your project as an embedded resource and call this:
                FieldPackEditor.LocateLicenseInThisAssembly();
            }
            catch
            {
                throw;
            }

            InitializeComponent();
        }

        private decimal _latitude = 41.0356m;

        public decimal Latitude
        {
            set { _latitude = value; }
            get { return _latitude; }
        }

        private decimal _longitude = 41.0356m;

        public decimal Longitude
        {
            set { _longitude = value; }
            get { return _longitude; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Fails under windows x64 !! Why ?
                this.tableTableAdapter.Fill(this.sampleDataSet.table);
            }
            catch (Exception)
            {
            }

            // Setup of the latitude/longitude editor
            //---------------------------------------

            tbLatitude.Text = (41.0356).ToString();
            tbLongitude.Text = (-3.46892).ToString();

            //            latLongEditor.Latitude = 41.0356;
            //          latLongEditor.Longitude = -3.46892;

            latLongEditor.LatitudeNullString = "No latitude";
            latLongEditor.LongitudeNullString = "No longitude";
            latLongEditor.GetFieldPack(LatitudeFieldPack.FieldPackName).DataBindings.Add(
                new Binding("Value", tbLatitude, "Text", false, DataSourceUpdateMode.OnPropertyChanged));
            latLongEditor.GetFieldPack(LongitudeFieldPack.FieldPackName).DataBindings.Add(
                new Binding("Value", tbLongitude, "Text", false, DataSourceUpdateMode.OnPropertyChanged));

            // Host of the week field pack
            //----------------------------

            DateTimeFieldPack dtFieldPack = new DateTimeFieldPack("dddd dd MMMM yyyy");
            weekEditor.AddFieldPack(dtFieldPack);

            dtFieldPack.MinValue = new DateTime(1753, 1, 1);
            dtFieldPack.MaxValue = new DateTime(9998, 12, 31);
            dtFieldPack.SeparatorWidth = 4;
            dtFieldPack.Value = DateTime.Now;
            dtFieldPack.ValueChanged += new EventHandler(dtFieldPack_ValueChanged);

            // DTP of the Nullify tab

            ResourceManager resourceManager = new ResourceManager("FieldPackEditorShowRoom.Resource1", Assembly.GetExecutingAssembly());
            Bitmap bmp = (Bitmap)resourceManager.GetObject("clear");

//            ButtonField buttonField = dateTimePicker3.AddButton("Clear");
            ButtonField buttonField = dateTimePicker3.AddButton(bmp);
            buttonField.Clicked += new EventHandler(OnButtonFieldClicked);

            dateTimePicker1.NullString = "Type to set...";
            dateTimePicker2.NullString = "Type to set...";
            dateTimePicker3.NullString = "Type to set...";

            // Setup of the bound DTPs

            dtpBound1.NullString = "Type to set...";

            dtFieldPack = new DateTimeFieldPack("d");
            dtpBound2.AddFieldPack(dtFieldPack);
            dtFieldPack.Value = DateTime.Now;
            dtFieldPack.SeparatorWidth = 0;
            dtFieldPack.DataBindings.Add(new Binding("Value", this.tableBindingSource, "date", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dtFieldPack.NullString = "(no date)";
            dtFieldPack.FixedWidth = false;

            FieldPack stFieldPack = new FieldPack();
            stFieldPack.IsNull = false;
            StaticField stField = new StaticField(" at ");
            dtpBound2.AddFieldPack(stFieldPack);
            stFieldPack.AddField(stField);
            stFieldPack.SeparatorWidth = 0;

            dtFieldPack = new DateTimeFieldPack("t");
            dtpBound2.AddFieldPack(dtFieldPack);
            dtFieldPack.Value = DateTime.Now;
            dtFieldPack.DataBindings.Add(new Binding("Value", this.tableBindingSource, "time", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dtFieldPack.NullString = "(no time)";
            dtFieldPack.FixedWidth = false;

            dtpBound1.DataBindingsUpdated += new EventHandler(dtpBound1_DataBindingsUpdated);

            foreach(FieldPack pack in dtpBound2.FieldPackCollection)
                pack.DataBindingsUpdated += new EventHandler(dtpBound1_DataBindingsUpdated);

            this.Location = new Point(60, 450);

            durationEditor1.MaxDuration = new TimeSpan(100, 0, 0, 0);
        }

        void OnButtonFieldClicked(object sender, EventArgs e)
        {
            (sender as Field).OwnerFieldPack.OwnerEditor.SelectedField.OwnerFieldPack.Value = null;
        }

        void dtpBound1_DataBindingsUpdated(object sender, EventArgs e)
        {
            this.dataGridView1.Refresh();
        }

        void dtFieldPack_ValueChanged(object sender, EventArgs e)
        {
            FieldPack weekPack = (sender as FieldPack).OwnerEditor.GetFieldPack("weekfieldpack");
            if (weekPack != null)
            {
                ((WeekNumberField)weekPack.GetField(WeekNumberField.WeekNumberFieldName)).DateTime =
                    (DateTime)((sender as FieldPack).Value);
            }
        }

        void weekField_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            FieldPack dtPack = (sender as Field).OwnerFieldPack.OwnerEditor.GetFieldPack(DateTimeFieldPack.FieldPackName);
            dtPack.Value = (sender as WeekNumberField).DateTime;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                if (e.ChangedItem.PropertyDescriptor.Name == "RightToLeft")
                {
                    msDateTimePicker.RightToLeft = smartDateTimePicker.RightToLeft;
                    msDateTimePicker.RightToLeftLayout = (msDateTimePicker.RightToLeft == RightToLeft.Yes);
                    cbCustomFormat.RightToLeft = smartDateTimePicker.RightToLeft;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "CustomFormat")
                {
                    cbCustomFormat.Text = smartDateTimePicker.CustomFormat;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "BackColor")
                {
                    msDateTimePicker.BackColor = smartDateTimePicker.BackColor;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "ForeColor")
                {
                    msDateTimePicker.ForeColor = smartDateTimePicker.ForeColor;
                }
                else if ((e.ChangedItem.PropertyDescriptor.Name == "Font") || ((e.ChangedItem.Parent.PropertyDescriptor != null) &&
                    (e.ChangedItem.Parent.PropertyDescriptor.Name == "Font")))
                {
                    msDateTimePicker.Font = smartDateTimePicker.Font;
                }
                else if ((e.ChangedItem.PropertyDescriptor.Name == "CalendarFont") || ((e.ChangedItem.Parent.PropertyDescriptor != null) &&
               (e.ChangedItem.Parent.PropertyDescriptor.Name == "CalendarFont")))
                {
                    msDateTimePicker.Font = smartDateTimePicker.Font;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "Format")
                {
                    msDateTimePicker.Format = smartDateTimePicker.Format;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "ShowCheckBox")
                {
                    msDateTimePicker.ShowCheckBox = smartDateTimePicker.ShowCheckBox;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "Text")
                {
                    msDateTimePicker.Text = smartDateTimePicker.Text;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "CalendarForeColor")
                {
                    msDateTimePicker.CalendarForeColor = smartDateTimePicker.CalendarForeColor;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "CalendarMonthBackground")
                {
                    msDateTimePicker.CalendarMonthBackground = smartDateTimePicker.CalendarMonthBackground;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "CalendarTitleBackColor")
                {
                    msDateTimePicker.CalendarTitleBackColor = smartDateTimePicker.CalendarTitleBackColor;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "CalendarTitleForeColor")
                {
                    msDateTimePicker.CalendarTitleForeColor = smartDateTimePicker.CalendarTitleForeColor;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "CalendarTrailingForeColor")
                {
                    msDateTimePicker.CalendarTrailingForeColor = smartDateTimePicker.CalendarTrailingForeColor;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "Checked")
                {
                    msDateTimePicker.Checked = smartDateTimePicker.Checked;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "MinDate")
                {
                    msDateTimePicker.MinDate = smartDateTimePicker.MinDate;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "MaxDate")
                {
                    msDateTimePicker.MaxDate = smartDateTimePicker.MaxDate;
                }
                else if (e.ChangedItem.PropertyDescriptor.Name == "Value")
                {
                    if (smartDateTimePicker.Value is DateTime)
                        msDateTimePicker.Value = (DateTime)smartDateTimePicker.Value;
                }
            }
            catch (Exception)
            {
            }
        }

        void cbCustomFormat_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cbCustomFormat.Items.Contains(cbCustomFormat.Text))
                str = new string(cbCustomFormat.Text[0], 1);
            else
                str = cbCustomFormat.Text;

            smartDateTimePicker.CustomFormat = str;
            msDateTimePicker.CustomFormat = str;
            this.dateTimePicker1.CustomFormat = str;
            this.dateTimePicker2.CustomFormat = str;
            this.dateTimePicker3.CustomFormat = str;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }

        private void OnDateTimePicker2KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as VisualHint.SmartFieldPackEditor.DateTimePack.DateTimePicker).Value = DBNull.Value;
        }

        private void dtpBound1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as VisualHint.SmartFieldPackEditor.DateTimePack.DateTimePicker).Value = DBNull.Value;
        }

        private void dtpBound2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as FieldPackEditor).SelectedField.OwnerFieldPack.Value = DBNull.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FieldPack wPack = weekEditor.GetFieldPack("weekfieldpack");

            if (wPack == null)
            {
                // Here is the dirty implementation of a week editor. It could be done better by
                // creating a special WeekNumberFieldPack class that would attach itself automatically
                // to a DateTimeFieldPack in the editor
                //------------------------------------------------------------------------------------

                FieldPack weekFieldPack = new FieldPack();
                weekEditor.AddFieldPack(weekFieldPack);

                weekFieldPack.Name = "weekfieldpack"; // for easy retrieval
                weekFieldPack.IsNull = false;
                weekFieldPack.SeparatorWidth = 4;
                weekFieldPack.BackColor = Color.Beige;
                weekFieldPack.AddField(new StaticField("(Week# "));
                FieldPack dtPack = weekEditor.GetFieldPack(DateTimeFieldPack.FieldPackName);
                WeekNumberField weekField = new WeekNumberField((DateTime)dtPack.Value);
                weekFieldPack.AddField(weekField);
                weekFieldPack.AddField(new StaticField(")"));
                weekField.ValueChanged += new Field.ValueChangedEventHandler(weekField_ValueChanged);

                weekEditor.RecalcLayout();
            }
        }

        private void ipAddressEditor1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as VisualHint.SmartFieldPackEditor.IpAddressPack.IpAddressEditor).Value = null;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            tableTableAdapter.Update(this.sampleDataSet.table);
        }
    }
}
