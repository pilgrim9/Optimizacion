using System.Collections.Generic;
using UnityEngine;

public class BoxPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialPoolSize = 10;
    private Queue<GameObject> pool;
    
    void Start()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Spawn()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            obj.transform.position = transform.position;
            pool.Enqueue(obj);
            return obj;
        }

        GameObject newObj = Instantiate(prefab);
        pool.Enqueue(newObj);
        return newObj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Replace with your spawn button
        {
            Spawn();
        }

        if (Input.GetKeyDown(KeyCode.R)) // Replace with your despawn button
        {
            foreach (var obj in pool)
            {
                if (obj.activeInHierarchy)
                {
                    Despawn(obj);
                    break;
                }
            }
        }
    }
}

