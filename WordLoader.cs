using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class WordLoader : MonoBehaviour
{
    List<Word> allWordList;
    WordData data;

    string path, json;

    public List<Word> GetAllWordList()
    {
        //creates path regardless of the OS to safely
        path = Path.Combine(Application.streamingAssetsPath, "words.json");
        json = File.ReadAllText(path);

        data = JsonUtility.FromJson<WordData>(json);
        allWordList = data.words;

        return allWordList;
    }

    void Start()
    {

    }

}
