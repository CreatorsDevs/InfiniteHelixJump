using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HelixGenerator : MonoBehaviour
{
    [SerializeField] private RingPool ringPool;
    [SerializeField] private int numberOfRingPlatforms;

    private List<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = ringPool.GetPool();
        GenerateHelix();
    }

    private void GenerateHelix()
    {
        for (int i = 0; i < numberOfRingPlatforms; i++)
        {
            GameObject selectedRingPlatform = GetRandomPlatform();

            if (selectedRingPlatform != null)
            {
                selectedRingPlatform.transform.SetPositionAndRotation(new Vector3(0, -i * (selectedRingPlatform.transform.localScale.y * 2), 0), Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
                selectedRingPlatform.SetActive(true);
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
