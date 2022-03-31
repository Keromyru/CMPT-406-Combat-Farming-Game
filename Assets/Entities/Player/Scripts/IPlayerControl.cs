using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public interface IPlayerControl 
{
    public void setHealth(float newHealth);
    public float getHealth();
    public float getMaxHealth();
    public void heal(float healValue);
    public Vector2 getLocation();
    public void setLocation(Vector2 newLocation);
    public void resetHealth();
    public void setNewOnAttack(PlayerOnAttackSO newAttack);
}
