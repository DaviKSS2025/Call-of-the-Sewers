using System;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SetCurrentHealth(int currentHealth, int currentCharacter)
    {
        /*var data = SaveManager.Instance.Data.slots[currentCharacter];
        data.CurrentHealth = currentHealth;

        SaveManager.Instance.Save();*/
    }
}
