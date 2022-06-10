using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LluviaLasers : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private BoxCollider col;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject spriteHolder=null;
    [SerializeField] private GameObject player;
    public float damage;
    public float initialDistanceToGround;
    public float distanceToGround;
    AudioSource sonidoChoqueConSuelo;
    void Start()
    {
        sonidoChoqueConSuelo = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerStatus>().gameObject;
        SpawnSpriteOnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Feedback();
    }
    private void Feedback()
    {       
        if (spriteHolder != null)
        {
            distanceToGround = Vector3.Distance(transform.position - new Vector3(0, transform.localScale.y / 2, 0),
            spriteHolder.transform.position);
            spriteHolder.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(.4f, 1, .4f),
            distanceToGround / initialDistanceToGround);
        }
        
    }
    private void SpawnSpriteOnPlayer()
    {

        spriteHolder = Instantiate(sprite);
        spriteHolder.transform.position = player.transform.position;

        initialDistanceToGround = Vector3.Distance(transform.position - new Vector3(0, transform.localScale.y / 2, 0),
            spriteHolder.transform.position);
    }
    private IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(0.2f);
       // Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //  if (other.CompareTag("Player") || other.CompareTag("PlayerWeapon"))
        //  PlayerSingleton.Instance.playerCurrentHP -= damage;
        if (other.CompareTag("PlayerWeapon"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        sonidoChoqueConSuelo.Play();
        col.enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(spriteHolder);
        StartCoroutine(DestroyThisObject());
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position-new Vector3(0,transform.localScale.y/2,0), -transform.up * 10,Color.red);
    }
}
