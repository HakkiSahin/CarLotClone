using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    public NavMeshAgent _agent;

    [SerializeField] private float waitParkTime = 10f;
    [SerializeField] private float waitEntranceTime = 15f;

    public bool parkCount, entranceCount = true;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SelectCar();
    }

    private void SelectCar()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (entranceCount)
        {
            waitEntranceTime -= Time.deltaTime;
            if (waitEntranceTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (parkCount)
        {
            waitParkTime -= Time.deltaTime;
            if (waitParkTime <= 0)
            {
                _agent.enabled = false;
                transform.Translate(0, 0, -Time.deltaTime * 1.5f);
                Destroy(gameObject, 4f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Park")
        {
            parkCount = true;
        }
    }
}