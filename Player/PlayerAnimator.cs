using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    float IdleAnimationSpeed = 1f;

    public void SetAnimationSpeed(float speed)
    {
        this.animator.speed = IdleAnimationSpeed;
    }


    void Start()
    {
        this.animator = GetComponent<Animator>();


        SetAnimationSpeed(IdleAnimationSpeed);
    }

    void Update()
    {

    }
}
