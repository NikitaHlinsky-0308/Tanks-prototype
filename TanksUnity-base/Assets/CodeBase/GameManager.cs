using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class GameManager : MonoBehaviour
    {

        private void OnPlayerDead()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}