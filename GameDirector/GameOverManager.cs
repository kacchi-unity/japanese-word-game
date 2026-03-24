using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    GameObject gameOverText, enemyGenerator;
    public TMP_InputField inputField; 

    public void GameOverSetting()
    {
        gameOverText.GetComponent<TextMeshProUGUI>().text = "게임 오버!";
        enemyGenerator.GetComponent<EnemyGenerator>().isSpawn = false;
        DestroyAllEnemies();
        inputField.interactable = false;
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void Start()
    {
        gameOverText = GameObject.Find("GameOverText");
        enemyGenerator = GameObject.Find("EnemyGenerator");
        gameOverText.GetComponent<TextMeshProUGUI>().text = "";
    }

    void Update()
    {
        
    }
}
