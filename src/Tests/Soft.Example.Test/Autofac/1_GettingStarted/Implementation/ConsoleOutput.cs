using System;
using Soft.Example.Test.Autofac._1_GettingStarted.Interfaces;

namespace Soft.Example.Test.Autofac._1_GettingStarted.Implementation
{
    /// <summary>
    /// Implementacion de la interfaz que escribe en consola
    /// Tambien se puede implemebtar para que escriba en 
    /// debug, trace, log etc
    /// </summary>
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine("ConsoleOutput HashCode " + GetHashCode());
            Console.WriteLine(content);
        }
    }
}