using UnityEngine;
using System.IO;

public class WordLoader : MonoBehaviour
{
    string path, json;
    WordData data;

    void Start()
    {
        //creates path regardless of the OS to safely
        path = Path.Combine(Application.streamingAssetsPath, "words.json");
        json = File.ReadAllText(path);

        data = JsonUtility.FromJson<WordData>(json);

        DebugTest();
    }

    void DebugTest()
    {
        //Debug.Log(path);
        //Debug.Log(json);

        WordData data = JsonUtility.FromJson<WordData>(json);

        /*foreach (var word in data.words)
        {
            Debug.Log(word.kanji + " = " + word.meaning);
        }*/


        Word randomWord = data.words[(Random.Range(0, data.words.Count))];
        Debug.Log("랜덤 단어 하나 = " + randomWord.kanji);
    }
}
