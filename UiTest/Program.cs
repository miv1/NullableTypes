namespace UiTest
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UiTest.Models;
    using UiTest.Services;
    using UiTest.Utils;

    public class Program
    {
        private static List<Todo> todos;
        private static List<User> usersResponse;
        private static Todo createdTodo;
        private static User user;
        private static User createdUser;

        private static TodoService todoService;

        public static void Main(string[] args)
        {
            todoService = new TodoService();

            string email = string.Empty;
            string password = string.Empty;
            bool showMenu = true;
            string option;

            for (int i = 1; i < 4; i++)
            {
                ShowTitle("Login");
                Console.WriteLine("Insert your email and password then press enter");
                Console.WriteLine();
                Console.WriteLine("Email: ");
                email = Console.ReadLine();
                Console.WriteLine("Password: ");
                password = Console.ReadLine();
                Console.WriteLine("Logging in...");

                Task.WaitAll(GetLogInUser(email, password));

                Console.Clear();

                if (user.Name == null)
                {
                    ShowLoginErrorMessage(i);
                }
                else
                {
                    ShowTitleMessage(user);
                    i = 4;
                }
            }

            while (showMenu)
            {
                switch (user.Role)
                {
                    case "Administrator":
                        ShowTitle("Option Menu");
                        Console.WriteLine("1. List/Refresh Users");
                        Console.WriteLine("2. Add a new User");
                        Console.WriteLine("3. Exit");
                        Console.WriteLine("Please insert an option number and then press enter");

                        option = Console.ReadLine();
                        Console.Clear();

                        switch (option)
                        {
                            case "1":
                                ShowTitleMessage(user, "User List");
                                ListUsers();
                                break;
                            case "2":
                                ShowTitleMessage(user, "New User");
                                CreateUserMenu(user);
                                break;
                            case "3":
                                showMenu = false;
                                break;
                            default:
                                Console.WriteLine("Invalid Option");
                                Console.WriteLine("Press enter");
                                Console.ReadLine();
                                break;
                        }

                        break;
                    case "User":
                        ShowTitle("Option Menu");
                        Console.WriteLine("1. List/Refresh ToDos");
                        Console.WriteLine("2. Add a new ToDo");
                        Console.WriteLine("3. Exit");
                        Console.WriteLine("Please insert an option number and then press enter");
                        
                        option = Console.ReadLine();
                        Console.Clear();

                        switch (option)
                        {
                            case "1":
                                ShowTitleMessage(user, "ToDos List");
                                ListTodos(user);
                                break;
                            case "2":
                                ShowTitleMessage(user, "Add ToDo");
                                AddTodo();
                                break;
                            case "3":
                                showMenu = false;
                                break;
                            default:
                                Console.WriteLine("Invalid Option");
                                Console.WriteLine("Press enter");
                                Console.ReadLine();
                                break;
                        }

                        break;
                    case "Guest":
                        Console.Clear();
                        ShowTitleMessage(user, "ToDos List");
                        ListTodos(user);
                        ShowTitle("Option Menu");
                        Console.WriteLine("1. Refresh List");
                        Console.WriteLine("2. Exit");
                        Console.WriteLine("Please insert an option number and then press enter");
                        
                        option = Console.ReadLine();
                        Console.Clear();

                        switch (option)
                        {
                            case "1":
                                continue;
                            case "2":
                                showMenu = false;
                                break;
                            default:
                                Console.WriteLine("Invalid Option");
                                Console.WriteLine("Press a key to continue...");
                                Console.ReadLine();
                                break;
                        }

                        break;
                    default:
                        Console.WriteLine("Acces Denied");
                        showMenu = false;
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static async Task GetTodosTask()
        {
            todos = await todoService.GetAllTodos();
        }

        public static async Task AddTodoTask(string description, User user)
        {
            Todo newTodo = new Todo
            {
                IsActive = true,
                Description = description,
                User = user
            };
            createdTodo = await todoService.CreateNewTodo(newTodo);
        }

        public static void AddTodo()
        {
            string todoDescription;
            do
            {
                Console.WriteLine("The ToDo Description should be less than 50 characteres length!");
                Console.WriteLine(new string('-', 30));
                Console.WriteLine("Enter the new TODO:");
                todoDescription = Console.ReadLine();
            }
            while (todoDescription.Length > 50);

            Task.WaitAll(AddTodoTask(todoDescription, user));
            ListCreatedToDo();
        }

        public static void ListCreatedToDo()
        {
            Console.Clear();
            ShowTitleMessage(user, "ToDo created!");
            Console.WriteLine("Description: " + createdTodo.Description);
            Console.WriteLine();
        }

        public static void ListTodos(User user)
        {
            int constant = 1;
            string[] headers = new string[]
            {
                "ID",
                "Description",
                "Owner",
                "Editable"
            };
            int[] headersSize = new int[]
            {
                headers[0].Length + constant,
                headers[1].Length + constant,
                headers[2].Length + constant,
                headers[3].Length + constant
            };
            string sizeOptionR = "{0, -%value%}";
            string sizeOptionL = "{0, %value%}";
            Task.WaitAll(GetTodosTask());

            foreach (Todo item in todos)
            {
                headersSize[0] = Math.Max(headersSize[0], item.Id.ToString().Length + constant);
                headersSize[1] = Math.Max(headersSize[1], item.Description.ToString().Length + constant);
                headersSize[2] = Math.Max(headersSize[2], item.User.Email.ToString().Length + constant);
            }

            Console.WriteLine(
                "|{0}|{1}|{2}|{3}|",
                string.Format(sizeOptionR.Replace("%value%", headersSize[0].ToString()), headers[0]),
                string.Format(sizeOptionR.Replace("%value%", headersSize[1].ToString()), headers[1]),
                string.Format(sizeOptionR.Replace("%value%", headersSize[2].ToString()), headers[2]),
                string.Format(sizeOptionR.Replace("%value%", headersSize[3].ToString()), headers[3]));
            Console.WriteLine(new string('-', headersSize[0] + headersSize[1] + headersSize[2] + headersSize[3] + 5));

            foreach (Todo item in todos)
            {
                string isEditableText = (user.Role == Constants.UserRole && user.Email == item.User.Email) ? "Yes" : "No";
                string isEditableTextFormatted = string.Format(sizeOptionL.Replace("%value%", (headersSize[3] / 2).ToString()), isEditableText);

                Console.WriteLine(
                    "|{0}|{1}|{2}|{3}|",
                    string.Format(sizeOptionR.Replace("%value%", headersSize[0].ToString()), item.Id),
                    string.Format(sizeOptionR.Replace("%value%", headersSize[1].ToString()), item.Description),
                    string.Format(sizeOptionR.Replace("%value%", headersSize[2].ToString()), item.User.Email),
                    string.Format(sizeOptionR.Replace("%value%", headersSize[3].ToString()), isEditableTextFormatted));
            }

            Console.WriteLine();
        }

        public static async Task GetUsersTask()
        {
            UserService service = new UserService();
            usersResponse = await service.GetAllUsers();
        }

        public static void ListUsers()
        {
            Task.WaitAll(GetUsersTask());
            var sb = new StringBuilder();
            bool line = true;
            sb.Append(string.Format("{0,3}{1,2}{2,12}{3,2}{4,12}{5,2}{6,20}{7,2}{8,12}{9,2}{10,12}\n", "Id", "|", "Name", "|", "Last name", "|", "Email", "|", "Password", "|", "Role"));

            foreach (User user in usersResponse)
            {
                if (line)
                {
                    sb.Append(string.Format("{0,3}{1,2}{2,12}{3,2}{4,12}{5,2}{6,20}{7,2}{8,12}{9,2}{10,12}\n", "---", "--", "------------", "--", "------------", "--", "--------------------", "--", "------------", "--", "------------"));
                    line = false;
                }

                sb.Append(string.Format("{0,3}{1,2}{2,12}{3,2}{4,12}{5,2}{6,20}{7,2}{8,12}{9,2}{10,12}\n", user.Id, "|", user.Name, "|", user.LastName, "|", user.Email, "|", user.Password, "|", user.Role));
            }

            Console.WriteLine(sb);
        }

        public static async Task GetLogInUser(string email, string password)
        {
            UserService userService1 = new UserService();
            user = await userService1.ValidateUser(email, password);
        }

        public static async Task PostUserTask(User newUser)
        {
            UserService service = new UserService();
            createdUser = await service.CreateNewUser(newUser);
        }

        public static void CreateUserMenu(User user)
        {
            User newUser = new User();
            string aux = string.Empty;
            Console.WriteLine("Insert the new user information: ");
            Console.WriteLine(new string('*', 30));
            newUser.Id = 0;
            Console.Write("User name: ");
            aux = Console.ReadLine();
            newUser.Name = aux;
            Console.Write("User last name: ");
            aux = Console.ReadLine();
            newUser.LastName = aux;
            Console.Write("User email: ");
            aux = Console.ReadLine();
            newUser.Email = aux;
            Console.Write("User password: ");
            aux = Console.ReadLine();
            newUser.Password = aux;
            Console.WriteLine("Select role:\n1. User\n2. Guest");
            do
            {
                aux = Console.ReadLine();
            }
            while (!aux.Equals("1") && !aux.Equals("2"));
            newUser.Role = aux.Equals("1") ? "User" : "Guest";
            Task.WaitAll(PostUserTask(newUser));
            ListCreatedUser(user, newUser);
        }

        public static void ListCreatedUser(User user, User createdUser)
        {
            Console.Clear();
            ShowTitleMessage(user, "User created!");
            var sb = new StringBuilder();
            sb.Append(string.Format("{0,12}{1,2}{2,12}{3,2}{4,20}{5,2}{6,12}{7,2}{8,12}\n", "Name", "|", "Last name", "|", "Email", "|", "Password", "|", "Role"));
            sb.Append(string.Format("{0,12}{1,2}{2,12}{3,2}{4,20}{5,2}{6,12}{7,2}{8,12}\n", "------------", "--", "------------", "--", "--------------------", "--", "------------", "--", "------------"));
            sb.Append(string.Format("{0,12}{1,2}{2,12}{3,2}{4,20}{5,2}{6,12}{7,2}{8,12}\n", createdUser.Name, "|", createdUser.LastName, "|", createdUser.Email, "|", createdUser.Password, "|", createdUser.Role));
            Console.WriteLine(sb);
        }

        public static void ShowTitleMessage(User user, string title = null)
        {
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Welcome " + user.Name + " " + user.LastName);
            Console.WriteLine("Logged in as: " + user.Role);
            Console.WriteLine(new string('-', 30));

            if (title != null)
            {
                Console.WriteLine(title);
                Console.WriteLine(new string('-', 30));
            }
        }

        public static void ShowLoginErrorMessage(int i)
        {
            Console.WriteLine("Sorry you dont have permitions");
            Console.WriteLine("You have " + (3 - i) + " chances");
        }

        public static void ShowTitle(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine(new string('-', 30));
        }
    }
}
