using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData; //received from generator
    GameObject enemyListManager, ScoreManager;
    ScoreManager ScoreManagerScript;

    public bool isKilledbyPlayer = false;
    bool isMove = true;
    int scoreAmount = 1;
    float moveSpeed = 0.02f;

    public void Die()
    {
        //case of enemy killed by player attack
        //isKilledbyPlayer becomes true by EnemyListManager
        if (isKilledbyPlayer)
        {
            Destroy(gameObject, 2f);
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            TextMeshProUGUI[] listUI = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI TMProUI in listUI)
            {
                if (TMProUI.name.Equals("Kanji"))
                {
                    TMProUI.text = "";
                    break;
                }
            }

            //dying message
            ShowEnemyDyingMessage(scoreAmount);

            //increase score
            ScoreManagerScript.increaseScore(scoreAmount);
        }

        //case of enemy collider with player
        else
        {
            Destroy(gameObject);
            //(나중) 체력 깎는 매니저 호출해
        }
    }

    void ShowEnemyDyingMessage(int scoreAmount)
    {
        TextMeshProUGUI[] listUI = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI TMProUI in listUI)
        {
            if (TMProUI.name.Equals("DyingMessage"))
            {
                TMProUI.text = "+" + scoreAmount;
                isMove = false;
                break;
            }
        }

    }//Die()

    //to collide with player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isKilledbyPlayer = false;
            Debug.Log("플레이어 충돌!");
            enemyListManager.GetComponent<EnemyListManager>().RemoveEnemyData(enemyData);
            this.Die();
        }
    }

    void Start()
    {
        enemyListManager = GameObject.Find("EnemyListManager");
        ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();

    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(new Vector3(-moveSpeed, 0f, 0f));
        }
    }

    
}