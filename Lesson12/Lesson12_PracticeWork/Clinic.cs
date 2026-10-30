using System;
namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using System.Threading;
using static System.Array;

public class Hospital
{
  private PriorityQueue<Patient, int> _queue = new PriorityQueue<Patient, int>();


public void RegisterPatient(Patient patient)
{
    if (patient == null) return;
    _queue.Enqueue(patient, patient.PriorityValue);

    WriteLine($"[Регистратура] Пациент {patient.Name} добавлен в очередь. Приоритет: {patient.PriorityValue}");
}


public void AdmitNextPatient()
{
    if (_queue.Count == 0)
    {
        WriteLine("[Кабинет] В очереди никого нет. Врач отдыхает.");
        return;
    }

    Patient nextPatient = _queue.Dequeue();

    WriteLine($"[Врач] На прием заходит: {nextPatient.Name} | Диагноз: {nextPatient.PatientDiagnosis} (Срочность: {nextPatient.PriorityValue})");
}


    public int GetWaitingCount()
    {
        return _queue.Count;
    }
}
