using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    public enum AISTATE {PATROL=0, CHASE=1};

    private NavMeshAgent ThisAgent = null;
    private Transform PlayerTransform = null;
    public float ChaseDistance = 5f;

    public AISTATE CurrentState {
        get {
            return _CurrentState;
        }
        set {
            StopAllCoroutines();
            _CurrentState = value;

            switch(CurrentState){
            case AISTATE.PATROL:
                StartCoroutine(StatePatrol());
                break;
            case AISTATE.CHASE:
                StartCoroutine(StateChase());
                break;
            }
        }
    }
    [SerializeField]
    private AISTATE _CurrentState = AISTATE.PATROL;

    private void Awake() {
        ThisAgent = GetComponent<NavMeshAgent>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
    }
    private void Start() {
        CurrentState = AISTATE.PATROL;
    }


    public IEnumerator StatePatrol(){
        GameObject[] Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject CurrentWaypoint = Waypoints[Random.Range(0, Waypoints.Length)];
        float TargetDistance = 2f;

        while(CurrentState == AISTATE.PATROL){
            ThisAgent.SetDestination(CurrentWaypoint.transform.position);

            if(Vector3.Distance(transform.position, CurrentWaypoint.transform.position) < TargetDistance){
                CurrentWaypoint = Waypoints[Random.Range(0, Waypoints.Length)];
            }

            yield return null;
        }
    }

    public IEnumerator StateChase(){

        while(CurrentState == AISTATE.CHASE){

            if(Vector3.Distance(transform.position, PlayerTransform.position) > ChaseDistance){
                CurrentState = AISTATE.PATROL;
                Debug.Log("Exit Chase");
                yield break;
            }
            ThisAgent.SetDestination(PlayerTransform.position);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ball")){
            CurrentState = AISTATE.CHASE;
            Debug.Log("Enter Chase");
        }
    }
}
