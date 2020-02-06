using System;
using UnityEngine;

public class PlayerOLD : MonoBehaviour //OLD
{
  public float health = 100;

  [Header("Скорость")]
  public float speed1 = 100;
  public float speed2 = 180;
  public float speed3 = 380;
  [Header("Сила прыжка")]
  public float jumpForce1 = 220;
  public float jumpForce2 = 345;
  public float jumpForce3 = 435;

  public Collider2D groundCheck;
  public LayerMask groundLayer;

  public UI_Player UIPlayer;

  private bool _lowHealthWarning = false;
  // Скорости падения персонажа на землю с высоты 7-12 тайлов.
  private float[] _fallingSpeeds = { 9.61f, 10.40f, 10.99f, 11.58f, 12.16f, 12.75f };
  private float _maxSafeSpeed = 9.614f; // Макс. скорость (включительно) для падения без урона (округленный урон < 0.1)
  // Желаемое распределение повреждений при падении с высот 7-12 тайлов. Использовал для вычисления коэффициентов.
  private int[] _damagesFalling = { 0, 10, 25, 45, 70, 100 };
  // Коэффициенты для желаемого распределения повреждений.
  private float[] _damageFactors = { 0, 12.66f, 25.42f, 33.90f, 43.10f, 50.85f };

  private float curSpeed, curJumpForce, direction = 1,
                sumForceState1, sumForceState2, sumForceState3,
                speedFS1, speedFS2, speedFS3,
                jumpForceFS1, jumpForceFS2, jumpForceFS3;

  private int forceStateCount;
  private directionState _directionState = directionState.right;
  private forceState _forceState = forceState.normal;
  private moveState _moveState = moveState.idle;

  // Скорости для анимации.
  private float _asMove1 = 1.0f, _asMove2 = 1.8f, _asMove3 = 3.8f;

  // Хэши для анимации:
  private int _ahIdleR, _ahIdleL, _ahMoveR, _ahMoveL, _ahMoveR_idleR, _ahMoveL_idleL,
              _ahFallR, _ahFallL, _ahJumpR, _ahJumpL, _ahJumpR_idleR, _ahJumpL_idleL,
              _ahJMoveR, _ahJMoveL, _ahJMoveR_moveR, _ahJMoveL_moveL,
              _ahPreTriumphR, _ahPreTriumphL, _ahTriumphR, _ahTriumphL,
              _ahDeathR, _ahDeathL, _ahASpeed;

  private Transform _thisTransform;
  private Rigidbody2D _rigidbody;
  private Animator _animator;
  private ContactFilter2D _groundFilter = new ContactFilter2D();
  private Collider2D[] _groundColliders = new Collider2D[5];

  private void OnSave()
  {
    string playerToSave = string.Format("{{\n_thisTransform.name = \"{0}\"\n", _thisTransform.name);
    playerToSave += string.Format("_thisTransform.position = {0}\n", _thisTransform.position.ToString("0.00"));
    playerToSave += string.Format("_rigidbody.velocity = {0}\n", _rigidbody.velocity.ToString("0.00"));
    playerToSave += string.Format("Player.health = {0}\n", health.ToString("0.0"));
    playerToSave += string.Format("Player._directionState = {0}\n", _directionState);
    playerToSave += string.Format("Player._forceState = {0}\n", _forceState);
    playerToSave += string.Format("Player._moveState = {0}\n}}\n", _moveState);

    GManager.Instance.AddObjectToSave(playerToSave);
  }

  private void OnLoad()
  {
    string playerToLoad = GManager.Instance.ReadNextObjectToLoad();
    _thisTransform.name = GManager.FindStringParameterInObject(playerToLoad, "_thisTransform.name");
    _thisTransform.position = GManager.FindVector3InObject(playerToLoad, "_thisTransform.position");
    _rigidbody.velocity = GManager.FindVector2InObject(playerToLoad, "_rigidbody.velocity");
    health = GManager.FindFloatParamInSObject(playerToLoad, "Player.health");
    _directionState = (directionState)Enum.Parse(typeof(directionState), GManager.FindStringParameterInObject(playerToLoad, "Player._directionState", "", ""));
    _forceState = (forceState)Enum.Parse(typeof(forceState), GManager.FindStringParameterInObject(playerToLoad, "Player._forceState", "", ""));
    _moveState = (moveState)Enum.Parse(typeof(moveState), GManager.FindStringParameterInObject(playerToLoad, "Player._moveState", "", ""));

    _lowHealthWarning = false;
    UIPlayer.SetHealth(health);
  }

  private void Awake()
  {
    curSpeed = speed2;
    curJumpForce = jumpForce2;
    sumForceState1 = speed1 + jumpForce1;
    sumForceState2 = speed1 + jumpForce2;
    sumForceState3 = speed3 + jumpForce1;
    speedFS1 = 0.34f * sumForceState1;
    speedFS2 = 0.34f * sumForceState2;
    speedFS3 = 0.34f * sumForceState3;
    jumpForceFS1 = 0.66f * sumForceState1;
    jumpForceFS2 = 0.66f * sumForceState2;
    jumpForceFS3 = 0.66f * sumForceState3;

    forceStateCount = Enum.GetNames(typeof(forceState)).Length;

    // Получаем хэши для анимации:
    _ahIdleR = Animator.StringToHash("IdleR");
    _ahIdleL = Animator.StringToHash("IdleL");
    _ahMoveR = Animator.StringToHash("MoveR");
    _ahMoveL = Animator.StringToHash("MoveL");
    _ahMoveR_idleR = Animator.StringToHash("MoveR-IdleR");
    _ahMoveL_idleL = Animator.StringToHash("MoveL-IdleL");
    _ahFallR = Animator.StringToHash("FallR");
    _ahFallL = Animator.StringToHash("FallL");
    _ahJumpR = Animator.StringToHash("JumpR");
    _ahJumpL = Animator.StringToHash("JumpL");
    _ahJumpR_idleR = Animator.StringToHash("JumpR-IdleR");
    _ahJumpL_idleL = Animator.StringToHash("JumpL-IdleL");
    _ahJMoveR = Animator.StringToHash("JMoveR");
    _ahJMoveL = Animator.StringToHash("JMoveL");
    _ahJMoveR_moveR = Animator.StringToHash("JMoveR-MoveR");
    _ahJMoveL_moveL = Animator.StringToHash("JMoveL-MoveL");
    _ahPreTriumphR = Animator.StringToHash("PreTriumphR");
    _ahPreTriumphL = Animator.StringToHash("PreTriumphL");
    _ahTriumphR = Animator.StringToHash("TriumphR");
    _ahTriumphL = Animator.StringToHash("TriumphL");
    _ahDeathR = Animator.StringToHash("DeathR");
    _ahDeathL = Animator.StringToHash("DeathL");
    _ahASpeed = Animator.StringToHash("ASpeed");

    _thisTransform = GetComponent<Transform>();
    _rigidbody = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _groundFilter.SetLayerMask(groundLayer);

    UIPlayer.SetForceState(1);
  }

  private void Start()
  {
    GManager.Instance.save += OnSave;
    GManager.Instance.load += OnLoad;
  }

  private void FixedUpdate()
  {
    if (_moveState == moveState.jump_idle &&
       (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash == _ahIdleR ||
        _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == _ahIdleL))
    {
      _moveState = moveState.idle;
    }
    else if (_moveState == moveState.jMove_move &&
            (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash == _ahMoveR ||
             _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == _ahMoveL))
    {
      _moveState = moveState.move;
    }

    if (GroundCheck())
    {
      if (_moveState == moveState.jump || _moveState == moveState.fall)
      {
        _moveState = moveState.jump_idle;
        DamageFromFalling();
      }
      else if (_moveState == moveState.jMove)
      {
        _moveState = moveState.jMove_move;
        DamageFromFalling();
      }
    }
    else
    {
      if (_moveState == moveState.idle || _moveState == moveState.move || _moveState == moveState.move_idle)
      {
        _moveState = moveState.fall;
      }
    }

    if (_moveState == moveState.jMove && _rigidbody.velocity.x == 0)
    {
      _moveState = moveState.jump;
    }

    if (_moveState == moveState.preTriumph &&
       (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash == _ahTriumphR ||
        _animator.GetCurrentAnimatorStateInfo(0).shortNameHash == _ahTriumphL))
    {
      _moveState = moveState.triumph;
    }

    Animation();
  }

  public void Move(float _direction)
  {
    if (_moveState == moveState.idle || _moveState == moveState.move || _moveState == moveState.move_idle)
    {
      _moveState = moveState.move;

      DirectionSwitch(_direction);

      switch (_forceState)
      {
        case forceState.min:
          curSpeed = speed1;
          break;

        case forceState.normal:
          curSpeed = speed2;
          break;

        case forceState.max:
          curSpeed = speed3;
          break;
      }
      // Если сделать через AddForce(), тогда будет плавная смена направления движения, а не мгновенная.
      _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity,
                                          Vector2.right * direction * curSpeed * Time.deltaTime,
                                         Time.deltaTime * 5.0f);
    }
    else if (_moveState == moveState.fall || _moveState == moveState.jump || _moveState == moveState.jMove)
    {
      //Debug.Log("M -> JM, DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
      JumpMove(_direction);
    }
  }

  public void MoveToIdle()
  {
    if (_moveState == moveState.move || _moveState == moveState.move_idle)
    {
      if (_rigidbody.velocity.x != 0)
      {
        _moveState = moveState.move_idle;
        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.zero, Time.deltaTime);
      }
      else
      {
        _moveState = moveState.idle;
      }
    }
  }

  public void Jump()
  {
    if (_moveState == moveState.idle)
    {
      _moveState = moveState.jump;

      ForceStateJump();

      _rigidbody.AddForce(Vector2.up * curJumpForce * Time.deltaTime, ForceMode2D.Impulse);
    }
    else if (_moveState == moveState.move || _moveState == moveState.move_idle)
    {
      //Debug.Log("J -> JM, DS: " + _directionState + ", dir: " + direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
      JumpMove(0);
    }
  }

  public void JumpMove(float _direction)
  {
    //Debug.Log("JM start, DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
    DirectionSwitch(_direction);

    switch (_moveState)
    {
      case moveState.idle:
        _moveState = moveState.jMove; // jMove

        ForceStateJMove_Idle();

        _rigidbody.AddForce(new Vector2(_direction * curSpeed, curJumpForce) * Time.deltaTime, ForceMode2D.Impulse);
        //Debug.Log("...JM end (idle), DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
        //Debug.Log("...(idle) (_dir * curS, curJF) * T.dT, curS: " + curSpeed + ", curJF: " + curJumpForce); // Debug.
        break;

      case moveState.move:
      case moveState.move_idle:
        _moveState = moveState.jMove; // jMove

        ForceStateJMove1();

        if ((_rigidbody.velocity.x > 0 && _direction >= 0) ||
            (_rigidbody.velocity.x < 0 && _direction <= 0))
        {
          _rigidbody.AddForce(Vector2.up * curJumpForce, ForceMode2D.Impulse); // curJumpForce уже относительно Time.deltaTime (после ForceStateJMove1())
          //Debug.Log("...JM end (move/move-idle) 1) DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
          //Debug.Log("...1) (0, curJF [уже относительно T.dT] ), curJF: " + curJumpForce); // Debug.
        }
        else // Если сменили направление...
        {
          _rigidbody.AddForce(new Vector2(-2 * _rigidbody.velocity.x, curJumpForce), ForceMode2D.Impulse);
          //Debug.Log("...JM end (move/move-idle) 2) DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
          //Debug.Log("...2) (-2 * vel.x, curJF [уже относительно T.dT] ), curJF: " + curJumpForce); // Debug.
        }
        break;

      case moveState.fall:
      case moveState.jump:
      case moveState.jMove:
        _moveState = moveState.jMove; // jMove

        if ((_rigidbody.velocity.x >= 0 && _direction > 0) ||
            (_rigidbody.velocity.x <= 0 && _direction < 0))
        {
          ForceStateJMove2();

          if (Mathf.Abs(_rigidbody.velocity.x) < curSpeed) // Если горизонтальная скорость меньше возможной...
          {
            _rigidbody.AddForce(Vector2.right * _direction * 0.1f * curSpeed, ForceMode2D.Impulse); // curSpeed уже относительно Time.deltaTime (после ForceStateJMove2())
            //Debug.Log("...JM end (fall/jump/jmove) 1-1) DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
            //Debug.Log("...1-1) (_dir * 0.1 * curS [уже относительно T.dT], 0), curS: " + curSpeed); // Debug.
          }
          else
          {
            //Debug.Log("...JM end (fall/jump/jmove) 1-2) DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
            //Debug.Log("...1-2) (0, 0) - скорость уже равна или больше возможной"); // Debug.
          }
        }
        else // Если сменили направление...
        {
          ForceStateJMove1();
          _rigidbody.AddForce(Vector2.right * -2 * _rigidbody.velocity.x, ForceMode2D.Impulse);
          //Debug.Log("...JM end (fall/jump/jmove) 2) DS: " + _directionState + ", _dir: " + _direction + ", MS: " + _moveState + ", vel: " + _rigidbody.velocity); // Debug.
          //Debug.Log("...2) (-2 * vel.x, 0)"); // Debug.
        }
        break;

      case moveState.jump_idle:
      case moveState.jMove_move:
        //Debug.Log("...JM end (jump-idle/jMove-move)"); // Debug.
        break;
    }
  }

  private bool GroundCheck()
  {
    int i = Physics2D.OverlapCollider(groundCheck, _groundFilter, _groundColliders);
    if (i < 1)
    {
      return false;
    }
    else
    {
      return true;
    }
  }

  private void DamageFromFalling()
  {
    //Debug.Log("Скорость падения: " + _rigidbody.velocity.y); // DEBUG
    float damage;

    if (Mathf.Abs(_rigidbody.velocity.y) <= _maxSafeSpeed) // Если упали с безопасной высоты (чуть больше 7 тайлов)...
      return;

    for (int i = 1; i < _fallingSpeeds.Length; i++)
    {
      if (Mathf.Abs(_rigidbody.velocity.y) <= _fallingSpeeds[i])
      {
        damage = _damagesFalling[i - 1];
        damage += (float)Math.Round((Mathf.Abs(_rigidbody.velocity.y) - _fallingSpeeds[i - 1]) * _damageFactors[i],
                                     1, MidpointRounding.AwayFromZero);
        health = (float)Math.Round(health - damage, 1, MidpointRounding.AwayFromZero);

        UIPlayer.SetHealth(health);
        MessageManager.ShowMessageOnlyText("От падения с большой высоты персонаж получил урон: " + damage);

        if (health <= 0)
        {
          MessageManager.ShowMessageOnlyText("Ваш персонаж погиб от падения с большой высоты!");
          _moveState = moveState.death;
        }
        else if (health < 20 && !_lowHealthWarning)
        {
          _lowHealthWarning = true;
          MessageManager.ShowMessageOnlyText("Персонаж имеет опасно низкий уровень здоровья. Будьте осторожнее!");
        }
        return;
      }
    }

    // Если скорость падения больше чем при падении с 12 тайлов...
    damage = _damagesFalling[_fallingSpeeds.Length - 1];
    health = (float)Math.Round(health - damage, 1, MidpointRounding.AwayFromZero);

    UIPlayer.SetHealth(health);
    MessageManager.ShowMessageOnlyText("Ваш персонаж погиб от падения с большой высоты!");
    _moveState = moveState.death;
  }

  public void PassedLevel()
  {
    _moveState = moveState.preTriumph;
  }

  private void Animation()
  {
    switch (_moveState)
    {
      case moveState.idle:
        PlayDirectionAnimation(_ahIdleR, _ahIdleL);
        break;

      case moveState.move:
        switch (_forceState)
        {
          case forceState.min:
            _animator.SetFloat(_ahASpeed, _asMove1);
            break;

          case forceState.normal:
            _animator.SetFloat(_ahASpeed, _asMove2);
            break;

          case forceState.max:
            _animator.SetFloat(_ahASpeed, _asMove3);
            break;
        }

        PlayDirectionAnimation(_ahMoveR, _ahMoveL);
        break;

      case moveState.move_idle:
        PlayDirectionAnimation(_ahMoveR_idleR, _ahMoveL_idleL);
        break;

      case moveState.fall:
        PlayDirectionAnimation(_ahFallR, _ahFallL);
        break;

      case moveState.jump:
        PlayDirectionAnimation(_ahJumpR, _ahJumpL);
        break;

      case moveState.jump_idle:
        PlayDirectionAnimation(_ahJumpR_idleR, _ahJumpL_idleL);
        break;

      case moveState.jMove:
        PlayDirectionAnimation(_ahJMoveR, _ahJMoveL);
        break;

      case moveState.jMove_move:
        PlayDirectionAnimation(_ahJMoveR_moveR, _ahJMoveL_moveL);
        break;

      case moveState.preTriumph:
        PlayDirectionAnimation(_ahPreTriumphR, _ahPreTriumphL);
        break;

      case moveState.triumph:
        PlayDirectionAnimation(_ahTriumphR, _ahTriumphL);
        break;

      case moveState.death:
        PlayDirectionAnimation(_ahDeathR, _ahDeathL);
        break;
    }
  }

  private void PlayDirectionAnimation(int rightAnim, int leftAnim)
  {
    if (_directionState == directionState.right)
    {
      _animator.Play(rightAnim);
    }
    else
    {
      _animator.Play(leftAnim);
    }
  }

  private void DirectionSwitch(float _direction)
  {
    if (_direction != 0)
    {
      direction = _direction;
    }

    if (direction > 0)
    {
      _directionState = directionState.right;
    }
    else if (direction < 0)
    {
      _directionState = directionState.left;
    }
  }

  public void ForceSwitch()
  {
    _forceState++;

    if ((int)_forceState == forceStateCount)
    {
      _forceState = forceState.min;
    }

    switch (_forceState)
    {
      case forceState.min:
        UIPlayer.SetForceState(0);
        break;

      case forceState.normal:
        UIPlayer.SetForceState(1);
        break;

      case forceState.max:
        UIPlayer.SetForceState(2);
        break;
    }
  }

  private void ForceStateJump()
  {
    switch (_forceState)
    {
      case forceState.min:
        curJumpForce = jumpForce1;
        break;

      case forceState.normal:
        curJumpForce = jumpForce2;
        break;

      case forceState.max:
        curJumpForce = jumpForce3;
        break;
    }
  }

  private void ForceStateJMove_Idle()
  {
    switch (_forceState)
    {
      case forceState.min:
        curSpeed = speedFS1;
        curJumpForce = jumpForceFS1;
        break;

      case forceState.normal:
        curSpeed = speedFS2;
        curJumpForce = jumpForceFS2;
        break;

      case forceState.max:
        curSpeed = speedFS3;
        curJumpForce = jumpForceFS3;
        break;
    }
  }

  private void ForceStateJMove1() // Производит нахождение силы прыжка относительно скорости движения.
  {
    //curSpeed = _rigidbody.velocity.x;

    switch (_forceState)
    {
      case forceState.min:
        curJumpForce = sumForceState1 * Time.deltaTime - Mathf.Abs(_rigidbody.velocity.x);
        break;

      case forceState.normal:
        curJumpForce = sumForceState2 * Time.deltaTime - Mathf.Abs(_rigidbody.velocity.x);
        break;

      case forceState.max:
        curJumpForce = sumForceState3 * Time.deltaTime - Mathf.Abs(_rigidbody.velocity.x);
        break;
    }

    if (curJumpForce > jumpForce3 * Time.deltaTime)
    {
      //Debug.Log("...JM (curJF > JF3), curJF: " + curJumpForce); // Debug.
      curJumpForce = jumpForce3 * Time.deltaTime;
      //Debug.Log("...(curJF > JF3) => curJF = " + curJumpForce); // Debug.
    }
  }

  private void ForceStateJMove2()
  {
    switch (_forceState)
    {
      case forceState.min:
        curSpeed = speed1 * Time.deltaTime;
        break;

      case forceState.normal:
        curSpeed = 1.2f * speed1 * Time.deltaTime;
        break;

      case forceState.max:
        curSpeed = 1.5f * speed1 * Time.deltaTime;
        break;
    }
  }

  // Перечисления состояний:
  private enum directionState { left, right };
  private enum forceState { min, normal, max }
  private enum moveState { idle, move, move_idle, fall, jump, jump_idle, jMove, jMove_move, preTriumph, triumph, death };
}
