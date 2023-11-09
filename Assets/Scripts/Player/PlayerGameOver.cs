using System.Collections;
using Tanteturner.UI;
using UnityEngine;

namespace Tanteturner.Player
{
    /// <summary>
    /// Class to handle the player's game over
    /// </summary>
    public class PlayerGameOver : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverParticles;
    
        private bool _isGameOver;
    
        public void GameOver()
        {
            if(_isGameOver) return;
            _isGameOver = true;
            StartCoroutine(GameOverCoroutine());
        }
    
        private IEnumerator GameOverCoroutine()
        {
            Time.timeScale = 0;
            Instantiate(_gameOverParticles, transform);
            yield return new WaitForSecondsRealtime(2);
            UIManager.Instance.OnGameOver();
        }
    }
}
