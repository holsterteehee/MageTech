using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGun : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Magic gun Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
