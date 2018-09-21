using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // References
    Animator _anim;
    Rigidbody2D _rb;

    // Variables
    float _horizontal;
    bool _isSprinting;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Input
        _horizontal = Input.GetAxisRaw("Horizontal");
        #endregion

        // Detects if player is trying to move
        if (_horizontal != 0 && currentRoutine == null)
        {
            // Sets animation and start sprint countdown
            _anim.SetBool("IsWalking", true);
            currentRoutine = WalkingRoutine();
            StartCoroutine(currentRoutine);
        }
        else if (_horizontal == 0 && currentRoutine != null)
        {
            _anim.SetBool("IsWalking", false);
            StopCoroutine(currentRoutine);
            currentRoutine = null;
            _isSprinting = false;
        }

        if (Input.GetButtonDown("Fire1"))
            ChangeCharacter();

        if (CanFlip())
            Flip();

        // Movement
        Vector2 inputDirection = new Vector2(_horizontal, 0);
        Vector2 moveDirection = transform.TransformDirection(inputDirection) * Time.deltaTime;
        float speedMultiplier = _isSprinting ? PlayerStatistics.instance.sprintSpeed : PlayerStatistics.instance.speed;
        _rb.MovePosition(_rb.position + moveDirection * speedMultiplier);
    }

    bool CanFlip()
    {
        return (transform.localScale.x == 1 && _horizontal < 0) || (transform.localScale.x == -1 && _horizontal > 0) ? true : false;
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private IEnumerator currentRoutine;
    IEnumerator WalkingRoutine()
    {
        yield return new WaitForSeconds(0.25f);
        _isSprinting = true;
    }

    void ChangeCharacter()
    {
        PlayerStatistics.instance.isDida ^= true;

        switch (PlayerStatistics.instance.isDida)
        {
            case true:
                _anim.SetLayerWeight(1, 1);
                break;
            case false:
                _anim.SetLayerWeight(1, 0);
                break;
        }
    }
}
