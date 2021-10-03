using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Environment;

namespace GameSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelChunk _levelChunkPrevious;
        [SerializeField] private LevelChunk _levelChunkCurrent;
        [SerializeField] private LevelChunk _levelChunkNext;

        [SerializeField] private GameObject[] _levelChunkCollection;

        private void Start()
        {
            if (_levelChunkPrevious == null)
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
                GameObject newLevelChunk = Instantiate(_levelChunkCollection[Random.Range(0, _levelChunkCollection.Length)], _levelChunkNext.GetComponent<LevelChunk>()._frontSpawnPos, Quaternion.identity);

                Vector3 _newChunkSpawnDifference = newLevelChunk.GetComponent<LevelChunk>()._frontSpawnPos - newLevelChunk.transform.position;
                Vector3 _spawnPoint = _levelChunkPrevious.GetComponent<LevelChunk>()._backSpawnPos - _newChunkSpawnDifference;

                newLevelChunk.transform.position = _spawnPoint;

                _levelChunkNext.DestroyLevel();

                _levelChunkNext = _levelChunkCurrent;
                _levelChunkCurrent = _levelChunkPrevious;

                _levelChunkPrevious = newLevelChunk.GetComponent<LevelChunk>();
            }
            else if (_levelChunkNext.WithinLevelBounds() && !_levelChunkCurrent.WithinLevelBounds())
            {
                GameObject newLevelChunk = Instantiate(_levelChunkCollection[Random.Range(0, _levelChunkCollection.Length)], _levelChunkNext.GetComponent<LevelChunk>()._frontSpawnPos, Quaternion.identity);

                Vector3 _newChunkSpawnDifference = newLevelChunk.GetComponent<LevelChunk>()._backSpawnPos - newLevelChunk.transform.position;
                Vector3 _spawnPoint = _levelChunkNext.GetComponent<LevelChunk>()._frontSpawnPos - _newChunkSpawnDifference;

                newLevelChunk.transform.position = _spawnPoint;

                _levelChunkPrevious.DestroyLevel();

                _levelChunkPrevious = _levelChunkCurrent;
                _levelChunkCurrent = _levelChunkNext;

                _levelChunkNext = newLevelChunk.GetComponent<LevelChunk>();
            }
        }
    }
}
