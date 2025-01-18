using System.Collections.Generic;
using UnityEngine;

public class HelixGenerator : MonoBehaviour
{
    [SerializeField] private GameObject baseRing;
    [SerializeField] private RingPool ringPool;
    [SerializeField] private int numberOfRingPlatforms;

    private List<GameObject> pooledObjects;
    bool generatedFirstHelix = false;

    private void Start()
    {
        pooledObjects = ringPool.GetPool();
        GenerateHelix();
    }

    private void GenerateHelix()
    {
        if (!generatedFirstHelix)
        {
            Vector3 pos = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, -180), 0);
            Instantiate(baseRing, pos, rotation);
            generatedFirstHelix = true;
        }

        if(generatedFirstHelix)
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

    private GameObject GetRandomPlatform()
    {
        if(pooledObjects.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, pooledObjects.Count);
            GameObject platform = pooledObjects[randomIndex];
            pooledObjects.RemoveAt(randomIndex);
            return platform;
        }
        return null;
    }
}
