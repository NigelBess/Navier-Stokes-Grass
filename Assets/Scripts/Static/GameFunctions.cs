using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public static class GameFunctions
{
    public static void ClearConsole()
    {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");

        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        clearMethod.Invoke(null, null);
    }
    public static Texture2D GetFrame(Camera cam)
    {
        cam.Render();
        RenderTexture rt = cam.targetTexture;
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width,rt.height);
        tex.ReadPixels(new Rect(0, 0,rt.width, rt.height), 0, 0);
        tex.Apply();
        return tex;
    }
    public static void Save(byte[] bytes, string path)
    {
        string dir = GetDirectory(path);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(GetDirectory(path));
            Debug.Log("created directory");
        }
        if (!File.Exists(path))
        {
            FileStream fs = File.Create(path);
            fs.Close();
        }
        File.WriteAllBytes(path, bytes);
        AssetDatabase.Refresh();
    }
    public static string GetDirectory(string filePath)
    {
        int lastSlash = filePath.LastIndexOf("/");
        return filePath.Substring(0,lastSlash);
    }
}
