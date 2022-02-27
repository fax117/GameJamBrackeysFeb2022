using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameEventWindow : EditorWindow
{
    private string _type;
    private string _path = "Assets/Scripts/GameEvents/EventTypes/";

    [MenuItem("Game Events/Create New")]
    public static void ShowWindow()
    {
        GetWindow(typeof(GameEventWindow));
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Enter a valid C# type with no spaces");
        string input = EditorGUILayout.TextField("Type", _type);
        if (input != null) _type = input.Trim();
        EditorGUILayout.LabelField("Enter path to event scripts or use default");
        string path = EditorGUILayout.TextField("Path", _path);
        if (path != null) _path = path.Trim();

        if(GUILayout.Button("Generate"))
        {
            CreateEvent();
            CreateCaller();
            CreateListener();
            AssetDatabase.Refresh();
        }
    }

    private void CreateEvent()
    {
        string path = $"{_path}{_type}Event.cs";
        Debug.Log($"Creating Event: {path}");
        using (StreamWriter outfile = new StreamWriter(path))
        {
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine($"[CreateAssetMenu(menuName = \"Events/{ _type} Event\")]");
            outfile.WriteLine($"public class {_type}Event : GameEvent<{_type}> {{}}");
        }
    }

    private void CreateCaller()
    {
        string path = $"{_path}{_type}EventCaller.cs";
        Debug.Log($"Creating Caller: {path}");
        using (StreamWriter outfile = new StreamWriter(path))
        {
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine($"public class {_type}EventCaller : GameEventCaller<{_type}> {{}}");
        }
    }

    private void CreateListener()
    {
        string path = $"{_path}{_type}EventListener.cs";
        Debug.Log($"Creating Listener: {path}");
        using (StreamWriter outfile = new StreamWriter(path))
        {
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("");
            outfile.WriteLine($"public class {_type}EventListener : GameEventListener<{_type}> {{}}");
        }
    }
}
