﻿using QuickUnity.Patterns;

/// <summary>
/// The Singleton namespace.
/// </summary>
namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestSingletonTwo.
    /// </summary>
    public class TestSingletonTwo : Singleton<TestSingletonTwo>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="TestSingletonTwo"/> class from being created.
        /// </summary>
        private TestSingletonTwo()
        {
            UnityEngine.Debug.Log("TestSingletonTwo");
        }

        public void Run()
        {
            UnityEngine.Debug.Log("TestSingletonTwo Run!");
        }
    }
}