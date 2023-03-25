using System;
using NUnit.Framework;
using UnityEngine;
using VRC.SDK3.Components;

public class SomeTest
{
    [Test]
    public void SuccessfulTest()
    {
        // Successful test
    }

    [Test]
    public void SecretTest()
    {
        // Successful test
        Debug.Log($"UNITY_LICENSE env variable: {Environment.GetEnvironmentVariable("UNITY_LICENSE")}");
    }

    // Reference VRCSDK
    private AbstractUdonBehaviour _behaviour;
}
