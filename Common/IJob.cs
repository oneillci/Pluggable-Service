using System;
using System.Linq;
using Quartz;

namespace Common
{
    //public interface IJob
    //{
    //    void Execute();
    //    void Start();
    //    void Stop();
    //    string CronSchedule { get; }
    //}

    public interface IObgJob : IJob
    {
        string CronSchedule { get; }
    }

    public interface IJobMetadata
    {
        string Description { get; }
    }
}
