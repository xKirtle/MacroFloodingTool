using System.Windows;
using System.Windows.Controls;
using WindowsInput;
using WindowsInput.Native;


namespace Flood
{
    public partial class MainWindow : Window
    {
        InputSimulator inp = new InputSimulator();

        private LowLevelKeyboardListener _listener;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) //Hooks the keyboard listener to a method.
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Unhooks the listener when the window closes
        {
            _listener.UnHookKeyboard();
        }

        private void _listener_OnKeyPressed(object sender, KeyPressedArgs e) 
        //if KeyPressed equals to any of the keys in the switch statement, the text on each corresponding textbox is "typed"
        {
            var key = e.KeyPressed.ToString();
            var ret = VirtualKeyCode.RETURN;

            switch (key)
            {
                case "F1":
                    try { inp.Keyboard.TextEntry(TF1.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F2":
                    try { inp.Keyboard.TextEntry(TF2.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F3":
                    try { inp.Keyboard.TextEntry(TF3.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F4":
                    try { inp.Keyboard.TextEntry(TF4.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F5":
                    try { inp.Keyboard.TextEntry(TF5.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F6":
                    try { inp.Keyboard.TextEntry(TF6.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F7":
                    try { inp.Keyboard.TextEntry(TF7.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F8":
                    try { inp.Keyboard.TextEntry(TF8.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F9":
                    try { inp.Keyboard.TextEntry(TF9.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F10":
                    try { inp.Keyboard.TextEntry(TF10.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F11":
                    try { inp.Keyboard.TextEntry(TF11.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
                case "F12":
                    try { inp.Keyboard.TextEntry(TF12.Text).Sleep(200); }
                    catch (System.ArgumentException) { }
                    inp.Keyboard.KeyPress(ret);
                    break;
            }
        }

        public Visibility Visible //Returns Visible
        {
            get { return Visibility.Visible; }
        }

        public Visibility Collapsed //Returns Collapsed
        {
            get { return Visibility.Collapsed; }
        }

        public void CollapseTexts() //Collapses all textboxes at once
        {
            var x = Collapsed;

            TF1.Visibility = x;
            TF2.Visibility = x;
            TF3.Visibility = x;
            TF4.Visibility = x;
            TF5.Visibility = x;
            TF6.Visibility = x;
            TF7.Visibility = x;
            TF8.Visibility = x;
            TF9.Visibility = x;
            TF10.Visibility = x;
            TF11.Visibility = x;
            TF12.Visibility = x;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Index = ComboBox.SelectedIndex; //Gets the index of the selected ComboBox
            var y = Visible;
            HabboImageHome.Visibility = Collapsed; //Collapses the initial picture in the main window
            CollapseTexts(); //Collapses every textbox

            if (AboutTabContent.IsVisible == true) //If the about window was selected, collapses the About window in order to clear out the space for the textboxes to be displayed
            {
                AboutTabContent.Visibility = Collapsed;
            }

            TextBoxes.Visibility = Visible; //Visibility of the StackPanel TextBoxes is set to Visible
            switch (Index) //Depending on the index Given, one of the textboxes will be set to Visible
            {
                case 0:
                    TF1.Visibility = y;
                    break;
                case 1:
                    TF2.Visibility = y;
                    break;
                case 2:
                    TF3.Visibility = y;
                    break;
                case 3:
                    TF4.Visibility = y;
                    break;
                case 4:
                    TF5.Visibility = y;
                    break;
                case 5:
                    TF6.Visibility = y;
                    break;
                case 6:
                    TF7.Visibility = y;
                    break;
                case 7:
                    TF8.Visibility = y;
                    break;
                case 8:
                    TF9.Visibility = y;
                    break;
                case 9:
                    TF10.Visibility = y;
                    break;
                case 10:
                    TF11.Visibility = y;
                    break;
                case 11:
                    TF12.Visibility = y;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //About Button, Collapses everything to display the About Content and resets the combobox index
        {
            ComboBox.SelectedIndex = -1;
            TextBoxes.Visibility = Collapsed;
            HabboImageHome.Visibility = Collapsed;
            AboutTabContent.Visibility = Visible;
        }
    }
}
