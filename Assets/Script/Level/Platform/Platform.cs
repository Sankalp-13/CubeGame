using UnityEngine;
using System.Collections;
using TMPro;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2Prefab;
    public float offset = 5.0f;
    public float spawnDelay = 2.5f;
    public float minDestroyTime = 14.0f;
    public float maxDestroyTime = 15.0f;
    public int platform1LastDirection = 0;
    public float spawnDuration = 4.0f;
    private float elapsedTime = 0f;
    private TextMeshPro timerText;

    void Start()
    {
        if (JsonFetcher.Instance != null)
        {
            JsonFetcher.Instance.DataLoaded += OnDataLoaded;

            if (JsonFetcher.Instance.loaded)
            {

                spawnDuration = Random.Range(minDestroyTime, maxDestroyTime);
                elapsedTime = spawnDuration;
                Destroy(platform1, spawnDuration);
                StartCoroutine(SpawnPlatform2WithDelay());
                timerText = GetComponentInChildren<TextMeshPro>();
            }
        }
        else
        {
            Debug.LogError("JsonFetcher instance is null.");
        }
    }

    void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = elapsedTime.ToString("0.00");
        }
    }

    private void OnDataLoaded()
    {

        maxDestroyTime = JsonFetcher.Instance.gameData.pulpit_data.max_pulpit_destroy_time;
        minDestroyTime = JsonFetcher.Instance.gameData.pulpit_data.min_pulpit_destroy_time;
        spawnDelay = JsonFetcher.Instance.gameData.pulpit_data.pulpit_spawn_time;
        spawnDuration = Random.Range(minDestroyTime, maxDestroyTime);
        elapsedTime = spawnDuration;
        Destroy(platform1, spawnDuration);
        StartCoroutine(SpawnPlatform2WithDelay());
        timerText = GetComponentInChildren<TextMeshPro>();

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

            int r;
            int lastDir = platform1.GetComponent<PlatformSpawner>().platform1LastDirection;
            while (true)
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
            StartCoroutine(ScalePlatform(platform2, 1.0f));
            platform2.GetComponent<PlatformSpawner>().platform1LastDirection = r;


        }
        else
        {
            Debug.LogError("Platform1 or Platform2Prefab is not assigned.");
        }
    }


    IEnumerator ScalePlatform(GameObject platform, float duration)
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 finalScale = Vector3.one * 0.8f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            platform.transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        platform.transform.localScale = finalScale;
    }
}