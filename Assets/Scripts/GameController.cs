using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    private NavMeshAgent selectedAgent;

    [SerializeField] private GameObject car;

    [SerializeField] private Transform respawnPos;

    [SerializeField] private Transform entrances;

    // Start is called before the first frame update
    void Start()
    {
        CreateCar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<CarController>())
                {
                    selectedAgent = hit.transform.GetComponent<NavMeshAgent>();
                }

                if (hit.transform.tag == "Park" && selectedAgent)
                {
                    Debug.Log(hit.transform.name);
                    selectedAgent.SetDestination(hit.transform.position);
                    selectedAgent.gameObject.layer = 2;
                    selectedAgent.GetComponent<CarController>().entranceCount = false;
                    selectedAgent.transform.parent = null;
                    selectedAgent = null;
                }
            }
        }
    }

    void CreateCar()
    {
        for (int i = 0; i < entrances.childCount; i++)
        {
            if (entrances.GetChild(i).gameObject.activeInHierarchy && entrances.GetChild(i).childCount <= 0)
            {
                GameObject cCar = Instantiate(car, respawnPos.position, Quaternion.identity);
                cCar.GetComponent<CarController>()._agent.SetDestination(entrances.GetChild(i).transform.position);
                cCar.transform.parent = entrances.GetChild(i);
                break;
            }
        }

        StartCoroutine(CreateCarTimer());
    }

    IEnumerator CreateCarTimer()
    {
        yield return new WaitForSeconds(3f);
        CreateCar();
    }
}