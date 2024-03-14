using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    public void Effect()
    {
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない
        Destroy(newParticle.gameObject, 1.0f);
    }
}