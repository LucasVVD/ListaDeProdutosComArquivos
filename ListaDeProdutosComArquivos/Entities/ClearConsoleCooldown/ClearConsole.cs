using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ListaDeProdutosComArquivos.Entities.ClearConsoleCooldown
{
    internal static class ClearConsole
    {
        static Timer? timer;

        public static void Cooldown()
        {
            TimerState timerState = new TimerState { Counter = 0 };

            timer = new Timer(
                callback: new TimerCallback(TimerTask),
                state: timerState,
                dueTime: 1000,
                period: 2000);

            timer.Dispose();
            Console.WriteLine("Pressione qualquer tecla para continuar!\n");
            Console.ReadKey();
            Console.Clear();
        }

        static void TimerTask(object? timerState)
        {
            TimerState state = (TimerState)timerState;
            Interlocked.Increment(ref state.Counter);
        }
    }

    file class TimerState
    {
        public int Counter;
    }
}

