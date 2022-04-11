using System;
using System.Drawing;
using System.Windows.Forms;
using VisualHint.SmartFieldPackEditor.LatitudeLongitude;
using VisualHint.SmartFieldPackEditor.DateTimePack;
using VisualHint.SmartFieldPackEditor;
using System.Resources;
using System.Reflection;
using VisualHint.SmartPropertyGrid;
using VisualHint.SmartPropertyGrid.SpgSfpeAdapter;
using VisualHint.SmartFieldPackEditor.Duration;

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
                VisualHint.SmartPropertyGrid.PropertyGrid.LocateLicenseInThisAssembly();
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

        public class Test
        {
            private PointF _coords = new(41.5f, -2.3f);
            public PointF Coords
            {
                set { _coords = value; }
                get { return _coords; }
            }
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

            smartPropertyGrid.Populate(smartDateTimePicker);

            tbComments.DataBindings.Add(new Binding("Text", smartPropertyGrid, "ActiveComment", false, DataSourceUpdateMode.OnPropertyChanged));

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

            // Second propertyGrid
            //--------------------

            smartPropertyGrid2.RegisterFeel("FeelLatLong", new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyLatLongFeel(smartPropertyGrid2));
            smartPropertyGrid2.RegisterFeel("FeelIpAddress", new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyIpAddressFeel(smartPropertyGrid2));
            smartPropertyGrid2.RegisterFeel("FeelTimeSpan", new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyTimeSpanFeel(smartPropertyGrid2));

            PropertyEnumerator rootEnum = smartPropertyGrid2.AppendRootCategory(0, "Data");

            PropertyEnumerator subEnum = smartPropertyGrid2.AppendSubCategory(rootEnum, 0, "Position");

            PropertyEnumerator propEnum = smartPropertyGrid2.AppendProperty(subEnum, 1, "Latitude", latLongEditor, "Latitude", "");
            propEnum.Property.Look = new PropertyLatLongLook(LatLongContent.Latitude, 4, 0, "No latitude", "No longitude", LatLongEditor.VisibleFieldsMode.DegMinSec);
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelLatLong");

            propEnum = smartPropertyGrid2.AppendProperty(subEnum, 2, "Longitude", latLongEditor, "Longitude", "");
            propEnum.Property.Look = new PropertyLatLongLook(LatLongContent.Longitude, 0, 1, "No latitude", "No longitude", LatLongEditor.VisibleFieldsMode.DegMinSec);
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelLatLong");

            propEnum = smartPropertyGrid2.AppendProperty(subEnum, 3, "Lat / Long", latLongEditor, "Latitude", "");
            propEnum.Property.AddValue(PropertyLatLongLook.LongitudeValue, latLongEditor, "Longitude", null);
            propEnum.Property.Look = new PropertyLatLongLook(LatLongContent.LatitudeAndLongitude, 4, 1, "No latitude", "No longitude", LatLongEditor.VisibleFieldsMode.DegMinSec);
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelLatLong");

            propEnum = smartPropertyGrid2.AppendProperty(subEnum, 5, "Fields", latLongEditor, "VisibleFields", "");

            propEnum = smartPropertyGrid2.AppendProperty(subEnum, 6, "Lat precision", latLongEditor, "LatitudePrecision", "");
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel(VisualHint.SmartPropertyGrid.PropertyGrid.FeelEditUpDown);

            subEnum = smartPropertyGrid2.AppendSubCategory(rootEnum, 0, "Network");

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 7, "IP Address", ipAddrEditor, "Value", "");
            propEnum.Property.Look = new PropertyIpAddressLook("Enter an IP address");
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelIpAddress");

            subEnum = smartPropertyGrid2.AppendSubCategory(rootEnum, 0, "Duration");

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 8, "Format", timeSpanEditor, "CustomFormat", "");

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 9, "ChangeValueAtFieldBounds", timeSpanEditor, "ChangeValueAtFieldBounds", "");
            propEnum.Property.Look = new PropertyCheckboxLook();
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox);
            propEnum.Property.Value.ResetDisplayedValues(new string[] { "", "" });

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 10, "ShowCheckBox", timeSpanEditor, "ShowCheckBox", "");
            propEnum.Property.Look = new PropertyCheckboxLook();
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox);
            propEnum.Property.Value.ResetDisplayedValues(new string[] { "", "" });

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 11, "CheckBoxMode", timeSpanEditor, "CheckBoxMode", "");

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 12, "Value", timeSpanEditor, "Value", "");
            propEnum.Property.Look = new PropertyTimeSpanLook(timeSpanEditor.Format, timeSpanEditor.CustomFormat, timeSpanEditor.MinDuration.Value, timeSpanEditor.MaxDuration.Value, "Enter a duration");
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelTimeSpan");
            smartPropertyGrid2.ExpandProperty(propEnum, true);

            propEnum = smartPropertyGrid2.AppendProperty(rootEnum, 13, "Max", timeSpanEditor, "MaxDuration", "");
            propEnum.Property.Look = new PropertyTimeSpanLook(VisualHint.SmartFieldPackEditor.Duration.TimeSpanEditor.TimeSpanEditorFormat.Custom, "d.h:m:s.fffffff",
                TimeSpan.Zero, TimeSpan.MaxValue, "Enter a duration");
            propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelTimeSpan");
            
            smartPropertyGrid2.PropertyCreated += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyCreatedEventHandler(SmartPropertyGrid2_PropertyCreated);
            smartPropertyGrid2.PropertyChanged += new VisualHint.SmartPropertyGrid.PropertyGrid.PropertyChangedEventHandler(SmartPropertyGrid2_PropertyChanged);
//            smartPropertyGrid2.SelectedObject = new Test();

            // timeSpanEditor
            //---------------

            var tsFieldPack = timeSpanEditor.GetFieldPack(TimeSpanFieldPack.FieldPackName);
            tsFieldPack.FieldSeparatorWidth = 1;

            // Host of the week field pack
            //----------------------------

            var dtFieldPack = new DateTimeFieldPack("dddd dd MMMM yyyy");
            weekEditor.AddFieldPack(dtFieldPack);

            dtFieldPack.MinValue = new DateTime(1753, 1, 1);
            dtFieldPack.MaxValue = new DateTime(9998, 12, 31);
            dtFieldPack.SeparatorWidth = 4;
            dtFieldPack.Value = DateTime.Now;
            dtFieldPack.ValueChanged += new EventHandler(DtFieldPack_ValueChanged);

            // DTP of the Nullify tab

            var resourceManager = new ResourceManager("FieldPackEditorShowRoom.Resource1", Assembly.GetExecutingAssembly());
            var bmp = (Bitmap)resourceManager.GetObject("clear");

//            ButtonField buttonField = dateTimePicker3.AddButton("Clear");
            var buttonField = dateTimePicker3.AddButton(bmp);
            buttonField.Clicked += new EventHandler(OnButtonFieldClicked);

            dateTimePicker1.NullString = "Type to set...";
            dateTimePicker2.AcceptsDeleteKey = true;
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

            var stFieldPack = new FieldPack();
            dtpBound2.AddFieldPack(stFieldPack);
            stFieldPack.IsNull = false;
            var stField = new StaticField(" at ");
            stFieldPack.AddField(stField);
            stFieldPack.SeparatorWidth = 0;

            dtFieldPack = new DateTimeFieldPack("t");
            dtpBound2.AddFieldPack(dtFieldPack);
            dtFieldPack.Value = DateTime.Now;
            dtFieldPack.DataBindings.Add(new Binding("Value", this.tableBindingSource, "time", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dtFieldPack.NullString = "(no time)";
            dtFieldPack.FixedWidth = false;

            dtpBound1.DataBindingsUpdated += new EventHandler(DtpBound1_DataBindingsUpdated);

            foreach(FieldPack pack in dtpBound2.FieldPackCollection)
                pack.DataBindingsUpdated += new EventHandler(DtpBound1_DataBindingsUpdated);
        }

        void SmartPropertyGrid2_PropertyChanged(object sender, VisualHint.SmartPropertyGrid.PropertyChangedEventArgs e)
        {
/*            if (e.AdditionalValueKey == null)
            {
                Test test = (Test)e.PropertyEnum.Property.Tag;
                float x = (float)e.PropertyEnum.Property.Value.GetValue();
                test.Coords = new PointF(x, test.Coords.Y);
            }
            else
            {
                Test test = (Test)e.PropertyEnum.Property.Tag;
                float y = (float)e.PropertyEnum.Property.GetValue(PropertyLatLongLook.LongitudeValue).GetValue();
                test.Coords = new PointF(test.Coords.X, y);
            }*/

            if (e.PropertyEnum.Property.Name == "CustomFormat")
            {
                var propEnum = (sender as VisualHint.SmartPropertyGrid.PropertyGrid).FindProperty(12);
                (propEnum.Property.Look as PropertyTimeSpanLook).CustomFormat = (string)e.PropertyEnum.Property.Value.GetValue();
            }
            else if (e.PropertyEnum.Property.Id == 13)
            {
                var propEnum = (sender as VisualHint.SmartPropertyGrid.PropertyGrid).FindProperty(12);
                (propEnum.Property.Look as PropertyTimeSpanLook).MaxDuration = (TimeSpan)e.PropertyEnum.Property.Value.GetValue();
            }
            else if ((e.PropertyEnum.Parent.Property != null) && (e.PropertyEnum.Parent.Property.Id == 13))
            {
                var propEnum = (sender as VisualHint.SmartPropertyGrid.PropertyGrid).FindProperty(12);
                (propEnum.Property.Look as PropertyTimeSpanLook).MaxDuration = (TimeSpan)e.PropertyEnum.Parent.Property.Value.GetValue();
            }
        }

        void SmartPropertyGrid2_PropertyCreated(object sender, PropertyCreatedEventArgs e)
        {
/*            if ((e.PropertyEnum.Property.Value != null) && (e.PropertyEnum.Property.Value.UnderlyingType.Equals(typeof(PointF))))
            {
                PointF pt = (PointF)e.PropertyEnum.Property.Value.GetValue();
                PropertyEnumerator propEnum = smartPropertyGrid2.AppendManagedProperty(e.PropertyEnum.Parent, 0,
                    "Coords", typeof(float), pt.X, "");
                propEnum.Property.AddManagedValue(PropertyLatLongLook.LongitudeValue, typeof(float),
                    pt.Y, null);
                propEnum.Property.Look = new PropertyLatLongLook(LatLongContent.LatitudeAndLongitude, 4, 1, "No latitude", "No longitude", LatLongEditor.VisibleFieldsMode.DegMinSec);
                propEnum.Property.Feel = smartPropertyGrid2.GetRegisteredFeel("FeelLatLong");
                smartPropertyGrid2.ShowProperty(e.PropertyEnum, false);
                propEnum.Property.Tag = e.PropertyEnum.Property.Value.TargetInstance;
            }*/
        }

        void OnButtonFieldClicked(object sender, EventArgs e)
        {
            (sender as Field).OwnerFieldPack.OwnerEditor.SelectedField.OwnerFieldPack.Value = null;
        }

        void DtpBound1_DataBindingsUpdated(object sender, EventArgs e)
        {
            this.dataGridView1.Refresh();
        }

        void DtFieldPack_ValueChanged(object sender, EventArgs e)
        {
            var weekPack = (sender as FieldPack).OwnerEditor.GetFieldPack("weekfieldpack");
            if (weekPack != null)
            {
                ((WeekNumberField)weekPack.GetField(WeekNumberField.WeekNumberFieldName)).DateTime =
                    (DateTime)((sender as FieldPack).Value);
            }
        }

        void WeekField_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var dtPack = (sender as Field).OwnerFieldPack.OwnerEditor.GetFieldPack(DateTimeFieldPack.FieldPackName);
            dtPack.Value = (sender as WeekNumberField).DateTime;
        }

        void SmartPropertyGrid_PropertyChanged(object sender, VisualHint.SmartPropertyGrid.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyEnum.Property.Name == "RightToLeft")
                {
                    msDateTimePicker.RightToLeft = smartDateTimePicker.RightToLeft;
                    msDateTimePicker.RightToLeftLayout = (msDateTimePicker.RightToLeft == RightToLeft.Yes);
                    cbCustomFormat.RightToLeft = smartDateTimePicker.RightToLeft;
                }
                else if (e.PropertyEnum.Property.Name == "CustomFormat")
                {
                    cbCustomFormat.Text = smartDateTimePicker.CustomFormat;
                }
                else if (e.PropertyEnum.Property.Name == "BackColor")
                {
                    msDateTimePicker.BackColor = smartDateTimePicker.BackColor;
                }
                else if (e.PropertyEnum.Property.Name == "ForeColor")
                {
                    msDateTimePicker.ForeColor = smartDateTimePicker.ForeColor;
                }
                else if (e.PropertyEnum.Property.Name == "Font")
                {
                    msDateTimePicker.Font = smartDateTimePicker.Font;
                }
                else if ((e.PropertyEnum.Parent.Property != null) && (e.PropertyEnum.Parent.Property.DisplayName == "Font"))
                {
                    if (e.PropertyEnum.Parent.Parent.Property.DisplayName == "Editor")
                        msDateTimePicker.Font = smartDateTimePicker.Font;
                    else if (e.PropertyEnum.Parent.Parent.Property.DisplayName == "Calendar")
                        msDateTimePicker.CalendarFont = smartDateTimePicker.CalendarFont;
                }
                else if (e.PropertyEnum.Property.Name == "Format")
                {
                    msDateTimePicker.Format = smartDateTimePicker.Format;
                }
                else if (e.PropertyEnum.Property.Name == "ShowCheckBox")
                {
                    msDateTimePicker.ShowCheckBox = smartDateTimePicker.ShowCheckBox;
                }
                else if (e.PropertyEnum.Property.Name == "Text")
                {
                    msDateTimePicker.Text = smartDateTimePicker.Text;
                }
                else if (e.PropertyEnum.Property.Name == "CalendarFont")
                {
                    msDateTimePicker.CalendarFont = smartDateTimePicker.CalendarFont;
                }
                else if (e.PropertyEnum.Property.Name == "CalendarForeColor")
                {
                    msDateTimePicker.CalendarForeColor = smartDateTimePicker.CalendarForeColor;
                }
                else if (e.PropertyEnum.Property.Name == "CalendarMonthBackground")
                {
                    msDateTimePicker.CalendarMonthBackground = smartDateTimePicker.CalendarMonthBackground;
                }
                else if (e.PropertyEnum.Property.Name == "CalendarTitleBackColor")
                {
                    msDateTimePicker.CalendarTitleBackColor = smartDateTimePicker.CalendarTitleBackColor;
                }
                else if (e.PropertyEnum.Property.Name == "CalendarTitleForeColor")
                {
                    msDateTimePicker.CalendarTitleForeColor = smartDateTimePicker.CalendarTitleForeColor;
                }
                else if (e.PropertyEnum.Property.Name == "CalendarTrailingForeColor")
                {
                    msDateTimePicker.CalendarTrailingForeColor = smartDateTimePicker.CalendarTrailingForeColor;
                }
                else if (e.PropertyEnum.Property.Name == "Checked")
                {
                    msDateTimePicker.Checked = smartDateTimePicker.Checked;
                }
                else if (e.PropertyEnum.Property.Name == "MinDate")
                {
                    msDateTimePicker.MinDate = smartDateTimePicker.MinDate;
                }
                else if (e.PropertyEnum.Property.Name == "MaxDate")
                {
                    msDateTimePicker.MaxDate = smartDateTimePicker.MaxDate;
                }
                else if (e.PropertyEnum.Property.Name == "Value")
                {
                    if (smartDateTimePicker.Value is DateTime time)
                        msDateTimePicker.Value = time;
                }
            }
            catch (Exception)
            {
            }
        }

        void CbCustomFormat_TextChanged(object sender, EventArgs e)
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

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }

        private void DtpBound1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as VisualHint.SmartFieldPackEditor.DateTimePack.DateTimePicker).Value = DBNull.Value;
        }

        private void DtpBound2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as FieldPackEditor).SelectedField.OwnerFieldPack.Value = DBNull.Value;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var wPack = weekEditor.GetFieldPack("weekfieldpack");

            if (wPack == null)
            {
                // Here is the dirty implementation of a week editor. It could be done better by
                // creating a special WeekNumberFieldPack class that would attach itself automatically
                // to a DateTimeFieldPack in the editor
                //------------------------------------------------------------------------------------

                var weekFieldPack = new FieldPack();
                weekEditor.AddFieldPack(weekFieldPack);

                weekFieldPack.Name = "weekfieldpack"; // for easy retrieval
                weekFieldPack.IsNull = false;
                weekFieldPack.SeparatorWidth = 4;
                weekFieldPack.BackColor = Color.Beige;
                weekFieldPack.AddField(new StaticField("(Week# "));
                var dtPack = weekEditor.GetFieldPack(DateTimeFieldPack.FieldPackName);
                var weekField = new WeekNumberField((DateTime)dtPack.Value);
                weekFieldPack.AddField(weekField);
                weekFieldPack.AddField(new StaticField(")"));
                weekField.ValueChanged += new Field.ValueChangedEventHandler(WeekField_ValueChanged);

                weekEditor.RecalcLayout();
            }
        }

        private void IpAddrEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as VisualHint.SmartFieldPackEditor.IpAddressPack.IpAddressEditor).Value = null;
        }

        private void LatLongEditor_LatitudeChanged(object sender, EventArgs e)
        {
            smartPropertyGrid2.Refresh();
        }

        private void LatLongEditor_LongitudeChanged(object sender, EventArgs e)
        {
            smartPropertyGrid2.Refresh();
        }

        private void IpAddrEditor_ValueChanged(object sender, EventArgs e)
        {
            smartPropertyGrid2.Refresh();
        }

        private void SmartPropertyGrid2_InPlaceCtrlCreated(object sender, InPlaceCtrlCreatedEventArgs e)
        {
            if (e.InPlaceCtrl is PropInPlaceTimeSpan)
            {
                (e.InPlaceCtrl as PropInPlaceTimeSpan).ValueChanged += new EventHandler(Form1_ValueChanged);
            }
        }

        void Form1_ValueChanged(object sender, EventArgs e)
        {
            var fieldPack = sender as FieldPack;
            var field = fieldPack.GetField(TimeSpanFieldPack.StaticFieldNameDays);
            if (field != fieldPack.EmptyField)
                SetDayLabel(field);
        }

        private void SmartPropertyGrid2_InPlaceCtrlHidden(object sender, InPlaceCtrlVisibleEventArgs e)
        {
            if (e.InPlaceCtrl is PropInPlaceTimeSpan)
            {
                (e.InPlaceCtrl as PropInPlaceTimeSpan).ValueChanged -= new EventHandler(Form1_ValueChanged);
            }

            if (e.PropertyEnum.Property.DisplayName == "Value")
            {
                string str = "";
                var fieldpack = (e.InPlaceCtrl as PropInPlaceTimeSpan).GetFieldPack(TimeSpanFieldPack.FieldPackName);
                foreach (Field field in fieldpack)
                {
                    if (field is StaticField)
                        str += "'" + field.Value + "'";
                    else
                        str += (field as TimeSpanField).CustomFormat;
                }

                PropertyTimeSpanLook look = (e.PropertyEnum.Property.Look as PropertyTimeSpanLook);
                e.PropertyEnum.Property.Look = new PropertyTimeSpanLook(TimeSpanEditor.TimeSpanEditorFormat.Custom, str, look.MinDuration, look.MaxDuration, look.NullString);
            }
        }

        private static void SetDayLabel(Field field)
        {
            var editor = (TimeSpanEditor)field.OwnerFieldPack.OwnerEditor;

            if (editor.Value != null)
            {
                var value = (TimeSpan)editor.Value;
                if (value.Days > 1)
                {
                    if (field.Value.Equals(" day "))
                    {
                        field.Value = " days ";
                        editor.RecalcLayout();
                    }
                }
                else
                {
                    if (field.Value.Equals(" days "))
                    {
                        field.Value = " day ";
                        editor.RecalcLayout();
                    }
                }
            }
        }
   }
}