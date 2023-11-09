using System.Collections;
using Tanteturner.Points;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanteturner.UI
{
    /// <summary>
    /// Class to manage the Game Over UI
    /// </summary>
    public class GameOverUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _pointsNumber;
    
        [SerializeField] private TMP_Text _comboText;
        [SerializeField] private TMP_Text _comboNumber;

        [SerializeField] private GameObject _retryInput;
    
        [SerializeField] private AudioSource _impactSound;

        private void Awake()
        {
            _pointsNumber.text = PointsManager.Instance.Points.ToString();
            _comboNumber.text = PointsManager.Instance.HighestCombo.ToString();

            StartCoroutine(ShowResults());
        }
    
        private IEnumerator ShowResults()
        {
            yield return StartCoroutine(WaitForSecondsOrInput(1f,KeyCode.Space));
            _pointsText.gameObject.SetActive(true);
            _impactSound.Play();
        
            yield return StartCoroutine(WaitForSecondsOrInput(0.5f,KeyCode.Space));
            _pointsNumber.gameObject.SetActive(true);
            _impactSound.Play();
        
            yield return StartCoroutine(WaitForSecondsOrInput(0.8f,KeyCode.Space));
            _comboText.gameObject.SetActive(true);
            _impactSound.Play();
        
            yield return StartCoroutine(WaitForSecondsOrInput(0.5f,KeyCode.Space));
            _comboNumber.gameObject.SetActive(true);
            _impactSound.Play();
            yield return StartCoroutine(WaitForSecondsOrInput(0.25f,KeyCode.Space));
            _retryInput.SetActive(true);
        }
        

        /// <summary>
        /// Function to wait for a certain amount of time or a key input
        /// </summary>
        /// <param name="time">Time to wait for</param>
        /// <param name="keyCode">Key that breaks the wait time</param>
        private IEnumerator WaitForSecondsOrInput(float time, KeyCode keyCode)
        {
            var currentTime = 0f;
            while (currentTime < time && !Input.GetKeyDown(keyCode))
            {
                currentTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        private void Update()
        {
            if (_retryInput.activeSelf && Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(0);
        }
    }
}
