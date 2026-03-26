using System.Collections.Generic;
using UnityEngine;

public class WordListFlowManager : MonoBehaviour
{
    public GameObject WordLoader;
    public GameObject RandomWordTable;
    public GameObject EnemyGenerator;

    List<Word> allWordList, selectedWordList;

    public int selectAmount = 10;

    void Start()
    {
        allWordList= WordLoader.GetComponent<WordLoader>().GetAllWordList();

        RandomWordTable RandomCompo = RandomWordTable.GetComponent<RandomWordTable>();
        selectedWordList = RandomCompo.GetSelectWordList(allWordList, selectAmount);

        EnemyGenerator.GetComponent<EnemyGenerator>().SetSelectedWordList(selectedWordList);

        RandomCompo.ShowList();
    }
    
    void Update()
    {
        
    }
}
