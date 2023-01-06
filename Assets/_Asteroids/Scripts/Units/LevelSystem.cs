using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static event Action<float, float, int, bool> ExperienceChanged;

    private int level;
    private int experience;
    private int experienceToNextLevel;

    private void Awake()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 20;
    }

    public void AddExperience(int amount)
    {
        var percentageBefore = (float) experience / experienceToNextLevel;

        experience += amount;
        var percentageAfter = (float) experience / experienceToNextLevel;

        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            experienceToNextLevel += experienceToNextLevel / 2;

            percentageAfter = (float) experience / experienceToNextLevel;
            ExperienceChanged?.Invoke(percentageBefore, percentageAfter, level, true);
        }
        else
        {
            ExperienceChanged?.Invoke(percentageBefore, percentageAfter, level, false);
        }
    }
}