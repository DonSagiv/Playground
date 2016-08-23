﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrency
{
    class Program
    {
        public static ConcurrentCircularBuffer circularBuffer = new ConcurrentCircularBuffer(5);

        static void Main(string[] args)
        {
            var ints = Enumerable.Range(1, 20).ToList();

            circularBuffer.ValueChanged += CircularBuffer_ValueChanged;

            foreach (var item in ints)
            {
                Task.Run(() =>
                {
                    circularBuffer.addToQueue(item);
                    Thread.Sleep(500);
                });

                ConcurrentCircularBuffer.autoResetEvent.WaitOne();
            }
                
            Console.ReadLine();
        }

        private static void CircularBuffer_ValueChanged(int obj)
        {
            Console.WriteLine(obj);
        }
    }
}
