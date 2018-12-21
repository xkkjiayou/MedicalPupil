using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class SortableBindingList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {
        /// <summary>
        /// The is sorted core
        /// </summary>
        private bool isSortedCore = true;
        /// <summary>
        /// The sort direction core
        /// </summary>
        private ListSortDirection sortDirectionCore = ListSortDirection.Ascending;
        /// <summary>
        /// The sort property core
        /// </summary>
        private PropertyDescriptor sortPropertyCore = null;
        /// <summary>
        /// The default sort item
        /// </summary>
        private string defaultSortItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        public SortableBindingList() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public SortableBindingList(IList<T> list) : base(list) { }

        /// <summary>
        /// Gets a value indicating whether [supports sorting core].
        /// </summary>
        /// <value><c>true</c> if [supports sorting core]; otherwise, <c>false</c>.</value>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether [supports searching core].
        /// </summary>
        /// <value><c>true</c> if [supports searching core]; otherwise, <c>false</c>.</value>
        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is sorted core.
        /// </summary>
        /// <value><c>true</c> if this instance is sorted core; otherwise, <c>false</c>.</value>
        protected override bool IsSortedCore
        {
            get { return isSortedCore; }
        }

        /// <summary>
        /// Gets the sort direction core.
        /// </summary>
        /// <value>The sort direction core.</value>
        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirectionCore; }
        }

        /// <summary>
        /// Gets the sort property core.
        /// </summary>
        /// <value>The sort property core.</value>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyCore; }
        }

        /// <summary>
        /// Finds the core.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Int32.</returns>
        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (Equals(prop.GetValue(this[i]), key)) return i;
            }
            return -1;
        }

        /// <summary>
        /// Applies the sort core.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="direction">The direction.</param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            isSortedCore = true;
            sortPropertyCore = prop;
            sortDirectionCore = direction;
            Sort();
        }

        /// <summary>
        /// Removes the sort core.
        /// </summary>
        protected override void RemoveSortCore()
        {
            if (isSortedCore)
            {
                isSortedCore = false;
                sortPropertyCore = null;
                sortDirectionCore = ListSortDirection.Ascending;
                Sort();
            }
        }

        /// <summary>
        /// Gets or sets the default sort item.
        /// </summary>
        /// <value>The default sort item.</value>
        public string DefaultSortItem
        {
            get { return defaultSortItem; }
            set
            {
                if (defaultSortItem != value)
                {
                    defaultSortItem = value;
                    Sort();
                }
            }
        }

        /// <summary>
        /// Sorts this instance.
        /// </summary>
        private void Sort()
        {
            List<T> list = (this.Items as List<T>);
            list.Sort(CompareCore);
            ResetBindings();
        }

        /// <summary>
        /// Compares the core.
        /// </summary>
        /// <param name="o1">The o1.</param>
        /// <param name="o2">The o2.</param>
        /// <returns>System.Int32.</returns>
        private int CompareCore(T o1, T o2)
        {
            int ret = 0;
            if (SortPropertyCore != null)
            {
                ret = CompareValue(SortPropertyCore.GetValue(o1), SortPropertyCore.GetValue(o2), SortPropertyCore.PropertyType);
            }
            if (ret == 0 && DefaultSortItem != null)
            {
                PropertyInfo property = typeof(T).GetProperty(DefaultSortItem, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.IgnoreCase, null, null, new Type[0], null);
                if (property != null)
                {
                    ret = CompareValue(property.GetValue(o1, null), property.GetValue(o2, null), property.PropertyType);
                }
            }
            if (SortDirectionCore == ListSortDirection.Descending) ret = -ret;
            return ret;
        }

        /// <summary>
        /// Compares the value.
        /// </summary>
        /// <param name="o1">The o1.</param>
        /// <param name="o2">The o2.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Int32.</returns>
        private static int CompareValue(object o1, object o2, Type type)
        {

            if (o1 == null) return o2 == null ? 0 : -1;
            else if (o2 == null) return 1;
            else if (type.IsPrimitive || type.IsEnum) return Convert.ToDouble(o1).CompareTo(Convert.ToDouble(o2));
            else if (type == typeof(DateTime)) return Convert.ToDateTime(o1).CompareTo(o2);
            else if (type == typeof(bool)) return Convert.ToBoolean(o1).CompareTo(o2);
            else return String.Compare(o1.ToString().Trim(), o2.ToString().Trim());
        }
    }

}
