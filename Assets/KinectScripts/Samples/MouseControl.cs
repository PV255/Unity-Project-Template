//
// This script can be used to control the system mouse - position of the mouse cursor and clicks
// Author: Akhmad Makhsadov
//

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class MouseControl
{
    // Import function mouse_event() from WinApi
    [DllImport("User32.dll")] 
    private static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, System.UIntPtr dwExtraInfo);

    // Flags needed to specify the mouse action 
    [System.Flags]
    private enum MouseFlags { 
        Move = 0x0001, 
        LeftDown = 0x0002, 
        LeftUp = 0x0004, 
        RightDown = 0x0008,
        RightUp = 0x0010,
        Absolute = 0x8000,
        }
                
//    public static int MouseXSpeedCoef = 45000; // Cursor rate in Х direction
//    public static int MouseYSpeedCoef = 45000; // Cursor rate in Y direction

    // Public function to move the mouse cursor to the specified position
    public static void MouseMove(Vector3 screenCoordinates)
    {
        Vector2 mouseCoords = new Vector2();
		
        mouseCoords.x = screenCoordinates.x * 65535;
        mouseCoords.y = (1.0f - screenCoordinates.y) * 65535;
		
        mouse_event(MouseFlags.Absolute | MouseFlags.Move, (int)mouseCoords.x, (int)mouseCoords.y, 0, System.UIntPtr.Zero);
    }

    // Public function to emulate a mouse button click (left button)
    public static void MouseClick()
    {
        mouse_event(MouseFlags.LeftDown, 0, 0, 0, System.UIntPtr.Zero);
        mouse_event(MouseFlags.LeftUp, 0, 0, 0, System.UIntPtr.Zero);
    }
	
    // Public function to emulate a mouse drag event (left button)
    public static void MouseDrag()
    {
        mouse_event(MouseFlags.LeftDown, 0, 0, 0, System.UIntPtr.Zero);
    }
	
    // Public function to emulate a mouse release event (left button)
    public static void MouseRelease()
    {
        mouse_event(MouseFlags.LeftUp, 0, 0, 0, System.UIntPtr.Zero);
    }
	
}


