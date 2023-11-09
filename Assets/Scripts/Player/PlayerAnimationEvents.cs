using UnityEngine;

namespace Tanteturner.Player
{
    /// <summary>
    /// Class to manage events called from the player's animation
    /// </summary>
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _pushParticles;
        [SerializeField] private AudioSource _pushSound;
    
        public void Push()
        {
            _pushParticles.Play();
            _pushSound.Play();
        }
    }
}
