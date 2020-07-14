using System;
using System.Windows;
using System.Configuration;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Collections.Generic;
using WindowsInput;
using WindowsInput.Native;

namespace MacroFloodingTool
{
    public partial class MainWindow : Window
    {
        private readonly InputSimulator inp;
        private readonly List<TextBox> _textBoxes;
        private LowLevelKeyboardListener _listener;
        public Visibility Visible { get { return Visibility.Visible; } }

        public Visibility Collapsed { get { return Visibility.Collapsed; } }

        public MainWindow()
        {
            InitializeComponent();
            inp = new InputSimulator();

            //HotKeys array for the comboBox
            string[] hotKeys = new string[12];
            for (int i = 1; i < 13; i++)
                hotKeys[i - 1] = "F" + i;
            ArrayExtension arrayExtension = new ArrayExtension(hotKeys);
            ComboBox.ItemsSource = arrayExtension.Items;

            //TextBoxes
            _textBoxes = new List<TextBox>(12);
            for (int i = 1; i < 13; i++)
            {
                TextBox textBox = new TextBox
                {
                    Margin = new Thickness(5, 5, 4, 0),
                    Name = "TB" + i,
                    Visibility = Collapsed,
                    Height = 105,
                    TextWrapping = TextWrapping.Wrap,
                    VerticalScrollBarVisibility = (ScrollBarVisibility)Visible,
                    AcceptsReturn = true,
                    MaxLength = 100
                };

                _textBoxes.Add(textBox);

                //Adding it to the xaml TextBoxes StackPanel
                TextBoxes.Children.Add(_textBoxes[i - 1]);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) //Hooks the keyboard listener to a method.
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();

            //Load all the strings into the Text Boxes
            int counter = 0;
            foreach (SettingsProperty property in Settings.Default.Properties)
                _textBoxes[counter++].Text = Settings.Default[property.Name].ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Unhooks the listener when the window closes
        {
            _listener.UnHookKeyboard();

            //Save all the strings in the Text Boxes
            int counter = 0;
            foreach (SettingsProperty property in Settings.Default.Properties)
            {
                Settings.Default[property.Name] = _textBoxes[counter++].Text;
                Settings.Default.Save();
            }
        }

        private void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            int key = -1;
            string pressedKey = e.KeyPressed.ToString();
            if (pressedKey.Contains("F"))
                int.TryParse(pressedKey.Replace("F", ""), out key);

            //Tries typing string and pressing the Return key
            if (key >= 1 && key <= 12 && !string.IsNullOrEmpty(_textBoxes[key - 1].Text))
                try { inp.Keyboard.TextEntry(_textBoxes[key - 1].Text); inp.Keyboard.KeyPress(VirtualKeyCode.RETURN); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void CollapseTexts() //Collapses all textboxes at once
        {
            foreach (TextBox textBox in _textBoxes)
                textBox.Visibility = Collapsed;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HabboImageHome.Visibility = Collapsed; //Collapses the initial picture in the main window
            CollapseTexts(); //Collapses every textbox

            //If the about window was selected, collapses the About window in order to clear out the space for the textboxes to be displayed
            if (AboutTabContent.IsVisible)
                AboutTabContent.Visibility = Collapsed;

            TextBoxes.Visibility = Visible; //Visibility of the StackPanel TextBoxes is set to Visible

            if (ComboBox.SelectedIndex != -1)
                _textBoxes[ComboBox.SelectedIndex].Visibility = Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //About Button, Collapses everything to display the About Content and resets the combobox index
            ComboBox.SelectedIndex = -1;
            TextBoxes.Visibility = Collapsed;
            HabboImageHome.Visibility = Collapsed;
            AboutTabContent.Visibility = Visible;
        }
    }
}
