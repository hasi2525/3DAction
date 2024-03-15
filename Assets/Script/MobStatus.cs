using UnityEngine;

/// <summary>
/// �v���C���[�̊�{�I�ȏ�Ԃ��Ǘ����钊�ۃN���X
/// </summary>
public abstract class MobStatus : MonoBehaviour
{
    // ��Ԓ�`
    protected enum StateEnum
    {
        // �ʏ�
        Normal,
        // �U����
        Attack,
        //���
        Rolling,
        // ���S
        Die
    }

    // ���C�t�̍ő�l
    [SerializeField] 
    private float lifeMax = 10;

    // �ړ��\��
    public bool IsMoveble =>   StateEnum.Normal == _state;

    // �U���\��
    public bool IsAttackable => StateEnum.Normal == _state;

    //�@��𒆂�
    public bool IsRolling => StateEnum.Normal == _state;

    // ���C�t�̍ő�l
    public float LifeMax => lifeMax;

    // ���C�t�̒l
    public float Life => _life;

    protected Animator _animator;
    private StateEnum _state = StateEnum.Normal;
    private float _life;

    protected virtual void Start()
    {
        // �����̓��C�t���^��
        _life = lifeMax;
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// �L�����N�^�[���|�ꂽ���ɌĂ΂�郁�\�b�h
    /// </summary>
    protected virtual void OnDie()
    {
        // �I�[�o�[���C�h���ē���̃A�N�V���������s����
    }

    /// <summary>
    /// �w��l�̃_���[�W���󂯂郁�\�b�h
    /// </summary>
    /// <param name="damage">�󂯂�_���[�W�̗�</param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;

        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");
        OnDie();
    }

    /// <summary>
    /// �U���\�ȏ�ԂɈڍs���郁�\�b�h
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// �ʏ�̏�ԂɈڍs���郁�\�b�h
    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }   
    ///�@<summary>
    ///�@����̏�ԂɈڍs���郁�\�b�h
    /// </summary>
 �@   public void GoToRollingStateIfPossible()
   �@ {
        if (!IsRolling) return;

        _state = StateEnum.Rolling;
        _animator.SetTrigger("Rolling");
   �@ }
}