// =================================================================
// Author: Muhammad Bintang Bagas Prasetya
// Project: TinyDesk
// =================================================================

using System.Windows.Forms;

namespace InputController.Config
{
    public static class HotkeyConfig
    {
        public static readonly (Keys modifier, Keys key) HOTKEY_ENABLE_TOUCHPAD = (Keys.Control, Keys.T);
        public static readonly (Keys modifier, Keys key) HOTKEY_ENABLE_ALL = (Keys.Control, Keys.K);
        public static readonly (Keys modifier, Keys key) HOTKEY_TOGGLE_WINDOW = (Keys.Control | Keys.Shift, Keys.M);
    }

    public static class UIConfig
    {
        public static readonly bool ALWAYS_ON_TOP = true;
        public static readonly bool START_MINIMIZED = true;
        public static readonly bool SHOW_IN_TASKBAR = false;
        public static readonly string WINDOW_TITLE = "Tiny Desk";
        public static readonly int FORM_WIDTH = 300;
        public static readonly int FORM_HEIGHT = 230;
    }

    public static class DeviceConfig
    {
        public static readonly string TOUCHPAD_DEVICE_ID = "*PNP0F13";
        public static readonly string DISABLE_COMMAND = "/c devcon disable {0}";
        public static readonly string ENABLE_COMMAND = "/c devcon enable {0}";
    }

    public static class BehaviorConfig
    {
        public static readonly bool AUTO_ENABLE_ON_EXIT = true;
        public static readonly bool SILENT_MODE = true;
    }
}
