using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager _instance;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnPlayerDead()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}