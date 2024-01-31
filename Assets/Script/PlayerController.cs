using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
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
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    void Update()
    {
        Debug.Log(_characterController.isGrounded ? "�n��" : "��");
        if (Input.GetButtonDown("Fire1"))
        {
            _attack.AttackIfPossible();
        }

        if (_status.IsMoveble)
        {
            _moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
            _moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;
            //�ړ�����������
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0,
                 _moveVelocity.z));
        }
        else
        {
            _moveVelocity.x = 0;
            _moveVelocity.y = 0;
        }
        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                print("�W�����v");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        _characterController.Move(_moveVelocity * Time.deltaTime);
        //�ړ��X�s�[�h��animator�ɔ��f����
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0,
        _moveVelocity.z).magnitude);
    }
}