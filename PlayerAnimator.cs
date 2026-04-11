using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;

    float idleSpeed = 1.0f;
    float deathSpeed = 0.5f;
    float attackSpeed = 1.5f;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        this.animator.speed = idleSpeed;
        animator.SetTrigger("Idle");
    }

    public void PlayDeath()
    {
        this.animator.speed = deathSpeed;
        animator.SetTrigger("Death");
    }

    public void PlayAttack()
    {
        this.animator.speed = attackSpeed;
        int randomAttack = Random.Range(0, 2);
        animator.SetTrigger(randomAttack == 0 ? "AttackHorizontal" : "AttackVertical");
    }

}
