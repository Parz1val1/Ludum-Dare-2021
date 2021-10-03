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

        private void Start()
        {
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
            Destroy(this.gameObject);
        }
    }
}
