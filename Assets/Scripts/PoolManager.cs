using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public PoolObject[] objectPrefab;
    public List<PoolObject> objectList;
    public List<GameObject> pooledObjects;

    private GameInputManager player;
    PoolObjectParams par = new PoolObjectParams();

    void Start()
    {
        //objectList = new List<PoolObject>();
        //AddObject(new PoolObjectParams() );
        pooledObjects = new List<GameObject>();

        player = GameObject.FindObjectOfType<GameInputManager>();

        foreach (PoolObject item in objectList)
        {
            for(int i = 0; i < item.amountToPool; i++)
            {
                   GameObject obj = (GameObject)Instantiate(item.gameObject);
                   obj.SetActive(false);
                   pooledObjects.Add(obj);
            }
        }

        StartCoroutine(TimerToSpawn(par));
    }

    private void Update()
    {
        
    }

    public void AddObject(PoolObjectParams parameters)
    {
        int ran = Random.Range(0,objectList.Count);
        PoolObject enemy= objectList[ran];
        GameObject enemyObj = enemy.gameObject;

        if(!enemyObj.activeInHierarchy)
        {
            objectList[ran].gameObject.SetActive(true);
            enemy.InitializePoolObject(parameters);
        }
    }

    public void StoreObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    IEnumerator TimerToSpawn(PoolObjectParams par)
    {
        yield return new WaitForSeconds(3);
        AddObject(par);
        int ran = Random.Range(0, objectPrefab.Length-1);
        EnemyMovement enemy = Instantiate(objectPrefab[ran]).GetComponent<EnemyMovement>();
        enemy.InitializePoolObject(par);
        if(player.transform.position.x > 0)
        {
            par.startingPosition = new Vector3(-10, -1, 0);
        }
        else
        {
            par.startingPosition = new Vector3(10, -1, 0);
            enemy.speed = Mathf.Abs(enemy.speed);
        }
        StartCoroutine(TimerToSpawn(par));
    }

}


public class PoolObjectParams
{
    public Vector3 startingPosition;
}
