using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    List<Transform> transforms;
    GameObject[] find;
    public GameObject dust_effect;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    List<Transform> Trans(){
        transforms = new List<Transform>();
        find = GameObject.FindGameObjectsWithTag("player");
        for (int i = 0; i < find.Length; i++)
        {
            if(find[i].gameObject.transform != gameObject.transform){
                transforms.Add(find[i].gameObject.transform);
            }
        }
        return transforms;
    }
    Transform GetClosestEnemy (List<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.tag == "sphere"){
            dust_effect.SetActive(false);
            GetComponent<AgentScript>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death() {
        agent.GetComponent<CapsuleCollider>().isTrigger = true;
        yield return new WaitForSeconds (1f);
        Destroy(gameObject);
        yield return null;
    }
    private void Update() {
        find = GameObject.FindGameObjectsWithTag("player");
        if(find.Length != 1){
            dust_effect.SetActive(true);
            agent.SetDestination(GetClosestEnemy(Trans()).transform.position);
            agent.speed = Random.Range(1,100);
            agent.GetComponent<Rigidbody>().mass = Random.Range(100,150);
        }
        else{
            agent.speed = 0;
        }
    }


    
}
