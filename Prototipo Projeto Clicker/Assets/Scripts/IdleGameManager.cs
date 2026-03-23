using System;
using UnityEngine;

public class IdleGameSystem
{
    // Dados do jogo
    public double totalClicks = 0;
    public double baseRate = 1;
    public double multiplier = 1;
    public int upgrades = 1;

    // Config
    private double maxOfflineSeconds = 86400; // 24 horas

    // Produção por segundo
    public double CalculateProduction()
    {
        return baseRate * multiplier * upgrades;
    }

    // Calcula progresso offline baseado no tempo que você passar
    public void ApplyOfflineProgress(DateTime lastTime, DateTime currentTime)
    {
        double secondsAway = (currentTime - lastTime).TotalSeconds;

        // Proteção contra tempo negativo
        if (secondsAway < 0)
            secondsAway = 0;

        // Limite de tempo offline
        secondsAway = Math.Min(secondsAway, maxOfflineSeconds);

        double productionPerSecond = CalculateProduction();
        double offlineClicks = secondsAway * productionPerSecond;

        totalClicks += offlineClicks;

        Debug.Log($"Tempo offline: {secondsAway} segundos");
        Debug.Log($"Produção por segundo: {productionPerSecond}");
        Debug.Log($"Ganhou offline: {offlineClicks} cliques");
        Debug.Log($"Total atual: {totalClicks}");
    }
}
