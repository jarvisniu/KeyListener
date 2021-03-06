KeyListener
===========

[【中文 Chinese】](https://github.com/jarvisniu/KeyListener/blob/master/README-zh-CN.md)

a background keyboard listener for .NET/C#

You can:

1. bring up your program window when it has no focus
2. set the shortcut keys


## Usage

1. Import the dll or add the source code to your project.
2. Add namespace using
3. Create an KeyListener instance
4. Create an response function
5. Bind the Keys to the response function

## Demo Code

shortcut binding:

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

shortcut setting:

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

There are only two pair of members:

- `onSettingChange: void Function(string keyString)`
- `onSettingConfirm: void Function(string keyString)`
- `onPress(string keys, Action response)`
- `onRelease(string keys, Action response)`

The parameter `keys` is the keyboard shortcuts, not case sensitive. Here are some examples:

- `"F1"`  - a single key
- `"Ctrl+C"` - a combined key
- `"Ctrl+R F5"` - multiple shortcuts

## Key List

- `Backspace`
- `Tab`
- `Enter`
- `Shift`
- `Ctrl` or `Control`
- `Alt` or `Alter`
- `Left, Right, Up, Down`
- `A-Z`
- `F1`-`F12`

## History

v0.1.0

- support background shortcuts

v0.2.0

- support shorcut setting

## Plan

- supplement the keys
