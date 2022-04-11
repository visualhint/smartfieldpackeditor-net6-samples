using System;
using VisualHint.SmartPropertyGrid;
using System.ComponentModel;
using VisualHint.SmartFieldPackEditor.DateTimePack;
using VisualHint.SmartPropertyGrid.SpgSfpeAdapter;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;
using System.Reflection;

namespace FieldPackEditorShowRoom
{
    public class MyPropertyGrid : VisualHint.SmartPropertyGrid.PropertyGrid
    {
        public MyPropertyGrid()
        {
            resourceManager = new ResourceManager("FieldPackEditorShowRoom.Resource1", Assembly.GetExecutingAssembly());
        }

        private int _id = 1;

        private ResourceManager resourceManager;

        public void Populate(VisualHint.SmartFieldPackEditor.DateTimePack.DateTimePicker dtp)
        {
            PropertyEnumerator rootEnum = AppendRootCategory(_id++, "Appearance");

            PropertyEnumerator subEnum = AppendSubCategory(rootEnum, _id++, "Editor");

            PropertyEnumerator propEnum;

            propEnum = AppendProperty(subEnum, _id++, "Culture", dtp, "CultureInfo", "");
            propEnum.Property.Comment = propEnum.Property.Value.PropertyDescriptor.Description;

            propEnum = AppendProperty(subEnum, _id++, "Background color", dtp, "BackColor", "");
            propEnum.Property.Look = new PropertyColorLook();
            propEnum.Property.Comment = "BackColor is natively supported. In the MS DTP, this property is useless " +
                "and the effect can only be achieved by using a trick";

            propEnum = AppendProperty(subEnum, _id++, "Foreground color", dtp, "ForeColor", "");
            propEnum.Property.Look = new PropertyColorLook();
            propEnum.Property.Comment = "ForeColor is natively supported. In the MS DTP, this property is useless " +
                "and there is no easy way to make it work.";

            propEnum = AppendProperty(subEnum, _id++, "Readonly foreground color", dtp, "ReadonlyForeColor", "");
            propEnum.Property.Look = new PropertyColorLook();

            propEnum = AppendProperty(subEnum, _id++, "Disabled background color", dtp, "DisabledBackColor", "");
            propEnum.Property.Look = new PropertyColorLook();
            propEnum.Property.Comment = "This options does not exist in the MS DTP. Here, for accessibility reasons, " +
                "you can change the background color of the disabled control.";

            propEnum = AppendProperty(subEnum, _id++, "Disabled foreground color", dtp, "DisabledForeColor", "");
            propEnum.Property.Look = new PropertyColorLook();
            propEnum.Property.Comment = "This options does not exist in the MS DTP. Here, for accessibility reasons, " +
                "you can change the color of disabled text.";

            propEnum = AppendProperty(subEnum, _id++, "Font", dtp, "Font", "",
                new PropertyHideAttribute("GdiCharSet"),
                new PropertyHideAttribute("GdiVerticalFont"),
                new PropertyHideAttribute("Unit"));
            propEnum.Property.Comment = "This is not everyday that you will use an italic font in this kind of " +
                "control, but look how this is better handled in SFPE.";
            AppendProperty(subEnum, _id++, "Border style", dtp, "BorderStyle", "");
            AppendProperty(subEnum, _id++, "Fixed width", dtp, "FixedWidth", "");
            AppendProperty(subEnum, _id++, "Format", dtp, "Format", "",
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelRadioButton),
                new PropertyLookAttribute(typeof(PropertyRadioButtonLook)));
            AppendProperty(subEnum, _id++, "Custom format", dtp, "CustomFormat", "");
            AppendProperty(subEnum, _id++, "Null string", dtp, "NullString", "");
            AppendProperty(subEnum, _id++, "Support for RTL languages", dtp, "RightToLeft", "");
            AppendProperty(subEnum, _id++, "Highlight pack on drop", dtp, "HighlightPackWhenDroppedDown", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Show checkbox", dtp, "ShowCheckBox", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Show dropdown button", dtp, "ShowDropDown", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Show up/down button", dtp, "ShowUpDown", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Text", dtp, "Text", "");

            subEnum = AppendSubCategory(rootEnum, _id++, "Calendar");
 //           ExpandProperty(subEnum, false);

            AppendProperty(subEnum, _id++, "Show Today button", dtp, "ShowCalendarTodayButton", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Today label", dtp, "CalendarTodayLabel", "");
            AppendProperty(subEnum, _id++, "Show Clear button", dtp, "ShowCalendarClearButton", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Clear label", dtp, "CalendarClearLabel", "");
            AppendProperty(subEnum, _id++, "Background color", dtp, "CalendarMonthBackground", "");
            AppendProperty(subEnum, _id++, "Foreground color", dtp, "CalendarForeColor", "");
            AppendProperty(subEnum, _id++, "Title background color", dtp, "CalendarTitleBackColor", "");
            AppendProperty(subEnum, _id++, "Title foreground color", dtp, "CalendarTitleForeColor", "");
            AppendProperty(subEnum, _id++, "Trailing foreground color", dtp, "CalendarTrailingForeColor", "");
            AppendProperty(subEnum, _id++, "Selected date background color", dtp, "CalendarSelectedDateBackColor", "");
            AppendProperty(subEnum, _id++, "Selected date foreground color", dtp, "CalendarSelectedDateForeColor", "");
            AppendProperty(subEnum, _id++, "Today rectangle color", dtp, "CalendarTodayRectColor", "");
            AppendProperty(subEnum, _id++, "Day names foreground color", dtp, "CalendarDayNamesForeColor", "");
            AppendProperty(subEnum, _id++, "Font", dtp, "CalendarFont", "",
                new PropertyHideAttribute("GdiCharSet"),
                new PropertyHideAttribute("GdiVerticalFont"),
                new PropertyHideAttribute("Unit"));

            rootEnum = AppendRootCategory(_id++, "Behavior");

            AppendProperty(rootEnum, _id++, "Enabled", dtp, "Enabled", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));

            subEnum = AppendSubCategory(rootEnum, _id++, "Checkbox");

            AppendProperty(subEnum, _id++, "Mode", dtp, "CheckBoxMode", "",
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelRadioButton),
                new PropertyLookAttribute(typeof(PropertyRadioButtonLook)));
            AppendProperty(subEnum, _id++, "Checked", dtp, "Checked", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));

            subEnum = AppendSubCategory(rootEnum, _id++, "Keyboard");

            AppendProperty(subEnum, _id++, "Field auto jump", dtp, "JumpToNextFieldOnEdition", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Accepts delete key", dtp, "AcceptsDeleteKey", "",
                new PropertyValueDisplayedAsAttribute(new string[] { "", "" }),
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelCheckbox),
                new PropertyLookAttribute(typeof(PropertyCheckboxLook)));
            AppendProperty(subEnum, _id++, "Tab key mode", dtp, "TabKeyNavigationMode", "",
                new PropertyFeelAttribute(VisualHint.SmartPropertyGrid.PropertyGrid.FeelRadioButton),
                new PropertyLookAttribute(typeof(PropertyRadioButtonLook)));

            rootEnum = AppendRootCategory(_id++, "Data");

            RegisterFeel("sfpedatetime", new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyDateTimeFeel(this));

            propEnum = AppendProperty(rootEnum, _id++, "Minimum value", dtp, "MinDate", "");
            propEnum.Property.Look = new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyDateTimeLook();
            propEnum.Property.Feel = GetRegisteredFeel("sfpedatetime");

            propEnum = AppendProperty(rootEnum, _id++, "Maximum value", dtp, "MaxDate", "");
            propEnum.Property.Look = new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyDateTimeLook();
            propEnum.Property.Feel = GetRegisteredFeel("sfpedatetime");

            propEnum = AppendProperty(rootEnum, _id++, "Value", dtp, "Value", "");
            propEnum.Property.Look = new VisualHint.SmartPropertyGrid.SpgSfpeAdapter.PropertyDateTimeLook();
            propEnum.Property.Feel = GetRegisteredFeel("sfpedatetime");
        }

        private string _activeComment = "";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ActiveComment
        {
            get { return _activeComment; }
            set
            {
                _activeComment = value;
                if (ActiveCommentChanged != null)
                    ActiveCommentChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler ActiveCommentChanged;

        protected override void OnPropertySelected(PropertySelectedEventArgs e)
        {
            ActiveComment = e.PropertyEnum.Property.Comment;
            base.OnPropertySelected(e);
        }

        protected override void OnPropertyPreFilterOut(PropertyPreFilterOutEventArgs e)
        {
            if (e.PropertyDescriptor.Name == "AccessibleDescription")
                e.FilterOut = PropertyPreFilterOutEventArgs.FilterModes.FilterOut;
            else if (e.PropertyDescriptor.Name == "AccessibleName")
                e.FilterOut = PropertyPreFilterOutEventArgs.FilterModes.FilterOut;
            else if (e.PropertyDescriptor.Name == "AccessibleRole")
                e.FilterOut = PropertyPreFilterOutEventArgs.FilterModes.FilterOut;
            else if (e.PropertyDescriptor.Name == "DataBindings")
                e.FilterOut = PropertyPreFilterOutEventArgs.FilterModes.FilterOut;
            else if (e.PropertyDescriptor.Name == "Text")
                e.FilterOut = PropertyPreFilterOutEventArgs.FilterModes.DontFilter;

            base.OnPropertyPreFilterOut(e);
        }

        protected override void OnInPlaceCtrlCreated(InPlaceCtrlCreatedEventArgs e)
        {
            if ((e.PropertyEnum.Property.Id == 3) || (e.PropertyEnum.Property.Id == 4) ||
                (e.PropertyEnum.Property.Id == 5) || (e.PropertyEnum.Property.Id == 6))
            {
                System.Windows.Forms.TextBox tb = ((IInPlaceControl)e.InPlaceCtrl).TextBox;
                if (tb != null)
                {
                    if (e.PropertyEnum.Property.Value.ReadOnly)
                    {
                        tb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
                    }
                    else
                    {
                        string[] displayedValues = e.PropertyEnum.Property.Value.GetDisplayedValues();
                        if (displayedValues.Length > 0)
                        {
                            System.Windows.Forms.AutoCompleteStringCollection coll = new System.Windows.Forms.AutoCompleteStringCollection();
                            coll.AddRange(displayedValues);

                            tb.AutoCompleteCustomSource = coll;
                            tb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                            tb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                        }
                    }
                }
            }

            base.OnInPlaceCtrlCreated(e);
        }
    }
}
