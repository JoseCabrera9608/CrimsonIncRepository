using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PasivePatrol : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    private Vector3[] position;

    [SerializeField] private float speed;
     private int index;
    [SerializeField] private float scanTime;
    private NavMeshAgent agent;

     private float distanceToDestination;
     private bool scanning;
    [SerializeField] private GameObject scanLight;
    private Vector3 initialLightRotation;
    private float lightT;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        index = 1;

        position = new Vector3[patrolPoints.Length];
        for(int i = 0; i < patrolPoints.Length; i++)
        {
            position[i] = patrolPoints[i].position;
        }
        SetAgentDestination();
        agent.speed = speed;
        initialLightRotation = scanLight.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToDestination = Vector3.Distance(transform.position, position[index]);
        AgentPosCheck();
        ScanLight();
    }
    private void AgentPosCheck()
    {
        Vector3 destinationIgnoringY = new Vector3(position[index].x, 0, position[index].z);
        if (transform.position.x==destinationIgnoringY.x&&transform.position.z==destinationIgnoringY.z)
        {
            if (!scanning) StartCoroutine(ScanTerrain());
            scanning = true;
            Debug.Log("Scanning");
            
        }
    }
    private void ScanLight()
    {
        Vector3 lightDesiredRotation = new Vector3(initialLightRotation.x,
            initialLightRotation.y + 40, initialLightRotation.z);

        if (scanning)
        {
            scanLight.SetActive(true);
            lightT += Time.deltaTime;
            float lightRotationDuration = lightT/scanTime;
            scanLight.transform.localEulerAngles = Vector3.Lerp(initialLightRotation,
                lightDesiredRotation, lightRotationDuration);
        }
        else
        {
            scanLight.SetActive(false);
            lightT = 0;
        }
            
    }
    private void SetAgentDestination()=> agent.SetDestination(position[index]);

    private IEnumerator ScanTerrain()
    {
        yield return new WaitForSeconds(scanTime);
        if (index == position.Length - 1) index = 0;
        else index++;
        scanning = false;
        SetAgentDestination();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Transform pos in patrolPoints)
        {
            Gizmos.DrawWireSphere(pos.position, .2f);
        }
        for(int i = 0; i < patrolPoints.Length-1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
        } 
        if(patrolPoints.Length>2) Gizmos.DrawLine(patrolPoints[0].position,
            patrolPoints[patrolPoints.Length - 1].position);

    }
}
