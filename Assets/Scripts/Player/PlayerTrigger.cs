using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public void HealthChange(int decrease)
    {
        currentHealth-=decrease;
    }
}
