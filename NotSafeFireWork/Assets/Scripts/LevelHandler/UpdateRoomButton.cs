using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpdateRoomButton : EditorWindow
{
  

    [MenuItem("Window/Edit Mode Functions")]
    public static void ShowWindow()
    {
        GetWindow<UpdateRoomButton>("Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("UpdateEnemy"))
        {
            LevelHandler.Instance.RoomClearFeedback();
        }
    }
}
