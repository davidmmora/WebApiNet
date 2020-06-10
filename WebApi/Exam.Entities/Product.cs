namespace Exam.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Product
    {
        public int IdProduct { get; set; }
        public string Product1 { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<int> Inventary { get; set; }
    }
}
