using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerAttack : MonoBehaviour
{
    public GameObject testTarget; //test
    private void Update() //test
    {
        if (Input.GetKeyDown(KeyCode.Y) && state == PlayerState.Idle)
        {
            StartCoroutine(AttackCoroutine(testTarget.transform.position));
        }
    }

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
        EnemyListManager.OnAnswerCorrect += Attack;
    }

    private void OnDisable()
    {
        EnemyListManager.OnAnswerCorrect -= Attack;
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
        GetComponent<Collider2D>().enabled = false;
        state = PlayerState.Attacking;
        originalPos = transform.position;
        transform.position = pos + new Vector3(-1.5f, 0f, 0f);
        Debug.Log("공격!!");
        playerAnimator.PlayAttack();
        yield return new WaitForSeconds(0.4f);
        transform.position = originalPos;
        GetComponent<Collider2D>().enabled = true;
        playerAnimator.PlayIdle();
        state = PlayerState.Idle;
        
        
    }
}
