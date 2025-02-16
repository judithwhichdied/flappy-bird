using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private Mover _birdMover;
    [SerializeField] private EnemyPool _enemySpawner;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private MapMover _map;
    [SerializeField] private ScoreViewer _scoreViewer;

    private void Awake()
    {
        Time.timeScale = 0f;
        _startScreen.Open();
    }

    private void OnEnable()
    {
        _startScreen.StartButtonClicked += OnPlayButtonClick;
        _endScreen.RestartButtonClicked += OnRestartButtonClick;
        _bird.Died += OnDeath;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClicked -= OnPlayButtonClick;
        _endScreen.RestartButtonClicked -= OnRestartButtonClick;
        _bird.Died -= OnDeath;
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();

        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _endScreen.Close();

        SceneManager.LoadScene("SampleScene");

        StartGame();
    }

    private void OnDeath()
    {
        EndGame();
        _endScreen.Open();
    }

    private void StartGame()
    {       
        Time.timeScale = 1;
        _bird.Reset();
        _birdMover.Reset();
        _map.Reset();
        _scoreViewer.Reset();   
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _enemySpawner.ClearPool();
    }
}
