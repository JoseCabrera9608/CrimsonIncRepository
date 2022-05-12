using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BombaFlash : MonoBehaviour
{
    private float t;
    public float delay;
    public float flashDuration;
    private bool exploded;
    private bool touchedSomething=false;
    private SphereCollider[] col=new SphereCollider[2];
    [SerializeField]private GameObject flashPanel;
    private GameObject player;
    private void Start()
    {
        col = GetComponents<SphereCollider>();
        flashPanel = GameObject.Find("FlashPanel");
        player = FindObjectOfType<PlayerStatus>().gameObject;
        GetComponent<Rigidbody>().centerOfMass = Vector3.one;
        AlignToPlayer();
        ApplyForce();
    }
    void Update()
    {
        CountDown();
    }
    private void CountDown()
    {
        if (exploded == false && touchedSomething == true)
        {
            t += Time.deltaTime;
            if(t>=delay)
            {
                exploded = true;
                t = 0;
                col[0].enabled = false;
                col[1].enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                Flash();
            }
        }
    }
    private void Flash()
    {
        //flashPanel.SetActive(true);
        Sequence flash = DOTween.Sequence();
        flash.Append(flashPanel.GetComponent<CanvasGroup>().DOFade(1, .2f).SetEase(Ease.InQuint));
        flash.Append(flashPanel.GetComponent<CanvasGroup>().DOFade(0, flashDuration).SetEase(Ease.InQuint));

        flash.OnComplete(DestroyObjects);
    }
    private void DestroyObjects()
    {
        //flashPanel.SetActive(false);
        Destroy(gameObject);
    }
    private void AlignToPlayer()
    {
        var lookPos = player.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        Vector3 direction = new Vector3(-20, 0, 0);
        transform.localEulerAngles += direction;

    }

    private void ApplyForce()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * ((Vector3.Distance(transform.position,player.transform.position)*0.2f));
    }
    private void OnCollisionEnter(Collision collision)
    {
        touchedSomething = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon")) Destroy(gameObject);
    }
}
