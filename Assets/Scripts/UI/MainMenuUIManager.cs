using UnityEngine;

namespace Tanteturner.UI
{
    /// <summary>
    /// Class to manage the Main Menu UI
    /// </summary>
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _creditsScreen;
    
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_creditsScreen.activeSelf)
            {
                StartGame();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _creditsScreen.SetActive(!_creditsScreen.activeSelf);
            }
        }

        private void StartGame()
        {
            _creditsScreen.SetActive(false);
            Time.timeScale = 1;
            enabled = false;
        }
    }
}
