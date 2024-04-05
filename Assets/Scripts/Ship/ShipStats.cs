using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] private Material _shieldMat;
    [SerializeField] private Material _shipMat;

    public const int MaxHealth = 10;
    public int Health { get; private set; } = MaxHealth;
    public int Score { get; private set; }

    public float Multiplier => CalculateMultiplier();
    
    private bool _isShielded;
    public bool IsShielded
    {
        get => _isShielded;
        set
        {
            _isShielded = value;
            ChangeShipMaterial(_isShielded);
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

    private float CalculateMultiplier()
    {
        var initialMultiplier = 1f;
        var rateOfIncrease = 1.02f;

        return initialMultiplier * Mathf.Pow(rateOfIncrease, Score);
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
    
    private void ChangeShipMaterial(bool isShielded)
    {
        gameObject.GetComponent<MeshRenderer>().material = isShielded ? _shieldMat : _shipMat;
    }
}
