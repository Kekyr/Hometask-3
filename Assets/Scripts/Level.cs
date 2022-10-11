using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class Level : MonoBehaviour
    {
        [Header("Timer")] 
        [SerializeField] private bool _timerIsOn;
        [SerializeField] private float _timerValue;
        [SerializeField] private Text _timerView;

        [Header("Objects")] 
        [SerializeField] private Player _player;
        [SerializeField] private Exit _exitFromLevel;

        [Header("UI")] 
        [SerializeField] private GameObject WinWindow;
        [SerializeField] private GameObject LoseWindow;
        [SerializeField] private GameObject GameEndWindow;
        
        private int _nextSceneBuildIndex;
        private int _currentSceneBuildIndex;
        private float _timer = 0;
        private bool _gameIsEnded = false;

        private void Awake()
        {
            _timer = _timerValue;
            _currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }

        private void Start()
        {
            _exitFromLevel.Close();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                RestartLevel();
            
            if (_gameIsEnded)
                return;

            TimerTick();
            LookAtPlayerHealth();
            LookAtPlayerInventory();
            TryCompleteLevel();
        }

        private void TimerTick()
        {
            if (_timerIsOn == false)
                return;

            _timer -= Time.deltaTime;
            _timerView.text = $"{_timer:F1}";

            if (_timer <= 0)
                Lose();
        }

        private void TryCompleteLevel()
        {
            if (_exitFromLevel.IsOpen == false)
                return;

            var flatExitPosition =
                new Vector2(_exitFromLevel.transform.position.x, _exitFromLevel.transform.position.z);
            var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);

            if (flatExitPosition == flatPlayerPosition)
                Victory();
        }

        private void LookAtPlayerHealth()
        {
            if (_player.IsAlive)
                return;

            Lose();
            Destroy(_player.gameObject);
        }

        private void LookAtPlayerInventory()
        {
            if (_player.HasKey)
                _exitFromLevel.Open();
        }

        public void Victory()
        {
            _gameIsEnded = true;
            _player.Disable();
            ChooseAndShowWindow();
        }

        private void ChooseAndShowWindow()
        {
            if (_currentSceneBuildIndex == SceneManager.sceneCountInBuildSettings-1)
            {
                GameEndWindow.SetActive(true);
            }
            else
            {
                WinWindow.SetActive(true);
            }
        }

        public void Lose()
        {
            _gameIsEnded = true;
            _player.Disable();
            LoseWindow.SetActive(true);
        }
        
        public void NextLevel()
        {
            _nextSceneBuildIndex = _currentSceneBuildIndex + 1;

            if (_nextSceneBuildIndex >= SceneManager.sceneCountInBuildSettings)
                return;
            
            SceneManager.LoadScene(_nextSceneBuildIndex);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(_currentSceneBuildIndex);
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}