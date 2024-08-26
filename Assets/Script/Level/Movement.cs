using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Start()
    {
        // Subscribe to the DataLoaded event
        if (JsonFetcher.Instance != null)
        {
            JsonFetcher.Instance.DataLoaded += OnDataLoaded;
        }
        else
        {
            Debug.LogError("JsonFetcher instance is null.");
        }
    }

    private void OnDataLoaded()
    {
        // Set the speed after the data is loaded
        moveSpeed = JsonFetcher.Instance.gameData.player_data.speed;
        Debug.Log("Speed loaded: " + moveSpeed);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime*1.3f;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime * 1.3f;

        transform.Translate(new Vector3(moveX, 0f, moveZ));
    }
}
