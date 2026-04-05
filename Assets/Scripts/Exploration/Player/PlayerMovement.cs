using UnityEngine;

public class PlayerMovement
{

    //class to control the player RigidBody!

    private Rigidbody2D _rigidBody;
    private PlayerStatsExploration _playerStats;
    private SpriteRenderer _spriteRenderer;
    private bool _wasFacingLeft;
    public PlayerMovement(Rigidbody2D rigidBody, PlayerStatsExploration playerStats, SpriteRenderer spriteRenderer)
    {
        _rigidBody = rigidBody;
        _playerStats = playerStats;
        _spriteRenderer = spriteRenderer;
    }

    public void ApplyMovement(Vector2 input)
    {
        Vector2 direction;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            direction = new Vector2(Mathf.Sign(input.x), 0);
        else
            direction = new Vector2(0, Mathf.Sign(input.y));

        if (direction.x != 0)
            _wasFacingLeft = direction.x < 0;

        _rigidBody.linearVelocity = direction * _playerStats.MaxMovementSpeed;
        _spriteRenderer.flipX = _wasFacingLeft;
    }
    public void StartIdle()
    {
        _rigidBody.linearVelocity = Vector2.zero;
        _spriteRenderer.flipX = _wasFacingLeft;
    }
    public void CantMove()
    {
        _rigidBody.linearVelocityX = 0;
        _rigidBody.linearVelocityY = 0;
    }
}
