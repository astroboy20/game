using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxItem : MonoBehaviour {
    public enum ITEM { MAGNET, X2, SHIELD}
    public GameObject destroyFX;
    public AudioClip soundCollect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    ActiveMagnet();
                    break;
                case 1:
                    ActiveX2();
                    break;
                case 2:
                    ActiveShield();
                    break;
                default:break;
            }
        }

        if (destroyFX)
            Instantiate(destroyFX, transform.position, Quaternion.identity);
        SoundManager.PlaySfx(soundCollect);

        Destroy(gameObject);
    }

    void ActiveMagnet()
    {

    }

    void ActiveX2()
    {

    }

    void ActiveShield()
    {

    }
}
