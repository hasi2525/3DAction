using UnityEngine;

/// <summary>
/// プレイヤーの操作を管理するクラス。
/// </summary>
public class PlayerController3 : MonoBehaviour
{
    //Animatorコンポーネント
    [SerializeField]
    private Animator animator;
    // プレイヤーの基本移動速度
    [SerializeField]
    private float moveSpeed = 3;

    private Transform _transform;
    //private Vector3 _moveVelocity; 
    // プレイヤーの状態を管理するコンポーネント
    private PlayerStatus _status; 
    // 敵への攻撃を管理するコンポーネント
    private MobAttack _attack; 
    private Rigidbody rb; 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    private void Update()
    {
        AttackMove();
    }

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    private void Move()
    {
        // 移動が可能なら移動処理を行う
        if (_status.IsMoveble)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // キーボードからの入力を取得
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // カメラの正面方向に基づいて、プレイヤーの向きを変える
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // プレイヤーを移動方向に向ける
                _transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                // 走っているかどうかに応じて移動速度を変更
                float currentSpeed = isRunning ? moveSpeed * 2 : moveSpeed;

                // プレイヤーを移動させる
                rb.velocity = new Vector3(moveDir.x * currentSpeed, rb.velocity.y, moveDir.z * currentSpeed);

                // 移動速度と走っている状態をanimatorに反映させる
                animator.SetFloat("MoveSpeed", direction.magnitude * (isRunning ? 2 : 1));
                animator.SetBool("RunMoveSpeed", isRunning);
            }
            else
            {
                // 入力がない場合は動きを止める
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                animator.SetFloat("MoveSpeed", 0);
                animator.SetBool("RunMoveSpeed", false);
            }
        }
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    void AttackMove()
    {
        // 攻撃ボタンが押されたら攻撃
        if (Input.GetButtonDown("Fire1"))
        {
            //攻撃アニメーション中などにプレイヤーが動き続けないようにする
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            _attack.AttackIfPossible();
        }
    }
}