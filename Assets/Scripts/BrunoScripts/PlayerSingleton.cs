using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton _instance;
    public static PlayerSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerSingleton");
                go.AddComponent<PlayerSingleton>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;

        #region defaultVars
        playerMaxHP = 100;
        playerCurrentHP = 100;
        playerCurrentHealingCharges = 5;
        playerMaxHealingCharges = 5;
        playerHitted = false;
        #endregion
    }

    public float playerMaxHP { set; get; }
    public float playerCurrentHP { set; get; }
    public float playerCurrentHealingCharges { set; get; }
    public float playerMaxHealingCharges { set; get; }
    public bool playerHitted { set; get; }
}
