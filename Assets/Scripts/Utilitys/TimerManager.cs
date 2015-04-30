﻿using QuickUnity.Components;
using QuickUnity.DesignPattern;
using QuickUnity.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Utilitys namespace.
/// </summary>
namespace QuickUnity.Utilitys
{
    /// <summary>
    /// Hold and manage all timer objects.
    /// </summary>
    public class TimerManager : BehaviourSingleton<TimerManager>
    {
        /// <summary>
        /// Delegate OnTimerDelegate
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public delegate void OnTimerDelegate(float deltaTime);

        /// <summary>
        /// Occurs when [on timer].
        /// </summary>
        public event OnTimerDelegate OnTimer;

        /// <summary>
        /// The timer dictionary.
        /// </summary>
        private Dictionary<string, Timer> mTimers;

        /// <summary>
        /// The global timer.
        /// </summary>
        private Timer globalTimer;

        /// <summary>
        /// Gets or sets the delay time of global timer.
        /// </summary>
        /// <value>The delay.</value>
        public float Delay
        {
            get { return (globalTimer != null) ? globalTimer.Delay : 0.0f; }
            set
            {
                if (globalTimer != null)
                    globalTimer.Delay = value;
            }
        }

        /// <summary>
        /// Gets the current count of global timer.
        /// </summary>
        /// <value>The current count.</value>
        public int CurrentCount
        {
            get { return (globalTimer != null) ? globalTimer.CurrentCount : 0; }
        }

        /// <summary>
        /// Awake this script.
        /// </summary>
        private void Awake()
        {
            mTimers = new Dictionary<string, Timer>();
            globalTimer = new Timer(1.0f);
            globalTimer.AddEventListener(TimerEvent.TIMER, OnGlobalTimerHandler);
        }

        /// <summary>
        /// Called when [global timer handler].
        /// </summary>
        /// <param name="eventObj">The event object.</param>
        private void OnGlobalTimerHandler(Events.Event eventObj)
        {
            TimerEvent timerEvent = eventObj as TimerEvent;
            Delegate[] delegates = OnTimer.GetInvocationList();

            if (delegates.Length > 0)
                OnTimer(timerEvent.DeltaTime);
        }

        /// <summary>
        /// Update in fixed time.
        /// </summary>
        private void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;

            if (globalTimer != null)
                globalTimer.Update(deltaTime);

            foreach (KeyValuePair<string, Timer> kvp in mTimers)
            {
                Timer timer = kvp.Value;
                timer.Update(deltaTime);
            }
        }

        /// <summary>
        /// The global timer start timing.
        /// </summary>
        public void Start()
        {
            if (globalTimer != null)
                globalTimer.Start();
        }

        /// <summary>
        /// The global timer resets timing. Set currentCount to 0.
        /// </summary>
        public void Reset()
        {
            if (globalTimer != null)
                globalTimer.Reset();
        }

        /// <summary>
        /// The global timer stop timing.
        /// </summary>
        public void Stop()
        {
            if (globalTimer != null)
                globalTimer.Stop();
        }

        /// <summary>
        /// Gets the timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Timer.</returns>
        public Timer GetTimer(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (mTimers.ContainsKey(name))
                return mTimers[name];

            return null;
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start timer].</param>
        public void AddTimer(string name, Timer timer, bool autoStart = true)
        {
            if (GetTimer(name) != null)
                return;

            if (!string.IsNullOrEmpty(name))
            {
                mTimers.Add(name, timer);

                if (autoStart)
                    timer.Start();
            }
        }

        /// <summary>
        /// Removes the timer by timer name.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        public void RemoveTimer(string name)
        {
            Timer timer = GetTimer(name);

            if (timer != null)
                RemoveTimer(timer);
        }

        /// <summary>
        /// Removes the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        public void RemoveTimer(Timer timer)
        {
            if (GetTimer(name) != null)
                mTimers.Remove(name);
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        public void RemoveAllTimers()
        {
            mTimers.Clear();
        }

        /// <summary>
        /// Starts all timers.
        /// </summary>
        /// <param name="includeGlobalTimer">if set to <c>true</c> [include global timer].</param>
        public void StartAllTimers(bool includeGlobalTimer = true)
        {
            if (includeGlobalTimer && globalTimer != null)
                globalTimer.Start();

            foreach (KeyValuePair<string, Timer> kvp in mTimers)
            {
                Timer timer = kvp.Value;
                timer.Start();
            }
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        /// <param name="includeGloablTimer">if set to <c>true</c> [include gloabl timer].</param>
        public void ResetAllTimers(bool includeGlobalTimer = true)
        {
            if (includeGlobalTimer && globalTimer != null)
                globalTimer.Reset();

            foreach (KeyValuePair<string, Timer> kvp in mTimers)
            {
                Timer timer = kvp.Value;
                timer.Reset();
            }
        }

        /// <summary>
        /// Stops all timers.
        /// </summary>
        /// <param name="includeGlobalTimer">if set to <c>true</c> [include global timer].</param>
        public void StopAllTimers(bool includeGlobalTimer = true)
        {
            if (includeGlobalTimer && globalTimer != null)
                globalTimer.Stop();

            foreach (KeyValuePair<string, Timer> kvp in mTimers)
            {
                Timer timer = kvp.Value;
                timer.Stop();
            }
        }
    }
}