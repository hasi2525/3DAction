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
        //ì|Ç≥ÇÍÇΩéûÇÃè¡ñ≈ÉRÉãÅ[É`Éì
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }
    //ÇQïbå„Ç…è¡Ç¶ÇÈ
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}