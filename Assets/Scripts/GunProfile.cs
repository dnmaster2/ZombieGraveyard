using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Profile", menuName = "Create Gun", order = 1)]
public class GunProfile : ScriptableObject
{
    public int damage;
    public int ammo;
    public int magazineSize;
    public float cadency;
}
