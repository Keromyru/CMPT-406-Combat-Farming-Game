using UnityEngine;

[CreateAssetMenu(fileName = "OnHit Normal", menuName = "Plant Data/Plant Action/OnHit Normal")]
public class NormalPlantOnHitSO : PlantOnHitSO
{
    public override float onHit(float damage, GameObject source, GameObject thisObject)
    {
        return damage;
    }
}
