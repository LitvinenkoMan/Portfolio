using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerScriptableObject", order = 1)]
public class PlayerScriptableObject : ScriptableObject
{
    public string PrefubName;
    public string PlayerName;
    public string RoomName;
    public bool IsRoomCreator;
}
