using System;
using System.IO;
namespace ReminderApplication
{

    public class ReminderApp
    {
        List<Reminder> reminders = new List<Reminder>();
        private string storagePath = "reminders.csv";
        public ReminderApp()
        {
            LoadFromFile();
            while (true)
            {

                Console.WriteLine("===== Reminder App =====");
                Console.WriteLine("1. Add Reminder" + "\t\t" + "2. View All Reminders");
                Console.WriteLine("3. Search by Date" + "\t" + "4. Delete Reminder");
                Console.WriteLine("5. Exit");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            addReminder();
                            break;
                        case 2:
                            viewReminders();
                            break;
                        case 3:
                            searchReminders();
                            break;
                        case 4:
                            deleteReminder();
                            break;
                        default:
                            return;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }

        private void addReminder()
        {
            try
            {
                Console.Write("Enter Date (foramt: dd/mm/yy): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Date!!");
                    Console.ResetColor();
                    return;
                }
                Console.WriteLine("Enter reminder: ");
                string desc = Console.ReadLine();
                reminders.Add(new Reminder() { Date = date, Desc = desc });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("New reminder added!!");
                Console.ResetColor();
                Console.WriteLine();
                reminders.Sort((a, b) => a.Date.CompareTo(b.Date));
                File.WriteAllLines(storagePath, reminders.Select(r => r.ToString()));

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void viewReminders()
        {

            if (reminders.Count == 0)
            {
                Console.WriteLine("No reminders.......");
                return;
            }
            Console.WriteLine("---------------Reminders------------------");
            for (int i = 0; i < reminders.Count; ++i)
            {
                if (reminders[i].Date < DateTime.Now)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (reminders[i].Date > DateTime.Now)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{i + 1}. Date: {reminders[i].Date.ToShortDateString()} Desc: {reminders[i].Desc}");
            }
            Console.ResetColor();
            Console.WriteLine();

        }

        private void deleteReminder()
        {
            if (reminders.Count == 0)
            {
                Console.WriteLine("No reminders.....");
                return;
            }
            Console.WriteLine("Enter reminder number to delete: ");
            int position = Convert.ToInt32(Console.ReadLine());
            reminders.RemoveAt(position - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Reminder deleted successfully..");
            Console.ResetColor();

        }

        private void searchReminders()
        {
            Console.Write("Enter date (format: dd/mm/yy): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            var searchResult = reminders.Where(reminder => reminder.Date == date);
            if (searchResult.ToList().Count == 0)
            {
                Console.WriteLine("No reminders at that date...");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (Reminder reminder in searchResult)
            {
                Console.WriteLine($"\t- {reminder.Desc}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public class Reminder
        {
            public DateTime Date { get; set; }
            public string Desc { get; set; }
            public override string ToString()
            {
                return $"{Date.ToString()},{Desc}";
            }

            public static Reminder FromCsv(string line)
            {
                var parts = line.Split(',');
                return new Reminder() { Date = DateTime.Parse(parts[0]), Desc = string.Join(",", parts.Skip(1)) };
            }
        }
        void LoadFromFile()
        {
            if (File.Exists(storagePath))
            {
                reminders = File.ReadAllLines(storagePath).Select(line => Reminder.FromCsv(line)).ToList();
            }
        }

    }
}