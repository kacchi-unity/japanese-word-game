using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText, messageLabelText;
    public EnemyGenerator enemyGenerator;
    public TMP_InputField inputField;
    public GameSessionSO gameSessionSO;

    void OnEnable()
    {
        PlayerHpManager.OnPlayerHpZero += ProcessDefeat;
        TimerManager.OnTimeZero += ProcessVictory;
    }

    void OnDisable()
    {
        PlayerHpManager.OnPlayerHpZero -= ProcessDefeat;
        TimerManager.OnTimeZero -= ProcessVictory;
    }

    public void ProcessDefeat()
    {
        gameOverText.text = "게임 오버!";
        messageLabelText.text = "점수를 모두 잃었습니다";
        gameSessionSO.ResetScore();
        StartCoroutine(GameOverSetting());

    }

    public void ProcessVictory()
    {
        gameOverText.text = "생존!";
        messageLabelText.text = "결과창으로 이동합니다";
        StartCoroutine(GameOverSetting());
    }

    IEnumerator GameOverSetting()
    {
        enemyGenerator.isSpawn = false;
        inputField.interactable = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Scene_Result");
    }

    void Start()
    {
        gameOverText.text = "";
    }
}
