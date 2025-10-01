namespace TreeGridUGSample.Components.Data
{
    public class VirtualHierarchyData
    {
        public int TaskID { get; set; }
        public string FIELD1 { get; set; }
        public int FIELD2 { get; set; }
        public int? FIELD3 { get; set; }
        public int? FIELD4 { get; set; }
        public List<VirtualHierarchyData> Children { get; set; }

        static string[] Names = new string[] { "VINET", "TOMSP", "HANAR", "VICTE", "SUPRD", "HANAR", "CHOPS", "RICSU", "WELLI", "HILAA", "ERNSH", "CENTC", "OTTIK", "QUEDE", "RATTC", "ERNSH", "FOLKO", "BLONP", "WARTH", "FRANK", "GROSR", "WHITC", "WARTH", "SPLIR", "RATTC", "QUICK", "VINET", "MAGAA", "TORTU", "MORGK", "BERGS", "LEHMS", "BERGS", "ROMEY", "ROMEY", "LILAS", "LEHMS", "QUICK", "QUICK", "RICAR", "REGGC", "BSBEV", "COMMI", "QUEDE", "TRADH", "TORTU", "RATTC", "VINET", "LILAS", "BLONP", "HUNGO", "RICAR", "MAGAA", "WANDK", "SUPRD", "GODOS", "TORTU", "OLDWO", "ROMEY", "LONEP", "ANATR", "HUNGO", "THEBI", "DUMON", "WANDK", "QUICK", "RATTC", "ISLAT", "RATTC", "LONEP", "ISLAT", "TORTU", "WARTH", "ISLAT", "PERIC", "KOENE", "SAVEA", "KOENE", "BOLID", "FOLKO", "FURIB", "SPLIR", "LILAS", "BONAP", "MEREP", "WARTH", "VICTE", "HUNGO", "PRINI", "FRANK", "OLDWO", "MEREP", "BONAP", "SIMOB", "FRANK", "LEHMS", "WHITC", "QUICK", "RATTC", "FAMIA" };

        public static List<VirtualHierarchyData> GetVirtualHierarchyData()
        {
            List<VirtualHierarchyData> DataCollection = new List<VirtualHierarchyData>();
            var j = 0;
            for (var i = 1; i <= 10000; i++)
            {
                var random = new Random();
                var name = random.Next(50);
                VirtualHierarchyData Parent1 = new VirtualHierarchyData()
                {
                    TaskID = ++j,
                    FIELD1 = VirtualHierarchyData.Names[name],
                    FIELD2 = 1967,
                    FIELD3 = 395,
                    FIELD4 = 87,
                    Children = new List<VirtualHierarchyData>()
                };
                for (var k = 0; k < 4; k++)
                {
                    name = random.Next(5);
                    VirtualHierarchyData Child1 = new VirtualHierarchyData()
                    {
                        TaskID = ++j,
                        FIELD1 = VirtualHierarchyData.Names[name],
                        FIELD2 = 1968,
                        FIELD3 = 295,
                        FIELD4 = 44
                    };
                    Parent1.Children.Add(Child1);
                }

                name = random.Next(50);
                VirtualHierarchyData Parent2 = new VirtualHierarchyData()
                {
                    TaskID = ++j,
                    FIELD1 = VirtualHierarchyData.Names[name],
                    FIELD2 = 1968,
                    FIELD3 = 295,
                    FIELD4 = 44,
                    Children = new List<VirtualHierarchyData>()
                };
                for (var m = 0; m < 4; m++)
                {
                    name = random.Next(50);
                    VirtualHierarchyData Child2 = new VirtualHierarchyData()
                    {
                        TaskID = ++j,
                        FIELD1 = VirtualHierarchyData.Names[name],
                        FIELD2 = 1968,
                        FIELD3 = 295,
                        FIELD4 = 44
                    };
                    Parent2.Children.Add(Child2);
                }
                DataCollection.Add(Parent1);
                DataCollection.Add(Parent2);
            }
            return DataCollection;
        }
    }
}
