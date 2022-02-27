using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{

    //I don't know if this a good practice. Just attributes xd
    public float _baseDmgHuman = 1f;
    public float _baseDmgWerewolf = 0.3f;
    public float _armorDmgHuman = 0.25f;
    public float _armorDmgWerewolf = 1.5f;

    //Enemies
    public float _enemyDamageToPlayer = 5f;

}
