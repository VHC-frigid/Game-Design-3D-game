using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    public void UpdateHealthbar(float newValue)
    {
        healthBar.fillAmount = newValue;
    }
}
