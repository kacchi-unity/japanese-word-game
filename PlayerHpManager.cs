using UnityEngine;
using UnityEngine.UI; //for control UI

public class PlayerHpManager : MonoBehaviour
{
    float maxHp = 100;
    float currentHp = 100;
    Animator animator;

    Image hpGauge;
    GameObject gameOverManager;
    public PlayerAnimator playerAnimator;

    public void DecreaseHp(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            //send gameover signal
            hpGauge.GetComponent<Image>().fillAmount = 0f;
            gameOverManager.GetComponent<GameOverManager>().GameOverSetting();

            //change lie animation
            animator.SetTrigger("LieTrigger");
            playerAnimator.PlayDeath();
        }
        else
        {
            hpGauge.GetComponent<Image>().fillAmount = currentHp / maxHp;
        }
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

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
