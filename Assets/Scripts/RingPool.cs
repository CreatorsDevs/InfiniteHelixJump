using System.Collections.Generic;
using UnityEngine;

public class RingPool : MonoBehaviour
{
    [Header("Ring Model Prefabs Pool")]
    [SerializeField] private List<GameObject> ringPlatformPrefabs;
    [SerializeField] private Transform parentTransform;
    private const int copiesPerPrefab = 3;

    private List<GameObject> pool;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();

        foreach (GameObject prefab in ringPlatformPrefabs)
        {
            for(int i = 0; i < copiesPerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab, parentTransform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public List<GameObject> GetPool()
    {
        return pool;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }
}
