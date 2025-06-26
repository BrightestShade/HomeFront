using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour, IItem
{
    public int healAmount = 1; 
    public static event Action<int> OnHealthPickUp;

    public void Start()
    {
        Destroy(gameObject, 4);
    }
    


    public void OnTriggerEnter2D()
    {
        OnHealthPickUp.Invoke(healAmount); 
        Destroy(gameObject);
    }
}
