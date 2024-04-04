using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{ 
    public int Health { get; private set; } = 10;
    public int Score { get; set; }
    public bool IsShielded { get; set; }
    public bool IsStalled { get; set; }

    private UIController _ui;

    private void Start()
    {
        _ui = FindObjectOfType<UIController>();
    }
    
    public void IncreaseScore(int amount)
    {
        Score += amount;
        _ui.UpdateScoreText();
    }
    
    public void TakeDamage(int damage)
    {
        if (IsShielded)
        {
            IsShielded = false;
            return;
        }
        
        Health = Math.Max(0, Health - damage);
        _ui.UpdateHealthText();
    }

    public void GainHealth(int amount)
    {
        Health = Mathf.Min(Health + amount, 10);
        _ui.UpdateHealthText();
    }
}
