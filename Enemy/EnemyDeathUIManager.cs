using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathUIManager : MonoBehaviour
{
    public GameObject enemyDeathPrefab;
    GameObject prefabInstance;

    public void ShowEnemyDyingMessage(Vector3 enemyPos)
    {
        prefabInstance = Instantiate(enemyDeathPrefab);
        prefabInstance.transform.position = enemyPos;

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
