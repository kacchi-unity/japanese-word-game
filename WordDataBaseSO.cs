using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordDataBaseSO", menuName = "ScriptableObject/WordDataBaseSO")]
public class WordDataBaseSO : ScriptableObject
{
    //read only
    [Header("--- Internal Data (Do Not Touch) ---")]
    [SerializeField] private List<Word> wordDataBaseList = new List<Word>();
    private Dictionary<int, Word> wordDataBaseDic = new Dictionary<int, Word>();
    
    private void ValidateDic()
    {
        if (this.wordDataBaseDic.Count == 0 && this.wordDataBaseList.Count > 0)
        {
            wordDataBaseDic.Clear();
            foreach (var item in this.wordDataBaseList)
            {
                this.wordDataBaseDic[item.id] = item;
            }
        }
    }

    private void SyncList()
    {
        wordDataBaseList.Clear();
        foreach (var item in this.wordDataBaseDic)
        {
            this.wordDataBaseList.Add(item.Value);
        }
    }

    public void SetWordDataBase(List<Word> allWordList)
    {
        this.wordDataBaseList = allWordList;
        this.wordDataBaseDic.Clear();
        foreach (var item in this.wordDataBaseList)
        {
            this.wordDataBaseDic.Add(item.id, item);
        }

    }

    public Dictionary<int, Word> GetWordDataBase()
    {
        ValidateDic();
        return this.wordDataBaseDic;
    }

    public void ResetWordDataBase()
    {
        this.wordDataBaseDic.Clear();
        this.wordDataBaseList.Clear();
    }

    public void Test()
    {
        ValidateDic();
        Debug.Log(this.wordDataBaseDic.Count);
        Debug.Log(this.wordDataBaseList.Count);
    }
}
