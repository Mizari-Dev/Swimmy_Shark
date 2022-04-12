using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBehavior : MonoBehaviour
{
    //List ou on met les prefabs des obstacles on va venir piocher au hasard dedans
    public List<GameObject> hooksPrefabList = new List<GameObject>();
    public List<GameObject> rocksPrefabList = new List<GameObject>();
    public GameObject scoreCollision;


    private Scrolling[] componentsList;
    [SerializeField]
    private GameObject spawnPosUp;
    [SerializeField]
    private GameObject spawnPosDown;


    public float speed = 1.0f;
    public float speedMultiplier = 0.01f;
    public float spawnMultiplier = 0.01f;

    public float spawnRate = 8.0f;
    private float timer = 0.0f;
    private float timerTemp = 0.0f;
    private System.Random rnd;

    public bool begin = false;
    public void Start()
    {
        rnd = new System.Random();
    }


    // Initialisation de la coroutine

    /// <summary>
    /// On lui donne les points de spawn des obstacles, et les indexs prit aleatoirement
    /// </summary>
    /// <param name="_spawnPosUp"></param>
    /// <param name="_spawnPosDown"></param>
    /// <param name="_indexHookList"></param>
    /// <param name="_indexRockList"></param>
    /// <returns></returns>
    IEnumerator SpawnBarrier(Vector3 _spawnPosUp, Vector3 _spawnPosDown, int _indexHookList, int _indexRockList)
    {
        //translate les points de position des spawnPos sur y et y
        Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2));

        
        GameObject hook = Instantiate(hooksPrefabList[_indexHookList], _spawnPosUp, Quaternion.identity, this.transform);
        GameObject rock = Instantiate(rocksPrefabList[_indexRockList], _spawnPosDown, Quaternion.identity, this.transform);
        GameObject scoring = Instantiate(scoreCollision, _spawnPosDown, Quaternion.identity, this.transform);

        Vector2 posHook = hook.transform.position;
        Vector2 posRock = rock.transform.position;
        Vector2 posScoring = scoring.transform.position;

        //Donne la nouvelle position
        posHook += spawnPos;
        posRock += spawnPos;
        posScoring += spawnPos;
        posScoring.y = 0;

        hook.transform.position = posHook;
        rock.transform.position = posRock;
        scoring.transform.position = posScoring;

        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        // Va chercher dans chacun des obstacles la variable speed et la mets à jour
        componentsList = GetComponentsInChildren<Scrolling>();
        foreach (var item in componentsList)
        {
            item.speed = speed;
        }

        // début du jeu
        if (begin)
        {
            speed += Time.deltaTime * speedMultiplier;
            if(spawnRate > 2.0f) spawnRate -= Time.deltaTime * speedMultiplier;
            timer += Time.deltaTime;

            int temp = rnd.Next(0, hooksPrefabList.Count);
            int temp1 = rnd.Next(0, hooksPrefabList.Count);

            //Gere les intervales de spawn des obstacles
            if (timer>timerTemp)
            {
                timerTemp += spawnRate;
                StartCoroutine(SpawnBarrier(spawnPosUp.transform.localPosition, spawnPosDown.transform.localPosition, temp, temp1));
            }                
        }
    }

    //lance ou interrompt le jeu
    public void StartGame()
    {
        begin = !begin;
    }
}
