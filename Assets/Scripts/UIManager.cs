using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _scoreText;
    
    private ShipStats _ss;

    private void Awake()
    {
        _ss = FindObjectOfType<ShipStats>();
    }
    
    public void UpdateHealthText()
    {
        _healthText.text = $"Health: {_ss.Health}";
    }

    public void UpdateScoreText()
    {
        _scoreText.text = $"Score: {_ss.Score}";
    }
}
