using UnityEngine;

/// <summary>
/// �v���C���[�̑���𐧌䂷��N���X
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
    /// ����������
    /// </summary>
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    /// <summary>
    /// �v���C���[�̓��͂⓮����X�V���郁�\�b�h
    /// </summary>
    void Update()
    {
        // �n��܂��͋󒆂����f�o�b�O���O�ɕ\��
        Debug.Log(_characterController.isGrounded ? "�n��" : "��");

        // �U���{�^���������ꂽ��U�����\�b�h�����s
        if (Input.GetButtonDown("Fire1"))
        {
            _attack.AttackIfPossible();
        }

        // �ړ����\�Ȃ�ړ��������s��
        if (_status.IsMoveble)
        {
            // ���͂Ɋ�Â��Ĉړ��x�N�g����ݒ�
            _moveVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
            _moveVelocity.z = Input.GetAxisRaw("Vertical") * moveSpeed;

            // �ړ�����������
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        }
        else
        {
            // �ړ����s�\�Ȃ�ړ��x�N�g�����[���ɂ���
            _moveVelocity.x = 0;
            _moveVelocity.y = 0;
        }

        // �n��ɂ��鎞�A�W�����v�{�^���������ꂽ��W�����v
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
            // �󒆂ɂ���ꍇ�A�d�͂������ĉ��~������
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // �v���C���[���ړ�������
        _characterController.Move(_moveVelocity * Time.deltaTime);

        // �ړ��X�s�[�h��animator�ɔ��f����
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}