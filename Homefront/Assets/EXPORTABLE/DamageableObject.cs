using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        animator.SetBool("IsDamaged", true); // trigger the damage 
    }

    public void ResetDamageAnimation()
    {
        animator.SetBool("IsDamaged", false); // reset 
    }
}
