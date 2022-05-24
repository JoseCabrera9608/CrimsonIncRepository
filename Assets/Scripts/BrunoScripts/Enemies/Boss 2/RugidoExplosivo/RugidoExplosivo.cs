using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RugidoExplosivo : MonoBehaviour
{
    public GameObject fullSprite;

    public float dimension;
    public float chargeTime;
    public float damage;

    private GameObject player;
    [SerializeField] private GameObject particles;
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;

        transform.localScale = new Vector3(dimension*2,dimension*2,dimension*2);
        fullSprite.transform.localScale = Vector3.zero;
        fullSprite.transform.DOScale(0.097f,chargeTime).OnComplete(OnChargeComplete);
    }

    private void OnChargeComplete()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= dimension) PlayerSingleton.Instance.playerCurrentHP -= damage;
        foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
        GameObject obj = Instantiate(particles);
        obj.transform.position = transform.position;
        obj.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}
