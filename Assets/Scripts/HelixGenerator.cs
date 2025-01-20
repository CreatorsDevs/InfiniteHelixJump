using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixGenerator : MonoBehaviour
{
    [SerializeField] private RingPool ringPool;
    [SerializeField] private int numberOfRingPlatforms;
    [SerializeField] private Transform playerTransform;
    
    private const float platformHeight = 2f;
    private List<GameObject> pooledObjects;
    private List<GameObject> activeObjects = new();
    private bool generatedFirstHelix = false;

    private IEnumerator Start()
    {
        pooledObjects = ringPool.GetPool();
        yield return null;
        if(!generatedFirstHelix && pooledObjects != null)
            GenerateFirstHelix();
        GenerateHelix();
    }

    private void Update()
    {
        if(GameManager.Instance.GameStarted)
        {
            RecyclePlatforms();
        }
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
            activeObjects.Add(baseRing);
        }
    }
    private void GenerateHelix()
    {
        if (generatedFirstHelix)
        {
            for (int i = 1; i < numberOfRingPlatforms; i++)
            {
                float yPosition = -i * platformHeight;
                AddPlatformAt(yPosition);
            }
        }
    }

    private void AddPlatformAt(float yPosition)
    {
        GameObject selectedRingPlatform = GetRandomPlatform();

        if (selectedRingPlatform != null)
        {
            selectedRingPlatform.transform.SetPositionAndRotation(new Vector3(0, yPosition, 0), Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
            selectedRingPlatform.SetActive(true);
            activeObjects.Add(selectedRingPlatform);
        }
    }

    private void RecyclePlatforms()
    {
        if (activeObjects.Count == 0) return;

        GameObject topPlatform = activeObjects[0];
        if(playerTransform.position.y < topPlatform.transform.position.y - platformHeight * 1.5f)
        {
            topPlatform.SetActive(false);
            ringPool.ReturnToPool(topPlatform);
            AudioManager.Instance.Play("Fall");
            activeObjects.RemoveAt(0);

            float newYposition = activeObjects[activeObjects.Count - 1].transform.position.y - platformHeight;
            AddPlatformAt(newYposition);
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
                int randomIndex = UnityEngine.Random.Range(0, pooledObjects.Count);
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
