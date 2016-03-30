﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;

namespace Shared.Html.Excel
{
    public static class ObjectArrayToExcel
    {
        public static byte[] AsExcel(string sheetName, object[] objs)
        {
            return DumpAllProps(sheetName, objs);
        }

        public static byte[] AsExcel(string sheetName, object[] objs, string[] includeProperties)
        {
            return DumpAllProps(sheetName, objs, includeProperties);
        }

        private static byte[] DumpAllProps(string sheetName, object[] objs, string[] includeProperties = null)
        {
            if (objs != null && objs.Length > 0)
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    var workbook = package.Workbook;
                    var sheet = workbook.Worksheets.Add(sheetName);
                    //sheet.View.ShowGridLines = false;
                    sheet.View.ShowHeaders = true;
                    var prps = objs[0]
                        .GetType()
                        .GetProperties(System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.DeclaredOnly
                            | System.Reflection.BindingFlags.Public)
                        .Where(p => p.PropertyType == typeof(bool)
                                || p.PropertyType == typeof(bool?)
                                || p.PropertyType == typeof(byte)
                                || p.PropertyType == typeof(byte?)
                                || p.PropertyType == typeof(DateTime)
                                || p.PropertyType == typeof(DateTime?)
                                || p.PropertyType == typeof(decimal)
                                || p.PropertyType == typeof(decimal?)
                                || p.PropertyType == typeof(int)
                                || p.PropertyType == typeof(int?)
                                || p.PropertyType == typeof(string)
                        )
                        .ToArray();
                    if (includeProperties != null && includeProperties.Length > 0)
                    {
                        prps = (from ppp in prps
                                join ipp in includeProperties
                                on ppp.Name equals ipp
                                orderby Array.IndexOf(includeProperties, ipp)
                                select ppp)
                               .ToArray();
                        //prps = prps.Where(p => includeProperties.Contains(p.Name))
                        //    .ToArray();
                    }
                    int colCtr = 1;
                    int rowCtr = 1;
                    var prpFs = prps.Select(p => p.GetDisplayName());
                    foreach (var prp in prpFs)
                    {
                        //sheet.Cells[rowCtr, colCtr].Value = prp.Name;
                        sheet.Cells[rowCtr, colCtr].Value = prp;
                        colCtr++;
                    }
                    rowCtr++;
                    foreach (var obj in objs)
                    {
                        colCtr = 1; //reset for start of things.
                        foreach (var prp in prps)
                        {
                            if (prp.PropertyType == typeof(DateTime))
                            {
                                var dt = (DateTime)obj.GetType().GetProperty(prp.Name).GetValue(obj);
                                sheet.Cells[rowCtr, colCtr].Value = dt;
                                sheet.Cells[rowCtr, colCtr].Style.Numberformat.Format =  "yyyy-MMM-dd";// HH:mm
                            }
                            else if (prp.PropertyType == typeof(DateTime?))
                            {
                                var dt = (DateTime?)obj.GetType().GetProperty(prp.Name).GetValue(obj);
                                if (dt.HasValue)
                                {
                                    sheet.Cells[rowCtr, colCtr].Value = dt.Value;
                                }
                                sheet.Cells[rowCtr, colCtr].Style.Numberformat.Format =  "yyyy-MMM-dd";// HH:mm
                            }
                            else
                            {
                                sheet.Cells[rowCtr, colCtr].Value = obj.GetType().GetProperty(prp.Name).GetValue(obj);
                            }
                            colCtr++;
                        }
                        rowCtr++;
                    }
                    workbook.Worksheets[1].Select();

                    workbook.Properties.Title = workbook.Worksheets[1].Name;
                    workbook.Properties.Company = "";
                    workbook.Properties.SetCustomPropertyValue("Generated By", System.Reflection.Assembly.GetExecutingAssembly().FullName);
                    return package.GetAsByteArray();
                }
            }
            else
            {
                return new byte[0];
            }
        }

        private static string GetDisplayName(this MemberInfo prop)
        {
            if (prop.CustomAttributes == null || !prop.CustomAttributes.Any())
            {
                return prop.Name;
            }
            var displayAttribute = prop.CustomAttributes
                .FirstOrDefault(x => x.AttributeType == typeof(DisplayAttribute));

            if (displayAttribute == null
                || displayAttribute.NamedArguments == null
                || displayAttribute.NamedArguments.Count == 0
                )
            {
                return prop.Name;
            }
            if (displayAttribute.NamedArguments.Any(p => p.MemberName == "ShortName"))
            {
                return displayAttribute.NamedArguments.FirstOrDefault(p => p.MemberName == "ShortName").TypedValue.ToString().Replace("\"", "");
            }
            else if (displayAttribute.NamedArguments.Any(p => p.MemberName == "Name"))
            {
                return displayAttribute.NamedArguments.FirstOrDefault(p => p.MemberName == "Name").TypedValue.ToString().Replace("\"", "");
            }
            else
            {
                return prop.Name;
            }
        }
    }
}