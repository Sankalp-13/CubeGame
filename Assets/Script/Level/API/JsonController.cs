using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class JsonFetcher : MonoBehaviour
{
    public static JsonFetcher Instance { get; private set; }

    public GameData gameData;
    public bool loaded = false;
    private string jsonUrl = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";

    public delegate void OnDataLoaded();
    public event OnDataLoaded DataLoaded;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FetchJsonData());
    }

    IEnumerator FetchJsonData()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching JSON data: " + request.error);
        }
        else
        {

            string json = request.downloadHandler.text;
            gameData = JsonUtility.FromJson<GameData>(json);

            Debug.Log("Player Speed: " + gameData.player_data.speed);

            loaded = true;
            DataLoaded?.Invoke();
        }
    }
}
