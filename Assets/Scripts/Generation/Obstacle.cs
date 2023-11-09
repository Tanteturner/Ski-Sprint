using System.Collections.Generic;
using UnityEngine;

namespace Tanteturner.Generation
{
    /// <summary>
    /// Class to manage the obstacle's appearance
    /// </summary>
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

    
        private void OnEnable()
        {
            //Choose a random sprite and apply it to the sprite renderer
            _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
        }
    }
}
