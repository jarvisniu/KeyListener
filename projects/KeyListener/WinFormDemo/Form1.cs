﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using com.jarvisniu.utils;

namespace WinFormDemo
{
    public partial class Form1 : Form
    {
        KeyListener keyListener = new KeyListener();

        public Form1()
        {
            InitializeComponent();
            keyListener.onPress("F1", onPressHelp);           // a single key
            keyListener.onPress("Ctrl+R F5", onPressRefresh); // combined key & multiple combination

            keyListener.onSettingChange = onSettingChange;
            keyListener.onSettingConfirm = onSettingConfirm;
        }

        private void onPressHelp()
        {
            this.Invoke(new Action(delegate
            {
                label2.Text = "help keys pressed.";
            }));
        }
        private void onPressRefresh()
        {
            this.Invoke(new Action(delegate
            {
                label2.Text = "refresh keys pressed.";
            }));
        }

        private void onSettingChange(string keyString)
        {
            this.Invoke(new Action(delegate
            {
                textBox1.Text = keyString;
                labelSettingState.Text = "setting...";
            }));
        }

        private void onSettingConfirm(string keyString)
        {
            this.Invoke(new Action(delegate
            {
                textBox1.Text = keyString;
                labelSettingState.Text = "set complete";
            }));
        }

        private void buttonStartSetting_Click(object sender, EventArgs e)
        {
            keyListener.startSetting();
        }
        // end of class
    }
}
