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
    [SerializeField] private Material _shieldMat;
    [SerializeField] private Material _shipMat;

    public const int MaxHealth = 10;
    public int Health { get; private set; } = MaxHealth;
    public int Score { get; private set; }
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
    public bool IsDead { get; private set; }
    public float DifficultyMultiplier => CalculateDifficultyMultiplier();

    private UIManager _ui;
    private MeshRenderer _mr;
    
    private const float InitialMultiplier = 1f;
    private const float RateOfIncrease = 1.02f;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnDeath;

    private void Start()
    {
        _ui = FindObjectOfType<UIManager>();
        _mr = GetComponent<MeshRenderer>();
    }
    
    public void IncreaseScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    // Enemies' speed increases exponentially with score
    private float CalculateDifficultyMultiplier()
    {
        return InitialMultiplier * Mathf.Pow(RateOfIncrease, Score);
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

        if (Health == 0) OnDeath?.Invoke(Score);
        // _ui.UpdateHealthText();
        // CheckForDeath();
    }

    public void GainHealth(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        OnHealthChanged?.Invoke(Health);
        // _ui.UpdateHealthText();
    }
    
    private void ChangeShipMaterial(bool isShielded)
    {
        _mr.material = isShielded ? _shieldMat : _shipMat;
    }
}
