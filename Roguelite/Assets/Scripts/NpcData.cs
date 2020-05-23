using UnityEngine;

internal sealed class NpcData : MonoBehaviour
{
    public Vector3 StartLocation { get; private set; }
    public Vector3 HiddenLocation { get; private set; }
    public Vector3 CurrentLocation => transform.position;

    private void Start()
    {
        StartLocation = transform.position;
        HiddenLocation = new Vector3(1000, 1000, 1000);
    }
}
