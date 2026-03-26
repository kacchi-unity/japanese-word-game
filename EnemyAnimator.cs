using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;

    public float moveSpeed = 0.02f;
    //Modify when writing the difficulty adjustment script later.
    float walkAnimationSpeed = 20f;
    
    void SetWalkAnimationSpeed(float speed)
    {
        this.animator.speed = moveSpeed * walkAnimationSpeed;
    }

    void Start()
    {
        this.animator = GetComponent<Animator>();


        SetWalkAnimationSpeed(walkAnimationSpeed);
    }

    void Update()
    {
        
    }
}
