using UnityEngine;

namespace Tanteturner.Points
{
    /// <summary>
    /// Class to keep track of the player's points and combo
    /// </summary>
    public class PointsManager : MonoBehaviour
    {
        private static PointsManager _instance;
        public static PointsManager Instance
        {
            get => _instance;
            set
            {
                if (_instance == null)
                    _instance = value;
                else
                    Destroy(value.gameObject);
            }
        }
    
        private void Awake()
        {
            Instance = this;
        }

        [HideInInspector] public int Combo;

        [HideInInspector] public int HighestCombo;

        public int Points;
        public int NextComboPoints => Combo + 1 >= 10? 100 : (Combo + 1) * 10;

        public void OnGoal()
        {
            Points += NextComboPoints;
            Combo++;
            
            if(Combo > HighestCombo)
                HighestCombo = Combo;
        }

        public void ResetCombo()
        {
            Combo = 0;
        }
    }
}
