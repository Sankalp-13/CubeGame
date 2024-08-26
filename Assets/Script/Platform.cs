using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform1; 
    public GameObject platform2Prefab;  
    public float offset = 5.0f;  
    public float spawnDelay = 2.5f;  
    public float minDestroyTime = 14.0f;
    public float maxDestroyTime = 15.0f;
    public int platform1LastDirection=0;

    void Start()
    {
        if (JsonFetcher.Instance != null)
        {
            JsonFetcher.Instance.DataLoaded += OnDataLoaded;

            Destroy(platform1, Random.Range(minDestroyTime, maxDestroyTime));
            StartCoroutine(SpawnPlatform2WithDelay());
        }
        else
        {
            Debug.LogError("JsonFetcher instance is null.");
        }
    }

    private void OnDataLoaded()
    {

        maxDestroyTime = JsonFetcher.Instance.gameData.pulpit_data.max_pulpit_destroy_time;
        minDestroyTime = JsonFetcher.Instance.gameData.pulpit_data.min_pulpit_destroy_time;
        spawnDelay = JsonFetcher.Instance.gameData.pulpit_data.pulpit_spawn_time;
        

        Debug.Log("max des: " + maxDestroyTime);
        Debug.Log("min des: " + minDestroyTime);
        Debug.Log("spawn : " + spawnDelay);
    }

    IEnumerator SpawnPlatform2WithDelay()
    {

        yield return new WaitForSeconds(spawnDelay);


        SpawnPlatform2();
    }

    void SpawnPlatform2()
    {
        if (platform1 != null && platform2Prefab != null)
        {

            Vector3 platform1Position = platform1.transform.position;

            Vector3[] nextSpawnOffset = new Vector3[]
    {
        new Vector3(offset, 0, 0),  // Right
        new Vector3(-offset, 0, 0),  // Left
        new Vector3(0, 0, offset),  // Forward
        new Vector3(0, 0, -offset)  // Backward
    };

            int r ;
            int lastDir = platform1.GetComponent<PlatformSpawner>().platform1LastDirection;
            while(true)
            {
                r = Random.Range(0, 4);
                if ((r == 0 && lastDir != 1) || (r == 1 && lastDir != 0) || (r == 2 && lastDir != 3) || (r == 3 && lastDir != 2))
                {
                    break;
                }
            }
           // Debug.Log(r);

            Vector3 platform2Position = platform1Position + nextSpawnOffset[r];


            GameObject platform2 = Instantiate(platform2Prefab, platform2Position, Quaternion.identity) as GameObject;
            platform2.GetComponent<PlatformSpawner>().platform1LastDirection = r;


        }
        else
        {
            Debug.LogError("Platform1 or Platform2Prefab is not assigned.");
        }
    }
}
