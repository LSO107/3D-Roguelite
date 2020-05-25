using UnityEngine;

internal sealed class ActorData : MonoBehaviour
{
    public ActorType ActorType = 0;
}

public enum ActorType
{
    Player,
    Enemy
}
