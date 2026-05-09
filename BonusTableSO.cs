using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BonusTableSO", menuName = "ScriptableObject/BonusTableSO")]

public class BonusTableSO : ScriptableObject
{
    

    public RewardBonusTable rewardBonuse = new RewardBonusTable();

    [Serializable]
    public class RewardBonusTable
    {
        [Header("Word Count Settings")]
        [SerializeField] private float wordCount_Multiplier = 0.2f;
        [SerializeField] private float wordCount_Min = 0.02f;
        public float WordCount_Multiplier => wordCount_Multiplier;
        public float WordCount_Min => wordCount_Min;

        [Header("Player HP Settings")]
        [SerializeField] private float playerHp_Multiplier = 0.1f;
        [SerializeField] private float playerHp_Base = 1.1f;
        public float PlayerHp_Multiplier => playerHp_Multiplier;
        public float PlayerHp_Base => playerHp_Base;

        [Header("Enemy Speed Rate Settings")]
        [SerializeField] private float enemySpeedRate_Multiplier = 1.5f;
        [SerializeField] private float enemySpeedRate_Min = 0.02f;
        public float EnemySpeedRate_Multiplier => enemySpeedRate_Multiplier;
        public float EnemySpeedRate_Min => enemySpeedRate_Min;

        [Header("Enemy Spawn Delay Settings")]
        [SerializeField] private static float enemySpawnDelay_Multiplier = 0.5f;
        [SerializeField] private float enemySpawnDelay_Base = 1.5f;
        public float EnemySpawnDelay_Multiplier => enemySpawnDelay_Multiplier;
        public float EnemySpawnDelay_Base => enemySpawnDelay_Base;

        [Header("Time Limit Settings")]
        [SerializeField] private float timeLimit_Multiplier = 0.01f;
        [SerializeField] private float timeLimit_Min = 0.02f;
        public float TimeLimit_Multiplier => timeLimit_Multiplier;
        public float TimeLimit_Min => timeLimit_Min;

        [Header("Hint Active Time Settings")]
        [SerializeField] private float hintActiveTime_Multiplier = 0.15f;
        [SerializeField] private float hintActiveTime_Base = 3f;
        public float HintActiveTime_Multiplier => hintActiveTime_Multiplier;
        public float HintActiveTime_Base => hintActiveTime_Base;



        
    }
}
