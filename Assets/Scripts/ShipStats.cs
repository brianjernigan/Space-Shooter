using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{ 
    private int Health { get; set; } = 10;

    public bool IsShielded { get; set; }

    public void TakeDamage(int damage)
    {
        if (IsShielded)
        {
            IsShielded = false;
            return;
        }
        
        Health = Math.Max(0, Health - damage);
    }

    public void GainHealth(int amount)
    {
        Health = Mathf.Min(Health + amount, 10);
    }
}
