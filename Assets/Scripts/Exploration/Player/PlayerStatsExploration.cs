using System;
using UnityEngine;

public class PlayerStatsExploration
{
    //Class to put player's power, defense, mobility variables
    private int _maxHealth;
    private int _currentHealth;
    private float _movementSpeed;
    public PlayerStatsExploration(int maxHealth, float movementSpeed)
    {
        _maxHealth = maxHealth;
        _movementSpeed = movementSpeed;
    }

    public void Initialize()
    {
        _currentHealth = SaveManager.Instance.Data.PlayerData.CurrentHealth;
    }
    public float MaxMovementSpeed
    {
        get => _movementSpeed;
    }

    private void TakeDamage(int damageToSuffer)
    {
        _currentHealth -= damageToSuffer;
        if (_currentHealth < 0)
        {
            //playerDamageTakenManager.PlayerDying();
        }
    }
}
