using NUnit.Framework;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData; //received from generator
    public EnemyListSO enemyListSO;
    GameObject ScoreManager, PlayerObject;
    ScoreManager ScoreManagerScript;
    public TextMeshProUGUI hintText;

    public static event Action<int, float, float> OnPlayerDamaged;

    public bool isKilledbyPlayer = false;
    bool isMove = true;
    int scoreAmount = 1;
    float moveSpeed = 0.02f;
    float damageToPlayer = 20f;
    float fadeInDuration = 0.5f;
    float fadeOutDuration = 0.5f;

    private void OnEnable()
    {
        HintManager.OnHintStatusCheck += ApplyHint;
    }

    private void OnDisable()
    {
        HintManager.OnHintStatusCheck -= ApplyHint;
    }

    public void Die()
    {
        //case of enemy killed by player attack
        //isKilledbyPlayer becomes true by EnemyListManager
        if (isKilledbyPlayer)
        {
            Destroy(gameObject);
            ScoreManagerScript.increaseScore(scoreAmount);
        }

        //case of enemy collider with player
        else
        {
            Destroy(gameObject);
            PlayerObject.GetComponent<PlayerHpManager>().DecreaseHp(damageToPlayer);
        }
    }

    

    //to collide with player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isKilledbyPlayer = false;
            //Debug.Log("플레이어 충돌!");
            int enemyId = this.enemyData.GetId();
            OnPlayerDamaged?.Invoke(enemyId, this.fadeInDuration, this.fadeOutDuration); //HintManager, etc...
            enemyListSO.RemoveEnemyData(enemyData);
            Die();
        }
    }

    public void SetMeaning(string meaning)
    {
        //Hint text initialization
        hintText.text = meaning;

        //Check hint status
        ApplyHint(enemyData.GetId(), HintManager.HintRenderMode.Blink);
    }

    private void ApplyHint(int targetId, HintManager.HintRenderMode mode)
    {
        //Use absolute values ​​and (-) signs to maintain fade-out and hint hash order.

        if (this.enemyData.GetId() == Mathf.Abs(targetId))
        {
            if (HintStatus.isHintActive(targetId)) //bool type
            {
                if (mode == HintManager.HintRenderMode.Fade)
                {
                    StartCoroutine(FadeIn());
                }
                else if (mode == HintManager.HintRenderMode.Blink)
                {
                    hintText.alpha = 1f;
                    hintText.gameObject.SetActive(true);
                }
                
            }

            else
            {
                if (mode == HintManager.HintRenderMode.Fade)
                {
                    Debug.Log("Fade 여기실행");
                    StartCoroutine(FadeOut());
                }
                else if (mode == HintManager.HintRenderMode.Blink)
                {
                    hintText.alpha = 0f;
                    hintText.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator FadeIn()
    {
        if (fadeInDuration == 0f)
        {
            fadeInDuration = 0.1f;
        }

        hintText.gameObject.SetActive(true);
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            hintText.alpha = Mathf.Lerp(0, 1, elapsed/fadeInDuration);
            yield return null;
        }
        hintText.alpha = 1.0f;
    }

    IEnumerator FadeOut()
    {
        if (fadeOutDuration == 0f)
        {
            fadeOutDuration = 0.1f;
        }

        hintText.alpha = 1f;
        float elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            hintText.alpha = Mathf.Lerp(1, 0, elapsed / fadeOutDuration);
            yield return null;
        }
        hintText.alpha = 0f;
        hintText.gameObject.SetActive(false);
    }



    void Start()
    {
        ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();
        PlayerObject = GameObject.Find("Player");


    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(new Vector3(-moveSpeed, 0f, 0f));
        }
    }

    
}