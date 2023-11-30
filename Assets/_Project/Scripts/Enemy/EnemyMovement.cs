using System.Collections.Generic;
using Aoiti.Pathfinding;
using DG.Tweening;
using gameoff.Core;
using UnityEngine;

namespace gameoff.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviourWithMovementEffector
    {
        [SerializeField] private float gridSize = 0.5f;

        [SerializeField] private LayerMask obstaclesMask;
        [SerializeField] private bool drawDebugLines;

        [SerializeField] private ParticleSystem movementVFX;


        private Pathfinder<Vector2> _pathfinder;
        private List<Vector2> _path = new();
        private List<Vector2> _pathLeftToGo = new();

        public float MoveSpeed { get; private set; }

        public Rigidbody2D Rigidbody => _rb;

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private EnemyAnimationsHandler _animationsHandler;

        private void Awake()
        {
            _animationsHandler = GetComponent<EnemyAnimationsHandler>();
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);
        }

        private void FixedUpdate()
        {
            if (!IsDefaultMovementEnabled)
            {
                movementVFX.Stop();
                return;
            }

            if (_pathLeftToGo.Count > 0)
                HandleMovementToTarget();
            else
                Stop();
        }

        private void Update()
        {
            if (drawDebugLines)
                DrawDebugLines();

            _animationsHandler.HandleMovementAnimation(() => _pathLeftToGo.Count > 0);
        }

        public void SetDestination(Vector2 target)
        {
            Vector2 closestNode = GetClosestNode(transform.position);
            if (_pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out _path))
            {
                // if (_path.Count > 0)
                // {
                //     _pathLeftToGo = ShortenPath(_path);
                // }
                //else
                //{
                _pathLeftToGo = new List<Vector2>(_path);
                _pathLeftToGo.Add(target);
                //}
            }
        }

        public void Stop()
        {
            _pathLeftToGo.Clear();
            Rigidbody.velocity = Vector3.zero;
            movementVFX.Stop();
        }

        public void ChangeMoveSpeed(float newSpeed)
        {
            MoveSpeed = newSpeed;
        }

        public void FlipTowardsPosition(Vector2 position)
        {
            var dir = (Vector3) position - transform.position;
            _spriteRenderer.flipX = dir.x > 0;
        }

        private void HandleMovementToTarget()
        {
            var dir = (Vector3) _pathLeftToGo[0] - transform.position;
            FlipTowardsPosition(_pathLeftToGo[0]);
            Rigidbody.velocity = dir.normalized * (MoveSpeed * Time.deltaTime);
            if (((Vector2) transform.position - _pathLeftToGo[0]).sqrMagnitude <
                MoveSpeed * MoveSpeed * Time.deltaTime * Time.deltaTime)
            {
                //transform.position = _pathLeftToGo[0];
                _pathLeftToGo.RemoveAt(0);
            }

            if (!movementVFX.isPlaying)
                movementVFX.Play();
        }

        private void DrawDebugLines()
        {
            for (int i = 0; i < _pathLeftToGo.Count - 1; i++) //visualize your path in the sceneview
                Debug.DrawLine(_pathLeftToGo[i], _pathLeftToGo[i + 1]);
        }


        private Dictionary<Vector2, float> GetNeighbourNodes(Vector2 pos)
        {
            Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;

                    Vector2 dir = new Vector2(i, j) * gridSize;
                    if (!Physics2D.Linecast(pos, pos + dir, obstaclesMask))
                        neighbours.Add(GetClosestNode(pos + dir), dir.magnitude);
                }
            }

            return neighbours;
        }

        Vector2 GetClosestNode(Vector2 target)
        {
            return new Vector2(Mathf.Round(target.x / gridSize) * gridSize,
                Mathf.Round(target.y / gridSize) * gridSize);
        }

        private float GetDistance(Vector2 a, Vector2 b)
        {
            return (a - b).sqrMagnitude;
        }

        private List<Vector2> ShortenPath(List<Vector2> path)
        {
            List<Vector2> newPath = new List<Vector2>();

            for (int i = 0; i < path.Count; i++)
            {
                newPath.Add(path[i]);
                for (int j = path.Count - 1; j > i; j--)
                {
                    if (!Physics2D.Linecast(path[i], path[j], obstaclesMask))
                    {
                        i = j;
                        break;
                    }
                }

                newPath.Add(path[i]);
            }

            newPath.Add(path[^1]);
            return newPath;
        }
    }
}