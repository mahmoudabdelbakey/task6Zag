using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Xml.Linq;



namespace task6_Zag
{

    class Product
    {
        public int Id { set; get; }
        public string Name  { set; get; }
        public decimal Price { set; get; }
        public string Category { set; get; }

        public Product(int id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        // [يقارن references contains  عشان ال 
        // Override Equals و GetHashCode بحيث يقارن القيم
        public override bool Equals(object obj)
        {
            if (obj is not Product other) return false;
            return Id == other.Id &&
                   Name == other.Name &&
                   Price == other.Price &&
                   Category == other.Category;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Price, Category);
        }

        // Override ToString() علشان يطبع الخصائص بدل اسم الكلاس
        public override string ToString()
        {
            return $"{Id}   {Name}   {Price}   {Category}";
        }

        
    }

    public record Employee(string Name, string Department, decimal Salary);

    public record Course(string Title, List<string> Students);


    internal class Program
    {

        // Generic pagination method
        public static List<T> Paginate<T>(List<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        
        static void Main(string[] args)
        {

            /////////////////////////////////////----- Q1 -----///////////////////////////////////////
            //List<int> numbers = [3, 18, 7, 42, 10, 5, 29, 14, 6, 100];

            //// TODO: Write the query using Query Syntax
            //var resultQ = from n in numbers
            //              where n > 10 && n % 2 == 0
            //              orderby n descending
            //              select n;

            //foreach (var number in resultQ)
            //{
            //    Console.Write(number + "  ");
            //}
            //Console.WriteLine();

            //// TODO: Write the same query using Fluent Syntax
            //var resultF = numbers.Where(n => n > 10 && n % 2 == 0)
            //                     .OrderByDescending(n => n);

            //foreach (var number in resultF)
            //{
            //    Console.Write(number + "  ");
            //}
            //Console.WriteLine();




            /////////////////////////////////////----- Q2 -----///////////////////////////////////////

            List<Product> products = new List<Product>
            {
                new Product(1, "Laptop", 1200m, "Electronics"),
                new Product(2, "Phone", 800m, "Electronics"),
                new Product(3, "Desk", 350m, "Furniture"),
                new Product(4, "Chair", 150m, "Furniture"),
                new Product(5, "Headphones", 200m, "Electronics"),
            };


            //// 1. Get the first Electronics product
            //try
            //{
            //    var firstElectronics = products.First(p => p.Category == "Electronics");
            //    Console.WriteLine($"First: {firstElectronics.Name}");
            //}
            //catch (InvalidOperationException)
            //{
            //    Console.WriteLine("No Electronics product found");
            //}

            //var firstElectronicsOrDefault =
            //    products.FirstOrDefault(p => p.Category == "Electronics");

            //if (firstElectronicsOrDefault != null)
            //    Console.WriteLine($"FirstOrDefault: {firstElectronicsOrDefault.Name}");
            //else
            //    Console.WriteLine("FirstOrDefault returned null");

            //// 2. Get the last product with Price > 1000 (use OrDefault — handle null)

            //try
            //{
            //    var lastProductGreaterThan1000 = products.Last(p => p.Price > 1000);
            //    Console.WriteLine($"Last: {lastProductGreaterThan1000.Price}");
            //}
            //catch (InvalidOperationException)
            //{
            //    Console.WriteLine("No price product found");
            //}

            //var lastOrDefaultProductGreaterThan1000 = products.LastOrDefault(p => p.Price > 1000);

            //if (lastOrDefaultProductGreaterThan1000 != null)
            //    Console.WriteLine($"LastOrDefault: {lastOrDefaultProductGreaterThan1000.Price}");
            //else
            //    Console.WriteLine("LastOrDefault returned null");


            //// 3. Get the single Furniture item with Price > 300 (what if >1 match?)

            //try 
            //{
            //    var single = products.Single(p => p.Category == "Furniture" && p.Price > 300);
            //    Console.WriteLine($"Single: {single.Name}");
            //}
            //catch (InvalidOperationException)
            //{
            //    Console.WriteLine("Invalid Operation");
            //}

            //try
            //{
            //    var singleOrDefault =
            //        products.SingleOrDefault(p => p.Category == "Furniture" && p.Price > 100);

            //    if (singleOrDefault != null)
            //        Console.WriteLine($"SingleOrDefault: {singleOrDefault.Name}");
            //    else
            //        Console.WriteLine("SingleOrDefault returned null");
            //}
            //catch (InvalidOperationException)
            //{
            //    Console.WriteLine("Invalid Operation");
            //}

            //// 4. Get the element at index 3

            //try
            //{
            //    var elementAtIndex3 = products.ElementAt(3);
            //    Console.WriteLine($"Element at index [3]: {elementAtIndex3.Name}");
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    Console.WriteLine("Index out of range");
            //}

            /////////////////////////////////////----- Q3 -----///////////////////////////////////////

            ////1. Are ALL products priced above 100? → bool
            //bool allAbove100 = products.All(p => p.Price > 100);
            //Console.WriteLine(allAbove100);

            //// 2.Is THERE ANY product in the "Gaming" category ? → bool
            //bool anyProductGaming = products.Any(p => p.Category == "Gaming");
            //Console.WriteLine(anyProductGaming);

            ////3. Does the collection CONTAIN a product named "Chair"?
            ////(use the default comparer on the record)
            //// بتاع الكلاس  object  والcontains هنا عشان نقارن بين الاوبجكت الي في ال 
            //// مش قيم  references  الي بيتم انه بيقارن 
            ////Override Equals و GetHashCode بحيث يقارن القيم عشان كده استخدمت 
            //var chairName = products.Contains(new Product(4, "Chair", 150m, "Furniture"));
            //Console.WriteLine(chairName);


            ////4.Are ALL Electronics products priced above 500 ? → bool
            //bool allAbove500 = products.All(p => p.Category == "Electronics" && p.Price > 500);
            //Console.WriteLine(allAbove500);

            ////5.Is there ANY product cheaper than 200 ? → bool
            //bool anyabove200 = products.Any(p => p.Price > 200);
            //Console.WriteLine(anyabove200);

            /////////////////////////////////////----- Q4 -----///////////////////////////////////////



            ////1. Convert to Array
            //Product[] productsArray = products.ToArray();
            //Console.WriteLine("Array:");
            //foreach (var p in productsArray)
            //    Console.WriteLine(p);

            //Console.WriteLine();
            ////2. Convert to Dictionary keyed by Id

            //try
            //{
            //    Dictionary<int, Product> productsDict = products.ToDictionary(p => p.Id);
            //    Console.WriteLine("Dictionary keyed by Id:");
            //    foreach (var p in productsDict)
            //        Console.WriteLine($"{p.Key} -> {p.Value}");
            //}
            ////try,catch عشان كده عملنا  Exception لو في تكرر هيرمي 
            //catch (ArgumentException)
            //{
            //    Console.WriteLine("Keys are duplicated");
            //}

            //Console.WriteLine();

            ////3.Convert to HashSet of product Names
            //HashSet<string> productNames = products.Select(p => p.Name).ToHashSet();
            //Console.WriteLine("HashSet of product Names:");
            //foreach (var name in productNames)
            //    Console.WriteLine(name);

            ////4.Convert to Lookup keyed by Category
            //ILookup<string, Product> productCategory = products.ToLookup(p => p.Category);
            //Console.WriteLine("Lookup keyed by Category");
            //foreach (var category in productCategory)
            //{
            //    Console.WriteLine($"{category.Key}:");
            //    foreach (var p in category)
            //        Console.WriteLine($"  {p}");
            //}


            //// ToDictionary() => Exception لازم المفتاح يبقي فريد وميتكررش ولو اتكرر بيرمي 
            //// ToLookup() => Exception ممكن المفتاح يبقي في اكتر من قيمه ووقتها مبيرميش اي 

            /////////////////////////////////////----- Q5 -----///////////////////////////////////////

            //List<string> orders = new List<string>
            //{
            //    "ORD-001", "ORD-002", "ORD-003",
            //    "ORD-004", "ORD-005", "ORD-006", "ORD-007"
            //};

            //int pageSize = 3;

            //// 1. Get Page 1 (items 1–3)
            //var page1 = orders.Take(pageSize).ToList();
            //Console.WriteLine("Page 1:");
            //foreach(var p in page1)
            //    Console.WriteLine(p);

            //Console.WriteLine();
            //// 2. Get Page 2 (items 4–6)  ← use Skip + Take
            //var page2 = orders.Skip(pageSize).Take(pageSize).ToList();
            //Console.WriteLine("Page 2:");
            //foreach(var p in page2)
            //    Console.WriteLine(p);

            //Console.WriteLine();
            //// 3. Get the last 2 orders using TakeLast
            //var lastTwoOrders = orders.TakeLast(2);
            //Console.WriteLine("Last Two Orders: ");
            //foreach(var p in lastTwoOrders)
            //    Console.WriteLine(p);

            //Console.WriteLine();

            //// 4. Drop the first and last order using Skip + SkipLast
            //var dropFirstAndLastOrder = orders.Skip(1).SkipLast(1);
            //Console.WriteLine("Drop the first and last order");
            //foreach(var p in dropFirstAndLastOrder)
            //    Console.WriteLine(p);

            //Console.WriteLine();

            //// 5. BONUS: Write a generic Paginate(source, pageNumber, pageSize) method
            //int pageNumber = 3;
            //int pageSize1 = 3;
            //Console.WriteLine("Generic Pagination Example");
            //var page3 = Paginate(orders, pageNumber, pageSize1);
            //foreach(var p in page3)
            //    Console.WriteLine(p);

            /////////////////////////////////////----- Q6 -----///////////////////////////////////////

            List<Employee> employees = new List<Employee>
            {
                new Employee("Ali", "Engineering", 9000m),
                new Employee("Sara", "Engineering", 8500m),
                new Employee("Omar", "HR", 6000m),
                new Employee("Mona", "HR", 6200m),
                new Employee("Yara", "Marketing", 7000m),
                new Employee("Karim", "Marketing", 7500m),
                new Employee("Nada", "Engineering", 9500m)
            };

            //// 1. Project to anonymous type: { FullName = Name.ToUpper(), Salary }
            //var anonList = employees.Select(e => new { FullName = e.Name.ToUpper(), e.Salary }).ToList();
            //foreach(var e in anonList)
            //    Console.WriteLine(e);

            //Console.WriteLine();
            //// 2. Project to a formatted string: "Ali works in Engineering — EGP 9,000"
            //var infoForEmp = employees.Select(e => $"{e.Name} work in {e.Department} - EGP {e.Salary}").ToList();
            //foreach(var info in infoForEmp)
            //    Console.WriteLine(info);

            //Console.WriteLine();
            //// 3. Sort by Salary descending, then use indexed Select to add Rank:
            ////    { Rank = index + 1, Name, Salary }
            //var rankEmp = employees.OrderByDescending(e => e.Salary)
            //                        .Select((e, index) => new { Rank = index + 1, e.Name, e.Salary }).ToList();
            //foreach(var rank in rankEmp)
            //    Console.WriteLine(rank);

            ////BONUS: Project each employee to include a "SeniorityLevel" property:
            //var categoryEmp = employees.Select(e => new
            //{
            //    e.Name,
            //    e.Department,
            //    e.Salary,
            //    Level = e.Salary >= 9000 ? "Senior" :
            //            e.Salary >= 7000 ? "Mid Level" : "Junior"
            //}).ToList();
            //foreach(var category in categoryEmp)
            //    Console.WriteLine(category);



            /////////////////////////////////////----- Q7 -----///////////////////////////////////////

            //List<int> scores = [88, 92, 75, 60, 55, 80, 91, 45];


            //// 1. TakeWhile score >= 70  → expected: [88, 92, 75]
            //var takeWhile = scores.TakeWhile(score => score >= 70);
            //Console.WriteLine("TakeWhile score >= 70:");
            //foreach (var score in takeWhile)
            //    Console.Write(score + " ");

            //Console.WriteLine();
            //// 2. SkipWhile score >= 70  → expected: [60, 55, 80, 91, 45]
            //var skipWhile = scores.SkipWhile(score => score >= 70);
            //Console.WriteLine("SkipWhile score >= 70:");
            //foreach (var score in skipWhile)
            //    Console.Write(score + " ");


            //// 3. What is the difference between this and using Where? Explain in a comment

            //// Where دا بيسكب كل العناصر الي بتحقق الشرط
            //// SkipWhile & TakeWhile دول بيسكبو اول عنصر يحقق الشرط بس والباقي عادي ببقي معانا 

            /////////////////////////////////////----- Q8 -----///////////////////////////////////////

            //List<Employee> employees = new List<Employee>
            //{
            //    new Employee("Ali", "Engineering", 9000m),
            //    new Employee("Sara", "Engineering", 8500m),
            //    new Employee("Omar", "HR", 6000m),
            //    new Employee("Mona", "HR", 6200m),
            //    new Employee("Yara", "Marketing", 7000m),
            //    new Employee("Karim", "Marketing", 7500m),
            //    new Employee("Nada", "Engineering", 9500m)
            //};

            //// 1. Group by Department, print: "Engineering → Count: 3, Avg: 9000"
            //var groupByDept = employees.GroupBy(e => e.Department);
            //foreach (var g in groupByDept)
            //{
            //    int count = g.Count();
            //    decimal avgSalary = g.Average(e => e.Salary);
            //    Console.WriteLine($"{g.Key} → Count: {count}, Avg: {avgSalary}");


            //}

            //Console.WriteLine();
            //// 2. Find the department with the highest total salary budget
            //var highestTotalSalaryBudget = groupByDept.Select(g => new
            //{
            //    Depatment = g.Key,
            //    TotalSalary = g.Sum(e => e.Salary)
            //});

            //foreach(var totalSalary in highestTotalSalaryBudget)
            //    Console.WriteLine(totalSalary);

            //Console.WriteLine();
            //// 3. List employees in each group ordered by Salary descending
            //foreach (var g in groupByDept)
            //{
            //    Console.WriteLine($"{g.Key}:");
            //    foreach (var emp in g.OrderByDescending(e => e.Salary))
            //    {
            //        Console.WriteLine($"   {emp.Name}  {emp.Salary}");
            //    }
            //}

            /////////////////////////////////////----- Q9 -----///////////////////////////////////////

            //List<int> nums = [1, 2, 3, 4, 5];
            //var query = nums.Where(n => n > 2); // ← query defined here
            //nums.Add(10);   // ← source modified AFTER query
            //foreach (var n in query)
            //    Console.Write(n + " ");

            //Console.WriteLine();
            //// Q: What is printed? Why?
            //// [3 4 5 10 ]
            ////.ToList() من غير ما نعمل   list  عشان عندنا شرط ان الارقام تبقي اكبر من 2 وبعدين ضيف رقم 10 علي ال 

            //// Q: How would using .ToList() right after .Where(...) change the result?
            //List<int> nums0 = [1, 2, 3, 4, 5];
            //var query0 = nums0.Where(n => n > 2).ToList();
            ////النتيجه اتحفظت خلاص واي تعديل انت هتعمله مش هيسمع ومش هيعمل اي تاثير .ToList() هنا لما عملنا 
            //nums0.Add(17);
            //foreach (var n in query0)
            //    Console.Write(n + " ");
            //// Q: Name 3 LINQ operators that trigger immediate execution.

            ///////////////////////-------------- Immediate Execution -----------------////////////
            ////ToList()
            ////ToArray()
            ////Count()
            ////First()
            ////FirstOrDefault()
            ////Last()
            ////LastOrDefault()
            ////Single()
            ////Sum()
            ////Max()
            ////Min()
            ////Average()
            //Console.WriteLine();
            /////////////////////////////////////----- Q10 -----///////////////////////////////////////

            //List<string> words = ["apple", "fig", "banana", "kiwi",
            //                        "grape", "mango", "pear", "plum"];
            //// 1. Filter words longer than 4 characters
            //var wordsLongerThanFourCharacters = words.Where(w => w.Length > 4);
            //foreach (var item in wordsLongerThanFourCharacters)
            //{
            //    Console.Write(item+ " ");
            //}
            //Console.WriteLine();
            //// 2. Filter words at even indexes (0, 2, 4, 6...) using (item, index) overload
            //var wordsAtEvenIndexes = words.Where((w, index) => index % 2 == 0);
            //foreach (var item in wordsAtEvenIndexes)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();
            //// 3. Filter words that are BOTH longer than 4 chars AND at an even index
            //var wordsLongerThanFourCharsAndEvenIndex = words.Where((w, index) => w.Length > 4 && index % 2 == 0);
            //foreach (var item in wordsLongerThanFourCharsAndEvenIndex)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();
            //// 4. What is the index of "mango" in the filtered result from step 1?
            //int index = 0;
            //foreach (var item in wordsLongerThanFourCharacters)
            //{
            //    if(item == "mango")
            //        Console.WriteLine($"the index of \"mango\" is {index}");
            //    index++;
            //}
            //Console.WriteLine();

            //// طريقه تانيه :- 
            //var theIndexOfMango = words.IndexOf("mango");
            //Console.WriteLine($"the index of \"mango\" is {theIndexOfMango}");
            //Console.WriteLine();

            /////////////////////////////////////----- Q11 -----///////////////////////////////////////

            List<Course> courses =
            [
                new("C# Basics",["Ali", "Sara", "Omar"]),
                new("LINQ Mastery", ["Sara", "Mona", "Ali"]),
                new("ASP.NET Core", ["Yara", "Omar", "Karim"]),
            ];

            //// 1. Flatten to a single list of ALL student names (with duplicates)
            //var studentNames = courses.SelectMany(s => s.Students);
            //foreach (var student in studentNames)
            //    Console.WriteLine(student);
            //Console.WriteLine();
            //// 2. Get a distinct list of all student names
            //var studentNamesDistinct = courses.SelectMany(s => s.Students).Distinct();
            //foreach (var student in studentNamesDistinct)
            //    Console.WriteLine(student);
            //Console.WriteLine();
            //// 3. Find students who appear in MORE THAN ONE course
            //var studentNamesAppearMoreThanOne = courses
            //        .SelectMany(c => c.Students)
            //        .GroupBy(s => s)
            //        .Where(g => g.Count() > 1)
            //        .Select(g => g.Key);
            //foreach (var student in studentNamesAppearMoreThanOne)
            //    Console.WriteLine(student);
            //Console.WriteLine();

            //// 4. Use SelectMany with result selector to get (CourseName, StudentName) pairs
            //var resultSelector = courses
            //    .SelectMany(c => c.Students, (c, s) => new { CourseName = c.Title, StudentName = s });

            //foreach (var student in resultSelector)
            //    Console.WriteLine(student);
            //Console.WriteLine();

            ///////////////////////
            ////BONUS دي حته     
            //var groupByTitle = courses
            //    .GroupBy(c => c.Title);

            //foreach (var group in groupByTitle)
            //{
            //    Console.WriteLine($"--------------------------{group.Key}--------------------------");

            //    foreach (var student in group.SelectMany(c => c.Students))
            //    {
            //        Console.WriteLine("    " + student); 
            //    }

            //    Console.WriteLine();
            //}

            //Console.WriteLine();
            /////////////////////////////////////----- Q12 -----///////////////////////////////////////

            //1.From employees: get the TOP 2 highest - paid employees per department.
            var top2PerDept = employees
                .GroupBy(e => e.Department)  //Deferred                  
                .SelectMany(g => g          //Deferred 
                .OrderByDescending(e => e.Salary)           //Deferred        
                .Take(2));                              //Deferred               

            foreach (var e in top2PerDept)
                Console.WriteLine($"{e.Name} - {e.Department} - {e.Salary}");

            Console.WriteLine();
            //2. From courses: build a Dictionary<string, int>
            var courseDict = courses
                .Where(c => c.Students.Count > 2)      //Deferred      
                .ToDictionary(c => c.Title, c => c.Students.Count);     // Immediate


            foreach (var kv in courseDict)
                Console.WriteLine($"{kv.Key} → {kv.Value}");

            Console.WriteLine();


            //3. Check:
            bool anyEngineeringBelow8000 = employees
                .Where(e => e.Department == "Engineering")    //Deferred 
                .Any(e => e.Salary < 8000);              // Immediate   

            bool allHRAbove5500 = employees
                .Where(e => e.Department == "HR")         //Deferred   
                .All(e => e.Salary > 5500);              // Immediate  

            Console.WriteLine($"Any Engineering < 8000? {anyEngineeringBelow8000}");
            Console.WriteLine($"All HR > 5500? {allHRAbove5500}");

            Console.WriteLine();

            //4. Project the top-2-per-dept result 
            var top2WithRank = employees
                .GroupBy(e => e.Department)         //Deferred 
                .SelectMany(g => g                  //Deferred 
                .OrderByDescending(e => e.Salary)       //Deferred 
                .Take(2)                    //Deferred 
                .Select((emp, index) => new      //Deferred 
                {
                    Rank = index + 1,
                    Name = emp.Name,
                    Department = emp.Department,
                    Salary = emp.Salary,
                    SeniorityLevel = emp.Salary > 9000 ? "Senior" : "Junior"
                }));

            foreach (var e in top2WithRank)
            {
                Console.WriteLine($"{e.Rank} - {e.Name} - {e.Department} - {e.Salary} - {e.SeniorityLevel}");
            }

            //5. For each step above — is execution deferred or immediate?

            //---------------------------Deferred Execution-------------------------
            // Where, Select, OrderBy,GroupBy, Take


            //---------------------------Immediate Execution--------------------------
            //ToList(), ToArray(),ToDictionary(), Any(),All()


        }
    }
}
