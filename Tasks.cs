using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TaskManagerV2
{
    [Serializable]
    public class Tasks // для сериализации надо public
    {
        public List<Task> TaskList { get; set; } = new List<Task>();
    }
    [Serializable]
    public class Task
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Boolean Done { get; set; }
        public Task() { }  // для сериализации нужен конструктор без параметров
        public Task(string TaskName, string TaskDescription, DateTime DateStart, DateTime DateEnd, Boolean Done)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            this.DateStart = DateStart;
            this.DateEnd = DateEnd;
            this.Done = Done;
        }
    }
}
