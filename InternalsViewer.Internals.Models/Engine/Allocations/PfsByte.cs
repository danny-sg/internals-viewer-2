﻿
namespace InternalsViewer.Internals.Models.Engine.Allocations
{
    public class PfsByte
    {
        public SpaceFree PageSpaceFree { get; set; }

        public bool GhostRecords { get; set; }

        public bool Iam { get; set; }

        public bool Mixed { get; set; }

        public bool Allocated { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    "PFS Status: {0:Allocated ; ;Not Allocated }| {1} Full{2: | IAM Page ; ; }{3:| Mixed Extent ; ; }{4:| Has Ghost ; ; }",
                    Allocated ? 1 : 0,
                    SpaceFreeDescription(PageSpaceFree),
                    Iam ? 1 : 0,
                    Mixed ? 1 : 0,
                    GhostRecords ? 1 : 0);
        }

        public static string SpaceFreeDescription(SpaceFree spaceFree)
        {
            switch (spaceFree)
            {
                case SpaceFree.Empty:
                    return "0%";
                case SpaceFree.FiftyPercent:
                    return "50%";
                case SpaceFree.EightyPercent:
                    return "80%";
                case SpaceFree.NinetyFivePercent:
                    return "95%";
                case SpaceFree.OneHundredPercent:
                    return "100%";
                default:
                    return "Unknown";
            }
        }
    }
}