using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Enemy;
    public PlayerAnimator playerAnimator;
    Vector3 enemyPos;
    Vector3 originalPos;

    public enum PlayerState
    {
        Idle,
        Attacking
    }
    PlayerState state = PlayerState.Idle;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && (state == PlayerState.Idle))
        {
            enemyPos = Enemy.transform.position;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        state = PlayerState.Attacking;
        originalPos = transform.position;
        transform.position = enemyPos + new Vector3(0f, 0f, 0f);
        Debug.Log("공격!!");
        playerAnimator.PlayAttack();
        yield return new WaitForSeconds(1f);
        transform.position = originalPos;
        playerAnimator.PlayIdle();
        state = PlayerState.Idle;
        
    }
}
