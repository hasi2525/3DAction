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
    /// ����\�Ȃ�������
    /// </summary>
    public void RollingIfPossible()
    {
        if (!_status.IsRolling) return;

        _status.GoToRollingStateIfPossible();
    }
    /// <summary>
    /// ����J�n���ɌĂ΂�A�����̃R���C�_�[�𖳌�
    /// </summary>
    public void OnRollingStart()
    {
        RollingCollider.enabled = false;
    }
    /// <summary>
    /// ������I���������ɌĂ΂�A�N�[���_�E�����J�n����
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