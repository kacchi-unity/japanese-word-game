using System.Collections.Generic;

public static class SceneTracker
{
    public enum SceneType
    {
        None,
        Lobby,
        SwordRecord
    }

    public static SceneType previousScene;

    public static List<int> selectorSwordRecordWordList;

}
