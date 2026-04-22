using UnityEngine;
using System.Collections.Generic;

public static class HintStatus
{
    private static HashSet<int> activeHints = new HashSet<int>();

    //Automatically clear HashSet whenever the scene is loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Clear()
    {
        activeHints.Clear();
    }

    public static void Add(int targetId)
    {
        activeHints.Add(targetId);
    }

    public static void Remove(int targetId)
    {
        activeHints.Remove(targetId);
    }

    public static bool isHintActive(int targetId)
    {
        return activeHints.Contains(targetId);
    }
}
