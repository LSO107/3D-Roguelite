using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    internal sealed class LoginManager : MonoBehaviour
    {
        public void Login()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
