namespace Todo
{
    class TodoList()
    {
        List<string> tasks = new List<string>();

        // public TodoList()
        // {
        //     tasks = new List<string>();
        // }

        public void addTask(string task)
        {
            if (tasks.Contains(task))
            {
                Console.WriteLine("Already pending..");
                return;
            }
            tasks.Add(task);
        }

        public void removeTask(string task)
        {
            if (!tasks.Contains(task))
            {
                Console.WriteLine("Task is not there...");
                return;
            }

            tasks.Remove(task);
            Console.WriteLine("Congrats for completing the task......");
        }
    }
}