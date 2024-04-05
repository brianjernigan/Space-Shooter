//////////////////////////////////////////////
//Assignment/Lab/Project: Space Shooter
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/08/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    private const float InitialMultiplier = 1f;
    private const float RateOfIncrease = 1.02f;
    
    [SerializeField] private Material _shieldMat;
    [SerializeField] private Material _shipMat;

    public const int MaxHealth = 10;
    
    public event Action<int> OnHealthChanged;
    public event Action<int> OnDeath;
    public int Health { get; private set; } = MaxHealth;
    public bool IsDead { get; set; }
    
    public event Action<int> OnScoreChanged;
    private int _score;
    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            SetModifier();
        }
}
    
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

    public float Modifier => SetModifier();
    
    private MeshRenderer _mr;

    private void Start()
    {
        _mr = GetComponent<MeshRenderer>();
    }
    
    // Enemies' speed increases exponentially with score
    private float SetModifier()
    {
        return InitialMultiplier * Mathf.Pow(RateOfIncrease, Score);
    }
    
    public void IncreaseScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }
    
    
    public void TakeDamage(int damage)
    {
        if (IsShielded)
        {
            IsShielded = false;
            return;
        }

        Health = Math.Max(0, Health - damage);
        OnHealthChanged?.Invoke(Health);

        if (Health != 0) return;
        IsDead = true;
        OnDeath?.Invoke(Score);
    }

    public void GainHealth(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        OnHealthChanged?.Invoke(Health);
    }
    
    private void ChangeShipMaterial(bool isShielded)
    {
        _mr.material = isShielded ? _shieldMat : _shipMat;
    }
}
