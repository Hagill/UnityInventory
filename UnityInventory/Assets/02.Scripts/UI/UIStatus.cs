using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI defence;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI Critical;

    public void SetCharacterStats(Character player)
    {
        if (player != null)
        {
            attack.text = $"{player.curAttackPoint}";
            defence.text = $"{player.curDefencePoint}";
            health.text = $"{player.curHealth}";
            Critical.text = $"{player.Critical}";
        }
    }
}
