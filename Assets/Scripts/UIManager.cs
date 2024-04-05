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
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _finalScoreText;

    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _uiScreen;
    
    private ShipStats _ss;

    private void Awake()
    {
        _ss = FindObjectOfType<ShipStats>();

        _ss.OnHealthChanged += UpdateHealthText;
        _ss.OnScoreChanged += UpdateScoreText;
        _ss.OnDeath += OnPlayerDeath;
    }

    private void UpdateHealthText(int health)
    {
        _healthText.text = $"Health: {health}";
    }

    private void UpdateScoreText(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    private void UpdateFinalScoreText(int score)
    {
        _finalScoreText.text = $"Final Score: {score}";
    }

    private void EnableDeathScreen()
    {
        _deathScreen.SetActive(true);
        _uiScreen.SetActive(false);
    }

    private void OnPlayerDeath(int score)
    {
        UpdateFinalScoreText(score);
        EnableDeathScreen();
    }

    public void OnClickPlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (_ss == null) return;
        _ss.OnHealthChanged -= UpdateHealthText;
        _ss.OnScoreChanged -= UpdateScoreText;
    }
}
