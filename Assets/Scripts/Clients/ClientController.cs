using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClientController : MonoBehaviour
{
    public enum ClientStates
    {
        PATROL,
        SIT
    }

    private NavMeshAgent agent;
    [SerializeField] private Transform[] navPoints;
    [SerializeField] private float acceptanceRadius = 0.0f;
    private Transform target;

    private ClientStates actualState = ClientStates.PATROL;

    [SerializeField] private ChairBehav[] chairs;
    private ChairBehav chairSelected;
    [SerializeField] private float minSitTime = 0.0f, maxSitTime = 0.0f, sitTime = 0.0f;
    private float actualTime = 0.0f;
    [SerializeField] private GameManager.OrderTypes orderSelected = GameManager.OrderTypes.CAFE;
    private bool ordered = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameObject[] chairObjects = GameObject.FindGameObjectsWithTag("Chair");
        chairs = new ChairBehav[chairObjects.Length];

        for (int i = 0; i < chairObjects.Length; i++)
        {
            chairs[i] = chairObjects[i].GetComponent<ChairBehav>();
        }
    }

    private void Update()
    {
        switch (actualState)
        {
            case ClientStates.PATROL:
                if (target == null || (transform.position - target.position).sqrMagnitude < acceptanceRadius * acceptanceRadius)
                {
                    ChangeTarget(navPoints[Random.Range(0, navPoints.Length)]);

                    if (Random.Range(0, 100) < 50)
                    {
                        int chairIndex = Random.Range(0, chairs.Length);
                        ChangeTarget(chairs[chairIndex].transform);
                        chairSelected = chairs[chairIndex];
                    }
                }
                break;
            case ClientStates.SIT:
                if (agent.velocity == Vector3.zero)
                {
                    actualTime += Time.deltaTime;

                    if (chairSelected.client == null)
                    {
                        chairSelected.client = this;
                        sitTime = Random.Range(minSitTime, maxSitTime);
                        orderSelected = (GameManager.OrderTypes)Random.Range(1, (int)GameManager.OrderTypes.COUNT);
                        chairSelected.AddOrder(orderSelected);
                        orderSelected = GameManager.OrderTypes.VOID;
                    }                    
                }                

                if (actualTime > sitTime)
                {
                    actualTime = 0;
                    ChangeTarget(navPoints[Random.Range(0, navPoints.Length)]);
                    chairSelected.ClearOrder();
                    chairSelected.client = null;
                    ordered = false;
                    actualState = ClientStates.PATROL;
                }
                break;
            default:
                break;
        }        

        agent.SetDestination(target.position);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ChangeState(ClientStates newState)
    {
        actualState = newState;
    }

    public void ReciveOrder()
    {
        actualTime = 0;
        ChangeTarget(navPoints[Random.Range(0, navPoints.Length)]);
        chairSelected.ClearOrder();
        chairSelected.client = null;
        ordered = false;
        actualState = ClientStates.PATROL;

        Debug.Log("Thanks");
    }
}
