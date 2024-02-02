using UnityEngine;

/// <summary>
/// プレイヤーの操作を制御するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]
    private float jumpPower = 3;

    private CharacterController _characterController;
    private Transform _transform;
    private Vector3 _moveVelocity;
    private PlayerStatus _status;
    private MobAttack _attack;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    /// <summary>
    /// プレイヤーの入力や動作を更新するメソッド
    /// </summary>
    void Update()
    {
        // 地上または空中かをデバッグログに表示
        Debug.Log(_characterController.isGrounded ? "地上" : "空中");

        // 攻撃ボタンが押されたら攻撃メソッドを実行
        if (Input.GetButtonDown("Fire1"))
        {
            _attack.AttackIfPossible();
        }

        // 移動が可能なら移動処理を行う
        if (_status.IsMoveble)
        {
            // 入力に基づいて移動ベクトルを設定
            _moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
            _moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

            // 移動方向を向く
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        }
        else
        {
            // 移動が不可能なら移動ベクトルをゼロにする
            _moveVelocity.x = 0;
            _moveVelocity.y = 0;
        }

        // 地上にいる時、ジャンプボタンが押されたらジャンプ
        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                print("ジャンプ");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            // 空中にいる場合、重力をかけて下降させる
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // プレイヤーを移動させる
        _characterController.Move(_moveVelocity * Time.deltaTime);

        // 移動スピードをanimatorに反映する
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}