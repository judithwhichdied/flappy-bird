using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    private TextMeshProUGUI _scoreView;

    private int _startScore = 0;
    private int _currentScore;

    private void Awake()
    {
        _scoreView = GetComponent<TextMeshProUGUI>();

        Reset();
    }

    private void OnEnable()
    {
        _spawner.EnemyDied += ViewScore;
    }

    private void OnDisable()
    {
        _spawner.EnemyDied -= ViewScore;
    }

    public void Reset()
    {
        _currentScore = _startScore;

        _scoreView.text = _currentScore.ToString();
    }

    private void AddScore()
    {
        _currentScore++;
    }

    private void ViewScore()
    {
        AddScore();

        _scoreView.text = _currentScore.ToString();
    }
}
