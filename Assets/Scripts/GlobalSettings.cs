using System;
using UnityEngine;
using System.Collections;

public static class GlobalSettings  {

    
    public static MessageManager messageManager;

    public static void TogglePause(bool pause)
    {
        if (!pause)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
	
    public static bool IsPaused()
    {
        return Mathf.Abs(Time.timeScale) < 0.01f;
    }

    public static void SendMessage(MessageType messageType)
    {
        messageManager.SendMessage(messageType);
    }


}
