using System;
using Soft.Example.Test.Autofac._1_GettingStarted.Interfaces;

namespace Soft.Example.Test.Autofac._1_GettingStarted.Implementation
{
    public class TodayWriter : IDateWriter
    {
        private readonly IOutput _output;

        public TodayWriter(IOutput output)
        {
            _output = output;
        }

        public void WriteDate()
        {
            Console.WriteLine("TodayWriter HashCode " + GetHashCode());
            _output.Write(DateTime.Today.ToShortDateString());
        }
    }
}