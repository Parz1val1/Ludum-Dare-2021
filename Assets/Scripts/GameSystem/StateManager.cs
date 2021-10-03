using Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private MouseClickListener _mouseClickListener;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Rigidbody2D _playerRigidBody;
        [SerializeField] private GameObject _endUI;

        private bool _isTimerRunning {  get => TimerManager.IsTimerRunning();   }

        private bool _gameEnded = false;

        // Start is called before the first frame update
        void Start()
        {
            _endUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isTimerRunning && !_gameEnded)
            {
                _gameEnded = true;

                _mouseClickListener.enabled = false;
                _playerController.enabled = false;
                _playerMovement.enabled = false;

                _playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

                _endUI.SetActive(true);
            }
        }
    }
}
