﻿using System.Text;
using InternalsViewer.Internals.Models.Engine.Address;
using InternalsViewer.Internals.Models.Marking;

namespace InternalsViewer.Internals.Models.Engine.Records.Data
{
    public class DataRecord : Record
    {
        public SparseVector SparseVector { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("DataRecord | Page: {0} | Slot Offset: {1} | Allocation Unit: {2}\n",
                            Page.Header.PageAddress,
                            SlotOffset,
                            Page.Header.AllocationUnit);

            sb.Append("-----------------------------------------------------------------------------------------\n");
            sb.AppendFormat("Status Bits A:                {0}\n", GetStatusBitsDescription(this));
            sb.AppendFormat("Column count offset:          {0}\n", ColumnCountOffset);
            sb.AppendFormat("Number of columns:            {0}\n", ColumnCount);
            sb.AppendFormat("Null bitmap:                  {0}\n", HasNullBitmap ? GetNullBitmapString(NullBitmap) : "(No null bitmap)");
            sb.AppendFormat("Variable length column count: {0}\n", VariableLengthColumnCount);
            sb.AppendFormat("Column offset array:          {0}\n", HasVariableLengthColumns ? GetArrayString(ColOffsetArray) : "(no variable length columns)");

            foreach (var field in Fields)
            {
                sb.AppendLine(field.ToString());
            }
            return sb.ToString();
        }

        [MarkerStyle(MarkerStyleType.StatusBitsB)]
        public string StatusBitsBDescription => "";

        [MarkerStyle(MarkerStyleType.ForwardingRecord)]
        public RowIdentifier ForwardingRecord { get; set; }
    }
}
