# smartfieldpackeditor-net6-samples

This repository contains some sample code that allows you to play with the capabilities of the [Smart FieldPackEditor (SFPE) for .Net 6](https://visualhint.com/fieldpackeditor) library.

Note that **Smart FieldPackEditor** can be added to your own projects from [nuget](https://www.nuget.org/packages/VisualHint.SmartFieldPackEditor-net6.Trial).

## FieldPackEditorShowRoom

This sample showcases a lot of the features of Smart FieldPackEditor:

- DateTimePicker customization
- Nullify the DateTimePicker
- Custom FieldPack editor
- DataBinding with a DataGridView
- Use of other editors for Latitude/Longitude, TimeSpan, IpAddress

## FieldPackEditorShowRoom-spg

Same goal as the previous sample, but it uses Smart PropertyGrid to let the user customize several of the showcased editors. The PropertyGrid also uses adapters to be able to use the FieldPack editors inside its grid. This sample downloads [the adapter package from nuget](https://www.nuget.org/packages/VisualHint.SpgSfpeAdapter-net6.Trial), which in turns downloads its SPG and SFPE dependencies.
