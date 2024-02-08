using UnityEngine;

/// <summary>
/// �v���C���[�̑���𐧌䂷��N���X
/// </summary>
public class PlayerController2 : MonoBehaviour
{
    // �v���C���[�L�����N�^�[�̃A�j���[�^�[
    [SerializeField]
    private Animator animator;
    // �ړ����x
    [SerializeField]
    private float moveSpeed = 3;
    // �L�����N�^�[�R���g���[���[
    private CharacterController _characterController;
    // Transform�R���|�[�l���g
    private Transform _transform;
    // �ړ��x�N�g��
    private Vector3 _moveVelocity;
    // �v���C���[�̏��
    private PlayerStatus _status;
    // ���u�̍U��
    private MobAttack _attack;
    //�v���C���[�̉��
    private PlayerRolling _playerRolling;
    private Rigidbody rb;


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        //_characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
        _playerRolling = GetComponent<PlayerRolling>();
    }

    /// <summary>
    /// �v���C���[�̓��͂⓮����X�V���郁�\�b�h
    /// </summary>
    void FixedUpdate()
    {
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
                _playerRolling.RollingIfPossible();
                print("���");
            }
        }
        else
        {
            // �󒆂ɂ���ꍇ�A�d�͂������ĉ��~������
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }
        // �v���C���[���ړ�������
        //_characterController.Move(_moveVelocity * Time.deltaTime);
        // �v���C���[���ړ�������
        rb.velocity = new Vector3(_moveVelocity.x, rb.velocity.y, _moveVelocity.z);

        // �ړ��X�s�[�h��animator�ɔ��f����
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}