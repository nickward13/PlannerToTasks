using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerToTasksConsoleApp
{

    public class TasksApiResponse
    {
        public string odatacontext { get; set; }
        public int odatacount { get; set; }
        public List<PlannerTask> value { get; set; }
    }

    public class PlannerTask
    {
        public string odataetag { get; set; }
        public string planId { get; set; }
        public string planName { get; set; }
        public string bucketId { get; set; }
        public string title { get; set; }
        public string orderHint { get; set; }
        public string assigneePriority { get; set; }
        public int percentComplete { get; set; }
        public DateTime? startDateTime { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime? dueDateTime { get; set; }
        public bool hasDescription { get; set; }
        public string previewType { get; set; }
        public DateTime? completedDateTime { get; set; }
        public Completedby completedBy { get; set; }
        public int referenceCount { get; set; }
        public int checklistItemCount { get; set; }
        public int activeChecklistItemCount { get; set; }
        public string conversationThreadId { get; set; }
        public string id { get; set; }
        public Createdby createdBy { get; set; }
    }

    public class Completedby
    {
        public User user { get; set; }
    }

    public class User
    {
        public object displayName { get; set; }
        public string id { get; set; }
    }

    public class Createdby
    {
        public User user { get; set; }
    }

}
