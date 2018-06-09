﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using InternalsViewer.Internals.Models.Engine.Pages;
using InternalsViewer.Internals.Models.Marking;
using InternalsViewer.Internals.Models.Metadata;

namespace InternalsViewer.Internals.Models.Engine.Records
{
    /// <summary>
    /// DatabaseContainer Record Stucture
    /// </summary>
    public abstract class Record : Markable
    {
        /// <summary>
        /// Gets the record type description.
        /// </summary>
        /// <param name="recordType">Type of the record.</param>
        /// <returns></returns>
        protected static string GetRecordTypeDescription(RecordType recordType)
        {
            switch (recordType)
            {
                case RecordType.Primary:
                    return "Primary Record";
                case RecordType.Forwarded:
                    return "Forwarded Record";
                case RecordType.Forwarding:
                    return "Forwarding Record";
                case RecordType.Index:
                    return "Index Record";
                case RecordType.Blob:
                    return "BLOB Fragment";
                case RecordType.GhostIndex:
                    return "Ghost Index Record";
                case RecordType.GhostData:
                    return "Ghost Data Record";
                case RecordType.GhostRecordVersion:
                    return "Ghost Record Version";
                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// Gets a string representation of the null bitmap
        /// </summary>
        /// <returns></returns>
        protected static string GetNullBitmapString(BitArray nullBitmap)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < nullBitmap.Length; i++)
            {
                stringBuilder.Insert(0, nullBitmap[i] ? "1" : "0");
            }

            return stringBuilder.ToString();
        }

        public static string GetArrayString(ushort[] array)
        {
            var sb = new StringBuilder();

            foreach (var offset in array)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }

                sb.AppendFormat("{0} - 0x{0:X}", offset);
            }

            return sb.ToString();
        }

        internal static string GetStatusBitsDescription(Record record)
        {
            var statusDescription = string.Empty;

            if (record.HasVariableLengthColumns)
            {
                statusDescription += ", Variable Length Flag";
            }

            if (record.HasNullBitmap && record.HasVariableLengthColumns)
            {
                statusDescription += " | NULL Bitmap Flag";
            }
            else if (record.HasNullBitmap)
            {
                statusDescription += ", NULL Bitmap Flag";
            }

            return statusDescription;
        }

        /// <summary>
        /// Gets or sets the record's underlying Page
        /// </summary>
        /// <value>The Page.</value>
        public Page Page { get; set; }

        /// <summary>
        /// Gets or sets the record type
        /// </summary>
        /// <value>The type of the record.</value>
        public RecordType RecordType { get; set; }

        /// <summary>
        /// Gets or sets the slot offset in the page
        /// </summary>
        /// <value>The slot offset.</value>
        [MarkerStyle(MarkerStyleType.SlotOffset)]
        public ushort SlotOffset { get; set; }

        /// <summary>
        /// Gets or sets the Column Offset Array
        /// </summary>
        /// <value>The col offset array.</value>
        public ushort[] ColOffsetArray { get; set; }

        [MarkerStyle(MarkerStyleType.ColumnOffsetArray)]
        public string ColOffsetArrayDescription => GetArrayString(ColOffsetArray);

        /// <summary>
        /// Gets or sets the status bits A value
        /// </summary>
        /// <value>The status bits A (bitmap of row properties) value </value>
        public BitArray StatusBitsA { get; set; }

        [MarkerStyle(MarkerStyleType.StatusBitsA)]
        public string StatusBitsADescription => GetRecordTypeDescription(RecordType) + GetStatusBitsDescription(this);

        /// <summary>
        /// Gets or sets the status bits B value
        /// </summary>
        /// <value>The status bits B (bitmap of row properties) value</value>
        public BitArray StatusBitsB { get; set; }

        /// <summary>
        /// Gets or sets the column count bytes value
        /// </summary>
        /// <value>The number of bytes used for the column count.</value>
        /// <remarks>Used for SQL Server 2008 page compression</remarks>
        public short ColumnCountBytes { get; set; }

        /// <summary>
        /// Gets or sets the number of columns.
        /// </summary>
        /// <value>The number of columns in the record</value>
        [MarkerStyle(MarkerStyleType.ColumnCount)]
        public short ColumnCount { get; set; }

        /// <summary>
        /// Gets or sets the fixed column offset.
        /// </summary>
        /// <value>The offset location of the start of the fixed column fields</value>
        [MarkerStyle(MarkerStyleType.ColumnCountOffset)]
        public short ColumnCountOffset { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has variable length columns.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has variable length columns; otherwise, <c>false</c>.
        /// </value>
        public bool HasVariableLengthColumns { get; set; }

        /// <summary>
        /// Gets or sets the variable length data offset.
        /// </summary>
        /// <value>The variable length data offset.</value>
        public ushort VariableLengthDataOffset { get; set; }

        /// <summary>
        /// Gets or sets the variable length column count.
        /// </summary>
        /// <value>The variable length column count.</value>
        [MarkerStyle(MarkerStyleType.VariableLengthColumnCount)]
        public ushort VariableLengthColumnCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a null bitmap.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has null bitmap; otherwise, <c>false</c>.
        /// </value>
        public bool HasNullBitmap { get; set; }

        /// <summary>
        /// Gets or sets the size of the null bitmap in bytes
        /// </summary>
        /// <value>The size of the null bitmap in bytes</value>
        public short NullBitmapSize { get; set; }

        /// <summary>
        /// Gets or sets the null bitmap.
        /// </summary>
        /// <value>The null bitmap.</value>
        public BitArray NullBitmap { get; set; }

        [MarkerStyle(MarkerStyleType.NullBitmap)]
        public string NullBitmapDescription => HasNullBitmap ? GetNullBitmapString(NullBitmap) : string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a uniqueifier.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has a uniqueifier; otherwise, <c>false</c>.
        /// </value>
        public bool HasUniqueifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Record"/> is compressed.
        /// </summary>
        /// <value><c>true</c> if compressed; otherwise, <c>false</c>.</value>
        public bool Compressed { get; set; }

        /// <summary>
        /// Gets or sets the record fields.
        /// </summary>
        /// <value>The record fields.</value>
        public List<RecordField> Fields { get; set; }

        public RecordField[] FieldsArray => Fields.ToArray();

        /// <summary>
        /// Gets or sets the record structure.
        /// </summary>
        /// <value>The record structure.</value>
        public HobtStructure HobtStructure { get; set; }
    }
}
