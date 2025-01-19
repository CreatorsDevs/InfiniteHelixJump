using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixGenerator : MonoBehaviour
{
    [SerializeField] private RingPool ringPool;
    [SerializeField] private int numberOfRingPlatforms;

    private List<GameObject> pooledObjects;
    private bool generatedFirstHelix = false;
    private int randomIndex;

    IEnumerator Start()
    {
        pooledObjects = ringPool.GetPool();
        yield return null;
        if(!generatedFirstHelix && pooledObjects != null)
            GenerateFirstHelix();
        GenerateHelix();
    }

    private void GenerateFirstHelix()
    {
        GameObject baseRing = GetBaseRingFromPool();
        if (baseRing != null)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(-25, -180), 0);
            baseRing.transform.SetLocalPositionAndRotation(pos, rotation);
            baseRing.SetActive(true);
            generatedFirstHelix = true;
        }
    }
    private void GenerateHelix()
    {
        if (generatedFirstHelix)
        {
            for (int i = 1; i < numberOfRingPlatforms; i++)
            {
                GameObject selectedRingPlatform = GetRandomPlatform();

                if (selectedRingPlatform != null)
                {
                    selectedRingPlatform.transform.SetPositionAndRotation(new Vector3(0, -i * (selectedRingPlatform.transform.localScale.y * 2), 0), Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
                    selectedRingPlatform.SetActive(true);
                }
            }
        }
    }

    private GameObject GetBaseRingFromPool()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy && obj.CompareTag("BaseRing"))
                return obj;
        }
        return null;
    }

    private GameObject GetRandomPlatform()
    {
        if(pooledObjects.Count > 0)
        {
            for(int i=0; i<pooledObjects.Count; i++)
            {
                randomIndex = UnityEngine.Random.Range(0, pooledObjects.Count);
                if (!pooledObjects[randomIndex].activeInHierarchy)
                {
                    GameObject platform = pooledObjects[randomIndex];
                    pooledObjects.RemoveAt(randomIndex);
                    return platform;
                }
            }
        }
        return null;
    }
}
