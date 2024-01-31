using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Collider attackCollider; // Collider型を正しく指定

    private MobStatus _status;

    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    /// <summary>
    /// 攻撃可能な状態なら攻撃を試みる。
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;

        _status.GoToAttackStateIfPossible();
    }

    /// <summary>
    /// 特定の対象が攻撃範囲に入れば攻撃を試みる。
    /// </summary>
    /// <param name="collider">攻撃範囲に入った対象のコライダー</param>
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    /// <summary>
    /// 攻撃開始時に呼ばれ、攻撃用コライダーを有効にする。
    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    /// <summary>
    /// 攻撃コライダーが対象にヒットしたら呼ばれ、対象にダメージを与える
    /// </summary>
    /// <param name="collider">攻撃がヒットした対象のコライダー</param>
    public void OnHitAttack(Collider collider)
    {
        MobStatus targetPlayer = collider.GetComponent<MobStatus>();
        if (targetPlayer != null)
        {
            targetPlayer.Damage(1);
        }
    }

    /// <summary>
    /// 攻撃が終了した時に呼ばれ、攻撃用コライダーを無効にし、クールダウンを開始する
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
