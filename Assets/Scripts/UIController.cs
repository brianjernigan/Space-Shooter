using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private ShipStats _shipStats;

    public void UpdateTexts()
    {
        _healthText.text = $"Health: {_shipStats.Health}";
        _scoreText.text = $"Score: {_shipStats.Score}";
    }
}
