using System.Collections.Generic;
using UnityEngine;

public class WordCardSelector : MonoBehaviour
{
    List<int> selectedList = new List<int>();

    void Start()
    {
        this.selectedList.Add(1);
        this.selectedList.Add(3);
        this.selectedList.Add(7);

        SceneTracker.selectorSwordRecordWordList = new List<int>(this.selectedList);
    }
}
