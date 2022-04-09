using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBehavior : MonoBehaviour
{
    public List<GameObject> hooksPrefabList = new List<GameObject>();
    public List<GameObject> rocksPrefabList = new List<GameObject>();

    private Scrolling[] componentsList;
    [SerializeField]
    private GameObject spawnPosUp;
    [SerializeField]
    private GameObject spawnPosDown;


    public float speed = 1.0f;
    public float speedMultiplier = 0.01f;

    public float spawnRate = 8.0f;
    private float timer = 0.0f;
    private float timerTemp = 0.0f;
    private System.Random rnd;

    public bool begin = false;
    public void Start()
    {
        rnd = new System.Random();
    }

    IEnumerator SpawnBarrier(Vector3 _spawnPosUp, Vector3 _spawnPosDown, int _temp, int _temp1)
    {
        Instantiate(hooksPrefabList[_temp], _spawnPosUp, Quaternion.identity, this.transform);
        Instantiate(rocksPrefabList[_temp1], _spawnPosDown, Quaternion.identity, this.transform);

        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        componentsList = GetComponentsInChildren<Scrolling>();
        foreach (var item in componentsList)
        {
            item.speed = speed;
        }


        if (begin)
        {
            speed += Time.deltaTime * speedMultiplier;
            timer += Time.deltaTime;

            int temp = rnd.Next(0, hooksPrefabList.Count);
            int temp1 = rnd.Next(0, hooksPrefabList.Count);

            if (timer>timerTemp)
            {
                timerTemp += spawnRate;
                StartCoroutine(SpawnBarrier(spawnPosUp.transform.localPosition, spawnPosDown.transform.localPosition, temp, temp1));
            }                
        }
    }

    public void StartGame()
    {
        begin = !begin;
    }
}
