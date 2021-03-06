﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Awake
{
    internal class Awake
    {
        private static readonly DateTime EndTime = DateTime.Now.AddHours(8);
        private static readonly Random Random = new Random();
        private static ExecutionState State => ExecutionState.EsContinuous | ExecutionState.EsAwayModeRequired | ExecutionState.EsDisplayRequired | ExecutionState.EsSystemRequired;

        private static void Main()
        {
            while (DateTime.Now < EndTime)
            {
                SetThreadExecutionState(State);
                Thread.Sleep(Random.Next(6000, 180000));
            } 
        }

        [FlagsAttribute]
        private enum ExecutionState : uint
        {
            EsAwayModeRequired = 0x00000040,
            EsContinuous = 0x80000000,
            EsDisplayRequired = 0x00000002,
            EsSystemRequired = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);
    }
}
