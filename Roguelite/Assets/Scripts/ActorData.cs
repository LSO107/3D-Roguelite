using UnityEngine;

internal sealed class ActorData : MonoBehaviour
{
    public ActorType ActorType;
}

public enum ActorType
{
    Player,
    Enemy
}
