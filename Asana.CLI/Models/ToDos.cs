using System;

namespace MyApp.Models
{
    public class ToDos
    {
        private int? id;
        public int? Id
        {
            get { return id; }
            set
            {
                if (value != id)
                    value = id;
            }
        }

        private string? name;
        public string? Name
        {
            get { return name; }
            set
            {
                if (value != name)
                    value = name;
            }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (value != description)
                    value = description;
            }
        }

        private int? priority;
        public int? Priority
        {
            get { return priority; }
            set
            {
                if (value != priority)
                    value = priority;
            }
        }

        private bool? isComplete;
        public bool? IsComplete
        {
            get { return isComplete; }
            set
            {
                if (value != isComplete)
                    value = isComplete;
            }
        }
        
        private int? projectId;
        public int? ProjectId
        {
            get { return projectId; }
            set
            {
                if (value != projectId)
                    value = projectId;
            }
        }
    }
}