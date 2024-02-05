using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();

    }
    
    private void Update()
    {
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }
    protected override void OnDie()
    {
        //�|���ꂽ���̏��ŃR���[�`��
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }
    //�Q�b��ɏ�����
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}