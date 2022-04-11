// This is a part of the SmartFieldPackEditor Library
// Copyright (C) 2001-2010 VisualHint
// All rights reserved.
//
// This source code can be used, distributed or modified
// only under terms and conditions
// of the accompanying license agreement.

using System;
using VisualHint.SmartFieldPackEditor;
using System.Windows.Forms;

namespace FieldPackEditorShowRoom
{
    public class WeekNumberField : Field
    {
        public static readonly string WeekNumberFieldName = "week";

        private DateTime _datetime;

        public DateTime DateTime
        {
            get { return _datetime; }

            set
            {
                _datetime = value;
                Value = OwnerFieldPack.CultureInfo.Calendar.GetWeekOfYear(_datetime,
                    System.Globalization.CalendarWeekRule.FirstDay, OwnerFieldPack.CultureInfo.DateTimeFormat.FirstDayOfWeek);
                OwnerFieldPack.OwnerEditor.Refresh();
            }
        }

        public WeekNumberField(DateTime dt)
        {
            Name = WeekNumberFieldName;
            MaxChars = 2;
            _datetime = dt;
        }

        public override FieldPack OwnerFieldPack
        {
            set
            {
                Value = value.CultureInfo.Calendar.GetWeekOfYear(_datetime,
                    System.Globalization.CalendarWeekRule.FirstDay, value.CultureInfo.DateTimeFormat.FirstDayOfWeek);

                base.OwnerFieldPack = value;
            }

            get { return base.OwnerFieldPack; }
        }

        protected override int ValueCurrentlyInEdition
        {
            get
            {
                if (CurrentTypePos == -1)
                    return (int)Value;

                int value = 0;
                for (int i = 0; i < CurrentTypePos; i++)
                    value += EditionBuffer[i] * (int)Math.Pow(10.0, CurrentTypePos - i - 1);

                return value;
            }
        }

        public override bool IsValidChar(char c)
        {
            return (Char.IsDigit(c));
        }

        public override void Increment()
        {
            int value = ValueCurrentlyInEdition + 1;
            int delta = value - (int)Value;

            _datetime = OwnerFieldPack.CultureInfo.Calendar.AddWeeks(_datetime, delta);
            Value = OwnerFieldPack.CultureInfo.Calendar.GetWeekOfYear(_datetime,
                System.Globalization.CalendarWeekRule.FirstDay, OwnerFieldPack.CultureInfo.DateTimeFormat.FirstDayOfWeek);

            OwnerFieldPack.OwnerEditor.Refresh();
        }

        public override void Decrement()
        {
            int value = ValueCurrentlyInEdition - 1;
            int delta = value - (int)Value;

            _datetime = OwnerFieldPack.CultureInfo.Calendar.AddWeeks(_datetime, delta);
            Value = OwnerFieldPack.CultureInfo.Calendar.GetWeekOfYear(_datetime,
                System.Globalization.CalendarWeekRule.FirstDay, OwnerFieldPack.CultureInfo.DateTimeFormat.FirstDayOfWeek);

            OwnerFieldPack.OwnerEditor.Refresh();
        }

        public override void GoHome()
        {
            _datetime = OwnerFieldPack.CultureInfo.Calendar.AddWeeks(_datetime, -(int)Value + 1);
            Value = 0;
        }

        public override void GoEnd()
        {
            _datetime = OwnerFieldPack.CultureInfo.Calendar.AddWeeks(_datetime, 52 - (int)Value);
            Value = 0;
        }

        public override string Text
        {
            get
            {
                if (EditionInProgress)
                {
                    string str = "";
                    for (int i = 0; i < CurrentTypePos; i++)
                        str += ((int)EditionBuffer[i]).ToString();
                    return str;
                }

                return base.Text;
            }
        }

        public override void OnKeyPress(KeyPressEventArgs e, out bool outOfEdition)
        {
            outOfEdition = false;

            EditionInProgress = true;

            EditionBuffer[CurrentTypePos] = (char)Int32.Parse(Char.ToString(e.KeyChar));
            CurrentTypePos++;

            if (CurrentTypePos == MaxChars)
            {
                EditionInProgress = false;  // Will trigger a CommitChanges
                outOfEdition = true;
            }
        }

        protected override void CommitChanges()
        {
            int value = ValueCurrentlyInEdition;
            int delta = value - (int)Value;

            DateTime dt = OwnerFieldPack.CultureInfo.Calendar.AddWeeks(_datetime, delta);
            if (dt.Year == _datetime.Year)
            {
                _datetime = dt;
                Value = value;
            }

            base.CommitChanges();
        }
    }
}
