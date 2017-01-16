using UnityEngine;
using System.Collections;

public interface IDamageable {
    void TakeDamage(float damageTaken);
    void HealDamage(float healTaken);
}
