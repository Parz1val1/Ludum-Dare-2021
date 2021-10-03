using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Environment;

namespace GameSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private CameraManager _cameraManager;

        [SerializeField] private LevelChunk _levelChunkPrevious;
        [SerializeField] private LevelChunk _levelChunkCurrent;
        [SerializeField] private LevelChunk _levelChunkNext;

        [SerializeField] private GameObject[] _levelChunkCollection;

        private void Start()
        {
            if (_cameraManager == null)
            {
                Debug.LogError("No camera manager assigned.", this.gameObject);

                return;
            }
            else if (_levelChunkPrevious == null)
            {
                Debug.LogError("No previous level chunk manager assigned.", this.gameObject);

                return;
            }
            else if (_levelChunkCurrent == null)
            {
                Debug.LogError("No current level chunk assigned.", this.gameObject);

                return;
            }
            else if (_levelChunkNext == null)
            {
                Debug.LogError("No next level chunk assigned.", this.gameObject);

                return;
            }
        }

        private void Update()
        {
            if (_levelChunkPrevious.WithinLevelBounds() && !_levelChunkCurrent.WithinLevelBounds())
            {
                _levelChunkNext.DestroyLevel();

                _levelChunkNext = _levelChunkCurrent;
                _levelChunkCurrent = _levelChunkPrevious;

                GameObject newLevelChunk = Instantiate(_levelChunkCollection[Random.Range(0, _levelChunkCollection.Length)], _cameraManager._backSpawnPoint.transform.position, Quaternion.identity);

                _levelChunkPrevious = newLevelChunk.GetComponent<LevelChunk>();
            }
            else if (_levelChunkNext.WithinLevelBounds() && !_levelChunkCurrent.WithinLevelBounds())
            {
                _levelChunkPrevious.DestroyLevel();

                _levelChunkPrevious = _levelChunkCurrent;
                _levelChunkCurrent = _levelChunkNext;

                GameObject newLevelChunk = Instantiate(_levelChunkCollection[Random.Range(0, _levelChunkCollection.Length)], _cameraManager._frontSpawnPoint.transform.position, Quaternion.identity);

                _levelChunkNext = newLevelChunk.GetComponent<LevelChunk>();
            }
        }
    }
}
