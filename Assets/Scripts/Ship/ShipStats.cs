using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    private const int MaxHealth = 10;
    public int Health { get; private set; } = MaxHealth;
    public int Score { get; private set; }

    public event Action<bool> OnShieldedChanged;
    
    private bool _isShielded;
    public bool IsShielded
    {
        get => _isShielded;
        set
        {
            if (_isShielded == value) return;
            _isShielded = value;
            OnShieldedChanged?.Invoke(_isShielded);
        }
    }
    public bool IsStalled { get; set; }

    private UIManager _ui;

    private void Start()
    {
        _ui = FindObjectOfType<UIManager>();
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
        CheckForDeath();
    }

    public void GainHealth(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        _ui.UpdateHealthText();
    }

    private void CheckForDeath()
    {
        if (Health == 0)
        {
            Debug.Log("dead");
        }
    }
}
