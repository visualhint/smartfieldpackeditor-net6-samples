using System;
using VisualHint.SmartFieldPackEditor.Duration;
using VisualHint.SmartFieldPackEditor;

namespace FieldPackEditorShowRoom
{
    public class MyTimeSpanEditor : TimeSpanEditor
    {
        protected override void OnFieldAdded(VisualHint.SmartFieldPackEditor.FieldAddedEventArgs e)
        {
/*            if (e.Field.Name == DurationFieldPack.StaticFieldNameHours)
                e.Field.Value = "h ";
            else if (e.Field.Name == DurationFieldPack.StaticFieldNameMinutes)
                e.Field.Value = "min ";
            else if (e.Field.Name == DurationFieldPack.StaticFieldNameSeconds)
                e.Field.Value = "sec";
            else if (e.Field.Name == DurationFieldPack.StaticFieldNameLeading)
                e.Field.Value = "in ";
            else if (e.Field.Name == DurationFieldPack.StaticFieldNameDays)
                SetDayLabel(e.Field);
            */
            base.OnFieldAdded(e);
        }

        private void SetDayLabel(Field field)
        {
            if (Value != null)
            {
                TimeSpan value = (TimeSpan)Value;
                if (value.Days > 1)
                {
                    if (field.Value.Equals(" day "))
                    {
                        field.Value = " days ";
                        RecalcLayout();
                    }
                }
                else
                {
                    if (field.Value.Equals(" days "))
                    {
                        field.Value = " day ";
                        RecalcLayout();
                    }
                }
            }
        }

        protected override void OnValueChanged(FieldPack fieldPack, EventArgs eventargs)
        {
            Field field = fieldPack.GetField(TimeSpanFieldPack.StaticFieldNameDays);
            if (field != fieldPack.EmptyField)
                SetDayLabel(field);

            base.OnValueChanged(fieldPack, eventargs);
        } 
    }
}
