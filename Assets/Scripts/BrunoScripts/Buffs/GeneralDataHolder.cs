using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DATAHOLDER", menuName = "DATAHOLDER")]
public class GeneralDataHolder : ScriptableObject
{
    public Vector3 lastSafeSpot;
    public bool spawnDataRecoveryObject; 
}
