using UnityEngine;

namespace Tanteturner.Util
{
    /// <summary>
    /// Class that can be used by a UnityEvent or an animation event to delete the game object
    /// </summary>
    public class DeleteOnEvent : MonoBehaviour
    {
        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}
