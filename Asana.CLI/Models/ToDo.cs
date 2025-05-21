using System;
using Asana.CLI.Interfaces;

namespace MyApp.Models
{
    public class ToDo
    {
        public ToDo(string name, int projectId, IIdGenerator toDoIdGenerator)
        {
            this.name = name;
            this.projectId = projectId;
            id = toDoIdGenerator.GetNextId();
        }
        private int? id;
        public int? Id
        {
            get { return id; }
            set
            {
                if (value != id)
                    id = value;
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                    name = value;
            }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set
            {
                if (value != description)
                    description = value;
            }
        }

        private int? priority;
        public int? Priority
        {
            get { return priority; }
            set
            {
                if (value != priority)
                    priority = value;
            }
        }

        private bool isComplete = false;
        public bool IsComplete
        {
            get { return isComplete; }
            set
            {
                if (value != isComplete)
                    isComplete = value;
            }
        }

        private int? projectId;
        public int? ProjectId
        {
            get { return projectId; }
            set
            {
                if (value != projectId)
                    projectId = value;
            }
        }

        
        public override string ToString()
        {
            return $"{name} - {isComplete} - {description}";
        }
    }
}