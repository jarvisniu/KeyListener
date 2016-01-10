using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using com.jarvisniu.utils;

namespace WpfDemo
{
    public partial class MainWindow : Window
    {
        KeyListener keyListener = new KeyListener();

        public MainWindow()
        {
            InitializeComponent();
            keyListener.onSettingChange = onSettingChange;
            keyListener.onSettingConfirm = onSettingConfirm;
            keyListener.onPress("F1", onHelpRefresh);           // a single key
            keyListener.onPress("Ctrl+R F5", onPressRefresh);   // combined key & multiple combination
        }
        private void onHelpRefresh()
        {
            this.Dispatcher.Invoke(delegate
            {
                label1.Content = "help keys pressed.";
            });
        }

        private void onPressRefresh()
        {
            this.Dispatcher.Invoke(delegate
            {
                label1.Content = "refresh keys pressed.";
            });
        }
        
        private void onSettingChange(string keyString)
        {
            this.Dispatcher.Invoke(delegate
            {
                labelSettingState.Content = "Setting...";
                textBox.Text = keyString;
                // Console.WriteLine("onSettingChange: " + count);
            });
        }
        private void onSettingConfirm(string keyString)
        {
            this.Dispatcher.Invoke(delegate
            {
                labelSettingState.Content = "Set completed";
                textBox.Text = keyString;
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            keyListener.startSetting();
        }
        // end of class
    }
}
