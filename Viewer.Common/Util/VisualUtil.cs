////////////////////////////////////////////////////////////////////////////////
// VisualUtil.cs
// 2012.03.12, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

namespace Viewer.Common.Util
{
    /// <summary>
    /// WPF Visual element 관련 유틸리티들.
    /// </summary>
    public class VisualUtil
    {
        public static T FindTypedAncestor<T>(DependencyObject source) where T : DependencyObject
        {
            while (source != null && source.GetType() != typeof(T)) {
                if (source is Visual) {
                    source = VisualTreeHelper.GetParent(source);
                } else {
                    source = LogicalTreeHelper.GetParent(source);
                }
            }
            return (T)source;
        }

        public static T FindAncestor<T>(DependencyObject source) where T : DependencyObject
        {
            while (source != null && !(source is T)) {
                if (source is Visual) {
                    source = VisualTreeHelper.GetParent(source);
                } else {
                    source = LogicalTreeHelper.GetParent(source);
                }
            }
            return (T)source;
        }

        public static bool IsListViewColumnHeader(MouseButtonEventArgs e)
        {
            GridViewColumnHeader header = FindTypedAncestor<GridViewColumnHeader>(e.OriginalSource as DependencyObject);
            return header != null;
        }

        public static T FindVisualChild<T>(DependencyObject obj)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static T FindVisualChild<T>(FrameworkElement obj, string name)
            where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                FrameworkElement child = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
                if (child != null && child is T && child.Name.Equals(name))
                    return (T)child;
                else {
                    T childOfChild = FindVisualChild<T>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
