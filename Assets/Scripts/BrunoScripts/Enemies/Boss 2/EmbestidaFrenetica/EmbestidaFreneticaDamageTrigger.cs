using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmbestidaFreneticaDamageTrigger : MonoBehaviour
{
    [SerializeField] public EmbestidaFreneticaID type;
    [SerializeField] private GameObject parent;
    private GorgonopsiaBoss script;
    public float damage;
    [Header("==============PREFERENCES================")]
    public bool showDebugAreas;
    public float areaLenght;
    void Start()
    {
        parent = transform.parent.gameObject;
        script = FindObjectOfType<GorgonopsiaBoss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Break();
            switch (type)
            {
                case EmbestidaFreneticaID.middle:
                    Instakill();
                    //parent.SetActive(false);
                    break;

                default:
                    DamagePlayer();
                    //parent.SetActive(false);
                    break;
            }
        }

        if (other.CompareTag("Wall"))
        {
            script.embestidaTween.Kill();
            script.ResetEmbestidaParentColliders();
        }
    }
    private void DamagePlayer()
    {
        //PlayerSingleton.Instance.playerCurrentHP -= damage;
        PlayerStatus.damagePlayer?.Invoke(damage);
    }
    private void Instakill()
    {
        //PlayerSingleton.Instance.playerCurrentHP = 0;
        PlayerStatus.damagePlayer?.Invoke(9999);
    }
    private void OnDrawGizmos()
    {
        if (showDebugAreas == false)
            return;
        
        switch (type)
        {
            case EmbestidaFreneticaID.middle:
                Gizmos.color = new Vector4(1, 0, 0, .2f);
                Gizmos.DrawCube(transform.position + transform.forward * 10,
                    transform.localScale + new Vector3(0, 0, areaLenght));
                break;
            default:
                Gizmos.color = new Vector4(0,1,0,.2f);
                Gizmos.DrawCube(transform.position + transform.forward * 10,
                    transform.localScale + new Vector3(0, 0, areaLenght));
                break;
        }
    }
}
public enum EmbestidaFreneticaID
{
    left,
    middle,
    right
}
