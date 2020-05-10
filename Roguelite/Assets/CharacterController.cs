using UnityEngine;

internal sealed class CharacterController : MonoBehaviour
{
    private Rigidbody m_RigidBody;

    [SerializeField] private float m_ForwardSpeed = 5f;
    [SerializeField] private float m_BackwardSpeed = 3f;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var forward = 0;
        var side = 0;

        if (Input.GetKey(KeyCode.W))
        {
            forward += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            side -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            side += 1;
        }

        var movementSpeed = m_ForwardSpeed;
        if (forward < 0)
        {
            movementSpeed = m_BackwardSpeed;
        }

        m_RigidBody.velocity = transform.InverseTransformDirection(new Vector3(side, 0, forward) * movementSpeed);
    }
}
