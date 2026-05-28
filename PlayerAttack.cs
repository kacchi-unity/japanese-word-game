using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public PlayerAnimator playerAnimator;
    Vector3 enemyPos;
    Vector3 originalPos;

    public enum PlayerState
    {
        Idle,
        Attacking
    }
    PlayerState state = PlayerState.Idle;


    private void OnEnable()
    {
        QuizManager.OnAnswerCorrect += Attack;
    }

    private void OnDisable()
    {
        QuizManager.OnAnswerCorrect -= Attack;
    }

    void Attack(Vector3 pos)
    {
        if (state == PlayerState.Idle)
        {
            StartCoroutine(AttackCoroutine(pos));
        }
    }

    IEnumerator AttackCoroutine(Vector3 pos)
    {
        state = PlayerState.Attacking;
        originalPos = transform.position;
        transform.position = pos + new Vector3(-1.5f, 0f, 0f);
        playerAnimator.PlayAttack();
        yield return new WaitForSeconds(0.4f);
        transform.position = originalPos;
        playerAnimator.PlayIdle();
        state = PlayerState.Idle;
        
        
    }
}
