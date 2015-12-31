KeyListener
===========

a keyboard shortcut listener for .NET/C#

## Content

- Usage
- Demo Code
- API
- Key List
- Plan

## Usage

1. Import the dll or add the source code to your project.
2. Add namespace using
3. Create an KeyListener instance
4. Create an response function
5. Bind the Keys to the response function

## Demo Code

```c#

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


## API

There are only two methods:

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

## Plan

- supplement the keys
- detect the current pressed key combination for setting
