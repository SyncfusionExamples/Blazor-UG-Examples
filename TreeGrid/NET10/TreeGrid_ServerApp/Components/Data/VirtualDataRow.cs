namespace TreeGrid_ServerApp.Components.Data
{
    public class VirtualDataRow
    {
        public int? TaskID { get; set; }
        public string FIELD1 { get; set; }
        public int? FIELD2 { get; set; }
        public int? FIELD3 { get; set; }
        public int? FIELD4 { get; set; }
        public List<VirtualDataRow> Children { get; set; }

        public static List<VirtualDataRow> GetVirtualDataRow()
        {
            List<VirtualDataRow> DataCollection = new List<VirtualDataRow>();

            for (var i = 1; i <= 1000; i++)
            {
                VirtualDataRow Parent1 = new VirtualDataRow()
                {
                    TaskID = 1,
                    FIELD1 = "VINET",
                    FIELD2 = 1967,
                    FIELD3 = 395,
                    FIELD4 = 87,
                    Children = new List<VirtualDataRow>()
                };
                VirtualDataRow Child1 = new VirtualDataRow()
                {
                    TaskID = 2,
                    FIELD1 = "TOMSP",
                    FIELD2 = 1968,
                    FIELD3 = 295,
                    FIELD4 = 44
                };
                VirtualDataRow Child2 = new VirtualDataRow()
                {
                    TaskID = 3,
                    FIELD1 = "HANAR",
                    FIELD2 = 1969,
                    FIELD3 = 376,
                    FIELD4 = 22
                };
                VirtualDataRow Child3 = new VirtualDataRow()
                {
                    TaskID = 4,
                    FIELD1 = "VICTE",
                    FIELD2 = 1970,
                    FIELD3 = 123,
                    FIELD4 = 35
                };
                VirtualDataRow Child4 = new VirtualDataRow()
                {
                    TaskID = 5,
                    FIELD1 = "SUPRD",
                    FIELD2 = 1971,
                    FIELD3 = 567,
                    FIELD4 = 98
                };
                VirtualDataRow Child5 = new VirtualDataRow()
                {
                    TaskID = 6,
                    FIELD1 = "RICSU",
                    FIELD2 = 1972,
                    FIELD3 = 378,
                    FIELD4 = 56
                };
                VirtualDataRow Parent2 = new VirtualDataRow()
                {
                    TaskID = 1,
                    FIELD1 = "TOMSP",
                    FIELD2 = 1968,
                    FIELD3 = 295,
                    FIELD4 = 44,
                    Children = new List<VirtualDataRow>()
                };
                VirtualDataRow Child6 = new VirtualDataRow()
                {
                    TaskID = 2,
                    FIELD1 = "VINET",
                    FIELD2 = 1967,
                    FIELD3 = 395,
                    FIELD4 = 87
                };
                VirtualDataRow Child7 = new VirtualDataRow()
                {
                    TaskID = 3,
                    FIELD1 = "VICTE",
                    FIELD2 = 1970,
                    FIELD3 = 123,
                    FIELD4 = 35
                };
                VirtualDataRow Child8 = new VirtualDataRow()
                {
                    TaskID = 4,
                    FIELD1 = "RICSU",
                    FIELD2 = 1972,
                    FIELD3 = 378,
                    FIELD4 = 56
                };
                VirtualDataRow Child9 = new VirtualDataRow()
                {
                    TaskID = 5,
                    FIELD1 = "HANAR",
                    FIELD2 = 1969,
                    FIELD3 = 376,
                    FIELD4 = 22
                };
                VirtualDataRow Child10 = new VirtualDataRow()
                {
                    TaskID = 6,
                    FIELD1 = "SUPRD",
                    FIELD2 = 1971,
                    FIELD3 = 567,
                    FIELD4 = 98
                };
                Parent1.Children.Add(Child1);
                Parent1.Children.Add(Child2);
                Parent1.Children.Add(Child3);
                Parent1.Children.Add(Child4);
                Parent1.Children.Add(Child5);

                Parent2.Children.Add(Child6);
                Parent2.Children.Add(Child7);
                Parent2.Children.Add(Child8);
                Parent2.Children.Add(Child9);
                Parent2.Children.Add(Child10);

                DataCollection.Add(Parent1);
                DataCollection.Add(Parent2);
            }
            return DataCollection;
        }
    }
}
