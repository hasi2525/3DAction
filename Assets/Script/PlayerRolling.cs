using System.Collections;
using UnityEngine;

public class PlayerRolling : MonoBehaviour
{
    [SerializeField] private float RollingCooldown = 0.5f;
    [SerializeField] private Collider RollingCollider;

    private MobStatus _status;

    void Start()
    {
        _status = GetComponent<MobStatus>();
    }
    /// <summary>
    /// 回避可能なら回避する
    /// </summary>
    public void RollingIfPossible()
    {
        if (!_status.IsRolling) return;

        _status.GoToRollingStateIfPossible();
    }
    /// <summary>
    /// 回避開始時に呼ばれ、自分のコライダーを無効
    /// </summary>
    public void OnRollingStart()
    {
        RollingCollider.enabled = false;
    }
    /// <summary>
    /// 回避が終了した時に呼ばれ、クールダウンを開始する
    /// </summary>
    public void OnRollingFinished()
    {
        RollingCollider.enabled = true;
        StartCoroutine(CooldownCoroutine());
    }
    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(RollingCooldown);
        _status.GoToNormalStateIfPossible();
    }
}