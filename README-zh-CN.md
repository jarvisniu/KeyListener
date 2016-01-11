KeyListener
===========

一个后台键盘监听器 .NET/C#

你可以:

1. 当程序窗口没有焦点的时候，调出它
2. 设置快捷键


## 使用

1. 导入DLL或者添加源文件到项目中
2. 添加命名空间
3. 创建一个KeyListener的实例
4. 创建一个响应函数
5. 绑定快捷键与响应函数

## 示例代码

快捷键绑定:

```C#

using System.Windows;

using com.jarvisniu.utils;

namespace WpfDemo
{
    public partial class MainWindow : Window
    {
        KeyListener keyListener = new KeyListener();

        public MainWindow()
        {
            InitializeComponent();
            keyListener.onPress("Ctrl+R F5", onPressRefresh);
        }

        private void onPressRefresh()
        {
            this.Dispatcher.Invoke(delegate
            {
                label1.Content = "refresh keys pressed.";
            });
        }
    }
}


```

快捷键设置:

``` C#

using System.Windows;
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
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            keyListener.startSetting();
        }

        private void onSettingChange(string keyString)
        {
            Console.WriteLine("setting change to: " + keyString);
        }

        private void onSettingConfirm(string keyString)
        {
            Console.WriteLine("setting confirm to: " + keyString);
        }
    }
}

```

## API

只有两对成员:

- `onSettingChange: void Function(string keyString)`
- `onSettingConfirm: void Function(string keyString)`
- `onPress(string keys, Action response)`
- `onRelease(string keys, Action response)`

参数 `keys` 是键盘快捷键，不区分大小写。这是一些例子：

- `"F1"`  - 单个的快捷键
- `"Ctrl+C"` - 组合的快捷键
- `"Ctrl+R F5"` - 多个快捷键

## 按键列表

- `Backspace`
- `Tab`
- `Enter`
- `Shift`
- `Ctrl` or `Control`
- `Alt` or `Alter`
- `Left, Right, Up, Down`
- `A-Z`
- `F1`-`F12`

## 更新历史

v0.1.0

- 支持后台快捷键

v0.2.0

- 支持快捷键设置

## 计划

- 扩充快捷键
