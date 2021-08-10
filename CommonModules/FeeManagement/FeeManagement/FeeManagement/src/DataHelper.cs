using DocumentFormat.OpenXml.Office.CustomUI;
using IMP_old.Models;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;



namespace IMP_old.Src
{
    public static class DataHelper
    {
        static DataHelper()
        {
            string value = System.IO.File.ReadAllText($"{System.IO.Directory.GetCurrentDirectory()}{@"\config\data.json"}");
            Root = JsonConvert.DeserializeObject<Root>(value);
        }

        public static void FetchLeafNodes(List<Item> item, string name)
        {
            foreach (var itm in item)
            {
                if (string.IsNullOrEmpty(itm.Hierarchy))
                    itm.Hierarchy = string.Empty;



                TryAddToDictionary(itm, name);
                if (!ProductsCache.ContainsKey(itm.Name.ToLower()))
                    ProductsCache.Add(itm.Name.ToLower(), itm);
                FetchLeafNodes(itm, name);
            }
        }

        public static List<Item> GetItems(string product)
        {
            //if (string.IsNullOrEmpty(product))
            if (!string.IsNullOrEmpty(product))
            {
                var col = GetAllItems(product);
                if (col.Count > 0) return col;
            }



            {
                //print all
                return GetTopHierarchyItems();
            }
        }



        public static List<Item> GetAllItems(string product)
        {
            product = product.ToLower();
            if (CategoryGroupCache.ContainsKey(product))
            {
                return CategoryGroupCache[product].Where(cc => !cc.Name.ToLower().Equals(product)).ToList();
            }
            if (ProductsCache.ContainsKey(product))
            {
                if (ProductsCache[product].items != null && ProductsCache[product].items.Count > 0) return ProductsCache[product].items;



                var listOfItems = new List<Item>();
                listOfItems.Add(ProductsCache[product]);
                return listOfItems;
            }



            return new List<Item>();
        }



        private static List<Item> GetAllLeafItems()
        {
            List<Item> itemsToBePrinted = new List<Item>();
            foreach (var itm in CategoryGroupCache)
            {
                foreach (var it in itm.Value)
                {
                    if (it.items.Count == 0) //string.IsNullOrEmpty(it.Hierarchy))
                    {
                        itemsToBePrinted.Add(it);
                    }
                }
            }



            return itemsToBePrinted;
        }
        private static List<Item> GetTopHierarchyItems()
        {
            List<Item> itemsToBePrinted = new List<Item>();
            foreach (var itm in CategoryGroupCache)
            {
                foreach (var it in itm.Value)
                {
                    if (string.IsNullOrEmpty(it.Hierarchy)) //string.IsNullOrEmpty(it.Hierarchy))
                    {
                        itemsToBePrinted.Add(it);
                    }
                }
            }



            return itemsToBePrinted;
        }



        public static Dictionary<string, List<Item>> GroupList(string product)
        {
            var itms = GetItems(product);
            return GroupList(itms);
        }



        public static Dictionary<string, List<Item>> GroupList(List<Item> items)
        {
            Dictionary<string, List<Item>> rowsToPrint = new Dictionary<string, List<Item>>();
            int max = 3;
            List<Item> currentRow = null;
            foreach (var itm in items)
            {
                if (currentRow == null || currentRow.Count == max)
                {
                    currentRow = new List<Item>();
                    rowsToPrint.Add(rowsToPrint.Count.ToString(), currentRow);
                }
                currentRow.Add(itm);
            }



            return rowsToPrint;
        }



        public static void FetchLeafNodes(Item item, string name)
        {
            if (item.items == null || item.items.Count == 0)
            {
                item.Hierarchy = name;
                if (item.Hierarchy == null)
                    item.Hierarchy = string.Empty;
                LeafItems.Add(item);
                return;
            }
            else
            {
                FetchLeafNodes(item.items, FormatHierarchy(item.Name, name));
            }
        }



        public static void TryAddToDictionary(Item item, string name)
        {
            string key = name;
            if (string.IsNullOrEmpty(key))
            {
                key = item.Name;
            }



            key = key.ToLower();



            List<Item> cacheDict = null;
            if (!CategoryGroupCache.ContainsKey(key))
            {
                cacheDict = new List<Item>();
                CategoryGroupCache[key] = cacheDict;
            }
            else
            {
                cacheDict = CategoryGroupCache[key];
            }



            bool match = cacheDict.Any(cc => cc.Hierarchy.Equals(item.Hierarchy) && cc.Name.Equals(item.Name));
            if (!match) cacheDict.Add(item);
        }



        public static Dictionary<string, List<Item>> CategoryGroupCache = new Dictionary<string, List<Item>>();
        public static Dictionary<string, Item> ProductsCache = new Dictionary<string, Item>();



        public static string FormatHierarchy(string child, string parent)
        {
            return (string.IsNullOrEmpty(parent) ? child : child + "=>" + parent);
        }



        public static string PrintGroup;



        public static Root Root { get; set; }



        public static List<Item> LeafItems = new List<Item>();
    }
}