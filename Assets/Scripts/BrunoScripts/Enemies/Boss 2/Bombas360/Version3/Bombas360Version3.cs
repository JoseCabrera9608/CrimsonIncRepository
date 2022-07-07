using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombas360Version3 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject explotionObj;
    public SphereCollider col;
    public float distanceToPlayer;
    public float bombsToSpawn;
    

    [Range(0, 1)]
    public float t;

    //VARIABLES DE STATS
    public float distanceTreshold;
    public float timeToDamage;
    public float damage;

    [Header("========MATH======")]
    public float perimeter;
    public float explotionPerimeter;
    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;       
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        StartCoroutine(AutoDestoy(timeToDamage));
        SpawnBombs();
    }
    private IEnumerator AutoDestoy(float time)
    {
        yield return new WaitForSeconds(time);
        GorgonopsiaSFX.Instance.Play("bombaNapalm");
        Destroy(gameObject);
    }
    private void SpawnBombs()
    {
        perimeter = 2 * Mathf.PI * distanceToPlayer;
        explotionPerimeter = 2 * Mathf.PI * distanceTreshold;

        bombsToSpawn = 1 / ((distanceTreshold / perimeter) * 2);
        for(int i = 0; i < bombsToSpawn; i++)
        {
            GameObject obj = Instantiate(explotionObj);
            obj.transform.position = GetPos((distanceTreshold/perimeter)*(i*2));

            Bomba360Explotion script = obj.GetComponent<Bomba360Explotion>();
            script.endScale = distanceTreshold;
            script.damage = damage;
            script.timeToDamage = timeToDamage;
        }
    }
    private Vector3 GetPos(float _t)
    {
        float x = distanceToPlayer * Mathf.Cos(2 * Mathf.PI * _t);
        float z = distanceToPlayer * Mathf.Sin(2 * Mathf.PI * _t);

        return new Vector3(transform.position.x + x, transform.position.y+0.1f, transform.position.z + z);
    }
    private void OnDrawGizmos()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToPlayer);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetPos(t),distanceTreshold);


        //math
        perimeter = 2 * Mathf.PI * distanceToPlayer;
        explotionPerimeter = 2 * Mathf.PI * distanceTreshold;
               
    }
}
