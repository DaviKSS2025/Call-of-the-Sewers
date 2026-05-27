using UnityEngine;

public class PlayerMovement
{

    //class to control the player RigidBody!

    private Rigidbody2D _rigidBody;
    private PlayerStatsExploration _playerStats;
    public PlayerMovement(Rigidbody2D rigidBody, PlayerStatsExploration playerStats)
    {
        _rigidBody = rigidBody;
        _playerStats = playerStats;
    }

    public void ApplyMovement(Vector2 input)
    {
        Vector2 direction;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            direction = new Vector2(Mathf.Sign(input.x), 0);
        else
            direction = new Vector2(0, Mathf.Sign(input.y));

        _rigidBody.linearVelocity = direction * _playerStats.MaxMovementSpeed;
    }
    public void StartIdle()
    {
        CantMove();
    }
    public void CantMove()
    {
        _rigidBody.linearVelocity = Vector2.zero;
    }
}
