using UnityEngine;
using TMPro;

public class EnemyData
{
    private GameObject enemyGameObject;
    private string kanji;
    private string meaning; //korean meaning

    //constructor
    public EnemyData (GameObject enemyGameObject, string kanji, string mean)
    {
        this.enemyGameObject = enemyGameObject;
        this.kanji = kanji;
        this.meaning = mean;

        //input kanji to prefab canvas ui
        WriteKanjitoUI(kanji);
    }

    //set TMPro Kanji UI
    void WriteKanjitoUI(string kanji)
    {
        //Kanji object is in Prefab/Canvas
        TextMeshProUGUI[] listUI = enemyGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI TMProUI in listUI)
        {
            if (TMProUI.name.Equals("Kanji"))
            {
                TMProUI.text = kanji;
                break;
            }
        }
    }

    //get method
    public GameObject GetEnemyGameObject()
    {
        return this.enemyGameObject;
    }

    public string GetKanji()
    {
        return this.kanji;
    }

    public string GetMeaning()
    {
        return this.meaning;
    }

    
}