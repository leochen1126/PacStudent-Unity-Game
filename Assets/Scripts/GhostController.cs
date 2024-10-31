using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public bool IsScared { get; private set; }
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetScared(bool scared)
    {
        IsScared = scared;
        animator.SetBool("IsScared", scared);
    }

    public void SetDead()
    {
        animator.SetTrigger("Die");
        StartCoroutine(ResetToWalkingStateAfterDelay(5f));
    }

    private IEnumerator ResetToWalkingStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsScared", false);
    }
}
