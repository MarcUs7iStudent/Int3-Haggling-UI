using System.Runtime.InteropServices;

namespace HagglingUI.Screen;

public static class ConsoleCheck
{
    [DllImport("libc")]
    private static extern int isatty(int fd);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool AllocConsole();

    public static bool IsConsoleAttached()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetConsoleWindow() != IntPtr.Zero;
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                 RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return isatty(1) != 0;
        }

        return false;
    }

    public static void EnsureConsole()
    {
        if (IsConsoleAttached())
        {
            return;
        }
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            AllocConsole();
        }
        else
        {
            throw new Exception($"Console is not attached! Can't get console on {RuntimeInformation.OSDescription}");
        }
    }
}
