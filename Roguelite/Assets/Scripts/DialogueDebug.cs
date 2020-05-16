using UnityEngine;

public class DialogueDebug : MonoBehaviour
{
    private NpcEngine engine;

    private void Awake()
    {
        engine = GetComponent<NpcEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("FFFF");
            engine.StartDialogue(0);
        }
    }
}
