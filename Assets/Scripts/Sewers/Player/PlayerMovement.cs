using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement
{

    //class to control the player RigidBody!

    private Rigidbody2D _rigidBody;
    private PlayerInputReader _playerInputReader;
    private PlayerStatsExploration _playerStats;
    private SpriteRenderer _spriteRenderer;
    public PlayerMovement(Rigidbody2D rigidBody, PlayerInputReader playerInputReader, PlayerStatsExploration playerStats, SpriteRenderer spriteRenderer)
    {
        _rigidBody = rigidBody;
        _playerInputReader = playerInputReader;
        _playerStats = playerStats;
        _spriteRenderer = spriteRenderer;
    }

    public void ApplyMovement(Vector2 direction, bool shouldFaceLeftSide)
    {
        _rigidBody.linearVelocity = direction * _playerStats.MaxMovementSpeed;
        DecideFacingDirectionBasedOnInput(shouldFaceLeftSide);
    }
    public void StartIdle(bool shouldFaceLeftSide)
    {
        CantMove();
        DecideFacingDirectionBasedOnInput(shouldFaceLeftSide);
    }
    public void CantMove()
    {
        _rigidBody.linearVelocityX = 0;
        _rigidBody.linearVelocityY = 0;
    }
    private void DecideFacingDirectionBasedOnInput(bool shouldFaceLeftSide)
    {
        _spriteRenderer.flipX = shouldFaceLeftSide;
    }
}
