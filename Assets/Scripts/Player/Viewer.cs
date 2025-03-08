using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Viewer : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _jumpedSprite;
    [SerializeField] private Sprite _diedSprite;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetDefaultSprite()
    {
        _renderer.sprite = _defaultSprite;
    }

    public void SetJumpSprite()
    {
        _renderer.sprite = _jumpedSprite;
    }

    public void SetDiedSprite()
    {
        _renderer.sprite = _diedSprite;
    }
}