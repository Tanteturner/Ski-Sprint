using System.Collections.Generic;
using System.Linq;
using Tanteturner.Points;
using UnityEngine;
using UnityEngine.Pool;

namespace Tanteturner.Generation
{
    /// <summary>
    /// Class to generate and keep track of the map's obstacles and goals
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        private static MapGenerator _instance;

        public static MapGenerator Instance
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
    
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private Goal _goalPrefab;
    
        [SerializeField] private Transform _player;
    
        private float _lastHeight;
    
        private ObjectPool<Obstacle> _obstaclePool;
        [HideInInspector] public List<Obstacle> Obstacles = new();
        private List<Obstacle> _toBeRealeasedObstacles = new();
    
        private ObjectPool<Goal> _goalPool;
        [HideInInspector] public List<Goal> Goals = new();
        private List<Goal> _toBeRealeasedGoals = new();


        private void Awake()
        {
            _obstaclePool = new ObjectPool<Obstacle>(
                () => { return Instantiate(_obstaclePrefab, transform); },
                obstacle => { obstacle.gameObject.SetActive(true); Obstacles.Add(obstacle); },
                obstacle => { obstacle.gameObject.SetActive(false); Obstacles.Remove(obstacle); },
                obstacle => { Destroy(obstacle.gameObject); Obstacles.Remove(obstacle); },
                false,
                20,
                50
            );
        
            _goalPool = new ObjectPool<Goal>(
                () => { return Instantiate(_goalPrefab, transform); },
                goal => { goal.gameObject.SetActive(true); Goals.Add(goal); },
                goal => { goal.gameObject.SetActive(false); Goals.Remove(goal); },
                goal => { Destroy(goal.gameObject); Goals.Remove(goal); },
                false,
                4,
                10
            );
        
            Instance = this;
        }

        private void Update()
        {
            var height = Mathf.RoundToInt(_player.position.y);

            if (!(height < _lastHeight)) return;
            _lastHeight = height;
            PointsManager.Instance.Points++;

            if (height % 15 == 0)
                GenerateGoal(height - 10);
            else if (-height % 15 >= 3 && -height % 15 <= 12)
                GenerateObstacle(height - 10);
        }

    
        /// <summary>
        /// Generates a goal at the given height
        /// Automatically chooses a random position within the track
        /// </summary>
        /// <param name="height">The height the goal should be generated at</param>
        private void GenerateGoal(int height)
        {
            ReleaseUnusedGoals();
        
            var trackX = GetMapCoordinate(height);
            var goalPos = new Vector2(Random.Range(trackX - 1.5f, trackX + 1.5f), height);
            SetGoal(goalPos);
        }
        /// <summary>
        /// Spawns a goal at the given position
        /// </summary>
        /// <param name="pos">The position the goal should be spawned at</param>
        private void SetGoal(Vector2 pos)
        {
            var goal = _goalPool.Get();
            goal.transform.position = pos;
        }
        /// <summary>
        /// Releases all goals that are above the camera
        /// </summary>
        private void ReleaseUnusedGoals()
        {
            foreach (var goal in Goals.Where(goal => goal.transform.position.y > _player.position.y + 5))
            {
                _toBeRealeasedGoals.Add(goal);
            }

            foreach (var goal in _toBeRealeasedGoals)
            {
                _goalPool.Release(goal);
            }
            _toBeRealeasedGoals.Clear();   
        }
    
    
        /// <summary>
        /// Generates an obstacle at the given height
        /// Automatically chooses a random position within the track
        /// </summary>
        /// <param name="height">The height the obstacle should be generated at</param>
        private void GenerateObstacle(int height)
        { 
            ReleaseUnusedObstacles();

            if (Random.Range(0f, 1f) < 0.3f) return;
            var trackX = GetMapCoordinate(height);
            var ObsticlePos = new Vector2(Random.Range(trackX - 5f, trackX + 5f), height);
            SetObstacle(ObsticlePos);
        }
        /// <summary>
        /// Spawns an obstacle at the given position
        /// </summary>
        /// <param name="pos">The position the obstacle should be spawned at</param>
        private void SetObstacle(Vector2 pos)
        {
            var obstacle = _obstaclePool.Get();
            obstacle.transform.position = pos;
        }
        /// <summary>
        /// Releases all obstacles that are above the camera
        /// </summary>
        private void ReleaseUnusedObstacles()
        {
            foreach (var obstacle in Obstacles.Where(obstacle => obstacle.transform.position.y > _player.position.y + 5))
            {
                _toBeRealeasedObstacles.Add(obstacle);
            }

            foreach (var obstacle in _toBeRealeasedObstacles)
            {
                _obstaclePool.Release(obstacle);
            }
            _toBeRealeasedObstacles.Clear();
        }
    
    
        /// <summary>
        /// Sample map's x coordinate using the same function that is used in the shader
        /// </summary>
        /// <param name="y">y coordinate that the according x coordinate should be calculated for</param>
        /// <returns>x coordinate according to the input y coordinate</returns>
        public static float GetMapCoordinate(float y)
        {
            return (SampleSine(0.01f, 1f, y) +
                    SampleSine(0.1f, 0.2f, y) +
                    SampleSine(0.3f, -0.1f, y) +
                    SampleSine(0.02f, 1f, y) +
                    SampleSine(0.001f, 3f, y)) * 13.54f;
        }
    
        private static float SampleSine(float frequency, float amplitute, float coordinate)
        {
            return Mathf.Sin(coordinate * frequency) * amplitute;
        }
    }
}