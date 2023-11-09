using UnityEngine;

namespace Tanteturner.Points
{
    /// <summary>
    /// Class that is used on the popup point animation when crossing a goal
    /// </summary>
    public class Points : MonoBehaviour
    {
        public void Init(int points)
        {
            var animator = GetComponent<Animator>();
            animator.CrossFade("Points" + points, 0, 0);
        }
    }
}
