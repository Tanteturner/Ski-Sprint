using Tanteturner.Points;
using TMPro;
using UnityEngine;

namespace Tanteturner.UI
{
    /// <summary>
    /// Class to manage all of the UI in the game
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

        public static UIManager Instance
        {
            get => _instance;
            private set
            {
                if (_instance == null)
                    _instance = value;
                else
                    Destroy(value.gameObject);
            }
        }

        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _comboText;
    
        [SerializeField] private GameObject _inGameUI;
        [SerializeField] private GameObject _gameOverUI;

        private float _displayPoints;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _displayPoints = Mathf.Lerp(_displayPoints, PointsManager.Instance.Points, Time.unscaledDeltaTime * 5);
        
            _pointsText.text = Mathf.CeilToInt(_displayPoints).ToString();
            _comboText.text = PointsManager.Instance.Combo.ToString();
        }
    
        public void OnGameOver()
        {
            _inGameUI.SetActive(false);
            _gameOverUI.SetActive(true);
        }
    }
}
