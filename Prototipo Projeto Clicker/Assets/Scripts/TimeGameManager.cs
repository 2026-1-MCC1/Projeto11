using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private IdleGameSystem idleSystem;

    //Guarda o último momento que o jogador esteve online
    private DateTime lastTime;

    void Start()
    {
        idleSystem = new IdleGameSystem();

        //Aqui ele define o tempo salvo
        //eu coloquei uma simulação de 2 minutos offline, da pra alterar dps
        lastTime = DateTime.UtcNow.AddMinutes(-2);

        //Tempo atual
        DateTime currentTime = DateTime.UtcNow;

        //Aplica progresso offline
        idleSystem.ApplyOfflineProgress(lastTime, currentTime);
    }

    void Update()
    {
        //Geração em tempo real
        idleSystem.totalClicks += idleSystem.CalculateProduction() * Time.deltaTime;
    }

    //Simula sair do jogo
    void OnApplicationQuit()
    {
        lastTime = DateTime.UtcNow;

        Debug.Log("Saiu do jogo em: " + lastTime);
    }

}
