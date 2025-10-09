// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

class Barrier :IDisposable
{
    public Barrier(Int32 participantCount, Action<Barrier> postPhaseAction) { }
    public void Dispose() { }

    public Int64 AddParticipants(Int32 participantCount)
    { return 0; } // Добавление участников
    public Int64 RemoveParticipants(Int32 participants)
    {return 0; } // Удаление участников
    public void SignalAndWait(Int32 millisecondssTimeout, CancellationToken cancellationToken) { }
    public Int64 CurrentPhaseNumber { get; }    // Показывает фазы процесса
                                                // начиная с 0
    public Int32 ParticipantCount { get; }      // Количество участников
    public Int32 ParticipantsRemaining { get; } // Число потоков, необходимых
                                                // для вызова SignalAndWait
}