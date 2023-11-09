using System.Linq;
using Tanteturner.Generation;
using Tanteturner.Points;
using UnityEngine;

namespace Tanteturner.Player
{
    /// <summary>
    /// Class to manage the player's collisions with obstacles and goals
    /// </summary>
    [RequireComponent(typeof(PlayerGameOver))]
    public class PlayerCollider : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _goalParticles;
        [SerializeField] private AudioSource _goalSound;
        [SerializeField] private AudioSource _goalMissSound;
        [SerializeField] private Points.Points _Points;

        private Vector2 _goalPos;
    
        private void Update()
        {
            CheckObstacleCollision();
            CheckGoalCollision();
        }

        private void CheckObstacleCollision()
        {
            foreach (var obstacle in MapGenerator.Instance.Obstacles.Where(obstacle => !(Mathf.Abs(obstacle.transform.position.y - transform.position.y) > 2f)))
            {
                if(Vector2.Distance(obstacle.transform.position, transform.position) < 0.5f)
                    GetComponent<PlayerGameOver>().GameOver();
            }
        }

        private void CheckGoalCollision()
        {
            foreach (var goal in MapGenerator.Instance.Goals.Where(goal => !goal.WasPassed))
            {
                _goalPos = goal.transform.position;
                if (_goalPos.y - 0.5f > transform.position.y)
                {
                    OnMissedGoal();
                    goal.WasPassed = true;
                    continue;
                }
                if(Mathf.Abs(_goalPos.y - transform.position.y) > 0.5f) continue;
                if(_goalPos.x + 1 < transform.position.x || _goalPos.x - 1 > transform.position.x) continue;
                goal.WasPassed = true;
                OnGoal(goal);
            }
        }

        private void OnGoal(Goal goal)
        {
            _goalParticles.Play();
            _goalSound.Play();
            var points = Instantiate(_Points,goal.transform);
            points.Init(PointsManager.Instance.NextComboPoints);
            PointsManager.Instance.OnGoal();
        }
    
        private void OnMissedGoal()
        {
            if(PointsManager.Instance.Combo == 0) return;
            PointsManager.Instance.ResetCombo();
            _goalMissSound.Play();
        }
    }
}
