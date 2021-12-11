using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    [SerializeField]
    private bool canDoubleJump;
    public bool CanDoubleJump
    {
        get => canDoubleJump;
        set { canDoubleJump = value; }
    }

    [SerializeField]
    private bool canDash;
    public bool CanDash
    {
        get => canDash;
        set { canDash = value; }
    }

    [SerializeField]
    private bool canBecomeBall;
    public bool CanBecomeBall
    {
        get => canBecomeBall;
        set { canBecomeBall = value; }
    }

    [SerializeField]
    private bool canDropBomb;
    public bool CanDropBomb
    {
        get => canDropBomb;
        set { canDropBomb = value; }
    }


}
