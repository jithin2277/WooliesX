using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesX.Data.IntegrationTests
{
    /// <summary>
    /// Methods for determining if arrays are sorted.
    /// </summary>
    public static class SortTools
    {
        /// <summary>
        /// Determines if double array is sorted from 0 -> Max
        /// </summary>
        public static bool IsSorted(double[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if string array is sorted from A -> Z
        /// </summary>
        public static bool IsSorted(string[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1].CompareTo(arr[i]) > 0) // If previous is bigger, return false
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if double array is sorted from 0 -> Max
        /// </summary>
        public static bool IsSorted(long[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if double array is sorted from Max -> 0
        /// </summary>
        public static bool IsSortedDescending(double[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] < arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if string array is sorted from Z -> A
        /// </summary>
        public static bool IsSortedDescending(string[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i].CompareTo(arr[i + 1]) < 0) // If previous is smaller, return false
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if double array is sorted from Max -> 0
        /// </summary>
        public static bool IsSortedDescending(long[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] < arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
    }

}
