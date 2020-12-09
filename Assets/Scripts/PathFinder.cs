using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GoHere(Vector2 target)
    {
        agent.isStopped = false;
        agent.SetDestination(target);
        while ( (Vector2)this.transform.position != target)
        {}
        agent.isStopped = true;
    }
}
