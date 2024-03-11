//using UnityEngine;

///// <summary>
///// プレイヤーの操作を制御するクラス
///// </summary>
//public class PlayerController2 : MonoBehaviour
//{
//    // プレイヤーキャラクターのアニメーター
//    [SerializeField]
//    private Animator animator;
//    // 移動速度
//    [SerializeField]
//    private float moveSpeed = 3;
//    // キャラクターコントローラー
//    private CharacterController _characterController;
//    // Transformコンポーネント
//    private Transform _transform;
//    // 移動ベクトル
//    private Vector3 _moveVelocity;
//    // プレイヤーの状態
//    private PlayerStatus _status;
//    // モブの攻撃
//    private MobAttack _attack;
//    //プレイヤーの回避
//    private PlayerRolling _playerRolling;
//    private Rigidbody rb;


//    /// <summary>
//    /// 初期化処理
//    /// </summary>
//    void Start()
//    {
//        //_characterController = GetComponent<CharacterController>();
//        rb = GetComponent<Rigidbody>();
//        _transform = transform;
//        _status = GetComponent<PlayerStatus>();
//        _attack = GetComponent<MobAttack>();
//        _playerRolling = GetComponent<PlayerRolling>();
//    }

//    /// <summary>
//    /// プレイヤーの入力や動作を更新するメソッド
//    /// </summary>
//    void FixedUpdate()
//    {
//        // 攻撃ボタンが押されたら攻撃メソッドを実行
//        if (Input.GetButtonDown("Fire1"))
//        {
//            _attack.AttackIfPossible();
//        }

//        // 移動が可能なら移動処理を行う
//        if (_status.IsMoveble)
//        {
//            // 入力に基づいて移動ベクトルを設定
//            _moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
//            _moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

//            // 移動方向を向く
//            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
//        }
//        else
//        {
//            // 移動が不可能なら移動ベクトルをゼロにする
//            _moveVelocity.x = 0;
//            _moveVelocity.y = 0;
//        }
//        //地上にいる時、ジャンプボタンが押されたらジャンプ
//        if (_characterController.isGrounded)
//        {
//            if (Input.GetButtonDown("Jump"))
//            {
//                _playerRolling.RollingIfPossible();
//                print("回避");
//            }
//        }
//        else
//        {
//            // 空中にいる場合、重力をかけて下降させる
//            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
//        }
//        //プレイヤーを移動させる
//        _characterController.Move(_moveVelocity * Time.deltaTime);
//        // プレイヤーを移動させる
//        rb.velocity = new Vector3(_moveVelocity.x, rb.velocity.y, _moveVelocity.z);

//        // 移動スピードをanimatorに反映する
//        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
//    }
//}

using UnityEngine;

/// <summary>
/// プレイヤーの操作を制御するクラス
/// </summary>
public class PlayerController2 : MonoBehaviour
{
    [SerializeField]
    private Animator animator; // プレイヤーキャラクターのアニメーター
    [SerializeField]
    private float moveSpeed = 3; // 移動速度
    private Rigidbody _rigidbody; // Rigidbodyコンポーネント
    private Transform _transform; // Transformコンポーネント
    private PlayerStatus _status; // プレイヤーの状態
    private MobAttack _attack; // モブの攻撃
    private PlayerRolling _playerRolling; // プレイヤーの回避
    public LayerMask groundLayer; // 地面レイヤー
    public Transform groundCheck; // 地面チェック位置
    public float groundDistance = 0.4f; // 地面チェックの距離
    private bool _isGrounded; // 地上にいるかどうか

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
        _playerRolling = GetComponent<PlayerRolling>();
    }

    void Update()
    {
        // 地面にいるかどうかをチェックし、Raycastを可視化
        _isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);

        // 攻撃ボタンが押されたら攻撃
        if (Input.GetButtonDown("Fire1"))
        {
            _attack.AttackIfPossible();
        }

        // 移動スピードをanimatorに反映
        animator.SetFloat("MoveSpeed", _rigidbody.velocity.magnitude);

        // 地上にいるときにジャンプ
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("回避");
            _playerRolling.RollingIfPossible();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!_status.IsMoveble) return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 移動と回転
        _rigidbody.velocity = movement * moveSpeed;
        if (movement != Vector3.zero)
        {
            _transform.LookAt(_transform.position + movement);
        }
    }
}