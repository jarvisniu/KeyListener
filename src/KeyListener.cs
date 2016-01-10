/**
 * KeyListener v0.2.0
 * by Jarvis Niu
 * at Jan 1, 2016
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;

namespace com.jarvisniu.utils
{
    public class KeyListener
    {

        #region DLL & key map

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(int nVirtKey);

        private static Dictionary<string, int> keyMap = new Dictionary<string, int> {
            // !!! must be upper case
            { "BACKSPACE", 8 },
            { "TAB", 9 },
            { "ENTER", 13 },
            { "SHIFT", 16 },
            { "CTRL",  17 },
            { "ALT",   18 },
            { "ESC", 27 },
            { "SPACE", 32 },
            { "LEFT", 37 },
            { "RIGHT", 39 },
            { "UP", 38 },
            { "DOWN", 40 },
            { "A", 65 },
            { "B", 66 },
            { "C", 67 },
            { "D", 68 },
            { "E", 69 },
            { "F", 70 },
            { "G", 71 },
            { "H", 72 },
            { "I", 73 },
            { "J", 74 },
            { "K", 75 },
            { "L", 76 },
            { "M", 77 },
            { "N", 78 },
            { "O", 79 },
            { "P", 80 },
            { "Q", 81 },
            { "R", 82 },
            { "S", 83 },
            { "T", 84 },
            { "U", 85 },
            { "V", 86 },
            { "W", 87 },
            { "X", 88 },
            { "Y", 89 },
            { "Z", 90 },
            { "F1", 112 },
            { "F2", 113 },
            { "F3", 114 },
            { "F4", 115 },
            { "F5", 116 },
            { "F6", 117 },
            { "F7", 118 },
            { "F8", 119 },
            { "F9", 120 },
            { "F10", 121 },
            { "F11", 122 },
            { "F12", 123 },
        };

        private static Dictionary<string, string> keyAlias = new Dictionary<string, string>
        {
            {"CONTROL", "CTRL" },
            {"ALTER", "ALT" },
        };

        #endregion

        #region variables

        private List<string> watchingCombinedKeys;                          // 已添加的所有组合键
        private List<string> watchingSingleKeys;                            // 已添加的所有单键
        private Dictionary<string, bool> currentSingleKeyStates;            // 这次检测时单键的状态
        private Dictionary<string, bool> lastSingleKeyStates;               // 上次检测时单键的状态
        private Dictionary<string, List<string>> singleMapToCombination;    // 从单键检索包含该单键的组合键
        private Dictionary<string, Action> pressActions;                    // 各快捷键组合在按下时要执行的回调函数
        private Dictionary<string, Action> releaseActions;                  // 各快捷键组合在放开时要执行的回调函数

        private Timer timer = new Timer(20);

        // 快捷键设置
        public delegate void StringAction(string keyString);
        public StringAction onSettingChange;
        public StringAction onSettingConfirm;
        private bool isSetting = false;
        private List<string> settingKeysAll = new List<string>();       // 在本轮设置阶段按下过的所有键，包括现在和曾经
        private List<string> settingKeysNow = new List<string>();       // 在本轮设置阶段目前正按下的键

        #endregion

        #region constructor
        public KeyListener()
        {
            watchingCombinedKeys = new List<string>();
            watchingSingleKeys = new List<string>();
            lastSingleKeyStates = new Dictionary<string, bool>();
            currentSingleKeyStates = new Dictionary<string, bool>();
            singleMapToCombination = new Dictionary<string, List<string>>();
            pressActions = new Dictionary<string, Action>();
            releaseActions = new Dictionary<string, Action>();

            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        #endregion

        #region APIs

        public KeyListener onPress(string keyString, Action cb)
        {
            string[] combinedKeys = extracKeys(keyString);
            foreach (string combinedKey in combinedKeys)
            {
                if (combinedKey.Length == 0) continue;
                watchCombinedKey(combinedKey);
                pressActions[combinedKey] = cb;
            }
            return this;
        }

        public KeyListener onRelease(string keyString, Action cb)
        {
            string[] keys = extracKeys(keyString);
            foreach (string key in keys)
            {
                if (key.Length == 0) continue;
                watchCombinedKey(key);
                releaseActions[key] = cb;
            }
            return this;
        }

        public void startSetting()
        {
            isSetting = true;
            if(onSettingChange != null)onSettingChange.Invoke("");
        }

        #endregion

        #region logic

        private void watchSingleKey(String singleKey)
        {
            lastSingleKeyStates[singleKey] = false;
            currentSingleKeyStates[singleKey] = false;

            if (!watchingSingleKeys.Contains(singleKey))
                watchingSingleKeys.Add(singleKey);

            //Console.WriteLine(singleKey);
        }

        private void watchCombinedKey(String combinedKey)
        {
            combinedKey = combinedKey.ToUpper();

            if (!watchingCombinedKeys.Contains(combinedKey))
                watchingCombinedKeys.Add(combinedKey);

            string[] singleKeys = combinedKey.Split('+');
            foreach (string singleKey in singleKeys)
            {
                int keyValue = getKeyCode(singleKey);
                if (keyValue != -1)
                {
                    watchSingleKey(singleKey);

                    // 添加到索引
                    if (!singleMapToCombination.ContainsKey(singleKey))
                    {
                        singleMapToCombination[singleKey] = new List<string>();
                    }

                    if (!singleMapToCombination[singleKey].Contains(combinedKey))
                    {
                        singleMapToCombination[singleKey].Add(combinedKey);
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid KeyCode " + singleKey);
                }
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isSetting)
            {
                detectSetting();
            }
            else
            {
                detectBinding();
            }
        }

        private void detectBinding()
        {
            // 设置被监测单键的状态
            foreach (string key in watchingSingleKeys)
            {
                currentSingleKeyStates[key] = detectKeyDown(key);
            }

            // 根据检测单键的变更触发绑定的热键
            foreach (string key in watchingSingleKeys)
            {
                if (lastSingleKeyStates[key] != currentSingleKeyStates[key])
                {
                    List<string> relateCombinations = singleMapToCombination[key];
                    foreach (string combinedKey in relateCombinations)
                    {
                        if (isCombinedKeyInSameState(combinedKey))
                        {
                            bool isDownNow = currentSingleKeyStates[key];

                            if (isDownNow)
                            {
                                if (pressActions.ContainsKey(combinedKey))
                                {
                                    pressActions[combinedKey].Invoke();
                                }
                            }
                            else
                            {
                                if (releaseActions.ContainsKey(combinedKey))
                                    releaseActions[combinedKey].Invoke();
                            }

                        }

                    }

                }
            }

            // 记录上次按键状态
            foreach (string key in watchingSingleKeys)
            {
                lastSingleKeyStates[key] = currentSingleKeyStates[key];
            }
        }

        private void detectSetting()
        {
            foreach (string key in keyMap.Keys)
            {
                // 检测所有键的状态
                currentSingleKeyStates[key] = detectKeyDown(key);
                // 与上次记录做比较
                if (lastSingleKeyStates.ContainsKey(key) && lastSingleKeyStates[key] != currentSingleKeyStates[key])
                {
                    bool isDownNow = currentSingleKeyStates[key];
                    // 如果有新键按下，则加入设置键列表，并触发settingChange
                    if (isDownNow)
                    {
                        if (!settingKeysAll.Contains(key))
                        {
                            settingKeysAll.Add(key);
                            if (onSettingChange != null)
                                onSettingChange.Invoke(combineKeyString(settingKeysAll));
                        }
                        if (!settingKeysNow.Contains(key))
                        {
                            settingKeysNow.Add(key);
                        }
                    }
                    // 如果所有键都松开，而设置键列表不为空，则触发settingConfirm
                    else
                    {
                        if (settingKeysNow.Contains(key))
                        {
                            settingKeysNow.Remove(key);
                        }
                        if (settingKeysNow.Count == 0)
                        {
                            isSetting = false;
                            if(onSettingConfirm!=null)
                                onSettingConfirm.Invoke(combineKeyString(settingKeysAll));
                            settingKeysAll.Clear();
                            settingKeysNow.Clear();
                        }
                    }
                }
                // 记录上次状态
                lastSingleKeyStates[key] = currentSingleKeyStates[key];
            }
        }

        private string combineKeyString(List<string> keys)
        {
            if (keys.Count == 0) return "";

            string keyString = keys[0];
            for (int i = 1; i < keys.Count; i++)
            {
                keyString += " + " + keys[i];
            }
            return keyString;
        }

        #endregion

        #region utils

        private int getKeyCode(string keyCode)
        {
            string keyCodeU = keyCode.ToUpper();
            if (keyMap.ContainsKey(keyCodeU))
                return keyMap[keyCodeU];
            else
                return -1;
        }

        public bool detectKeyDown(string keyString)
        {
            int keyCode = getKeyCode(keyString);
            int keyState = GetAsyncKeyState(keyCode);  // keyState < 0 -> Key is pressed, it won't be > 0
            if (keyState < 0)
                return true;
            else
                return false;
        }

        public bool isCombinedKeyInSameState(string combinedKey)
        {
            string[] keys = combinedKey.Split('+');
            for (var i = 0; i < keys.Length - 1; i++)
            {
                if (currentSingleKeyStates[keys[i]] != currentSingleKeyStates[keys[i + 1]])
                    return false;
            }
            return true;
        }

        private string[] extracKeys(string rawStr)
        {
            rawStr = regReplace(rawStr, " *\\+ *", "+");    // remove spaces beside "+"
            rawStr = regReplace(rawStr, " +", " ");         // reduce multiple space to one
            rawStr = rawStr.ToUpper();
            foreach (KeyValuePair<string, string> alias in keyAlias)
            {
                rawStr = rawStr.Replace(alias.Key, alias.Value);
            }
            return rawStr.Split(' ');
        }

        private string regReplace(string str, string from, string to)
        {
            return new Regex(from).Replace(str, to);
        }

        #endregion

        // end of class
    }
}
