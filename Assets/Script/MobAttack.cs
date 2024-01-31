using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider; // Collider�^�𐳂����w��

    private MobStatus _status;

    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    /// <summary>
    /// �U���\�ȏ�ԂȂ�U�������݂�B
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();
    }

    /// <summary>
    /// ����̑Ώۂ��U���͈͂ɓ���΍U�������݂�B
    /// </summary>
    /// <param name="collider">�U���͈͂ɓ������Ώۂ̃R���C�_�[</param>
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    /// <summary>
    /// �U���J�n���ɌĂ΂�A�U���p�R���C�_�[��L���ɂ���B
    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    /// <summary>
    /// �U���R���C�_�[���ΏۂɃq�b�g������Ă΂�A�ΏۂɃ_���[�W��^����
    /// </summary>
    /// <param name="collider">�U�����q�b�g�����Ώۂ̃R���C�_�[</param>
    public void OnHitAttack(Collider collider)
    {
        MobStatus targetPlayer = collider.GetComponent<MobStatus>();
        if (targetPlayer != null)
        {
            targetPlayer.Damage(1);
        }
    }

    /// <summary>
    /// �U�����I���������ɌĂ΂�A�U���p�R���C�_�[�𖳌��ɂ��A�N�[���_�E�����J�n����
    /// </summary>
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
    }
}
