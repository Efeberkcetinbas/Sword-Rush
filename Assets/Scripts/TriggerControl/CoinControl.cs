using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : Obstacable
{
    [SerializeField] private ParticleSystem moneyParticle;

    [SerializeField] private GameObject coin;
    internal override void DoAction(PlayerTrigger player)
    {
        MoneyManager.Instance.UpdateMoney();
        //Debug.Log(PlayerPrefs.GetInt("money"));
        PlayParticle();
        coin.SetActive(false);
    }

    private void PlayParticle()
    {
        moneyParticle.Play();
    }

    internal override void InteractionExit(PlayerTrigger player)
    {
        Destroy(gameObject,1);
    }
}
