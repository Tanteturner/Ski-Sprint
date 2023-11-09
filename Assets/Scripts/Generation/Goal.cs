using UnityEngine;

namespace Tanteturner.Generation
{
    /// <summary>
    /// Class to keep track of whether the player has passed a goal or not
    /// </summary>
    public class Goal : MonoBehaviour
    {
        [HideInInspector] public bool WasPassed;

        private void OnEnable()
        {
            WasPassed = false;
        }
    }
}
