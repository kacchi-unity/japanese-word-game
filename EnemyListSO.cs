using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "ScriptableObject/EnemyList")]
public class EnemyListSO : ScriptableObject
{
    private List<EnemyData> enemyList = new List<EnemyData>();

    private void OnEnable()
    {
        ResetList();
    }

    public void ResetList()
    {
        enemyList.Clear();
    }

    public List<EnemyData> GetEnemyList()
    {
        return this.enemyList;
    }

    //add enemy data to list (from generator script)
    public void AddEnemyData(EnemyData data)
    {
        enemyList.Add(data);
    }

    //remove enemy data from list
    public void RemoveEnemyData(EnemyData data)
    {
        enemyList.Remove(data);
    }
}
