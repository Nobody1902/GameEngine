using GLFW;
using System;

namespace GameEngine;

struct Key
{
    public Keys keys;

    public bool pressed;
    public bool down;
    public bool up;

    public Key(Keys keys)
    {
        this.keys = keys;
        pressed = false;
        down = false;
        up = false;
    }
    public Key(Keys keys, bool pressed, bool down, bool up)
    {
        this.keys = keys;
        this.pressed = pressed;
        this.down = down;
        this.up = up;
    }
    // override object.Equals
    public override bool Equals(object obj)
    {
        //       
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237  
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return ((Key)obj).keys == this.keys;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        unchecked // only needed if you're compiling with arithmetic checks enabled
        { // (the default compiler behaviour is *disabled*, so most folks won't need this)
            int hash = 13;
            hash = (hash * 7) + keys.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(Key obj1, Key obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }
    public static bool operator !=(Key obj1, Key obj2)
    {
        return !(obj1 == obj2);
    }
}
public class Input
{
    
    private static Vector2 _mousePosition;
    public static Vector2 MousePosition => _mousePosition = new();

    private static bool _mouseDown1;
    private static bool _mouseDown2;
    private static bool _mouseDown3;

    private static Key[] KeysPressed = new Key[Enum.GetNames(typeof(Keys)).Length];

    public static void OnLoad()
    {
        var values = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToArray();

        for (int i = 0; i < values.Length; i++)
        {
            KeysPressed[i] = new(values[i]);
        }
    }
    public static void Update()
    {
        for (int i = 0; i < KeysPressed.Length; i++)
        {
            KeysPressed[i].down = false;
            KeysPressed[i].up = false;
        }
    }
    #region Callbacks
    internal static void _keyCallback(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods)
    {
        int i = Array.IndexOf(KeysPressed, new(key));
        if(state == InputState.Press)
        {
            KeysPressed[i].pressed = true;
            KeysPressed[i].down = true;
            KeysPressed[i].up = false;
        }
        else if (state == InputState.Release)
        {
            KeysPressed[i].pressed = false;
            KeysPressed[i].down = false;
            KeysPressed[i].up = true;
        }
        
    }
    internal static void _mouseCallback(Window window, double x, double y)
    {
        _mousePosition = new((float)x, (float)y);
    }
    internal static void _mouseButtonCallback(Window window, MouseButton button, InputState state, ModifierKeys modifiers)
    {
        if (state == InputState.Press)
        {
            switch (button)
            {
                case MouseButton.Left:
                    _mouseDown1 = true;
                    break;
                case MouseButton.Right:
                    _mouseDown2 = true;
                    break;
                case MouseButton.Middle:
                    _mouseDown3 = true;
                    break;
            }
        }
        else if(state == InputState.Release)
        {
            switch (button)
            {
                case MouseButton.Left:
                    _mouseDown1 = false;
                    break;
                case MouseButton.Right:
                    _mouseDown2 = false;
                    break;
                case MouseButton.Middle:
                    _mouseDown3 = false;
                    break;
            }
        }
    }
    #endregion
     
    public static bool MousePressed(int button)
    {
        return button switch
        {
            1 => _mouseDown1,
            2 => _mouseDown2,
            3 => _mouseDown3,
            _ => false,
        };
    }

    public static bool KeyPressed(Keys key)
    {
        int i = Array.IndexOf(KeysPressed, new(key));
        return KeysPressed[i].pressed;
    }
    public static bool KeyDown(Keys key)
    {
        int i = Array.IndexOf(KeysPressed, new(key));
        return KeysPressed[i].down;
    }
    public static bool KeyUp(Keys key)
    {
        int i = Array.IndexOf(KeysPressed, new(key));
        return KeysPressed[i].up;
    }

}