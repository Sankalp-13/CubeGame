using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class PlayerData
{
    public int speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}
