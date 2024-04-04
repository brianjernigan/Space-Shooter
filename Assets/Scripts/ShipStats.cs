using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{ 
    public int Health { get; private set; } = 10;
    public int Score { get; set; }
    
    public bool IsShielded { get; set; }

    public void IncreaseScore()
    {
        Score++;
    }
    
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
