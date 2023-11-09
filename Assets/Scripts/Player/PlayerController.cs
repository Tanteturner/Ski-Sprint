using Tanteturner.Generation;
using UnityEngine;

namespace Tanteturner.Player
{
    /// <summary>
    /// Class to manage the player's movement and steering
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Downward Movement")]
        public float Acceleration;
        public float MaxSpeed;
    
        [HideInInspector]
        public float Speed;
        private float _currentMaxSpeed;

        public float SpeedIncreasePerMeter;
    
        [Header("Steering")]
        public float steeringSpeed;
        private float _currentSteeringSpeed;

        private void Update()
        {
            transform.position += Input.GetAxis("Horizontal") * _currentSteeringSpeed * Time.deltaTime * Vector3.right;

            var trackX = MapGenerator.GetMapCoordinate(transform.position.y);
            var clampedX = Mathf.Clamp(transform.position.x, trackX - 5f, trackX + 5f);

            if (clampedX != transform.position.x)
            {
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
                Speed *= 1 - 0.25f * Time.deltaTime;
            }
            else
            {
                _currentMaxSpeed = MaxSpeed + SpeedIncreasePerMeter * -transform.position.y;
                _currentSteeringSpeed = (steeringSpeed + Speed / 2) * (0.5f + 0.5f * Speed / MaxSpeed);
            }
        
            Speed += Acceleration * Time.deltaTime;
            Speed = Mathf.Clamp(Speed, 0, _currentMaxSpeed);
        
            transform.position += Speed * Time.deltaTime * Vector3.down;
        }
    
    
    }
}
