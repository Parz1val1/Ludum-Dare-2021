using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public static class TimerManager
    {
        public static float defaultTimeRemaining = 60;
        private static float timeRemaining = 60;
        private static bool isTimerRunning = false;

        public static void AddTimeRemaining(float time)
        {
            timeRemaining += time;
        }

        public static void SubtractTimeRemaining(float time)
        {
            timeRemaining -= time;
        }

        public static void SetTimeRemaining(float time)
        {
            timeRemaining = time;
        }

        public static float GetTimeRemaining()
        {
            return timeRemaining;
        }

        public static string GetDisplayTimeRemaining()
        {
            float timeToDisplay = timeRemaining;
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            return string.Format("Full system reset in {0:00}:{1:00}", minutes, seconds);
        }

        public static void ResetTimer()
        {
            timeRemaining = defaultTimeRemaining;
            isTimerRunning = false;
        }

        public static void StartTimer()
        {
            isTimerRunning = true;
        }

        public static void PauseTimer()
        {
            isTimerRunning = false;
        }

        public static void StopTimer()
        {
            isTimerRunning = false;
            timeRemaining = 0;
        }

        public static bool IsTimerRunning()
        {
            return isTimerRunning;
        }
    }
}
