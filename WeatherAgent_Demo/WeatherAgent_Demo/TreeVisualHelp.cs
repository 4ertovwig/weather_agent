using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace WeatherAgent_Demo
{
    /// <summary>
    /// нашел на stackoverflow для нахождения дочерних визуальных элементов
    /// </summary>
    static class TreeVisualHelp
    {
        //для поиска по имени и родителю
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;

                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else
                    if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;

                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            foundChild = (T)child;
                            break;
                        }
                        else
                        {
                            foundChild = FindChild<T>(child, childName);

                            if (foundChild != null)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        foundChild = (T)child;
                        break;
                    }
            }

            return foundChild;
        }

        //для списка однотипных элементов в одном родителе
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }
                else
                {
                    var childOfChild = FindVisualChildren<T>(child);
                    if (childOfChild != null)
                    {
                        foreach (var subchild in childOfChild)
                        {
                            yield return subchild;
                        }
                    }
                }
            }
        }

    }
}
