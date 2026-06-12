using System;
using UnityEngine;
using UnityEngine.UI; //for control UI

public class PlayerHpManager : MonoBehaviour
{
    public static event Action OnPlayerHpZero;

    float maxHp, currentHp;
    
    Image hpGauge;
    GameObject gameOverManager;
    public PlayerAnimator playerAnimator;

    private void OnEnable()
    {
        EnemyController.OnPlayerDamaged += DecreaseHp;
    }

    private void OnDisable()
    {
        EnemyController.OnPlayerDamaged -= DecreaseHp;
    }

    public void DecreaseHp(int enemyId_unused, float fadeIn_unused, float fadeOut_unused, float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            //send gameover signal
            hpGauge.GetComponent<Image>().fillAmount = 0f;
            OnPlayerHpZero?.Invoke();
        }
        else
        {
            hpGauge.GetComponent<Image>().fillAmount = currentHp / maxHp;
        }
    }

    void Awake()
    {
        maxHp = GameDataManager.Instance.RuntimeLobbySetting.GetValue(SettingList.PlayerHp);
        currentHp = maxHp;
    }
    

    void Start()
    {

        Image[] imageUI = gameObject.GetComponentsInChildren<Image>();
        foreach (Image targetImage in imageUI)
        {
            if (targetImage.name.Equals("hpGauge"))
            {
                hpGauge = targetImage;
                break;
            }
        }
        hpGauge.GetComponent <Image>().fillAmount = currentHp/maxHp;

        gameOverManager = GameObject.Find("GameOverManager");

    }

}
