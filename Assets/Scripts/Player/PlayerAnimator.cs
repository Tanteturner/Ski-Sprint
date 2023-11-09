using UnityEngine;

namespace Tanteturner.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        private PlayerController _playerController;
    
        [SerializeField]
        private AnimationCurve _speedToAnimationSpeed;

        [SerializeField] private Transform _gfx;
    
        void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }
    
        void Update()
        {
            _animator.SetFloat("Speed", _speedToAnimationSpeed.Evaluate(_playerController.Speed));

            _gfx.eulerAngles = new Vector3(0, 0, Input.GetAxis("Horizontal") * 15f);
        }
    }
}
