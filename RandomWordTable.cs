using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class RandomWordTable : MonoBehaviour
{
    List<Word> selectedWordList = new List<Word>();

    public List<Word> GetSelectWordList(List<Word> allWordList, int selectAmount)
    {
        //if previously created list is exists
        if (selectedWordList != null && selectedWordList.Count > 0)
        {
            selectedWordList.Clear();
        }

        //Fisher-Yates Shuffle Algorithm
        List<int> randomNumList = new List<int>();
        for (int i = 0; i < allWordList.Count; i++ )
        {
            randomNumList.Add(i);
        }
        for (int i = randomNumList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            //swap
            int temp = randomNumList[i];
            randomNumList[i] = randomNumList[j];
            randomNumList[j] = temp;
        }

        //select random word using randomNumList
        for (int Index = 0; Index < selectAmount; Index++ )
        {
            Word randomWord = allWordList[randomNumList[Index]];
            selectedWordList.Add(randomWord);
        }

        return selectedWordList;
    }

    //set public to test debug log
    public void ShowList()
    {
        if (selectedWordList != null && selectedWordList.Count > 0)
        {
            //Debug
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(i + "번: " + selectedWordList[i].kanji + " = "
                    + selectedWordList[i].meaning + ", 번호: "
                    + selectedWordList[i].id);
            }
        }
        else
        {
            Debug.Log("there is no List table");
        }
        
    }

    void Start()
    {

    }

    
}
