using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelChunk : MonoBehaviour
    {
        private Collider2D _collider;
        private Camera _mainCamera;
        private Plane[] _cameraPlanes;
        [SerializeField]
        private GameObject[] myLevelParts;

        [SerializeField] private GameObject _frontSpawnPoint;
        [SerializeField] private GameObject _backSpawnPoint;

        public Vector3 _frontSpawnPos => _frontSpawnPoint.transform.position;
        public Vector3 _backSpawnPos => _backSpawnPoint.transform.position;

        private void Start()
        {
            if (_frontSpawnPoint == null)
            {
                Debug.LogError("You did not assign a front spawn point for this object", this.gameObject);
            }
            if (_backSpawnPoint == null)
            {
                Debug.LogError("You did not assign a back spawn point for this object", this.gameObject);
            }

            _collider = this.GetComponent<Collider2D>();
            _mainCamera = Camera.main;
        }

        public bool WithinLevelBounds()
        {
            _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(_mainCamera);

            if (GeometryUtility.TestPlanesAABB(_cameraPlanes, _collider.bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DestroyLevel()
        {
            for(int i = 0; i < myLevelParts.Length; i++)
            {
                Destroy(myLevelParts[i]);
            }
            Destroy(this.gameObject);
        }
    }
}
